using System;
using System.IO.Compression;
using System.Web;
using Finsa.Caravan.Common.DataModel.Security;
using Finsa.Caravan.DataAccess;
using PommaLabs.KVLite;
using System.Collections.Generic;

namespace Finsa.Caravan.WebForms
{
   public sealed class GlobalHelper
   {
      private GlobalHelper()
      {
         throw new InvalidOperationException();
      }

      public static void Application_Start(object sender, EventArgs args, string connectionString, HttpApplicationState Application)
      {
         // Sets the default connection string.
         DataAccess.Configuration.Instance.ConnectionString = connectionString;

         // Run vacuum on the persistent cache. It should be put AFTER the connection string is set,
         // since that string it stored on the cache itself and we do not want conflicts, right?
         PersistentCache.DefaultInstance.VacuumAsync();

         Application.Add("TRACK_USER_LIST", new Dictionary<string, SessionTracker>());
      }

      public static void Application_End(object sender, EventArgs args, HttpApplicationState Application)
      {
         Application.Remove("TRACK_USER_LIST");
      }

      public static void Application_PreSendRequestHeaders(object sender, EventArgs args)
      {
         // Ensures that, if GZip/Deflate Encoding is applied, then that headers are set.
         // Also works when error occurs if filters are still active.
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
         // Removes any special filtering, especially GZip filtering.
         // Assigning the response filter to a dummy variable is required
         // in order to avoid a .NET issue which is triggered by setting
         // the Filter property, without reading it first.
         var dummy = HttpContext.Current.Response.Filter;
         HttpContext.Current.Response.Filter = null;

         // Logs the error into the DB.
         Db.Logger.LogFatal<GlobalHelper>(HttpContext.Current.Server.GetLastError());
      }


      public static void Session_Start(object sender, EventArgs e, HttpApplicationState Application, string SessionID)
      {
         Application.Lock();
         try
         {
            var _st = new SessionTracker();
            //Recupero le informazioni relative al client collegato
            _st.FillData();
            //Leggo la lista degli utenti collegati
            Dictionary<string, SessionTracker> _userList = (Dictionary<string, SessionTracker>)Application.Get("TRACK_USER_LIST");
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
            Dictionary<string, SessionTracker> _userList = (Dictionary<string, SessionTracker>)Application.Get("TRACK_USER_LIST");

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
               Dictionary<string, SessionTracker> _userList = (Dictionary<string, SessionTracker>)Application.Get("TRACK_USER_LIST");
               //Leggo i dati relativi all'utente

               //Se trovo l'utente, aggiorno i dati
               if (_userList.ContainsKey(Cookies.Value))
               {
                  SessionTracker _st = (SessionTracker)_userList[Cookies.Value];
                  _st.LastVisit = DateTime.Now;
                  _st.Login = userName;
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