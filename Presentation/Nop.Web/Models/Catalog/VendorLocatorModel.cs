using Nop.Core.Domain.Directory;
using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Web.Models.Catalog
{
    public partial class VendorLocatorModel : BaseNopEntityModel
    {
        public VendorLocatorModel() 
        {
            SelectedCityId = 0;
            SelectedVendors = new List<VendorModel>();
            AvailableCities = new List<SelectListItem>();
        }

        public int SelectedCityId { set; get; }
        public IList<VendorModel> SelectedVendors { set; get; }
        public IList<SelectListItem> AvailableCities { get; set; }
        public City SelectedCity { set; get; }
    }
}