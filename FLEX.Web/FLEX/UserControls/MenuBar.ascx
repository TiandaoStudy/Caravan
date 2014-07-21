<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuBar.ascx.cs" Inherits="FLEX.Web.UserControls.MenuBar" %>
<%@ Import Namespace="FLEX.Web" %>
<%@ Import Namespace="FLEX.Web.MasterPages" %>

<style>
   @media (min-width: 768px) {
      .navbar-nav .caret { display: none; }

      .navbar-nav .open ul { display: none; }

      .navbar-default .navbar-nav > .open > a,
      .navbar-default .navbar-nav > .open > a:hover,
      .navbar-default .navbar-nav > .open > a:focus,
      .navbar-default .navbar-nav > .active > a,
      .navbar-default .navbar-nav > .active > a:hover,
      .navbar-default .navbar-nav > .active > a:focus { background: none; }

      .navbar-inverse .navbar-nav > .open > a,
      .navbar-inverse .navbar-nav > .open > a:hover,
      .navbar-inverse .navbar-nav > .open > a:focus,
      .navbar-inverse .navbar-nav > .active > a,
      .navbar-inverse .navbar-nav > .active > a:hover,
      .navbar-inverse .navbar-nav > .active > a:focus { background: none; }

      .navbar-default .navbar-nav > .hovernav:hover > a,
      .navbar-default .navbar-nav > .hovernav:hover > a:hover,
      .navbar-default .navbar-nav > .hovernav:hover > a:focus { background: transparent; }

      .navbar-inverse .navbar-nav > .hovernav:hover > a,
      .navbar-inverse .navbar-nav > .hovernav:hover > a:hover,
      .navbar-inverse .navbar-nav > .hovernav:hover > a:focus { background: transparent; }

      .navbar-nav .hovernav:hover > .dropdown-menu { display: block; }
   }


   .dropdown-submenu { position: relative; }

   .dropdown-submenu > .dropdown-menu {
      top: 0;
      left: 100%;
      margin-top: -6px;
      margin-left: -1px;
      -webkit-border-radius: 0 6px 6px 6px;
      -moz-border-radius: 0 6px 6px 6px;
      border-radius: 0 6px 6px 6px;
   }

   .dropdown-submenu:hover > .dropdown-menu { display: block; }

   .dropdown-submenu > a:after {
      display: block;
      content: " ";
      float: right;
      width: 0;
      height: 0;
      border-color: transparent;
      border-style: solid;
      border-width: 5px 0 5px 5px;
      border-left-color: #cccccc;
      margin-top: 5px;
      margin-right: -10px;
   }

   .dropdown-submenu:hover > a:after { border-left-color: #ffffff; }

   .dropdown-submenu .pull-left { float: none; }

   .dropdown-submenu.pull-left > .dropdown-menu {
      left: -100%;
      margin-left: 10px;
      -webkit-border-radius: 6px 0 6px 6px;
      -moz-border-radius: 6px 0 6px 6px;
      border-radius: 6px 0 6px 6px;
   }
</style>

<script lang="javascript">
   function FLEX_RunFromMenu(url) {
      try {
         document.location.href = '<%= Head.FlexPath %>/Pages/MenuNavigator.aspx?' + url;
      } catch (e) {
         alert(e.message);
      }
   }

   function FLEX_OWFromMenu(url) {
      var dID = new Date();
      try {
         window.open(url, dID.getTime().toString(), 'height=' + (window.screen.availHeight - 60) + ', width=' + (window.screen.availWidth - 10) + ', top=0, left=0, menubar=0, location=0, resizable=1, status=0, scrollbars=0, toolbar=0');
      } catch (e) {
         alert(e.message);
      }
   }

   function FLEX_NewBrowserMenu(url) {
      var dID = new Date();
      try {
         window.open(url, dID.getTime(), 'fullscreen=no,scrollbars=yes,status=yes,toolbar=yes,resizable=yes,menubar=yes,left=0,top=0');
      } catch (e) {
         alert(e.message);
      }
   }

   $(document).ready(function() {
      $('ul.navbar-nav li').addClass('hovernav');
   });
</script>

<header>
   <div id="menuBar" class="navbar navbar-default navbar-fixed-top">
      <div class="container-fluid">
         <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-default-collapse">
               <span class="icon-bar"></span>
               <span class="icon-bar"></span>
               <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#" onclick="<%= WebSettings.UserControls_MenuBar_BrandClick %>">
               <img src='<%= Head.MyFlexPath %>/Images/logo-menubar.png' class='logo' alt='Logo' />&nbsp;<%= WebSettings.ProjectName %>
            </a>
         </div>
         <div id="navbar-default-collapse" class="navbar-collapse collapse">
            <ul class="nav navbar-nav" id="ul_menu" runat="server"></ul>
            <ul class="nav navbar-nav navbar-right">
               <li>
                  <a href="#" onclick="<%= WebSettings.UserControls_MenuBar_HomeClick %>">
                     <span class="glyphicon glyphicon-home padded-icon"></span>Home
                  </a>
               </li>
               <li>
                  <a href="#" onclick="<%= WebSettings.UserControls_MenuBar_LogoutClick %>">
                     <span class="glyphicon glyphicon-circle-arrow-left padded-icon"></span>Logout
                  </a>
               </li>
               <li>
                  <a href="#" onclick="<%= WebSettings.UserControls_MenuBar_InfoClick %>">
                     <span class="glyphicon glyphicon-info-sign padded-icon"></span>Info
                  </a>
               </li>
            </ul>
         </div>
      </div>
   </div>
</header>