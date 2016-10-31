using Nop.Core.Domain.Catalog;
namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Represents vendor manufaturer mapping
    /// </summary>
    public partial class VendorManufacturer : BaseEntity
    {
        /// <summary>
        /// get or set vendor identifier
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        /// get or set manufacturer identifier
        /// </summary>
        public int ManufacturerId { get; set; }
        /// <summary>
        /// get or set the discount percentage for the vendor per manufacturer
        /// </summary>
        public decimal DiscountPercentage { get; set; }
        /// <summary>
        /// Gets the vendor
        /// </summary>
        public virtual Vendor Vendor { get; set; }
        /// <summary>
        /// Gets the manufacturer 
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
