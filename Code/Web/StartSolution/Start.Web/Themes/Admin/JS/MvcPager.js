﻿function ($) {
   $.fn.initMvcPagers = function () {
       return this.each(function () {
           new mvcPager(this).init();
       });
   };
   function mvcPager(wrapper) {
       this.wrapper = wrapper;
   }
   mvcPager.prototype = {
       wrapper: null,
       url: null,
       pageIndexName: null,
       updateTarget: null,
       onBegin: null,
       onComplete: null,
       onFailure: null,
       onSuccess: null,
       httpMethod: null,
       confirm: null,
       loadingElementId: null,
       loadingDuration: 0,
       partialLoading: null,
       currentPage: null,
       dataFormId: null,
       isFirstLoad: true, //页面是否首次加载
       initIndex: null, //页面第一次打开时的当前页索引，用于点击浏览器返回按钮并返回初始url时Ajax加载相应页面的数据
       allowReload: false,//用于提交html form表单时做为判断是否重新加载数据的标识
       searchCriteria: null, //搜索条件
       init: function () {
           var wrapper = $(this.wrapper);
           var isAjaxPager = wrapper.data("ajax") || false;
           var pageIndexName = wrapper.data("pageparameter");
           this.pageIndexName = pageIndexName;
           if (isAjaxPager) {
               this.updateTarget = wrapper.data("ajax-update");
               this.onBegin = wrapper.data("ajax-begin");
               this.onComplete = wrapper.data("ajax-complete");
               this.onFailure = wrapper.data("ajax-failure");
               this.onSuccess = wrapper.data("ajax-success");
               this.confirm = wrapper.data("ajax-confirm") || undefined;
               this.httpMethod = wrapper.data("ajax-method") || "GET";
               this.loadingElementId = wrapper.data("ajax-loading") || undefined;
               this.dataFormId = wrapper.data("ajax-dataformid") || undefined;
               this.loadingDuration = wrapper.data("ajax-loading-duration") || 0;
               this.partialLoading = wrapper.data("ajax-partialloading") || false;
               this.currentPage = wrapper.data("ajax-currentpage") || 1;
               this.url = wrapper.data("urlformat");
               this.initIndex = this.currentPage;
               var pagerSelector = "[data-mvcpager=true]";
               var hashIndex = getPageIndex(pageIndexName);
               if (hashIndex != this.currentPage && hashIndex > 0)
                   this.loadData(hashIndex, { type: this.httpMethod, data: [] });
               if (this.dataFormId !== undefined) {
                   var context = this;
                   var isAjaxForm = $(context.dataFormId).data("ajax") || false;
                   $(context.dataFormId).submit(function (event) {
                       context.searchCriteria = $(context.dataFormId).serializeArray();
                       if (isAjaxForm) {
                           if (context.currentPage !== 1) {
                               context.currentPage = 1;
                               setPageIndex(context.pageIndexName, -1);
                           } else {
                               context.allowReload = true;
                           }
                       } else {
                           context.allowReload = true;
                           if (context.currentPage === 1) {
                               context.loadData(1, { type: context.httpMethod, data: [] });
                           } else {
                               setPageIndex(context.pageIndexName, 1);
                               context.currentPage = 1;
                           }
                           event.preventDefault();
                       }
                   });
               }
               this.initHashChange();
               $(this.updateTarget).on("click", pagerSelector + " a[data-pageindex]", function (e) {
                   var pindex = $(this).data("pageindex");
                   e.preventDefault();
                   setPageIndex(pageIndexName, pindex);
               });
               $(this.updateTarget).on("keydown", pagerSelector + " input:text", function () {
                   validateInput(event);
               });
               $(this.updateTarget).on("click", pagerSelector + " input[type=button][data-submitbutton=true]", function () {
                   goToPage(this);
               });
               $(this.updateTarget).on("change", pagerSelector + " select[data-autosubmit=true],input:text[data-autosubmit=true]", function () {
                   goToPage(this);
               });
           }
           else {
               jQuery("input:text", this.wrapper).keydown(function (event) { validateInput(event); });
               jQuery("input[type=button][data-submitbutton=true]", this.wrapper).click(function () { goToPage(wrapper); });
               jQuery("select[data-autosubmit=true],input:text[data-autosubmit=true]", this.wrapper).change(function () { goToPage(wrapper); });
           }
       },
       initHashChange: function () {
           var docMode = document.documentMode;
           var context = this;
           if ("onhashchange" in window &&
               (docMode === undefined || docMode > 7)) //IE compatable mode
           {
               $(window).bind("hashchange", function () {
                   var pageIndex = getPageIndex(context.pageIndexName);
                   if (pageIndex === 0)
                       pageIndex = context.initIndex; //浏览器历史返回到无hash值的url时，当前页索引为页面首次打开时的页索引 articles/list/3#id=2返回articles/list/3时，初始页索引为3
                   context.loadData(pageIndex, { type: context.httpMethod, data: [] });
               });
           } else {
               var currentHash = window.location.hash;
               setInterval(function () {
                   if (window.location.hash != currentHash) {
                       currentHash = window.location.hash;
                       var pageIndex = getPageIndex(context.pageIndexName);
                       if (pageIndex === 0)
                           pageIndex = context.initIndex;
                       context.loadData(pageIndex, { type: context.httpMethod, data: [] });
                   }
               }, 200);
           }
       },
       loadData: function (index, options) {
           var context = this;
           if (index === -1 || (index === 0 && context.isFirstLoad) || (index == context.currentPage && !context.allowReload)) {
               return;
           }
           if (context.confirm && !window.confirm(context.confirm)) {
               return;
           }
           $.extend(options, {
               url: this.url.replace("__" + context.pageIndexName + "__", index),
               beforeSend: function (xhr) {
                   var formMethod = options.type.toUpperCase();
                   if (!(formMethod === "GET" || formMethod === "POST")) {
                       xhr.setRequestHeader("X-HTTP-Method-Override", formMethod);
                   }
                   var result = getFunction(context.onBegin, ["xhr"]).apply(this, arguments);
                   if (result !== false && context.loadingElementId !== undefined) {
                       $(context.loadingElementId).show(context.loadingDuration);
                   }
                   return result; //Ajax request will be cancelled if return false
               },
               complete: function (xhr, status) {
                   if (context.loadingElementId !== undefined) {
                       $(context.loadingElementId).hide(context.loadingDuration);
                   }
                   getFunction(context.onComplete, ["xhr", "status"]).apply(this, arguments);
               },
               success: function (data, status, xhr) {
                   if (context.partialLoading)
                       $(context.updateTarget).html($(context.updateTarget, data).html());
                   else
                       $(context.updateTarget).html(data);
                   context.currentPage = index;
                   context.isFirstLoad = false;
                   getFunction(context.onSuccess, ["data", "status", "xhr"]).apply(this, arguments);
               },
               error: getFunction(context.onFailure, ["xhr", "status", "error"])
           });
           if (context.dataFormId !== undefined) {
               pushData(options.data, context.searchCriteria);
           }
           options.data.push({ name: "X-Requested-With", value: "XMLHttpRequest" });
           var method = options.type.toUpperCase();
           if (!(method === "GET" || method === "POST")) {
               options.type = "POST";
               options.data.push({ name: "X-HTTP-Method-Override", value: method });
           }
           $.ajax(options);
       }
   };
   function pushData(dataArr, dataToPush) {
       if (dataToPush !== null && dataToPush !== undefined) {
           for (var i = 0; i < dataToPush.length; i++) {
               dataArr.push({ name: dataToPush[i].name, value: dataToPush[i].value });
           }
       }
   }
   function getPageIndex(pname) {
       var hash = window.location.hash.substring(1);
       if ($.trim(hash) != "") {
           var harr = hash.split('&');
           for (var i = 0; i < harr.length; i++) {
               var hval = harr[i].split("=");
               if (hval[0].toString().toLowerCase() === pname.toString().toLowerCase()) {
                   return parseInt(hval[1]) || 1;
               }
           }
       }
       return 0;
   }
   function setPageIndex(pname, pindex) {
       var hash = window.location.hash.substring(1);
       if ($.trim(hash) == "")
           window.location.hash = pname + "=" + pindex;
       else {
           var r = new RegExp(pname + "=[^\&]*", 'i');
           if (!r.test(hash))
               window.location.hash += "&" + pname + "=" + pindex;
           else {
               var index = hash.replace(r, pname + "=" + pindex);
               window.location.hash = index;
           }
       }
   }
   function getFunction(code, argNames) {
       var fn = window, parts = (code || "").split(".");
       while (fn && parts.length) { fn = fn[parts.shift()]; }
       if (typeof (fn) === "function") { return fn; } //onSuccess="functionName"
       if ($.trim(code).toLowerCase().indexOf("function") == 0) { return new Function("return (" + code + ").apply(this,arguments);"); } //onSuccess="function(data){alert(data);}"
       argNames.push(code);
       try {
           return Function.constructor.apply(null, argNames); //onSuccess="alert('hello');return false;"
       } catch (e) { alert("Error:\r\n" + code + "\r\nis not a valid callback function"); }
   }
   function validateInput(e) {
       var kc, pageIndexBox;
       if (window.event) {
           kc = e.keyCode;
           pageIndexBox = e.srcElement;
       } else if (e.which) {
           kc = e.which;
           pageIndexBox = e.target;
       }
       var valideKeys = [8, 37, 39, 46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105];
       if (kc !== null && valideKeys.indexOf(kc) < 0) {
           if (kc == 13 && pageIndexBox != null) {
               pageIndexBox.click();
           }
           if (e.preventDefault) {
               e.preventDefault();
           } else {
               event.returnValue = false;
           }
       }
   }
   function goToPage(curElement) {
       var pagerContainer = $(curElement).closest("[data-mvcpager=true]");
       var maxPages = pagerContainer.data("maxpages");
       var ivemsg = pagerContainer.data("invalidpageerrmsg");
       var oremsg = pagerContainer.data("outrangeerrmsg");
       var firstPageUrl = pagerContainer.data("firstpage");
       var urlformat = pagerContainer.data("urlformat");
       var pageIndexName = pagerContainer.data("pageparameter");
       var isAjaxPager = pagerContainer.data("ajax");
       var pageIndex = 0;
       var box = pagerContainer.find("select[data-pageindexbox=true],input:text[data-pageindexbox=true]");
       if (box.length > 0)
           pageIndex = box.val();
       var r = new RegExp("^\\s*(\\d+)\\s*$");
       if (!r.test(pageIndex)) {
           alert(ivemsg);
           return;
       } else if (RegExp.$1 < 1 || RegExp.$1 > maxPages) {
           alert(oremsg);
           return;
       }
       if (isAjaxPager) {
           setPageIndex(pageIndexName, pageIndex);
       } else {
           if (typeof firstPageUrl !== "undefined" && firstPageUrl !== false && pageIndex == "1")
               self.location.href = firstPageUrl;
           else
               self.location.href = decodeURI(urlformat).replace("__" + pageIndexName + "__", pageIndex);
       }
   }
)(jQuery);
(function () { $("[data-mvcpager=true]").initMvcPagers(); });