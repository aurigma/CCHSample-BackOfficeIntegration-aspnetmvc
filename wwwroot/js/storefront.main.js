window.Aurigma=function(t){var e={};function n(r){if(e[r])return e[r].exports;var o=e[r]={i:r,l:!1,exports:{}};return t[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}return n.m=t,n.c=e,n.d=function(t,e,r){n.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:r})},n.r=function(t){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},n.t=function(t,e){if(1&e&&(t=n(t)),8&e)return t;if(4&e&&"object"==typeof t&&t&&t.__esModule)return t;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var o in t)n.d(r,o,function(e){return t[e]}.bind(null,o));return r},n.n=function(t){var e=t&&t.__esModule?function(){return t.default}:function(){return t};return n.d(e,"a",e),e},n.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},n.p="",n(n.s=50)}([function(t,e,n){"use strict";var r=n(7),o=Object.prototype.toString;function i(t){return"[object Array]"===o.call(t)}function s(t){return void 0===t}function a(t){return null!==t&&"object"==typeof t}function u(t){if("[object Object]"!==o.call(t))return!1;var e=Object.getPrototypeOf(t);return null===e||e===Object.prototype}function c(t){return"[object Function]"===o.call(t)}function f(t,e){if(null!=t)if("object"!=typeof t&&(t=[t]),i(t))for(var n=0,r=t.length;n<r;n++)e.call(null,t[n],n,t);else for(var o in t)Object.prototype.hasOwnProperty.call(t,o)&&e.call(null,t[o],o,t)}t.exports={isArray:i,isArrayBuffer:function(t){return"[object ArrayBuffer]"===o.call(t)},isBuffer:function(t){return null!==t&&!s(t)&&null!==t.constructor&&!s(t.constructor)&&"function"==typeof t.constructor.isBuffer&&t.constructor.isBuffer(t)},isFormData:function(t){return"undefined"!=typeof FormData&&t instanceof FormData},isArrayBufferView:function(t){return"undefined"!=typeof ArrayBuffer&&ArrayBuffer.isView?ArrayBuffer.isView(t):t&&t.buffer&&t.buffer instanceof ArrayBuffer},isString:function(t){return"string"==typeof t},isNumber:function(t){return"number"==typeof t},isObject:a,isPlainObject:u,isUndefined:s,isDate:function(t){return"[object Date]"===o.call(t)},isFile:function(t){return"[object File]"===o.call(t)},isBlob:function(t){return"[object Blob]"===o.call(t)},isFunction:c,isStream:function(t){return a(t)&&c(t.pipe)},isURLSearchParams:function(t){return"undefined"!=typeof URLSearchParams&&t instanceof URLSearchParams},isStandardBrowserEnv:function(){return("undefined"==typeof navigator||"ReactNative"!==navigator.product&&"NativeScript"!==navigator.product&&"NS"!==navigator.product)&&("undefined"!=typeof window&&"undefined"!=typeof document)},forEach:f,merge:function t(){var e={};function n(n,r){u(e[r])&&u(n)?e[r]=t(e[r],n):u(n)?e[r]=t({},n):i(n)?e[r]=n.slice():e[r]=n}for(var r=0,o=arguments.length;r<o;r++)f(arguments[r],n);return e},extend:function(t,e,n){return f(e,(function(e,o){t[o]=n&&"function"==typeof e?r(e,n):e})),t},trim:function(t){return t.replace(/^\s*/,"").replace(/\s*$/,"")},stripBOM:function(t){return 65279===t.charCodeAt(0)&&(t=t.slice(1)),t}}},,function(t,e,n){t.exports=n(17)},function(t,e,n){"use strict";n.d(e,"a",(function(){return r}));class r{constructor(t){this.getCookies=()=>{const t=this._doc.cookie;return t?t.split(";").reduce((t,e,n,r)=>{const o=e.split("=");if(2!==o.length)throw new Error("Each cookie should be an equal sign separated string.");return t[o[0].trim()]=o[1],t},{}):{}},this._doc=t}getLanguage(){const t=this._doc.querySelector("html").getAttribute("lang");return Boolean(t)?t.substr(0,2):"EN"}}},function(t,e,n){"use strict";n.d(e,"a",(function(){return i}));var r=n(2),o=n.n(r);class i{constructor(t,e){this.apiUrl=null!=e?e:"https://customerscanvashub.com/",this.tenantId=t,this.http=o.a.create({baseURL:null!=e?e:"https://customerscanvashub.com/",validateStatus:t=>t>=200&&t<300})}async getIntegrationInfo(t){const e=await this.http.get(`/api/v1/tenants/${this.tenantId}/integrations/${t}`);return this.getDataFrom(e)}async getTemplateIntegrationInfo(t){const e=await this.http.get(`/api/v1/tenants/${this.tenantId}/integrations/template/${t}`);return this.getDataFrom(e)}async getToken(t,e){const n=await this.http.post(`/api/v1/tenants/${this.tenantId}/integrations/gettoken`,{userGuid:t,ecommerceDomain:e,origin:window.location.origin});return this.getDataFrom(n)}async getTokenAndUserId(t,e){const n=await this.http.post(`/api/v1/tenants/${this.tenantId}/integrations/getusertoken`,{userGuid:t,ecommerceDomain:e,origin:window.location.origin});return this.getDataFrom(n)}async getCcToken(t,e){const n=await this.http.post(`/api/v1/tenants/${this.tenantId}/integrations/getcctoken`,{userGuid:t,ecommerceDomain:e,origin:window.location.origin});return this.getDataFrom(n)}getAdditionalProducts(t,e,n){const r="https://customerscanvashub.com/"+`api/v1/tenants/${t}/integrations/GetAdditionalProducts`,o={origin:window.location.origin,ecommerceDomain:e,additionalProducts:n};return fetch(r,{method:"POST",headers:{"Content-Type":"application/json"},body:JSON.stringify(o)}).then(t=>t.json()).then(t=>{if(t.success)return t.result;throw new Error(t.error)})}getDataFrom(t){if(!this.http.defaults.validateStatus(t.status)||!t.data.success)throw new Error(`HTTP ${t.status} ${t.statusText}. ${JSON.stringify(t.data)}`);return t.data.result}}},function(t,e,n){"use strict";function r(t,e){return"string"==typeof t?o(t,e):Promise.all(t.map(t=>o(t.importData,t.url,t.isDefault)))}function o(t,e,n=!1){return new Promise((r,o)=>{const i="resolve_callback_"+Math.round(1e5*Math.random());window[i]=(...e)=>{delete window[i],document.head.removeChild(s);const n=Array.from(e);let o=t.split(",");o=o.map(t=>t.trim());const a=n.reduce((t,e,n)=>(t[o[n]]=e,t),{});r(a)};const s=document.createElement("script");s.type="module",s.onerror=t=>{console.error("Failer load script from "+e),console.error(t),o(t)},e+=(e.indexOf("?")>=0?"&":"?")+"t="+Date.now(),s.text=n?`import ${t} from '${e}'; ${i}(${t}.default || ${t} );`:`import { ${t} } from '${e}'; ${i}(${t});`,document.head.appendChild(s)})}n.d(e,"a",(function(){return r}))},function(t,e,n){"use strict";function r(){var t;let e=(new Date).getTime(),n=null!==(t=performance&&performance.now&&1e3*performance.now())&&void 0!==t?t:0;return"xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g,t=>{var r=16*Math.random();return e>0?(r=(e+r)%16|0,e=Math.floor(e/16)):(r=(n+r)%16|0,n=Math.floor(n/16)),("x"===t?r:3&r|8).toString(16)})}function o(t){return t.match(/^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$/i)}n.d(e,"a",(function(){return r})),n.d(e,"b",(function(){return o}))},function(t,e,n){"use strict";t.exports=function(t,e){return function(){for(var n=new Array(arguments.length),r=0;r<n.length;r++)n[r]=arguments[r];return t.apply(e,n)}}},function(t,e,n){"use strict";var r=n(0);function o(t){return encodeURIComponent(t).replace(/%3A/gi,":").replace(/%24/g,"$").replace(/%2C/gi,",").replace(/%20/g,"+").replace(/%5B/gi,"[").replace(/%5D/gi,"]")}t.exports=function(t,e,n){if(!e)return t;var i;if(n)i=n(e);else if(r.isURLSearchParams(e))i=e.toString();else{var s=[];r.forEach(e,(function(t,e){null!=t&&(r.isArray(t)?e+="[]":t=[t],r.forEach(t,(function(t){r.isDate(t)?t=t.toISOString():r.isObject(t)&&(t=JSON.stringify(t)),s.push(o(e)+"="+o(t))})))})),i=s.join("&")}if(i){var a=t.indexOf("#");-1!==a&&(t=t.slice(0,a)),t+=(-1===t.indexOf("?")?"?":"&")+i}return t}},function(t,e,n){"use strict";t.exports=function(t){return!(!t||!t.__CANCEL__)}},function(t,e,n){"use strict";(function(e){var r=n(0),o=n(23),i={"Content-Type":"application/x-www-form-urlencoded"};function s(t,e){!r.isUndefined(t)&&r.isUndefined(t["Content-Type"])&&(t["Content-Type"]=e)}var a,u={adapter:(("undefined"!=typeof XMLHttpRequest||void 0!==e&&"[object process]"===Object.prototype.toString.call(e))&&(a=n(11)),a),transformRequest:[function(t,e){return o(e,"Accept"),o(e,"Content-Type"),r.isFormData(t)||r.isArrayBuffer(t)||r.isBuffer(t)||r.isStream(t)||r.isFile(t)||r.isBlob(t)?t:r.isArrayBufferView(t)?t.buffer:r.isURLSearchParams(t)?(s(e,"application/x-www-form-urlencoded;charset=utf-8"),t.toString()):r.isObject(t)?(s(e,"application/json;charset=utf-8"),JSON.stringify(t)):t}],transformResponse:[function(t){if("string"==typeof t)try{t=JSON.parse(t)}catch(t){}return t}],timeout:0,xsrfCookieName:"XSRF-TOKEN",xsrfHeaderName:"X-XSRF-TOKEN",maxContentLength:-1,maxBodyLength:-1,validateStatus:function(t){return t>=200&&t<300}};u.headers={common:{Accept:"application/json, text/plain, */*"}},r.forEach(["delete","get","head"],(function(t){u.headers[t]={}})),r.forEach(["post","put","patch"],(function(t){u.headers[t]=r.merge(i)})),t.exports=u}).call(this,n(22))},function(t,e,n){"use strict";var r=n(0),o=n(24),i=n(26),s=n(8),a=n(27),u=n(30),c=n(31),f=n(12);t.exports=function(t){return new Promise((function(e,n){var p=t.data,l=t.headers;r.isFormData(p)&&delete l["Content-Type"];var d=new XMLHttpRequest;if(t.auth){var h=t.auth.username||"",m=t.auth.password?unescape(encodeURIComponent(t.auth.password)):"";l.Authorization="Basic "+btoa(h+":"+m)}var v=a(t.baseURL,t.url);if(d.open(t.method.toUpperCase(),s(v,t.params,t.paramsSerializer),!0),d.timeout=t.timeout,d.onreadystatechange=function(){if(d&&4===d.readyState&&(0!==d.status||d.responseURL&&0===d.responseURL.indexOf("file:"))){var r="getAllResponseHeaders"in d?u(d.getAllResponseHeaders()):null,i={data:t.responseType&&"text"!==t.responseType?d.response:d.responseText,status:d.status,statusText:d.statusText,headers:r,config:t,request:d};o(e,n,i),d=null}},d.onabort=function(){d&&(n(f("Request aborted",t,"ECONNABORTED",d)),d=null)},d.onerror=function(){n(f("Network Error",t,null,d)),d=null},d.ontimeout=function(){var e="timeout of "+t.timeout+"ms exceeded";t.timeoutErrorMessage&&(e=t.timeoutErrorMessage),n(f(e,t,"ECONNABORTED",d)),d=null},r.isStandardBrowserEnv()){var g=(t.withCredentials||c(v))&&t.xsrfCookieName?i.read(t.xsrfCookieName):void 0;g&&(l[t.xsrfHeaderName]=g)}if("setRequestHeader"in d&&r.forEach(l,(function(t,e){void 0===p&&"content-type"===e.toLowerCase()?delete l[e]:d.setRequestHeader(e,t)})),r.isUndefined(t.withCredentials)||(d.withCredentials=!!t.withCredentials),t.responseType)try{d.responseType=t.responseType}catch(e){if("json"!==t.responseType)throw e}"function"==typeof t.onDownloadProgress&&d.addEventListener("progress",t.onDownloadProgress),"function"==typeof t.onUploadProgress&&d.upload&&d.upload.addEventListener("progress",t.onUploadProgress),t.cancelToken&&t.cancelToken.promise.then((function(t){d&&(d.abort(),n(t),d=null)})),p||(p=null),d.send(p)}))}},function(t,e,n){"use strict";var r=n(25);t.exports=function(t,e,n,o,i){var s=new Error(t);return r(s,e,n,o,i)}},function(t,e,n){"use strict";var r=n(0);t.exports=function(t,e){e=e||{};var n={},o=["url","method","data"],i=["headers","auth","proxy","params"],s=["baseURL","transformRequest","transformResponse","paramsSerializer","timeout","timeoutMessage","withCredentials","adapter","responseType","xsrfCookieName","xsrfHeaderName","onUploadProgress","onDownloadProgress","decompress","maxContentLength","maxBodyLength","maxRedirects","transport","httpAgent","httpsAgent","cancelToken","socketPath","responseEncoding"],a=["validateStatus"];function u(t,e){return r.isPlainObject(t)&&r.isPlainObject(e)?r.merge(t,e):r.isPlainObject(e)?r.merge({},e):r.isArray(e)?e.slice():e}function c(o){r.isUndefined(e[o])?r.isUndefined(t[o])||(n[o]=u(void 0,t[o])):n[o]=u(t[o],e[o])}r.forEach(o,(function(t){r.isUndefined(e[t])||(n[t]=u(void 0,e[t]))})),r.forEach(i,c),r.forEach(s,(function(o){r.isUndefined(e[o])?r.isUndefined(t[o])||(n[o]=u(void 0,t[o])):n[o]=u(void 0,e[o])})),r.forEach(a,(function(r){r in e?n[r]=u(t[r],e[r]):r in t&&(n[r]=u(void 0,t[r]))}));var f=o.concat(i).concat(s).concat(a),p=Object.keys(t).concat(Object.keys(e)).filter((function(t){return-1===f.indexOf(t)}));return r.forEach(p,c),n}},function(t,e,n){"use strict";function r(t){this.message=t}r.prototype.toString=function(){return"Cancel"+(this.message?": "+this.message:"")},r.prototype.__CANCEL__=!0,t.exports=r},function(t,e,n){"use strict";n.d(e,"a",(function(){return i}));var r=n(2),o=n.n(r);class i{constructor({url:t,tenantId:e,http:n}){this.apiUrl=t,this.tenantId=e,this.http=null!=n?n:o.a}async warmUp(t,e,n){for(let r=0;r<t;r++){const o=await this.http.get(this.apiUrl+"/ping");switch(o.status){case 200:case 201:case 204:return!0;case 503:if(r<t-1){const t=new Promise(t=>setTimeout(t,1e3*e));Boolean(n)&&n(),await t}break;default:throw new Error(`Unexpected response from service: HTTP ${o.status} ${o.statusText}`)}}return!1}async post(t){return(await this.http.post(`${this.apiUrl}/tenants/${this.tenantId}/projects/create`,t,{headers:{"Content-Type":"application/json"}})).data}}},,function(t,e,n){"use strict";var r=n(0),o=n(7),i=n(18),s=n(13);function a(t){var e=new i(t),n=o(i.prototype.request,e);return r.extend(n,i.prototype,e),r.extend(n,e),n}var u=a(n(10));u.Axios=i,u.create=function(t){return a(s(u.defaults,t))},u.Cancel=n(14),u.CancelToken=n(32),u.isCancel=n(9),u.all=function(t){return Promise.all(t)},u.spread=n(33),u.isAxiosError=n(34),t.exports=u,t.exports.default=u},function(t,e,n){"use strict";var r=n(0),o=n(8),i=n(19),s=n(20),a=n(13);function u(t){this.defaults=t,this.interceptors={request:new i,response:new i}}u.prototype.request=function(t){"string"==typeof t?(t=arguments[1]||{}).url=arguments[0]:t=t||{},(t=a(this.defaults,t)).method?t.method=t.method.toLowerCase():this.defaults.method?t.method=this.defaults.method.toLowerCase():t.method="get";var e=[s,void 0],n=Promise.resolve(t);for(this.interceptors.request.forEach((function(t){e.unshift(t.fulfilled,t.rejected)})),this.interceptors.response.forEach((function(t){e.push(t.fulfilled,t.rejected)}));e.length;)n=n.then(e.shift(),e.shift());return n},u.prototype.getUri=function(t){return t=a(this.defaults,t),o(t.url,t.params,t.paramsSerializer).replace(/^\?/,"")},r.forEach(["delete","get","head","options"],(function(t){u.prototype[t]=function(e,n){return this.request(a(n||{},{method:t,url:e,data:(n||{}).data}))}})),r.forEach(["post","put","patch"],(function(t){u.prototype[t]=function(e,n,r){return this.request(a(r||{},{method:t,url:e,data:n}))}})),t.exports=u},function(t,e,n){"use strict";var r=n(0);function o(){this.handlers=[]}o.prototype.use=function(t,e){return this.handlers.push({fulfilled:t,rejected:e}),this.handlers.length-1},o.prototype.eject=function(t){this.handlers[t]&&(this.handlers[t]=null)},o.prototype.forEach=function(t){r.forEach(this.handlers,(function(e){null!==e&&t(e)}))},t.exports=o},function(t,e,n){"use strict";var r=n(0),o=n(21),i=n(9),s=n(10);function a(t){t.cancelToken&&t.cancelToken.throwIfRequested()}t.exports=function(t){return a(t),t.headers=t.headers||{},t.data=o(t.data,t.headers,t.transformRequest),t.headers=r.merge(t.headers.common||{},t.headers[t.method]||{},t.headers),r.forEach(["delete","get","head","post","put","patch","common"],(function(e){delete t.headers[e]})),(t.adapter||s.adapter)(t).then((function(e){return a(t),e.data=o(e.data,e.headers,t.transformResponse),e}),(function(e){return i(e)||(a(t),e&&e.response&&(e.response.data=o(e.response.data,e.response.headers,t.transformResponse))),Promise.reject(e)}))}},function(t,e,n){"use strict";var r=n(0);t.exports=function(t,e,n){return r.forEach(n,(function(n){t=n(t,e)})),t}},function(t,e){var n,r,o=t.exports={};function i(){throw new Error("setTimeout has not been defined")}function s(){throw new Error("clearTimeout has not been defined")}function a(t){if(n===setTimeout)return setTimeout(t,0);if((n===i||!n)&&setTimeout)return n=setTimeout,setTimeout(t,0);try{return n(t,0)}catch(e){try{return n.call(null,t,0)}catch(e){return n.call(this,t,0)}}}!function(){try{n="function"==typeof setTimeout?setTimeout:i}catch(t){n=i}try{r="function"==typeof clearTimeout?clearTimeout:s}catch(t){r=s}}();var u,c=[],f=!1,p=-1;function l(){f&&u&&(f=!1,u.length?c=u.concat(c):p=-1,c.length&&d())}function d(){if(!f){var t=a(l);f=!0;for(var e=c.length;e;){for(u=c,c=[];++p<e;)u&&u[p].run();p=-1,e=c.length}u=null,f=!1,function(t){if(r===clearTimeout)return clearTimeout(t);if((r===s||!r)&&clearTimeout)return r=clearTimeout,clearTimeout(t);try{r(t)}catch(e){try{return r.call(null,t)}catch(e){return r.call(this,t)}}}(t)}}function h(t,e){this.fun=t,this.array=e}function m(){}o.nextTick=function(t){var e=new Array(arguments.length-1);if(arguments.length>1)for(var n=1;n<arguments.length;n++)e[n-1]=arguments[n];c.push(new h(t,e)),1!==c.length||f||a(d)},h.prototype.run=function(){this.fun.apply(null,this.array)},o.title="browser",o.browser=!0,o.env={},o.argv=[],o.version="",o.versions={},o.on=m,o.addListener=m,o.once=m,o.off=m,o.removeListener=m,o.removeAllListeners=m,o.emit=m,o.prependListener=m,o.prependOnceListener=m,o.listeners=function(t){return[]},o.binding=function(t){throw new Error("process.binding is not supported")},o.cwd=function(){return"/"},o.chdir=function(t){throw new Error("process.chdir is not supported")},o.umask=function(){return 0}},function(t,e,n){"use strict";var r=n(0);t.exports=function(t,e){r.forEach(t,(function(n,r){r!==e&&r.toUpperCase()===e.toUpperCase()&&(t[e]=n,delete t[r])}))}},function(t,e,n){"use strict";var r=n(12);t.exports=function(t,e,n){var o=n.config.validateStatus;n.status&&o&&!o(n.status)?e(r("Request failed with status code "+n.status,n.config,null,n.request,n)):t(n)}},function(t,e,n){"use strict";t.exports=function(t,e,n,r,o){return t.config=e,n&&(t.code=n),t.request=r,t.response=o,t.isAxiosError=!0,t.toJSON=function(){return{message:this.message,name:this.name,description:this.description,number:this.number,fileName:this.fileName,lineNumber:this.lineNumber,columnNumber:this.columnNumber,stack:this.stack,config:this.config,code:this.code}},t}},function(t,e,n){"use strict";var r=n(0);t.exports=r.isStandardBrowserEnv()?{write:function(t,e,n,o,i,s){var a=[];a.push(t+"="+encodeURIComponent(e)),r.isNumber(n)&&a.push("expires="+new Date(n).toGMTString()),r.isString(o)&&a.push("path="+o),r.isString(i)&&a.push("domain="+i),!0===s&&a.push("secure"),document.cookie=a.join("; ")},read:function(t){var e=document.cookie.match(new RegExp("(^|;\\s*)("+t+")=([^;]*)"));return e?decodeURIComponent(e[3]):null},remove:function(t){this.write(t,"",Date.now()-864e5)}}:{write:function(){},read:function(){return null},remove:function(){}}},function(t,e,n){"use strict";var r=n(28),o=n(29);t.exports=function(t,e){return t&&!r(e)?o(t,e):e}},function(t,e,n){"use strict";t.exports=function(t){return/^([a-z][a-z\d\+\-\.]*:)?\/\//i.test(t)}},function(t,e,n){"use strict";t.exports=function(t,e){return e?t.replace(/\/+$/,"")+"/"+e.replace(/^\/+/,""):t}},function(t,e,n){"use strict";var r=n(0),o=["age","authorization","content-length","content-type","etag","expires","from","host","if-modified-since","if-unmodified-since","last-modified","location","max-forwards","proxy-authorization","referer","retry-after","user-agent"];t.exports=function(t){var e,n,i,s={};return t?(r.forEach(t.split("\n"),(function(t){if(i=t.indexOf(":"),e=r.trim(t.substr(0,i)).toLowerCase(),n=r.trim(t.substr(i+1)),e){if(s[e]&&o.indexOf(e)>=0)return;s[e]="set-cookie"===e?(s[e]?s[e]:[]).concat([n]):s[e]?s[e]+", "+n:n}})),s):s}},function(t,e,n){"use strict";var r=n(0);t.exports=r.isStandardBrowserEnv()?function(){var t,e=/(msie|trident)/i.test(navigator.userAgent),n=document.createElement("a");function o(t){var r=t;return e&&(n.setAttribute("href",r),r=n.href),n.setAttribute("href",r),{href:n.href,protocol:n.protocol?n.protocol.replace(/:$/,""):"",host:n.host,search:n.search?n.search.replace(/^\?/,""):"",hash:n.hash?n.hash.replace(/^#/,""):"",hostname:n.hostname,port:n.port,pathname:"/"===n.pathname.charAt(0)?n.pathname:"/"+n.pathname}}return t=o(window.location.href),function(e){var n=r.isString(e)?o(e):e;return n.protocol===t.protocol&&n.host===t.host}}():function(){return!0}},function(t,e,n){"use strict";var r=n(14);function o(t){if("function"!=typeof t)throw new TypeError("executor must be a function.");var e;this.promise=new Promise((function(t){e=t}));var n=this;t((function(t){n.reason||(n.reason=new r(t),e(n.reason))}))}o.prototype.throwIfRequested=function(){if(this.reason)throw this.reason},o.source=function(){var t;return{token:new o((function(e){t=e})),cancel:t}},t.exports=o},function(t,e,n){"use strict";t.exports=function(t){return function(e){return t.apply(null,e)}}},function(t,e,n){"use strict";t.exports=function(t){return"object"==typeof t&&!0===t.isAxiosError}},,,function(t,e,n){"use strict";n.d(e,"a",(function(){return r}));class r{static fromCart(t,e){const n=new r;return n.line_items=t.lineItems.map(t=>{var n,r,o,i,s;return{key:e(t),quantity:t.quantity,group:null===(n=t.props)||void 0===n?void 0:n._group,userId:null===(r=t.props)||void 0===r?void 0:r._userId,stateId:null===(o=t.props)||void 0===o?void 0:o._stateId,hidden:null===(i=t.props)||void 0===i?void 0:i._hidden,fields:null===(s=t.props)||void 0===s?void 0:s._fields,properties:t.props}}),n}}},,,,,function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.Subscription=void 0;var r=function(){function t(t,e){this.handler=t,this.isOnce=e,this.isExecuted=!1}return t.prototype.execute=function(t,e,n){if(!this.isOnce||!this.isExecuted){this.isExecuted=!0;var r=this.handler;t?setTimeout((function(){r.apply(e,n)}),1):r.apply(e,n)}},t}();e.Subscription=r},,function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.NonUniformEventList=e.EventList=e.EventHandlingBase=e.EventDispatcher=void 0;var r=n(45);Object.defineProperty(e,"EventDispatcher",{enumerable:!0,get:function(){return r.EventDispatcher}}),Object.defineProperty(e,"EventHandlingBase",{enumerable:!0,get:function(){return r.EventHandlingBase}}),Object.defineProperty(e,"EventList",{enumerable:!0,get:function(){return r.EventList}}),Object.defineProperty(e,"NonUniformEventList",{enumerable:!0,get:function(){return r.NonUniformEventList}})},function(t,e,n){"use strict";var r,o=this&&this.__extends||(r=function(t,e){return(r=Object.setPrototypeOf||{__proto__:[]}instanceof Array&&function(t,e){t.__proto__=e}||function(t,e){for(var n in e)Object.prototype.hasOwnProperty.call(e,n)&&(t[n]=e[n])})(t,e)},function(t,e){function n(){this.constructor=t}r(t,e),t.prototype=null===e?Object.create(e):(n.prototype=e.prototype,new n)});Object.defineProperty(e,"__esModule",{value:!0}),e.EventHandlingBase=e.EventList=e.NonUniformEventList=e.EventDispatcher=void 0;var i=n(46),s=function(t){function e(){return t.call(this)||this}return o(e,t),e.prototype.dispatch=function(t,e){this._dispatch(!1,this,arguments)},e.prototype.dispatchAsync=function(t,e){this._dispatch(!0,this,arguments)},e.prototype.asEvent=function(){return t.prototype.asEvent.call(this)},e}(i.DispatcherBase);e.EventDispatcher=s;var a=function(){function t(){this._events={}}return t.prototype.get=function(t){if(this._events[t])return this._events[t];var e=this.createDispatcher();return this._events[t]=e,e},t.prototype.remove=function(t){delete this._events[t]},t.prototype.createDispatcher=function(){return new s},t}();e.NonUniformEventList=a;var u=function(t){function e(){return t.call(this)||this}return o(e,t),e.prototype.createDispatcher=function(){return new s},e}(i.EventListBase);e.EventList=u;var c=function(){function t(){this._events=new u}return Object.defineProperty(t.prototype,"events",{get:function(){return this._events},enumerable:!1,configurable:!0}),t.prototype.subscribe=function(t,e){this._events.get(t).subscribe(e)},t.prototype.sub=function(t,e){this.subscribe(t,e)},t.prototype.unsubscribe=function(t,e){this._events.get(t).unsubscribe(e)},t.prototype.unsub=function(t,e){this.unsubscribe(t,e)},t.prototype.one=function(t,e){this._events.get(t).one(e)},t.prototype.has=function(t,e){return this._events.get(t).has(e)},t}();e.EventHandlingBase=c},function(t,e,n){"use strict";
/*!
 * Strongly Typed Events for TypeScript - Core
 * https://github.com/KeesCBakker/StronlyTypedEvents/
 * http://keestalkstech.com
 *
 * Copyright Kees C. Bakker / KeesTalksTech
 * Released under the MIT license
 */Object.defineProperty(e,"__esModule",{value:!0}),e.Subscription=e.EventListBase=e.DispatcherWrapper=e.DispatcherBase=void 0;var r=n(47);Object.defineProperty(e,"DispatcherBase",{enumerable:!0,get:function(){return r.DispatcherBase}}),Object.defineProperty(e,"DispatcherWrapper",{enumerable:!0,get:function(){return r.DispatcherWrapper}}),Object.defineProperty(e,"EventListBase",{enumerable:!0,get:function(){return r.EventListBase}});var o=n(42);Object.defineProperty(e,"Subscription",{enumerable:!0,get:function(){return o.Subscription}})},function(t,e,n){"use strict";var r=this&&this.__spreadArrays||function(){for(var t=0,e=0,n=arguments.length;e<n;e++)t+=arguments[e].length;var r=Array(t),o=0;for(e=0;e<n;e++)for(var i=arguments[e],s=0,a=i.length;s<a;s++,o++)r[o]=i[s];return r};Object.defineProperty(e,"__esModule",{value:!0}),e.DispatcherWrapper=e.EventListBase=e.DispatcherBase=void 0;var o=n(48),i=n(42),s=function(){function t(){this._wrap=new u(this),this._subscriptions=new Array}return Object.defineProperty(t.prototype,"count",{get:function(){return this._subscriptions.length},enumerable:!1,configurable:!0}),t.prototype.subscribe=function(t){var e=this;return t&&this._subscriptions.push(new i.Subscription(t,!1)),function(){e.unsubscribe(t)}},t.prototype.sub=function(t){return this.subscribe(t)},t.prototype.one=function(t){var e=this;return t&&this._subscriptions.push(new i.Subscription(t,!0)),function(){e.unsubscribe(t)}},t.prototype.has=function(t){return!!t&&this._subscriptions.some((function(e){return e.handler==t}))},t.prototype.unsubscribe=function(t){if(t)for(var e=0;e<this._subscriptions.length;e++)if(this._subscriptions[e].handler==t){this._subscriptions.splice(e,1);break}},t.prototype.unsub=function(t){this.unsubscribe(t)},t.prototype._dispatch=function(t,e,n){for(var i=this,s=function(r){var s=new o.EventManagement((function(){return i.unsub(r.handler)})),u=Array.prototype.slice.call(n);if(u.push(s),r.execute(t,e,u),a.cleanup(r),!t&&s.propagationStopped)return"break"},a=this,u=0,c=r(this._subscriptions);u<c.length;u++){if("break"===s(c[u]))break}},t.prototype.cleanup=function(t){if(t.isOnce&&t.isExecuted){var e=this._subscriptions.indexOf(t);e>-1&&this._subscriptions.splice(e,1)}},t.prototype.asEvent=function(){return this._wrap},t.prototype.clear=function(){this._subscriptions.splice(0,this._subscriptions.length)},t}();e.DispatcherBase=s;var a=function(){function t(){this._events={}}return t.prototype.get=function(t){var e=this._events[t];return e||(e=this.createDispatcher(),this._events[t]=e,e)},t.prototype.remove=function(t){delete this._events[t]},t}();e.EventListBase=a;var u=function(){function t(t){this._subscribe=function(e){return t.subscribe(e)},this._unsubscribe=function(e){return t.unsubscribe(e)},this._one=function(e){return t.one(e)},this._has=function(e){return t.has(e)},this._clear=function(){return t.clear()},this._count=function(){return t.count}}return Object.defineProperty(t.prototype,"count",{get:function(){return this._count()},enumerable:!1,configurable:!0}),t.prototype.subscribe=function(t){return this._subscribe(t)},t.prototype.sub=function(t){return this.subscribe(t)},t.prototype.unsubscribe=function(t){this._unsubscribe(t)},t.prototype.unsub=function(t){this.unsubscribe(t)},t.prototype.one=function(t){return this._one(t)},t.prototype.has=function(t){return this._has(t)},t.prototype.clear=function(){this._clear()},t}();e.DispatcherWrapper=u},function(t,e,n){"use strict";Object.defineProperty(e,"__esModule",{value:!0}),e.EventManagement=void 0;var r=function(){function t(t){this.unsub=t,this.propagationStopped=!1}return t.prototype.stopPropagation=function(){this.propagationStopped=!0},t}();e.EventManagement=r},,function(t,e,n){"use strict";var r;n.r(e),function(t){t[t.Custom=0]="Custom",t[t.Shopify=1]="Shopify"}(r||(r={}));var o=n(4);class i{constructor(t){this._apiClient=t}async findById(t){return await this._apiClient.getTemplateIntegrationInfo(t)}async findByProduct(t){return await this._apiClient.getIntegrationInfo(t)}}var s=n(3);var a=n(5);async function u(t){const e=[{importData:"editor",url:t.uiFrameworkUrl+"/editor.js",isDefault:!0},{importData:"ecommerceDriver",url:t.uiFrameworkUrl+c(t.ecommerceSystemType)}],n=await Object(a.a)(e);return{editor:n[0].editor,driver:n[1].ecommerceDriver}}function c(t){switch(t){case r.Shopify:return"/drivers/shopify-driver.js";default:return"/drivers/default-driver.js"}}class f{constructor(t){this._apiClient=t}async getBackOfficeCredentials(t,e){return await this._apiClient.getTokenAndUserId(t,e)}async getEditorToken(t,e){return await this._apiClient.getCcToken(t,e)}}var p=n(37),l=n(6),d=n(44);n(15);class h{constructor(){this.useProjectBuffer=!0}}class m{constructor(){this.options=new h}}class v{}class g{constructor(t){var e;this._onEditorFinish=new d.EventDispatcher,this.createBogusProduct=()=>({id:0,sku:"PRODUCT-001",title:"Test Product",description:"This is a test product.",options:[],price:1,attributes:[]}),this.options=t,this._apiClient=new o.a(t.tenantId,null!==(e=t.backOfficeUrl)&&void 0!==e?e:"https://customerscanvashub.com/"),this.templateService=new i(this._apiClient),this.userService=new f(this._apiClient)}get onFinish(){return this._onEditorFinish.asEvent()}get templates(){return this.templateService}async loadEditor(t,e,n,r,o){const i=await this.prepareEditor(e,n,r,o);return await this.renderEditor(i.driver,t)}async prepareEditor(t,e,n,r){var o;null!=n||(n=this.createBogusProduct());const i=await u(t);let s=this.prepareConfig(t.config);const a=this.getEditorSettings(t,this.options.pluginSettings);null!==(o=a.language)&&void 0!==o||(a.language=s.language);const c=await this.userService.getBackOfficeCredentials(e.id,this.options.backOfficeUrl);return i.driver=await i.driver.init(n,i.editor,s,a,r,n.quantity,{id:e.id,tokenId:await this.userService.getEditorToken(e.id,window.location.origin),data:e.data},{assetProcessorUrl:t.assetProcessorUrl,assetStorageUrl:t.assetStorageUrl,designAtomsApiUrl:t.designAtomsApiUrl,tenantId:this.options.tenantId,userId:c.userId,token:c.token}),i.driver.config.vars=i.driver.config.vars||{},i.driver.config.vars.token=c.token,i.driver.config.vars.backOfficeUserId=c.userId,i.driver.config.vars.unitId=this.options.tenantId,this.putAttributesToDriver(t.templateAttributes.map(t=>({name:t.name,value:t.value})),i.driver),i.driver.cart.onSubmitting.subscribe(t=>{this._onEditorFinish.dispatch(this,p.a.fromCart(t,t=>Object(l.a)()))}),i}async renderEditor(t,e){return e.innerHTML="",t.products.current.renderEditor(e,this.options.themeSettings,this.options.localization),new Promise((n,r)=>{e.addEventListener("load",()=>{n(t)})})}putAttributesToDriver(t,e){t.forEach(t=>{try{const e=JSON.parse(t.value);"object"==typeof e&&(t.value=e)}catch(t){}}),e.products.current.attributes.push(...t)}prepareConfig(t){var e,n;let r=JSON.parse(t);r=null!==(e=r.config)&&void 0!==e?e:r;const o=new s.a(document).getLanguage();return null!==(n=r.language)&&void 0!==n||(r.language=o),r}getEditorSettings(t,e){switch(t.ecommerceSystemType){case r.Custom:return Object.assign(Object.assign({},e),{customersCanvasBaseUrl:t.customerCanvasUrl});case r.Shopify:return Object.assign(Object.assign({},e),{editorUrl:t.customerCanvasUrl});default:return Object.assign(Object.assign({},e),{customersCanvasBaseUrl:t.customerCanvasUrl})}}}n.d(e,"BackOfficeOptions",(function(){return m})),n.d(e,"User",(function(){return v})),n.d(e,"BackOffice",(function(){return g}))}]);