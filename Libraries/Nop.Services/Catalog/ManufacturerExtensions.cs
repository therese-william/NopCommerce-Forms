using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class ManufacturerExtensions
    {
        /// <summary>
        /// Returns a ProductManufacturer that has the specified values
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>A ProductManufacturer that has the specified values; otherwise null</returns>
        public static ProductManufacturer FindProductManufacturer(this IList<ProductManufacturer> source,
            int productId, int manufacturerId)
        {
            foreach (var productManufacturer in source)
                if (productManufacturer.ProductId == productId && productManufacturer.ManufacturerId == manufacturerId)
                    return productManufacturer;

            return null;
        }

        /// <summary>
        /// Returns a VendorManufacturer that has the specified values
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>A VendorManufacturer that has the specified values; otherwise null</returns>
        public static VendorManufacturer FindVendorManufacturer(this IList<VendorManufacturer> source,
            int vendorId, int manufacturerId)
        {
            foreach (var vendorManufacturer in source)
                if (vendorManufacturer.VendorId == vendorId && vendorManufacturer.ManufacturerId == manufacturerId)
                    return vendorManufacturer;

            return null;
        }
    }
}
