function openHelp () {
	try { 
		__pagcmd = window.location.href.split("/");
		__pagext = __pagcmd[__pagcmd.length - 1].split("?");
		__page = __pagext[0].substring(0, __pagext[0].indexOf("."));
		var win = window.open("_Help/" + __page + ".html", __page, "scrollbars=yes,resizable=yes,width=400,height=500,left=0,top=0,status=no,location=no,toolbar=no,menubar=no");
	  win.focus();
	  return true
	} 
	catch (e) 
	{return true}		
}

function openOldReportViewer(url) {
	window.open('ReportViewer.aspx?' + url,(new Date()).getTime().toString(),'height=' + (window.screen.availHeight - 60) + ', width=' + (window.screen.availWidth - 10) + ', top=0, left=0, menubar=0, location=0, resizable=1, status=0, scrollbars=0, toolbar=0');
	return true;
}

/*
Apertura di una popup con gestione di:
- decode della query string in Base64
- tipo di popup aperta (finestra modale - solo per IE - oppure RadWindow di Telerik)
	
winType: inidica il tipo di finestra da aprire. Se non definito verrà aperta una showModalDialog può valere:
0 : openWindow senza callBack
1 : openWindow con callBack
2 : openWindow con SourceWin senza callBack
3 : openWindow con SourceWin con callBack
*/
function openOldModal(page, qs, base64, width, height, winType, winName, winCb, winSrc) {
   // Eseguo l'encode della query string in un singolo parametro, _qsenc_.
	if (base64 == "1") {
		qs = "_qsenc_=" + Base64.encode(qs);
	}

	var lW = 0;
	if (typeof (arguments[3]) != 'undefined') {
		if (arguments[3] != '') {
			lW = width;
		}
	}
	var lH = 0;
	if (typeof (arguments[4]) != 'undefined') {
		if (arguments[4] != '') {
			lH = height;
		}
	}
	if (typeof (arguments[5]) == 'undefined') {
		if (lW == 0) lW = '800';
		if (lH == 0) lH = '530';
		return window.showModalDialog(page + '?' + qs, null, 'dialogWidth:' + lW + 'px; dialogHeight:' + lH + 'px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
	}
	else {
		var oWin;
		switch (winType) {
			case 0:
				f_OpenWindow(winName, page + '?' + qs, null, lW, lH);
				break;
			case 1:
				f_OpenWindow(winName, page + '?' + qs, winCb, lW, lH);
				break;
			case 2:
				oWin = f_OpenWindow(winName, page + '?' + qs, null, lW, lH);
				oWin.SourceWin = winSrc;
				break;
			case 3:
				oWin = f_OpenWindow(winName, page + '?' + qs, winCb, lW, lH);
				oWin.SourceWin = winSrc;
				break;
			default:
				break;
		}
		return false;
	}
}

function URLEncode( vValue ) {
	// The Javascript escape and unescape functions do not correspond
	// with what browsers actually do...
	var SAFECHARS = "0123456789" +					// Numeric
					"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +	// Alphabetic
					"abcdefghijklmnopqrstuvwxyz" +
					"-_.!~*'()";					// RFC2396 Mark characters
	var HEX = "0123456789ABCDEF";

	var plaintext = vValue;
	var encoded = "";
	
	if (plaintext=='') return plaintext;
	
	for (var i = 0; i < plaintext.length; i++ ) {
		var ch = plaintext.charAt(i);
	    if (ch == " ") {
		    encoded += "+";				// x-www-urlencoded, rather than %20
		} else if (SAFECHARS.indexOf(ch) != -1) {
		    encoded += ch;
		} else {
		   var charCode = ch.charCodeAt(0);
			if (charCode > 255) {
				//Se il carattere è codificato con più di 8 bit non lo codifico
				encoded += ch;
			} else {
				encoded += "%";
				encoded += HEX.charAt((charCode >> 4) & 0xF);
				encoded += HEX.charAt(charCode & 0xF);
			}
		}
	} // for
	
	return encoded;
};

function URLDecode( vValue ){
   // Replace + with ' '
   // Replace %xx with equivalent character
   // Put [ERROR] in output if %xx is invalid.
   var HEXCHARS = "0123456789ABCDEFabcdef"; 
   var encoded = vValue;
   var plaintext = "";
   var i = 0;
   while (i < encoded.length) {
       var ch = encoded.charAt(i);
	   if (ch == "+") {
	       plaintext += " ";
		   i++;
	   } else if (ch == "%") {
			if (i < (encoded.length-2) 
					&& HEXCHARS.indexOf(encoded.charAt(i+1)) != -1 
					&& HEXCHARS.indexOf(encoded.charAt(i+2)) != -1 ) {
				plaintext += unescape( encoded.substr(i,3) );
				i += 3;
			} else {
				//alert( 'Bad escape combination near ...' + encoded.substr(i) );
				plaintext += "%[ERROR]";
				i++;
			}
		} else {
		   plaintext += ch;
		   i++;
		}
	} // while
	
   return plaintext;
};

/* Gestione delle finestre modali in CrossBrowsing */
String.prototype.isArgument=function()
{
	return /^([a-zA-Z]){1,}=([0-9]){1,}$/.test(this);
}

function dialog(url,name,feature,isModal) {
 if(url==null){return false;}
 url = url
 if(name==null){name=""}
 if(feature==null){feature=""};
 if(true)
 {
  	var WindowFeature = new Object();
	WindowFeature["width"] = 400;
	WindowFeature["height"]  =400;
	WindowFeature["left"]  = "";
	WindowFeature["top"]  =  "";
	WindowFeature["resizable"]  = "";

	if(feature !=null && feature!="")
	{
      feature = ( feature.toLowerCase()).split(",");
	
      for(var i=0;i< feature.length;i++)
		{
          if( feature[i].isArgument())
			{
               var featureName = feature[i].split("=")[0];
			   var featureValue = feature[i].split("=")[1];
			  
			   if(WindowFeature[featureName]!=null){WindowFeature[featureName] = featureValue; }
			}
		}
	}
 
  if(WindowFeature["resizable"]==1 || WindowFeature["resizable"]=="1" || WindowFeature["resizable"].toString().toLowerCase()=="yes"){WindowFeature["resizable"] = "resizable:1;minimize:1;maximize:1;"}
  if(WindowFeature["left"]!=""){WindowFeature["left"] ="dialogLeft:" +  WindowFeature["left"] +"px;";}
  if(WindowFeature["top"]!=""){WindowFeature["top"] ="dialogTop:" +  WindowFeature["Top"] +"px;"; }
  if(window.ModelessDialog ==null){window.ModelessDialog = new Object() ; };
  if(name!="")
  {
   if(window.ModelessDialog[name]!=null && !window.ModelessDialog[name].closed )
   {
     window.ModelessDialog[name].focus();
	 return window.ModelessDialog[name];
   }
  }
	var F = WindowFeature["left"] +WindowFeature["top"] +  "dialogWidth:"+WindowFeature["width"] +" px;dialogHeight:"+WindowFeature["height"]+"px;center:1;help:0;" + WindowFeature["resizable"] +"status:0;unadorned:0;edge: raised; ;border:thick;"
	if(isModal)
	{
	   try {
		   return window.showModalDialog(url,self,F);

	   } catch (e) { alert("Historycal error " + e.message); }
	}
	else
	{
		window.ModelessDialog[name] = window.showModelessDialog(url,self,F);
		return window.ModelessDialog[name];
	}	
 }
 else
 {
   if(document.getBoxObjectFor)
   {
	 if(isModal)
	 {		 
		 var Modal = window.open(url,name,"modal=1," + feature);
		 var ModalFocus = function()
		 {
			if (Modal==null) return null;
			
			if(!Modal.closed){
				Modal.focus();
			}else{
				Modal =null;window.removeEventListener("focus", ModalFocus, true); ModalFocus = null; 
			};	
		 }
		 window.addEventListener( "focus", ModalFocus, false ); 
		 return Modal;
	 }
	 else
	 {
		return window.open(url,name,"modal=1," + feature);
	 }	 
   }
   else
   { 
     return window.open(url,name,feature);
   }
 }
 return null;
}

function modal(url,feature){
	return dialog(url,"",feature,true);
}
/* [FINE] Gestione delle finestre modali */

// Removes leading whitespaces
function LTrim(value) {

	var re = /\s*((\S+\s*)*)/;
	return value.replace(re, "$1");

}

// Removes ending whitespaces
function RTrim(value) {

	var re = /((\s*\S+)*)\s*/;
	return value.replace(re, "$1");

}

// Removes leading and ending whitespaces
function trim(value) {

	return LTrim(RTrim(value));

}

function LPad(ContentToSize, PadChar) {
	// Usage LPad('a','0000') --> 000a
	return (PadChar.substr(0, (PadChar.length - ContentToSize.toString().length)) + ContentToSize.toString());
}


function f_getAppPath(path) {

	try {

		if ((path != '/') && (path.length != 0)) path += '/';

	} catch (e) { alert("f_getAppPath " + e.message); }
	return path;
}

function DisableShortcuts() {
	/* Disabilito gli Shortcuts di Mouse e Keywords (tasto destro del mouse, F5, F6, F11...)
	//N.B. - window.event è un oggetto che è presente solo su IE, se non è presente sono su FireFox*/
	/*if (typeof (window.event) == 'undefined') {
		document.oncontextmenu = noRightClick;
	} else {
		document.onmousedown = noRightClick;
	}*/
	//document.onkeydown = FlxKeyHandler;
}