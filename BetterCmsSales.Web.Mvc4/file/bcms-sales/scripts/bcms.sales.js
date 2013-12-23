/*jslint unparam: true, white: true, browser: true, devel: true */
/*global bettercms */
bettercms.define('bcms.sales', ['bcms.jquery', 'bcms', 'bcms.siteSettings', 'bcms.dynamicContent', 'bcms.ko.extenders', 'bcms.ko.grid', 'bcms.modal'],
    function ($, bcms, siteSettings, dynamicContent, ko, kogrid, modal) {
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
                saveBuyerUrl: null,
                deleteBuyerUrl: null,
                loadBuyersUrl: null,

                loadSiteSettingsSuppliersUrl: null,
                saveSupplierUrl: null,
                deleteSupplierUrl: null,
                loadSuppliersUrl: null,

                loadSiteSettingsPurchasesUrl: null,
                createPurchaseUrl: null,
                editPurchaseUrl: null,
                deletePurchaseUrl: null
            },
            globalization = {
                deleteProductDialogTitle: null,
                deleteUnitDialogTitle: null,
                deleteBuyerDialogTitle: null,
                deleteSupplierDialogTitle: null,
                deletePurchaseDialogTitle: null,

                buyersTabTitle: null,
                suppliersTabTitle: null,
                
                editPurchaseTitle: null,
                createNewPurchaseTitle: null
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

            ProductViewModel.prototype.getDeleteConfirmationMessage = function () {
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

        /**
        * Partners list view model
        */
        var PartnersListViewModel = (function (_super) {

            bcms.extendsClass(PartnersListViewModel, _super);

            function PartnersListViewModel(container, items, gridOptions, loadPartnersUrl, deleteConfirmationMessage) {
                _super.call(this, container, loadPartnersUrl, items, gridOptions);

                this.deleteConfirmationMessage = deleteConfirmationMessage;
            }

            PartnersListViewModel.prototype.createItem = function (item) {
                var newItem = new PartnerViewModel(this, item);

                return newItem;
            };

            return PartnersListViewModel;

        })(kogrid.ListViewModel);

        /**
        * Partner view model
        */
        var PartnerViewModel = (function (_super) {

            bcms.extendsClass(PartnerViewModel, _super);

            function PartnerViewModel(parent, item) {
                _super.call(this, parent, item);

                var self = this;

                self.name = ko.observable().extend({ required: "", maxLength: { maxLength: ko.maxLength.name } });
                self.email = ko.observable().extend({ email: "", maxLength: { maxLength: ko.maxLength.email } });
                self.phoneNumber = ko.observable().extend({ maxLength: { maxLength: ko.maxLength.name } });

                self.registerFields(self.name, self.email, self.phoneNumber);

                self.name(item.Name);
                self.email(item.Email);
                self.phoneNumber(item.PhoneNumber);
            }

            PartnerViewModel.prototype.getDeleteConfirmationMessage = function () {
                return $.format(this.parent.deleteConfirmationMessage, this.name());
            };

            PartnerViewModel.prototype.getSaveParams = function () {
                var params = _super.prototype.getSaveParams.call(this);
                params.Name = this.name();
                params.Email = this.email();
                params.PhoneNumber = this.phoneNumber();

                return params;
            };

            return PartnerViewModel;

        })(kogrid.ItemViewModel);

        /*
        * Initializes site settings buyers list
        */
        function initializeSiteSettingsBuyersList(container, json) {
            var data = (json.Success == true) ? json.Data : {};

            var viewModel = new PartnersListViewModel(container, data.Items, data.GridOptions, links.loadBuyersUrl, globalization.deleteBuyerDialogTitle);
            viewModel.deleteUrl = links.deleteBuyerUrl;
            viewModel.saveUrl = links.saveBuyerUrl;

            ko.applyBindings(viewModel, container.get(0));

            // Select search.
            var firstVisibleInputField = container.find('input[type=text],textarea,select').filter(':visible:first');
            if (firstVisibleInputField) {
                firstVisibleInputField.focus();
            }
        }

        /*
        * Initializes site settings suppliers list
        */
        function initializeSiteSettingsSuppliersList(container, json) {
            var data = (json.Success == true) ? json.Data : {};

            var viewModel = new PartnersListViewModel(container, data.Items, data.GridOptions, links.loadSuppliersUrl, globalization.deleteSupplierDialogTitle);
            viewModel.deleteUrl = links.deleteSupplierUrl;
            viewModel.saveUrl = links.saveSupplierUrl;

            ko.applyBindings(viewModel, container.get(0));

            // Select search.
            var firstVisibleInputField = container.find('input[type=text],textarea,select').filter(':visible:first');
            if (firstVisibleInputField) {
                firstVisibleInputField.focus();
            }
        }

        /**
        * Loads a partners tab with suppliers and buyers sub-tabs.
        */
        sales.loadSiteSettingsSalesPartners = function () {
            var tabs = [],
                onShow = function (container) {
                    var firstVisibleInputField = container.find('input[type=text],textarea,select').filter(':visible:first');
                    if (firstVisibleInputField) {
                        firstVisibleInputField.focus();
                    }
                };

            var buyers = new siteSettings.TabViewModel(globalization.buyersTabTitle, links.loadSiteSettingsBuyersUrl, initializeSiteSettingsBuyersList, onShow);
            tabs.push(buyers);

            var suppliers = new siteSettings.TabViewModel(globalization.suppliersTabTitle, links.loadSiteSettingsSuppliersUrl, initializeSiteSettingsSuppliersList, onShow);
            tabs.push(suppliers);

            siteSettings.initContentTabs(tabs);
        };

        function initializeEditPurchaseForm(dialog, json) {
            
        }

        function openEditPurchaseForm(id, postSuccess) {
            var title = (id) ? globalization.editPurchaseTitle : globalization.createNewPurchaseTitle,
                url = (id) ? $.format(links, links.createPurchaseUrl) : links.editPurchaseUrl;

            modal.open({
                title: title,
                onLoad: function (dialog) {
                    dynamicContent.bindDialog(dialog, url, {
                        contentAvailable: initializeEditPurchaseForm,

                        postSuccess: postSuccess
                    });
                }
            });
        }

        /**
        * Purchases list view model
        */
        var PurchasesListViewModel = (function (_super) {

            bcms.extendsClass(PurchasesListViewModel, _super);

            function PurchasesListViewModel(container, items, gridOptions, units) {
                _super.call(this, container, links.loadPurchasesUrl, items, gridOptions);
            }

            PurchasesListViewModel.prototype.createItem = function (item) {
                var newItem = new PurchaseViewModel(this, item);

                return newItem;
            };

            PurchasesListViewModel.prototype.addNewItem = function () {
                openEditPurchaseForm('', function(json) {
                    self.item.unshift(new PurchaseViewModel(self, json.Data));
                });
            };

            return PurchasesListViewModel;

        })(kogrid.ListViewModel);

        /**
        * Purchase view model
        */
        var PurchaseViewModel = (function (_super) {

            bcms.extendsClass(PurchaseViewModel, _super);

            function PurchaseViewModel(parent, item) {
                _super.call(this, parent, item);

                var self = this;

                self.supplierName = ko.observable();
                self.status = ko.observable();
                self.createdOn = ko.observable();

                self.supplierName(item.SupplierName);
                self.status(item.Status);
                self.createdOn(item.CreatedOn);
            }

            PurchaseViewModel.prototype.getDeleteConfirmationMessage = function () {
                return globalization.deletePurchaseDialogTitle;
            };
            
            PurchaseViewModel.prototype.openItem = function () {
                openEditPurchaseForm(self.id(), function (json) {
                    self.version(json.Data.Version);
                });
            };

            return PurchaseViewModel;

        })(kogrid.ItemViewModel);

        /**
        * Initializes loading of list of purchases.
        */
        function initializeSiteSettingsPurchases(json) {
            var container = siteSettings.getMainContainer(),
                data = (json.Success == true) ? json.Data : {};

            var viewModel = new PurchasesListViewModel(container, data.Items, data.GridOptions);

            ko.applyBindings(viewModel, container.get(0));

            // Select search.
            var firstVisibleInputField = container.find('input[type=text],textarea,select').filter(':visible:first');
            if (firstVisibleInputField) {
                firstVisibleInputField.focus();
            }
        }

        /**
        * Loads a purchases list
        */
        sales.loadSiteSettingsPurchases = function () {
            dynamicContent.bindSiteSettings(siteSettings, links.loadSiteSettingsPurchasesUrl, {
                contentAvailable: initializeSiteSettingsPurchases
            });
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