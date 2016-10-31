using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;
using System.Collections.Generic;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Represents a vendor
    /// </summary>
    public partial class Vendor : BaseEntity, ILocalizedEntity, ISlugSupported
    {

        private ICollection<VendorManufacturer> _vendorManufacturers;

        public virtual ICollection<VendorManufacturer> VendorManufacturers
        {
            get { return _vendorManufacturers ?? (_vendorManufacturers=new List<VendorManufacturer>()); }
            set { _vendorManufacturers = value; }
        }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the admin comment
        /// </summary>
        public string AdminComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }


        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the page size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers can select the page size
        /// </summary>
        public bool AllowCustomersToSelectPageSize { get; set; }

        /// <summary>
        /// Gets or sets the available customer selectable page size options
        /// </summary>
        public string PageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets the vendor's account number 
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the vendor's street address 
        /// </summary>
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the vendor's country 
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the vendor's state 
        /// </summary>
        public int? StateId { get; set; }

        /// <summary>
        /// Gets or sets the vendor's city 
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Gets or sets the vendor's location latitude 
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the vendor's location Longitude 
        /// </summary>
        public double? Longitude { get; set; }


        /// <summary>
        /// Gets or sets the country
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Gets or sets the state
        /// </summary>
        public virtual StateProvince State { get; set; }


    }
}
