(window["webpackJsonp"] = window["webpackJsonp"] || []).push([[1001],{

/***/ 100000:
/*!*******************************!*\
  !*** ./__extensions_index.ts ***!
  \*******************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var grid_extender_1 = __webpack_require__(/*! ./grid-extender */ 100001);
/**
 * The entry point of the extensions. Each extension bundle needs to have exactly one export
 * of the Extensions interface and it needs to be placed in the __extensions_index file.
 * Here all of the NgModules are returned and are loaded into the main module.
 */
var SamplesExtension = /** @class */ (function () {
    function SamplesExtension() {
    }
    /**
     * On application bootstrap this method is called to get all extensions as angular modules.
     */
    SamplesExtension.prototype.getNgModules = function () {
        return [
            grid_extender_1.GridExtenderModule
        ];
    };
    return SamplesExtension;
}());
exports.SamplesExtension = SamplesExtension;arguments[1].metadata = { compatibleVersionsTags: "", name: "grid.extensions" };


/***/ }),

/***/ 100001:
/*!********************************!*\
  !*** ./grid-extender/index.ts ***!
  \********************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = __webpack_require__(/*! tslib */ 100002);
var core_1 = __webpack_require__(/*! @angular/core */ 100004);
var image_component_1 = __webpack_require__(/*! ./image.component */ 100005);
var columns_provider_1 = __webpack_require__(/*! ./columns-provider */ 100008);
var platform_browser_1 = __webpack_require__(/*! @angular/platform-browser */ 100011);
var http_1 = __webpack_require__(/*! @angular/common/http */ 100006);
var GridExtenderModule = /** @class */ (function () {
    function GridExtenderModule() {
    }
    GridExtenderModule = tslib_1.__decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule,
            ],
            declarations: [
                image_component_1.ImageComponent
            ],
            entryComponents: [
                // The component needs to be registered here as it is instantiated dynamically.
                image_component_1.ImageComponent
            ],
            providers: [
                columns_provider_1.COLUMNS_PROVIDER
            ]
        })
    ], GridExtenderModule);
    return GridExtenderModule;
}());
exports.GridExtenderModule = GridExtenderModule;


/***/ }),

/***/ 100002:
/*!*******************************************************************************!*\
  !*** delegated ./node_modules/tslib/tslib.es6.js from dll-reference adminapp ***!
  \*******************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(0).__iris_require__('./node_modules/tslib/tslib.es6.js')

/***/ }),

/***/ 100003:
/*!***************************!*\
  !*** external "adminapp" ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = adminapp;

/***/ }),

/***/ 100004:
/*!****************************************************************************************!*\
  !*** delegated ./node_modules/@angular/core/fesm5/core.js from dll-reference adminapp ***!
  \****************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/core/fesm5/core.js')

/***/ }),

/***/ 100005:
/*!******************************************!*\
  !*** ./grid-extender/image.component.ts ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = __webpack_require__(/*! tslib */ 100002);
var core_1 = __webpack_require__(/*! @angular/core */ 100004);
var http_1 = __webpack_require__(/*! @angular/common/http */ 100006);
// Get a collection with images in base64 format from image-data.json file
// const imageData = require("./image-data.json");
var ImageComponent = /** @class */ (function () {
    function ImageComponent(httpClient) {
        this.httpClient = httpClient;
        this.imageHeight = 50;
    }
    ImageComponent.prototype.ngOnInit = function () {
        var _this = this;
        var dataItem = this.context.dataItem;
        var imageIdentifier = this.getImageIdentifier(dataItem);
        // Get this image's source for rendering
        if (imageIdentifier) {
            var set = dataItem.metadata.setName;
            var id = dataItem.data.Id;
            var oDataEndpoint = "https://localhost:44308/api/default/" + set + "(" + id + ")?$select=*&$expand=Image&sf_provider=OpenAccessProvider&sf_culture=en&sf_fallback_prop_names=Image.Url";
            this.httpClient.get(oDataEndpoint).subscribe(function (data) {
                _this.imageSource = data.Image[0].Url;
            }, function (error) {
                _this.imageSource = 'Draft';
            });
        }
    };
    // Returns a random number between min (inclusive) and max (exclusive)
    ImageComponent.prototype.getImageIdentifier = function (dataItem) {
        if (dataItem.data.hasOwnProperty('Image')) {
            //  return dataItem.title
            return 1;
        }
        return null;
    };
    ImageComponent = tslib_1.__decorate([
        core_1.Component({
            template: __webpack_require__(/*! ./image.component.html */ 100007)
        }),
        tslib_1.__metadata("design:paramtypes", [http_1.HttpClient])
    ], ImageComponent);
    return ImageComponent;
}());
exports.ImageComponent = ImageComponent;


/***/ }),

/***/ 100006:
/*!******************************************************************************************!*\
  !*** delegated ./node_modules/@angular/common/fesm5/http.js from dll-reference adminapp ***!
  \******************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/common/fesm5/http.js')

/***/ }),

/***/ 100007:
/*!********************************************!*\
  !*** ./grid-extender/image.component.html ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<img [style.height.px]=\"imageHeight\" [src]='imageSource' />\r\n";

/***/ }),

/***/ 100008:
/*!*******************************************!*\
  !*** ./grid-extender/columns-provider.ts ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";

Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = __webpack_require__(/*! tslib */ 100002);
var rxjs_1 = __webpack_require__(/*! rxjs */ 100009);
var v1_1 = __webpack_require__(/*! progress-sitefinity-adminapp-sdk/app/api/v1 */ 100010);
var image_component_1 = __webpack_require__(/*! ./image.component */ 100005);
var core_1 = __webpack_require__(/*! @angular/core */ 100004);
/**
 * The index provider provides the custom columns back to the AdminApp.
 */
var DynamicItemIndexColumnsProvider = /** @class */ (function () {
    function DynamicItemIndexColumnsProvider() {
        this.columnName = "image3";
        this.columnTitle = "Image";
    }
    /**
     * This method gets invoked by the AdminApp when the columns from all of the providers are needed.
     * @param entityData Provides metadata for the current type.
     */
    DynamicItemIndexColumnsProvider.prototype.getColumns = function (entityData) {
        // return the column model
        var column = {
            name: this.columnName,
            title: this.columnTitle,
            // The componentData object holds the type of component to initialize
            // properties can be passed as well. They will be set on the component once it is initialized.
            componentData: {
                type: image_component_1.ImageComponent
            }
        };
        // return an observable here, because this might be a time consuming operation
        return rxjs_1.of([column]);
    };
    DynamicItemIndexColumnsProvider = tslib_1.__decorate([
        core_1.Injectable()
    ], DynamicItemIndexColumnsProvider);
    return DynamicItemIndexColumnsProvider;
}());
/**
 * Export a 'multi' class provider so that multiple instances of the same provider can coexist.
 * This allows for more than one provider to be registered within one or more bundles.
 */
exports.COLUMNS_PROVIDER = {
    useClass: DynamicItemIndexColumnsProvider,
    multi: true,
    provide: v1_1.COLUMNS_TOKEN
};


/***/ }),

/***/ 100009:
/*!********************************************************************************!*\
  !*** delegated ./node_modules/rxjs/_esm5/index.js from dll-reference adminapp ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(0).__iris_require__('./node_modules/rxjs/_esm5/index.js')

/***/ }),

/***/ 100010:
/*!*****************************************************************************************************************!*\
  !*** delegated ./node_modules/progress-sitefinity-adminapp-sdk/app/api/v1/index.js from dll-reference adminapp ***!
  \*****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(0).__iris_require__('./node_modules/progress-sitefinity-adminapp-sdk/app/api/v1/index.js')

/***/ }),

/***/ 100011:
/*!****************************************************************************************************************!*\
  !*** delegated ./node_modules/@angular/platform-browser/fesm5/platform-browser.js from dll-reference adminapp ***!
  \****************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(0).__iris_require__('./node_modules/@angular/platform-browser/fesm5/platform-browser.js')

/***/ })

},[[100000,0]]]);
//# sourceMappingURL=grid.extensions.bundle.js.map