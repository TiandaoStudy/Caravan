﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Web;
using Finsa.Caravan.Common;
using Finsa.Caravan.Common.Models.Security;
using Finsa.Caravan.DataAccess;
using PommaLabs.KVLite;

namespace Finsa.Caravan.WebForms
{
    public sealed class GlobalHelper
    {
        private GlobalHelper()
        {
            throw new InvalidOperationException();
        }

        public static void Application_Start(object sender, EventArgs args, string connectionString)
        {
            // Sets the default connection string.
            CaravanDataSource.Manager.ConnectionString = connectionString;

            // After setting the connection string, we can use the logger.
            CaravanDataSource.Logger.LogInfo<GlobalHelper>("Application started");

            // Run vacuum on the persistent cache. It should be put AFTER the connection string is
            // set, since that string it stored on the cache itself and we do not want conflicts, right?
            PersistentCache.DefaultInstance.Vacuum();

            // Starts user tracking.
            HttpContext.Current.Application.Add("TRACK_USER_LIST", new Dictionary<string, SecSession>());
        }

        public static void Application_End(object sender, EventArgs args)
        {
            if (HttpContext.Current != null)
            {
                // Stops user tracking.
                HttpContext.Current.Application.Remove("TRACK_USER_LIST");
            }

            // Log application end.
            CaravanDataSource.Logger.LogInfo<GlobalHelper>("Application ended");
        }

        public static void Application_PreSendRequestHeaders(object sender, EventArgs args)
        {
            // Ensures that, if GZip/Deflate Encoding is applied, then that headers are set. Also
            // works when error occurs if filters are still active.
            if (HttpContext.Current != null && HttpContext.Current.Response != null)
            {
                var response = HttpContext.Current.Response;
                if (response.Filter is GZipStream && response.Headers["Content-encoding"] != "gzip")
                {
                    response.AppendHeader("Content-encoding", "gzip");
                }
                else if (response.Filter is DeflateStream && response.Headers["Content-encoding"] != "deflate")
                {
                    response.AppendHeader("Content-encoding", "deflate");
                }
            }
        }

        public static void Application_Error(object sender, EventArgs args)
        {
            // Removes any special filtering, especially GZip filtering. Assigning the response
            // filter to a dummy variable is required in order to avoid a .NET issue which is
            // triggered by setting the Filter property, without reading it first.
            var dummy = HttpContext.Current.Response.Filter;
            HttpContext.Current.Response.Filter = null;

            // Logs the error into the DB.
            CaravanDataSource.Logger.LogFatal<GlobalHelper>(HttpContext.Current.Server.GetLastError());
        }

        public static void Session_Start(object sender, EventArgs e, HttpApplicationState Application, string SessionID)
        {
            Application.Lock();
            try
            {
                var _st = new SecSession();
                //Recupero le informazioni relative al client collegato
                _st.FillData();
                //Leggo la lista degli utenti collegati
                Dictionary<string, SecSession> _userList = (Dictionary<string, SecSession>) Application.Get("TRACK_USER_LIST");
                if (!_userList.ContainsKey(SessionID))
                {
                    //Aggiungo il nuovo utente
                    _userList.Add(SessionID, _st);
                }
                else
                {
                    _userList[SessionID] = _st;
                }

                //Salvo i dati
                Application["TRACK_USER_LIST"] = _userList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {     //Rilascio l'oggetto applicazione
                Application.UnLock();
            }
        }

        public static void Session_End(object sender, EventArgs e, HttpApplicationState Application, string SessionID)
        {
            Application.Lock();
            try
            {
                //Leggo la lista degli utenti collegati
                Dictionary<string, SecSession> _userList = (Dictionary<string, SecSession>) Application.Get("TRACK_USER_LIST");

                if (_userList.ContainsKey(SessionID))
                {
                    _userList.Remove(SessionID);
                    //Salvo i dati
                    Application["TRACK_USER_LIST"] = _userList;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {     //Rilascio l'oggetto applicazione
                Application.UnLock();
            }
        }

        public static void Application_AuthenticateRequest(object sender, EventArgs e, HttpApplicationState Application, HttpCookie Cookies, string userName)
        {
            if (Cookies != null)
            {
                // Blocco l'oggetto applicazione per sincronizzare gli accessi
                Application.Lock();
                try
                {
                    //Leggo la lista degli utenti collegati
                    Dictionary<string, SecSession> _userList = (Dictionary<string, SecSession>) Application.Get("TRACK_USER_LIST");
                    //Leggo i dati relativi all'utente

                    //Se trovo l'utente, aggiorno i dati
                    if (_userList.ContainsKey(Cookies.Value))
                    {
                        SecSession _st = (SecSession) _userList[Cookies.Value];
                        _st.LastVisit = ServiceProvider.CurrentDateTime();
                        _st.UserLogin = userName;
                        _userList[Cookies.Value] = _st;
                        //Salvo i dati
                        Application["TRACK_USER_LIST"] = _userList;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {     //Rilascio l'oggetto applicazione
                    Application.UnLock();
                }
            }
        }
    }
}
