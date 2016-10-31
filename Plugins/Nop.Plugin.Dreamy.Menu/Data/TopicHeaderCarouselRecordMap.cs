using Nop.Plugin.Dreamy.Menu.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Data
{
    public class TopicHeaderCarouselRecordMap : EntityTypeConfiguration<TopicHeaderCarouselRecord>
    {
        public TopicHeaderCarouselRecordMap()
        {
            ToTable("TopicHeaderCarousel");
            HasKey(m => m.Id);

            Property(m => m.ContentType);
            Property(m => m.ContentUrl);
            Property(m => m.DisplayOrder);
            Property(m => m.IsActive);
            Property(m => m.LinkUrl);

            HasRequired(m => m.TopicHeader)
                .WithMany(h => h.TopicHeaderCarousels)
                .HasForeignKey(m => m.TopicHeaderId);
        }
    }
}
