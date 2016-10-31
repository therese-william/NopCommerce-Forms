using Nop.Plugin.Dreamy.Menu.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Data
{
    public class TopicHeaderRecordMap : EntityTypeConfiguration<TopicHeaderRecord>
    {
        public TopicHeaderRecordMap()
        {
            ToTable("TopicHeader");
            HasKey(m => m.Id);

            Property(m => m.HeaderType);
            Property(m => m.LinkUrl);
            Property(m => m.ContentUrl);

            HasRequired(m => m.Topic)
                .WithMany(t => t.TopicHeaders)
                .HasForeignKey(m => m.TopicId);
        }
    }
}
