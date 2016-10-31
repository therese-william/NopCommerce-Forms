using Nop.Core.Domain.Vendors;

namespace Nop.Data.Mapping.Vendors
{
    public partial class VendorMap : NopEntityTypeConfiguration<Vendor>
    {
        public VendorMap()
        {
            this.ToTable("Vendor");
            this.HasKey(v => v.Id);

            this.Property(v => v.Name).IsRequired().HasMaxLength(400);
            this.Property(v => v.Email).HasMaxLength(400);
            this.Property(v => v.MetaKeywords).HasMaxLength(400);
            this.Property(v => v.MetaTitle).HasMaxLength(400);
            this.Property(v => v.PageSizeOptions).HasMaxLength(200);
            this.Property(v => v.AccountNumber).HasMaxLength(50);
            this.Property(v => v.StreetAddress).IsRequired().HasMaxLength(4000);
            this.Property(v => v.CountryId);
            this.Property(v => v.StateId);
            this.Property(v => v.CityId).IsRequired();
            this.Property(v => v.Longitude);
            this.Property(v => v.Latitude);
        }
    }
}