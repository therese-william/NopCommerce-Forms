using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Manufacturer service
    /// </summary>
    public partial interface IManufacturerService
    {
        /// <summary>
        /// Deletes a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        void DeleteManufacturer(Manufacturer manufacturer);
        
        /// <summary>
        /// Gets all manufacturers
        /// </summary>
        /// <param name="manufacturerName">Manufacturer name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Manufacturers</returns>
        IPagedList<Manufacturer> GetAllManufacturers(string manufacturerName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false);

        /// <summary>
        /// Gets a manufacturer
        /// </summary>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>Manufacturer</returns>
        Manufacturer GetManufacturerById(int manufacturerId);

        /// <summary>
        /// Inserts a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        void InsertManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Updates the manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        void UpdateManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        void UpdateHasDiscountsApplied(Manufacturer manufacturer);

        /// <summary>
        /// Deletes a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        void DeleteProductManufacturer(ProductManufacturer productManufacturer);
        
        /// <summary>
        /// Gets product manufacturer collection
        /// </summary>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product manufacturer collection</returns>
        IPagedList<ProductManufacturer> GetProductManufacturersByManufacturerId(int manufacturerId,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Gets a product manufacturer mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product manufacturer mapping collection</returns>
        IList<ProductManufacturer> GetProductManufacturersByProductId(int productId, bool showHidden = false);
        
        /// <summary>
        /// Gets a product manufacturer mapping 
        /// </summary>
        /// <param name="productManufacturerId">Product manufacturer mapping identifier</param>
        /// <returns>Product manufacturer mapping</returns>
        ProductManufacturer GetProductManufacturerById(int productManufacturerId);

        /// <summary>
        /// Inserts a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        void InsertProductManufacturer(ProductManufacturer productManufacturer);

        /// <summary>
        /// Updates the product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        void UpdateProductManufacturer(ProductManufacturer productManufacturer);

        /// <summary>
        /// Gets vendor manufacturer mapping collection
        /// </summary>
        /// <param name="vendorId">vendor identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendor a manufacturer mapping collection</returns>
        IList<VendorManufacturer> GetVendorManufacturersByVendorId(int vendorId, bool showHidden = false);

        /// <summary>
        /// Gets vendor manufacturer mapping collection
        /// </summary>
        /// <param name="vendorId">vendor identifier</param>
        /// <param name="storeId">Store identifier (used in multi-store environment). "showHidden" parameter should also be "true"</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendor a manufacturer mapping collection</returns>
        IList<VendorManufacturer> GetVendorManufacturersByVendorId(int vendorId, int storeId, bool showHidden = false);

        /// <summary>
        /// Gets vendor manufacturer mapping collection
        /// </summary>
        /// <param name="manufacturerId">manufacturer identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendor a manufacturer mapping collection</returns>
        IList<VendorManufacturer> GetVendorManufacturersByManufacturerId(int manufacturerId, bool showHidden = false);

        /// <summary>
        /// Gets vendor manufacturer mapping collection
        /// </summary>
        /// <param name="vendorId">vendor identifier</param>
        /// <param name="manufacturerId">manufacturer identifier</param>
        /// <param name="storeId">Store identifier (used in multi-store environment). "showHidden" parameter should also be "true"</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendor a manufacturer mapping collection</returns>
        IList<VendorManufacturer> GetVendorManufacturersByVendorIdManufacturerId(int vendorId, int manufacturerId, int storeId, bool showHidden = false);

        /// <summary>
        /// Gets vendor manufacturer mapping collection
        /// </summary>
        /// <param name="vendorId">vendor identifier</param>
        /// <param name="manufacturerId">manufacturer identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendor a manufacturer mapping collection</returns>
        IList<VendorManufacturer> GetVendorManufacturersByVendorIdManufacturerId(int vendorId,int manufacturerId, bool showHidden = false);

        /// <summary>
        /// Gets vendor manufacturer mapping collection
        /// </summary>
        /// <param name="manufacturerId">manufacturer identifier</param>
        /// <param name="storeId">Store identifier (used in multi-store environment). "showHidden" parameter should also be "true"</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Vendor a manufacturer mapping collection</returns>
        IList<VendorManufacturer> GetVendorManufacturersByManufacturerId(int manufacturerId, int storeId, bool showHidden = false);

        /// <summary>
        /// Inserts a vendor manufacturer mapping
        /// </summary>
        /// <param name="vendorManufacturer">Vendor manufacturer mapping</param>
        void InsertVendorManufacturer(VendorManufacturer vendorManufacturer);

        /// <summary>
        /// Updates the vendor manufacturer mapping
        /// </summary>
        /// <param name="vendorManufacturer">Vendor manufacturer mapping</param>
        void UpdateVendorManufacturer(VendorManufacturer vendorManufacturer);

        /// <summary>
        /// Deletes a vendor manufacturer mapping
        /// </summary>
        /// <param name="vendorManufacturer">Vendor manufacturer mapping</param>
        void DeleteVendorManufacturer(VendorManufacturer vendorManufacturer);

        /// <summary>
        /// Gets a vendor manufacturer mapping 
        /// </summary>
        /// <param name="vendorManufacturerId">Vendor manufacturer mapping identifier</param>
        /// <returns>Vendor manufacturer mapping</returns>
        VendorManufacturer GetVendorManufacturerById(int vendorManufacturerId);
    }
}
