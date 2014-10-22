/*!
* jsModal - A pure JavaScript modal dialog engine v1.0d
* http://jsmodal.com/
*
* Author: Henry Rune Tang Kai <henry@henrys.se>
*
* (c) Copyright 2013 Henry Tang Kai.
*
* License: http://www.opensource.org/licenses/mit-license.php
*
* Date: 2013-7-11
*/

var Modal = (function () {
  "use strict";
  /*global document: false */
  /*global window: false */

  // create object method
  var method = {},
          settings = {},

          modalOverlay = document.createElement('div'),
          modalContainer = document.createElement('div'),
          modalHeader = document.createElement('div'),
          modalHeaderTitle = document.createElement('strong'),
          modalContent = document.createElement('div'),
          modalClose = document.createElement('div'),
          modalCloseTrigger = document.createElement('div'),

          centerModal,

          closeModalEvent,

          defaultSettings = {
            width: 'auto',
            height: 'auto',
            lock: false,
            hideClose: false,
            draggable: false,
            closeAfter: 0,
            openCallback: false,
            closeCallback: false,
            hideOverlay: false
          };

  // Open the modal
  method.open = function (parameters) {
    settings.width = parameters.width || defaultSettings.width;
    settings.height = parameters.height || defaultSettings.height;
    settings.lock = parameters.lock || defaultSettings.lock;
    settings.hideClose = parameters.hideClose || defaultSettings.hideClose;
    settings.draggable = parameters.draggable || defaultSettings.draggable;
    settings.closeAfter = parameters.closeAfter || defaultSettings.closeAfter;
    settings.closeCallback = parameters.closeCallback || defaultSettings.closeCallback;
    settings.openCallback = parameters.openCallback || defaultSettings.openCallback;
    settings.hideOverlay = parameters.hideOverlay || defaultSettings.hideOverlay;

    centerModal = function () {
      method.center({});
    };

    if (parameters.content && !parameters.ajaxContent) {
      modalContent.innerHTML = parameters.content;
    } else if (parameters.ajaxContent && !parameters.content) {
      modalContainer.className = 'modal-loading';
      method.ajax(parameters.ajaxContent, function insertAjaxResult(ajaxResult) {
        modalContent.innerHTML = ajaxResult;
      });
    } else {
      modalContent.innerHTML = '';
    }

    modalContainer.style.width = settings.width;
    modalContainer.style.height = settings.height;

     // The close trigger should not be visible.
     modalCloseTrigger.style.display = 'none';

    if (settings.lock || settings.hideClose) {
      modalClose.style.display = 'none';
    }
    if (!settings.hideOverlay) {
      modalOverlay.style.display = 'block';
    }
    modalContainer.style.display = 'block';

    method.center({});

    if (settings.lock || settings.hideClose) {
      modalClose.style.visibility = 'hidden';
    }
    if (!settings.hideOverlay) {
      modalOverlay.style.visibility = 'visible';
    }
    modalContainer.style.visibility = 'visible';

    /*document.onkeypress = function(e) {
    if (e.keyCode === 27 && settings.lock !== true) {
    method.close();
    }
    };*/

    modalCloseTrigger.onclick = function () {
      if (!settings.hideClose) {
        method.close();
      } else {
        return false;
      }
    };
    modalOverlay.onclick = function () {
      if (!settings.lock) {
        method.close();
      } else {
        return false;
      }
    };

    if (window.addEventListener) {
      window.addEventListener('resize', centerModal, false);
    } else if (window.attachEvent) {
      window.attachEvent('onresize', centerModal);
    }

    if (settings.draggable) {
      modalHeader.style.cursor = 'move';
      modalHeader.onmousedown = function (e) {
        method.drag(e);
        return false;
      };
    } else {
      modalHeader.onmousedown = function () {
        return false;
      };
    }
    if (settings.closeAfter > 0) {
      closeModalEvent = window.setTimeout(function () {
        method.close();
      }, settings.closeAfter * 1000);
    }
    if (settings.openCallback) {
      settings.openCallback();
    }
  };

  // Drag the modal
  method.drag = function (e) {
    var xPosition = (window.event !== undefined) ? window.event.clientX : e.clientX,
            yPosition = (window.event !== undefined) ? window.event.clientY : e.clientY,
            differenceX = xPosition - modalContainer.offsetLeft,
            differenceY = yPosition - modalContainer.offsetTop;

    document.onmousemove = function (e) {
      xPosition = (window.event !== undefined) ? window.event.clientX : e.clientX;
      yPosition = (window.event !== undefined) ? window.event.clientY : e.clientY;

      modalContainer.style.left = ((xPosition - differenceX) > 0) ? (xPosition - differenceX) + 'px' : 0;
      modalContainer.style.top = ((yPosition - differenceY) > 0) ? (yPosition - differenceY) + 'px' : 0;

      document.onmouseup = function () {
        window.document.onmousemove = null;
      };
    };
  };

  // Perform XMLHTTPRequest
  method.ajax = function (url, successCallback) {
    var i,
            XMLHttpRequestObject = false,
            XMLHttpRequestObjects = [
                function () {
                  return new window.XMLHttpRequest();  // IE7+, Firefox, Chrome, Opera, Safari
                },
                function () {
                  return new window.ActiveXObject('Msxml2.XMLHTTP.6.0');
                },
                function () {
                  return new window.ActiveXObject('Msxml2.XMLHTTP.3.0');
                },
                function () {
                  return new window.ActiveXObject('Msxml2.XMLHTTP');
                }
            ];

    for (i = 0; i < XMLHttpRequestObjects.length; i += 1) {
      try {
        XMLHttpRequestObject = XMLHttpRequestObjects[i]();
      } catch (ignore) {
      }

      if (XMLHttpRequestObject !== false) {
        break;
      }
    }

    XMLHttpRequestObject.open('GET', url, true);

    XMLHttpRequestObject.onreadystatechange = function () {
      if (XMLHttpRequestObject.readyState === 4) {
        if (XMLHttpRequestObject.status === 200) {
          successCallback(XMLHttpRequestObject.responseText);
          modalContainer.removeAttribute('class');
        } else {
          successCallback(XMLHttpRequestObject.responseText);
          modalContainer.removeAttribute('class');
        }
      }
    };

    XMLHttpRequestObject.send(null);
  };


  // Close the modal
  method.close = function () {
    try {
      modalContent.innerHTML = '';
      modalOverlay.setAttribute('style', '');
      modalOverlay.style.cssText = '';
      modalOverlay.style.visibility = 'hidden';
      modalOverlay.style.display = 'none';
      modalContainer.setAttribute('style', '');
      modalContainer.style.cssText = '';
      modalContainer.style.visibility = 'hidden';
      modalContainer.style.display = 'none';
      modalHeader.style.cursor = 'default';
      modalClose.setAttribute('style', '');
      modalClose.style.cssText = '';

      if (closeModalEvent) {
        window.clearTimeout(closeModalEvent);
      }

      if (settings.closeCallback) {
         settings.closeCallback();

         // Clears "top.returnValue" used by modals
         delete top.returnValue;
      }

      if (window.removeEventListener) {
        window.removeEventListener('resize', centerModal, false);
      } else if (window.detachEvent) {
        window.detachEvent('onresize', centerModal);
      }
    }
    catch (e) {
      alert('jsModal.close: ' + e.message);
    }
  };

  // Center the modal in the viewport
  method.center = function (parameters) {
    var documentHeight = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight),

            modalWidth = Math.max(modalContainer.clientWidth, modalContainer.offsetWidth),
            modalHeight = Math.max(modalContainer.clientHeight, modalContainer.offsetHeight),

            browserWidth = 0,
            browserHeight = 0,

            amountScrolledX = 0,
            amountScrolledY = 0;

    if (typeof (window.innerWidth) === 'number') {
      browserWidth = window.innerWidth;
      browserHeight = window.innerHeight;
    } else if (document.documentElement && document.documentElement.clientWidth) {
      browserWidth = document.documentElement.clientWidth;
      browserHeight = document.documentElement.clientHeight;
    }

    if (typeof (window.pageYOffset) === 'number') {
      amountScrolledY = window.pageYOffset;
      amountScrolledX = window.pageXOffset;
    } else if (document.body && document.body.scrollLeft) {
      amountScrolledY = document.body.scrollTop;
      amountScrolledX = document.body.scrollLeft;
    } else if (document.documentElement && document.documentElement.scrollLeft) {
      amountScrolledY = document.documentElement.scrollTop;
      amountScrolledX = document.documentElement.scrollLeft;
    }

    if (!parameters.horizontalOnly) {
      modalContainer.style.top = amountScrolledY + (browserHeight / 2) - (modalHeight / 2) + 'px';
    }

    modalContainer.style.left = amountScrolledX + (browserWidth / 2) - (modalWidth / 2) + 'px';

    // per ora uso css... 
    //modalOverlay.style.height = documentHeight + 'px';
    //modalOverlay.style.width = '100%';
  };

  // Set the id's, append the nested elements, and append the complete modal to the document body
  modalOverlay.setAttribute('id', 'modal-overlay');
  modalContainer.setAttribute('id', 'modal-container');
  modalHeader.setAttribute('id', 'modal-header');
  modalContent.setAttribute('id', 'modal-content');
  modalHeaderTitle.setAttribute('id', 'modal-header-title');
  modalClose.setAttribute('id', 'modal-close');
  modalCloseTrigger.setAttribute('id', 'modal-close-trigger');
  modalHeader.appendChild(modalHeaderTitle);
  modalHeader.appendChild(modalClose);
  modalHeader.appendChild(modalCloseTrigger);
  modalContainer.appendChild(modalHeader);
  modalContainer.appendChild(modalContent);

  modalOverlay.style.visibility = 'hidden';
  modalOverlay.style.display = 'none';
  modalContainer.style.visibility = 'hidden';
  modalContainer.style.display = 'none';

  if (window.addEventListener) {
    window.addEventListener('load', function () {
      document.body.appendChild(modalOverlay);
      document.body.appendChild(modalContainer);
    }, false);
  } else if (window.attachEvent) {
    window.attachEvent('onload', function () {
      document.body.appendChild(modalOverlay);
      document.body.appendChild(modalContainer);
    });
  }

  return method;
}());

/* Utilities per  usare jsModal in ASCESI */

// apre una popup non modale con iframe
function openWindow(url, width, height, drag, openCallback, closeCallback) {
  var w, h, d, open, close;

  // width
  if (typeof (arguments[1]) == 'undefined') {
    w = 'auto';
  }
  else {
    w = width + 'px';
  }

  // height
  if (typeof (arguments[2]) == 'undefined') {
    h = 'auto';
  }
  else {
    h = height + 'px';
  }

  // drag
  if (typeof (arguments[3]) == 'undefined') {
    d = true;
  }
  else {
    d = drag;
  }

  // openCallback
  if (typeof (arguments[4]) == 'undefined') {
    open = function openVoidCallback() {
      try {

      } catch (ex) { alert('openVoidCallback: ' + ex.message); }
      return false;
    };
  }
  else {
    open = openCallback;
  }

  // closeCallback
  if (typeof (arguments[5]) == 'undefined') {
    close = function closeVoidCallback() {
      try {

      } catch (ex) { alert('closeVoidCallback: ' + ex.message); }
      return false;
    };
  }
  else {
    close = closeCallback;
  }

  Modal.open({
    content: '<iframe id="modalWindowFrame" src="' + url + '"></iframe>',
    width: w,
    height: h,
    hideClose: false,
    lock: false,
    draggable: d,
    openCallback: open,
    closeCallback: close
  });

  document.getElementById('modalWindowFrame').width = w;
  if (h != 'auto')
    document.getElementById('modalWindowFrame').height = height - 20;
  else
    document.getElementById('modalWindowFrame').height = h;
}

// apre una popup modale contenente semplice html
function openModalWindowNoIFrame(html, width, height, drag, openCallback, closeCallback) {
  var w, h, d, open, close;

  // width
  if (typeof (arguments[1]) == 'undefined') {
    w = 'auto';
  }
  else {
    w = width;
  }

  // height
  if (typeof (arguments[2]) == 'undefined') {
    h = 'auto';
  }
  else {
    h = height;
  }

  // drag
  if (typeof (arguments[3]) == 'undefined') {
    d = true;
  }
  else {
    d = drag;
  }

  // openCallback
  if (typeof (arguments[4]) == 'undefined') {
    open = function openVoidCallback() {
      try {

      } catch (ex) { alert('openVoidCallback: ' + ex.message); }
      return false;
    };
  }
  else {
    open = openCallback;
  }

  // closeCallback
  if (typeof (arguments[5]) == 'undefined') {
    close = function closeVoidCallback() {
      try {

      } catch (ex) { alert('closeVoidCallback: ' + ex.message); }
      return false;
    };
  }
  else {
    close = closeCallback;
  }
  Modal.open({
    content: html,
    width: w,
    height: h,
    hideClose: false,
    draggable: d,
    openCallback: open,
    closeCallback: close
  });

}

// apre una popup modale con un iframe!
function openModalWindow(url, width, height, drag, openCallback, closeCallback) {
  var w, h, d, open, close;

  // width
  if (typeof (arguments[1]) == 'undefined') {
    w = 'auto';
  }
  else {
    w = width + 'px';
  }

  // height
  if (typeof (arguments[2]) == 'undefined') {
    h = 'auto';
  }
  else {
    h = height + 'px';
  }

  // drag
  if (typeof (arguments[3]) == 'undefined') {
    d = true;
  }
  else {
    d = drag;
  }

  // openCallback
  if (typeof (arguments[4]) == 'undefined') {
    open = function openVoidCallback() {
      try {

      } catch (ex) { alert('openVoidCallback: ' + ex.message); }
      return false;
    };
  }
  else {
    open = openCallback;
  }

  // closeCallback
  if (typeof (arguments[5]) == 'undefined') {
    close = function closeVoidCallback() {
      try {

      } catch (ex) { alert('closeVoidCallback: ' + ex.message); }
      return false;
    };
  }
  else {
    close = closeCallback;
  }

  Modal.open({
    content: '<iframe id="modalWindowFrame" src="' + url + '"></iframe>',
    width: w,
    height: h,
    lock: true, // Close button not shown because of lock
    draggable: d,
    openCallback: open,
    closeCallback: close
  });
  window.setTimeout(function () {
    document.getElementById('modalWindowFrame').width = w;
    if (h != 'auto')
      document.getElementById('modalWindowFrame').height = height - 20;
    else
      document.getElementById('modalWindowFrame').height = h;
  }
 , 100);

}

// Opens a modal window
function openModal(options) {
  var openVoidCallback = function () {
    try {

    } catch (ex) { alert('openVoidCallback: ' + ex.message); }
    return false;
  };

  var closeVoidCallback = function () {
    try {

    } catch (ex) { alert('closeVoidCallback: ' + ex.message); }
    return false;
  };

  var defaultOptions = {
    url: "",
    width: 'auto',
    height: 'auto',
    draggable: false,
    openCallback: openVoidCallback,
    closeCallback: closeVoidCallback,
    title: "TITLE",
    closeFunction: "closeWindow();"
  };

   var settings = {}
   settings.url = options.url || defaultOptions.url;
   settings.width = options.width || defaultOptions.width;
   settings.height = options.height || defaultOptions.height;
   settings.draggable = options.draggable || defaultOptions.draggable;
   settings.openCallback = options.openCallback || defaultOptions.openCallback;
   settings.closeCallback = options.closeCallback || defaultOptions.closeCallback;
   settings.title = options.title || defaultOptions.title;
   settings.closeFunction = options.closeFunction || defaultOptions.closeFunction;

  Modal.open({
    content: '<iframe id="modalWindowFrame" src="' + settings.url + '"></iframe>',
    width: settings.width,
    height: settings.height,
    lock: false, // La blocco manualmente più sotto, così che si veda CLOSE
    draggable: settings.draggable,
    openCallback: settings.openCallback,
    closeCallback: settings.closeCallback
  });

   window.setTimeout(function () {
         // Impostazione dimensioni frame
         var modalFrame = $("#modalWindowFrame");
         modalFrame.width(settings.width);
         modalFrame.height(settings.height);
         // Aggiustamento larghezza intestazione
         var modalHeader = $("#modal-header");
         modalHeader.width(modalFrame.outerWidth());
         // Aggiustamento altezza finestra
         $("#modal-container").height($("#modal-container").outerHeight());
         // Impostazione del titolo
         $("#modal-header-title").html(settings.title);
         // Impostazione del bottone di chiusura
         var modalClose = $("#modal-close");
         modalClose.html('<a href="#" class="close">x</a>');
         modalClose.addClass('modal-close');
         modalClose.click(function() { eval(settings.closeFunction); });
         // Rimozione click dall'overlay
         document.getElementById("modal-overlay").onclick = null;
      },
      100
   );
}

function fireClick(node) {
  if (document.createEvent) {
    var evt = document.createEvent('MouseEvents');
    evt.initEvent('click', true, false);
    node.dispatchEvent(evt);
  } else if (document.createEventObject) {
    //node.fireEvent('onclick');
    var evObj = document.createEventObject();
    var res = node.fireEvent('onclick', evObj);
  } else if (typeof node.onclick == 'function') {
    node.onclick();
  }
}

// chiude la popup modale
function closeWindow() {
   var closeBtnTrigger = window.parent.document.getElementById('modal-close-trigger');
   if (closeBtnTrigger == null) {
      closeBtnTrigger = window.parent.parent.document.getElementById('modal-close-trigger');
   }
   if (closeBtnTrigger != null) {
      fireClick(closeBtnTrigger);
   }
}

// devo chiamare l'evento click lato server al ritorno dalla finestra modale
function postbackClick(elemId) {
  sessionStorage.setItem("doServerSide", "true");
  var elemBtn = document.getElementById(elemId);
  if (elemBtn != null) {
    fireClick(elemBtn);
  }
  sessionStorage.setItem("doServerSide", "false");
}

// al click sul bottone apro la finestra modale, mentre al clcik evocato dalla callback al ritorno dalla modale passo la gestione al lato server
function postOnClick(onClickFunction) {
  if (sessionStorage.getItem("doServerSide") == null || sessionStorage.getItem("doServerSide") == "false") {
    return onClickFunction.apply(this, _.toArray(arguments).slice(1));
    //return onClickFunction(arguments[1]);
  }
  else {
    sessionStorage.setItem("doServerSide", "false");
    return true;
  }
}

//
function retCallbackSelMult(btn, form) {
  try {
    if (typeof (top.returnValue) != "undefined" && top.returnValue != null) {
      if (top.returnValue.Return) {
        if (form)
          postbackClick(btn);
        return true;
      } else {
        return false;
      }
    } else {
      return false;
    }
  } catch (ex) { alert('retCallbackSelMult: ' + ex.message); }
  return false;
}

//
function retCallbackLookup(btn) {
  try {
    if (typeof (top.returnValue) != "undefined" && top.returnValue != null) {
      postbackClick(btn);
      return true;
    } else {
      return false;
    }
  } catch (ex) { alert('retCallbackLookup: ' + ex.message); }
  return false;
}