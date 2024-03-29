(function() {
  var CHARACTERS, CHARMAP, INVALID_CHARACTERS, InvalidSequenceError, char, decode, encode, fromCharCode, i, pack, root, unpack, _i, _len, _ref, _ref1, _ref2,
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  /*
   * base64.coffee, v1.0
   * https://github.com/rwz/base64.coffee
   *
   * Copyright 2012 Pavel Pravosud
   * Licensed under the MIT license.
   * http://opensource.org/licenses/mit-license
   *
   * References: http://en.wikipedia.org/wiki/Base64
   *
   * Date: Sat Jan 7 17:30:44 ICT 2012
  */


  fromCharCode = String.fromCharCode;

  CHARACTERS = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

  INVALID_CHARACTERS = /[^a-z\d\+\=\/]/ig;

  CHARMAP = {};

  _ref = CHARACTERS.split('');
  for (i = _i = 0, _len = _ref.length; _i < _len; i = ++_i) {
    char = _ref[i];
    CHARMAP[char] = i;
  }

  InvalidSequenceError = (function(_super) {

    __extends(InvalidSequenceError, _super);

    InvalidSequenceError.prototype.name = 'InvalidSequence';

    function InvalidSequenceError(char) {
      if (char) {
        this.message = "\"" + char + "\" is an invalid Base64 character";
      } else {
        this.message = 'Invalid bytes sequence';
      }
    }

    return InvalidSequenceError;

  })(Error);

  encode = (_ref1 = this.btoa) != null ? _ref1 : this.btoa = function(input) {
    var chr1, chr2, chr3, enc1, enc2, enc3, enc4, invalidChar, output, _j, _len1, _ref2;
    output = '';
    i = 0;
    while (i < input.length) {
      chr1 = input.charCodeAt(i++);
      chr2 = input.charCodeAt(i++);
      chr3 = input.charCodeAt(i++);
      if (invalidChar = Math.max(chr1, chr2, chr3) > 0xFF) {
        throw new InvalidSequenceError(invalidChar);
      }
      enc1 = chr1 >> 2;
      enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
      enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
      enc4 = chr3 & 63;
      if (isNaN(chr2)) {
        enc3 = enc4 = 64;
      } else if (isNaN(chr3)) {
        enc4 = 64;
      }
      _ref2 = [enc1, enc2, enc3, enc4];
      for (_j = 0, _len1 = _ref2.length; _j < _len1; _j++) {
        char = _ref2[_j];
        output += CHARACTERS.charAt(char);
      }
    }
    return output;
  };

  decode = (_ref2 = this.atob) != null ? _ref2 : this.atob = function(input) {
    var chr1, chr2, chr3, enc1, enc2, enc3, enc4, length, output;
    output = '';
    i = 0;
    length = input.length;
    if (length % 4) {
      throw new InvalidSequenceError;
    }
    while (i < length) {
      enc1 = CHARMAP[input.charAt(i++)];
      enc2 = CHARMAP[input.charAt(i++)];
      enc3 = CHARMAP[input.charAt(i++)];
      enc4 = CHARMAP[input.charAt(i++)];
      chr1 = (enc1 << 2) | (enc2 >> 4);
      chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
      chr3 = ((enc3 & 3) << 6) | enc4;
      output += fromCharCode(chr1);
      if (enc3 !== 64) {
        output += fromCharCode(chr2);
      }
      if (enc4 !== 64) {
        output += fromCharCode(chr3);
      }
    }
    return output;
  };

  unpack = function(utfstring) {
    var c, string, _j, _ref3;
    utfstring = utfstring.replace(/\r\n/g, '\n');
    string = '';
    for (i = _j = 0, _ref3 = utfstring.length - 1; 0 <= _ref3 ? _j <= _ref3 : _j >= _ref3; i = 0 <= _ref3 ? ++_j : --_j) {
      c = utfstring.charCodeAt(i);
      if (c < 128) {
        string += fromCharCode(c);
      } else if (c > 127 && c < 2048) {
        string += fromCharCode((c >> 6) | 192);
        string += fromCharCode((c & 63) | 128);
      } else {
        string += fromCharCode((c >> 12) | 224);
        string += fromCharCode(((c >> 6) & 63) | 128);
        string += fromCharCode((c & 63) | 128);
      }
    }
    return string;
  };

  pack = function(string) {
    var c, c1, c2, c3, utfstring;
    utfstring = '';
    i = c = c1 = c2 = 0;
    while (i < string.length) {
      c = string.charCodeAt(i);
      if (c < 128) {
        utfstring += fromCharCode(c);
        i++;
      } else if ((c > 191) && (c < 224)) {
        c2 = string.charCodeAt(i + 1);
        utfstring += fromCharCode(((c & 31) << 6) | (c2 & 63));
        i += 2;
      } else {
        c2 = string.charCodeAt(i + 1);
        c3 = string.charCodeAt(i + 2);
        utfstring += fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
        i += 3;
      }
    }
    return utfstring;
  };

  this.Base64 = {
    encode64: function(str) {
      return encode(unpack(str));
    },
    decode64: function(str) {
      return pack(decode(str.replace(INVALID_CHARACTERS, '')));
    }
  };

}).call(this);
