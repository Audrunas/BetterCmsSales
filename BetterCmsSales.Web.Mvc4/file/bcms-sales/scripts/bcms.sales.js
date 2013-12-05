/*jslint unparam: true, white: true, browser: true, devel: true */
/*global bettercms */
bettercms.define('bcms.sales', ['bcms.jquery', 'bcms', 'bcms.siteSettings', 'bcms.dynamicContent', 'bcms.ko.extenders', 'bcms.ko.grid'],
    function ($, bcms, siteSettings, dynamicContent, ko, kogrid) {
        'use strict';

        var sales = {},
            selectors = {},
            links = {
                loadSiteSettingsProductsUrl: null,
                loadProductsUrl: null,
                saveProductUrl: null,
                deleteProductUrl: null,
                
                loadSiteSettingsUnitsUrl: null,
                loadUnitsUrl: null,
                saveUnitUrl: null,
                deleteUnitUrl: null,
                
                loadSiteSettingsBuyersUrl: null,
                loadSiteSettingsSuppliersUrl: null
            },
            globalization = {
                deleteProductDialogTitle: null,
                deleteUnitDialogTitle: null,
                buyersTabTitle: null,
                suppliersTabTitle: null
            };

        /**
        * Assign objects to module.
        */
        sales.links = links;
        sales.globalization = globalization;
        sales.selectors = selectors;

        /**
        * Products list view model
        */
        var ProductsListViewModel = (function (_super) {

            bcms.extendsClass(ProductsListViewModel, _super);

            function ProductsListViewModel(container, items, gridOptions, units) {
                _super.call(this, container, links.loadProductsUrl, items, gridOptions);

                var self = this;
                self.units = [];
                
                if (units && $.isArray(units)) {
                    for (var i = 0; i < units.length; i++) {
                        self.units.push({ id: units[i].Id, name: units[i].Title });
                    }
                }
            }

            ProductsListViewModel.prototype.createItem = function (item) {
                var newItem = new ProductViewModel(this, item);
                
                if (this.units && this.units.length > 0 && !item.Unit && !item.UnitName) {
                    newItem.unit(this.units[0].id);
                    newItem.unitName(this.units[0].name);
                }

                return newItem;
            };

            return ProductsListViewModel;

        })(kogrid.ListViewModel);

        /**
        * Product view model
        */
        var ProductViewModel = (function (_super) {

            bcms.extendsClass(ProductViewModel, _super);

            function ProductViewModel(parent, item) {
                _super.call(this, parent, item);

                var self = this;

                self.name = ko.observable().extend({ required: "", maxLength: { maxLength: ko.maxLength.name } });
                self.unit = ko.observable();
                self.unitName = ko.observable();

                self.registerFields(self.name, self.unit);

                self.name(item.Name);
                self.unit(item.Unit);
                self.unitName(item.UnitName);
            }

            ProductViewModel.prototype.getDeleteConfirmationMessage = function() {
                return $.format(globalization.deleteProductDialogTitle, this.name());
            };

            ProductViewModel.prototype.getSaveParams = function () {
                var params = _super.prototype.getSaveParams.call(this);
                params.Name = this.name();
                params.Unit = this.unit();

                return params;
            };

            return ProductViewModel;

        })(kogrid.ItemViewModel);

        /**
        * Initializes loading of list of products.
        */
        function initializeSiteSettingsProducts(json) {
            var container = siteSettings.getMainContainer(),
                data = (json.Success == true) ? json.Data : {};

            var viewModel = new ProductsListViewModel(container, data.Items, data.GridOptions, data.Units);
            viewModel.deleteUrl = links.deleteProductUrl;
            viewModel.saveUrl = links.saveProductUrl;
            
            ko.applyBindings(viewModel, container.get(0));
            
            // Select search.
            var firstVisibleInputField = container.find('input[type=text],textarea,select').filter(':visible:first');
            if (firstVisibleInputField) {
                firstVisibleInputField.focus();
            }
        }

        /**
        * Loads a products list to the site settings container.
        */
        sales.loadSiteSettingsSalesProducts = function () {
            dynamicContent.bindSiteSettings(siteSettings, links.loadSiteSettingsProductsUrl, {
                contentAvailable: initializeSiteSettingsProducts
            });
        };

        /**
        * Units list view model
        */
        var UnitsListViewModel = (function (_super) {

            bcms.extendsClass(UnitsListViewModel, _super);

            function UnitsListViewModel(container, items, gridOptions) {
                _super.call(this, container, links.loadUnitsUrl, items, gridOptions);
            }

            UnitsListViewModel.prototype.createItem = function (item) {
                var newItem = new UnitViewModel(this, item);
                return newItem;
            };

            return UnitsListViewModel;

        })(kogrid.ListViewModel);

        /**
        * Unit view model
        */
        var UnitViewModel = (function (_super) {

            bcms.extendsClass(UnitViewModel, _super);

            function UnitViewModel(parent, item) {
                _super.call(this, parent, item);

                var self = this;

                self.title = ko.observable().extend({ required: "", maxLength: { maxLength: ko.maxLength.name } });
                self.shortTitle = ko.observable().extend({ required: "", maxLength: { maxLength: ko.maxLength.name } });

                self.registerFields(self.title, self.shortTitle);

                self.title(item.Title);
                self.shortTitle(item.ShortTitle);
            }

            UnitViewModel.prototype.getDeleteConfirmationMessage = function () {
                return $.format(globalization.deleteUnitDialogTitle, this.title());
            };

            UnitViewModel.prototype.getSaveParams = function () {
                var params = _super.prototype.getSaveParams.call(this);
                params.Title = this.title();
                params.ShortTitle = this.shortTitle();

                return params;
            };

            return UnitViewModel;

        })(kogrid.ItemViewModel);

        /**
        * Initializes loading of list of units.
        */
        function initializeSiteSettingsUnits(json) {
            var container = siteSettings.getMainContainer(),
                data = (json.Success == true) ? json.Data : {};

            var viewModel = new UnitsListViewModel(container, data.Items, data.GridOptions);
            viewModel.deleteUrl = links.deleteUnitUrl;
            viewModel.saveUrl = links.saveUnitUrl;
            
            ko.applyBindings(viewModel, container.get(0));
            
            // Select search.
            var firstVisibleInputField = container.find('input[type=text],textarea,select').filter(':visible:first');
            if (firstVisibleInputField) {
                firstVisibleInputField.focus();
            }
        }

        /**
        * Loads a units list to the site settings container.
        */
        sales.loadSiteSettingsSalesUnits = function () {
            dynamicContent.bindSiteSettings(siteSettings, links.loadSiteSettingsUnitsUrl, {
                contentAvailable: initializeSiteSettingsUnits
            });
        };
        
        /*
        * Initializes site settings buyers list
        */
        function initializeSiteSettingsBuyersList() {
            
        }

        /*
        * Initializes site settings suppliers list
        */
        function initializeSiteSettingsSuppliersList() {
            
        }

        /**
        * Loads a partners tab with suppliers and buyers sub-tabs.
        */
        sales.loadSiteSettingsSalesPartners = function () {
            var tabs = [],
                onShow = function(container) {
                    var firstVisibleInputField = container.find('input[type=text],textarea,select').filter(':visible:first');
                    if (firstVisibleInputField) {
                        firstVisibleInputField.focus();
                    }
                };
            
            var buyers = new siteSettings.TabViewModel(globalization.buyersTabTitle, links.loadSiteSettingsBuyersUrl, initializeSiteSettingsBuyersList, onShow);
            tabs.push(buyers);
            
            var suppliers = new siteSettings.TabViewModel(globalization.suppliersTabTitle, links.loadSiteSettingsSupplierUrl, initializeSiteSettingsSuppliersList, onShow);
            tabs.push(suppliers);
            
            siteSettings.initContentTabs(tabs);
        };

        /**
        * Initializes sales module.
        */
        sales.init = function () {
            bcms.logger.debug('Initializing bcms.sales module.');
        };

        /**
        * Register initialization.
        */
        bcms.registerInit(sales.init);

        return sales;
    });