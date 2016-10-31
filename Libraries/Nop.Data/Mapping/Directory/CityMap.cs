using Nop.Core.Domain.Directory;

namespace Nop.Data.Mapping.Directory
{
    public partial class CityMap : NopEntityTypeConfiguration<City>
    {
        public CityMap()
        {
            this.ToTable("Cities");
            this.HasKey(c => c.Id);
            this.Property(c => c.CityName).IsRequired().HasMaxLength(50);
            this.Property(c => c.PostCode).HasMaxLength(5);
            this.Property(c => c.Area).HasMaxLength(50);
            this.Property(c => c.Latitude);
            this.Property(c => c.Longitude);
            this.Property(c => c.Zone);


            this.HasRequired(c => c.State)
                .WithMany(sp => sp.Cities)
                .HasForeignKey(c => c.StateId);
        }
    }
}