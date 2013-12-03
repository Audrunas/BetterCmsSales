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
                deleteProductUrl: null
            },
            globalization = {
                deleteProductDialogTitle: null
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

            function ProductsListViewModel(container, items, gridOptions) {
                _super.call(this, container, links.loadProductsUrl, items, gridOptions);
            }

            ProductsListViewModel.prototype.createItem = function (item) {
                var newItem = new ProductViewModel(this, item);
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

                self.registerFields(self.name);

                self.name(item.Name);
            }

            ProductViewModel.prototype.getDeleteConfirmationMessage = function() {
                return $.format(globalization.deleteProductDialogTitle, this.email());
            };

            ProductViewModel.prototype.getSaveParams = function () {
                var params = _super.prototype.getSaveParams.call(this);
                params.Name = this.name();

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

            var viewModel = new ProductsListViewModel(container, data.Items, data.GridOptions);
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
        * Loads a partners tab with suppliers and buyers sub-tabs.
        */
        sales.loadSiteSettingsSalesPartners = function () {
            alert('TODO');
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