using System.Linq;
using System.Web.Mvc;
using Nop.Admin.Extensions;
using Nop.Admin.Models.Vendors;
using Nop.Core.Domain.Vendors;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Services.Catalog;
using Nop.Web.Framework.Mvc;
using System;
using GoogleMaps.LocationServices;
using Nop.Services.Directory;

namespace Nop.Admin.Controllers
{
    public partial class VendorController : BaseAdminController
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IVendorService _vendorService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IPermissionService _permissionService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly VendorSettings _vendorSettings;
        private readonly ICityService _cityService;
        private readonly IStateProvinceService _stateProvinceService;

        #endregion

        #region Constructors

        public VendorController(ICustomerService customerService, 
            ILocalizationService localizationService,
            IVendorService vendorService, 
            IManufacturerService manufacturerService,
            IPermissionService permissionService,
            IUrlRecordService urlRecordService,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            VendorSettings vendorSettings,
            ICityService cityService,
            IStateProvinceService stateProvinceService)
        {
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._vendorService = vendorService;
            this._manufacturerService = manufacturerService;
            this._permissionService = permissionService;
            this._urlRecordService = urlRecordService;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._vendorSettings = vendorSettings;
            this._cityService = cityService;
            this._stateProvinceService = stateProvinceService;
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void UpdateLocales(Vendor vendor, VendorModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(vendor,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.Description,
                                                           localized.Description,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                //search engine name
                var seName = vendor.ValidateSeName(localized.SeName, localized.Name, false);
                _urlRecordService.SaveSlug(vendor, seName, localized.LanguageId);
            }
        }

        #endregion

        #region Methods

        //list
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var model = new VendorListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, VendorListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendors = _vendorService.GetAllVendors(model.SearchName, command.Page - 1, command.PageSize, true);
            var gridModel = new DataSourceResult
            {
                Data = vendors.Select(x =>
                {
                    var vendorModel = x.ToModel();
                    return vendorModel;
                }),
                Total = vendors.TotalCount,
            };

            return Json(gridModel);
        }

        //create

        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();


            var model = new VendorModel();
            //locales
            AddLocales(_languageService, model.Locales);
            //default values
            model.PageSize = 4;
            model.Active = true;
            model.AllowCustomersToSelectPageSize = true;
            model.PageSizeOptions = _vendorSettings.DefaultVendorPageSizeOptions;
            model.AvailableCities = _cityService.GetCitiesForCombo();

            //default value
            model.Active = true;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public ActionResult Create(VendorModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var vendor = model.ToEntity();
                
                //fill latitude and longitude from address
                if (vendor.StreetAddress != null && vendor.StreetAddress != "")
                {
                    var city = _cityService.GetCityById(vendor.CityId.Value);
                    vendor.StateId = city.StateId;
                    var state = _stateProvinceService.GetStateProvinceById(vendor.StateId.Value);
                    vendor.CountryId = state.CountryId;
                    GoogleLocationService locservice = new GoogleLocationService();
                    MapPoint point = locservice.GetLatLongFromAddress(vendor.StreetAddress + ", " + city.CityName + ", " + state.Abbreviation + " " + city.PostCode);
                    vendor.Latitude = point.Latitude;
                    vendor.Longitude = point.Longitude;
                }

                _vendorService.InsertVendor(vendor);
                //search engine name
                model.SeName = vendor.ValidateSeName(model.SeName, vendor.Name, true);
                _urlRecordService.SaveSlug(vendor, model.SeName, 0);
                //locales
                UpdateLocales(vendor, model);

                SuccessNotification(_localizationService.GetResource("Admin.Vendors.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = vendor.Id }) : RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            return View(model);
        }


        //edit
        public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendor = _vendorService.GetVendorById(id);
            if (vendor == null || vendor.Deleted)
                //No vendor found with the specified id
                return RedirectToAction("List");

            var model = vendor.ToModel();
            //locales
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Name = vendor.GetLocalized(x => x.Name, languageId, false, false);
                locale.Description = vendor.GetLocalized(x => x.Description, languageId, false, false);
                locale.MetaKeywords = vendor.GetLocalized(x => x.MetaKeywords, languageId, false, false);
                locale.MetaDescription = vendor.GetLocalized(x => x.MetaDescription, languageId, false, false);
                locale.MetaTitle = vendor.GetLocalized(x => x.MetaTitle, languageId, false, false);
                locale.SeName = vendor.GetSeName(languageId, false, false);
            });
            //associated customer emails
            model.AssociatedCustomerEmails = _customerService
                    .GetAllCustomers(vendorId: vendor.Id)
                    .Select(c => c.Email)
                    .ToList();

            model.AvailableCities = _cityService.GetCitiesForCombo();
            if (model.CityId == null)
            {
                model.CityId = 0;
            }

            foreach (var manufacturer in _manufacturerService.GetAllManufacturers(showHidden: true))
            {
                model.AvailableManufacturers.Add(new SelectListItem
                {
                    Text = manufacturer.Name,
                    Value = manufacturer.Id.ToString()
                });
            }

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(VendorModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendor = _vendorService.GetVendorById(model.Id);
            if (vendor == null || vendor.Deleted)
                //No vendor found with the specified id
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                vendor = model.ToEntity(vendor);

                //fill latitude and longitude from address
                if (vendor.StreetAddress != null && vendor.StreetAddress != "")
                {
                    var city = _cityService.GetCityById(vendor.CityId.Value);
                    vendor.StateId = city.StateId;
                    var state = _stateProvinceService.GetStateProvinceById(vendor.StateId.Value);
                    vendor.CountryId = state.CountryId;
                    GoogleLocationService locservice = new GoogleLocationService();
                    MapPoint point = locservice.GetLatLongFromAddress(vendor.StreetAddress + ", " + city.CityName + ", " + state.Abbreviation + " " + city.PostCode);
                    vendor.Latitude = point.Latitude;
                    vendor.Longitude = point.Longitude;
                }

                _vendorService.UpdateVendor(vendor);
                //search engine name
                model.SeName = vendor.ValidateSeName(model.SeName, vendor.Name, true);
                _urlRecordService.SaveSlug(vendor, model.SeName, 0);
                //locales
                UpdateLocales(vendor, model);

                SuccessNotification(_localizationService.GetResource("Admin.Vendors.Updated"));
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabIndex();

                    return RedirectToAction("Edit",  new {id = vendor.Id});
                }
                return RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form

            //associated customer emails
            model.AssociatedCustomerEmails = _customerService
                    .GetAllCustomers(vendorId: vendor.Id)
                    .Select(c => c.Email)
                    .ToList();
            return View(model);
        }

        //delete
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendor = _vendorService.GetVendorById(id);
            if (vendor == null)
                //No vendor found with the specified id
                return RedirectToAction("List");

            _vendorService.DeleteVendor(vendor);
            SuccessNotification(_localizationService.GetResource("Admin.Vendors.Deleted"));
            return RedirectToAction("List");
        }


        #region Vendor Manufacturers

        [HttpPost]
        public ActionResult VendorManufacturerList(DataSourceRequest command, int vendorId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendorManufacturers = _manufacturerService.GetVendorManufacturersByVendorId(vendorId, true);
            var vendorManufacturersModel = vendorManufacturers
                .Select(x => new VendorModel.VendorManufacturerModel
                {
                    Id = x.Id,
                    Manufacturer = _manufacturerService.GetManufacturerById(x.ManufacturerId).Name,
                    VendorId = x.VendorId,
                    ManufacturerId = x.ManufacturerId,
                    DiscountPercentage = x.DiscountPercentage
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = vendorManufacturersModel,
                Total = vendorManufacturersModel.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult VendorManufacturerInsert(VendorModel.VendorManufacturerModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendorId = model.VendorId;
            var manufacturerId = model.ManufacturerId;

            //a vendor should have access only to his products
            //if (_workContext.CurrentVendor != null)
            //{
            //    var product = _productService.GetProductById(productId);
            //    if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
            //    {
            //        return Content("This is not your product");
            //    }
            //}

            var existingVendormanufacturers = _manufacturerService.GetVendorManufacturersByVendorId(vendorId, showHidden: true);
            if (existingVendormanufacturers.FindVendorManufacturer(vendorId, manufacturerId) == null)
            {
                var vendorManufacturer = new VendorManufacturer
                {
                    VendorId = vendorId,
                    ManufacturerId = manufacturerId,
                    DiscountPercentage = model.DiscountPercentage
                };
                //a vendor cannot edit "IsFeaturedProduct" property
                //if (_workContext.CurrentVendor == null)
                //{
                //    productManufacturer.IsFeaturedProduct = model.IsFeaturedProduct;
                //}
                _manufacturerService.InsertVendorManufacturer(vendorManufacturer);
            }

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult VendorManufacturerUpdate(VendorModel.VendorManufacturerModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendorManufacturer = _manufacturerService.GetVendorManufacturerById(model.Id);
            if (vendorManufacturer == null)
                throw new ArgumentException("No vendor manufacturer mapping found with the specified id");

            //a vendor should have access only to his products
            //if (_workContext.CurrentVendor != null)
            //{
            //    var product = _productService.GetProductById(productManufacturer.ProductId);
            //    if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
            //    {
            //        return Content("This is not your product");
            //    }
            //}
            vendorManufacturer.VendorId = model.VendorId;
            vendorManufacturer.ManufacturerId = model.ManufacturerId;
            vendorManufacturer.DiscountPercentage = model.DiscountPercentage;
            //a vendor cannot edit "IsFeaturedProduct" property
            //if (_workContext.CurrentVendor == null)
            //{
            //    productManufacturer.IsFeaturedProduct = model.IsFeaturedProduct;
            //}
            _manufacturerService.UpdateVendorManufacturer(vendorManufacturer);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult VendorManufacturerDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            var vendorManufacturer = _manufacturerService.GetVendorManufacturerById(id);
            if (vendorManufacturer == null)
                throw new ArgumentException("No product manufacturer mapping found with the specified id");

            //var vendorId = vendorManufacturer.VendorId;

            //a vendor should have access only to his products
            //if (_workContext.CurrentVendor != null)
            //{
            //    var product = _productService.GetProductById(productId);
            //    if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
            //    {
            //        return Content("This is not your product");
            //    }
            //}

            _manufacturerService.DeleteVendorManufacturer(vendorManufacturer);

            return new NullJsonResult();
        }
        

        #endregion

        #endregion
    }
}
