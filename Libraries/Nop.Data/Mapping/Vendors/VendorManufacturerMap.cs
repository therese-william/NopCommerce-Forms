
using Nop.Core.Domain.Vendors;

namespace Nop.Data.Mapping.Vendors
{
    class VendorManufacturerMap : NopEntityTypeConfiguration<VendorManufacturer>
    {
        public VendorManufacturerMap() 
        {
            this.ToTable("Vendor_Manufacturer_Mapping");
            this.HasKey(vm => vm.Id);

            this.HasRequired(vm => vm.Manufacturer)
                .WithMany()
                .HasForeignKey(vm => vm.ManufacturerId);

            this.HasRequired(vm => vm.Vendor)
                .WithMany(v => v.VendorManufacturers)
                .HasForeignKey(vm => vm.VendorId);
        }
    }
}
