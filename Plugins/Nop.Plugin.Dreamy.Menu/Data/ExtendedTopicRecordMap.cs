using Nop.Plugin.Dreamy.Menu.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Menu.Data
{
    public class ExtendedTopicRecordMap : EntityTypeConfiguration<ExtendedTopicRecord>
    {
        public ExtendedTopicRecordMap()
        {
            ToTable("ExtendedTopic");
            HasKey(m => m.Id);

            Property(m => m.ImageUrl);
            Property(m => m.IsSection);
            Property(m => m.ParentTopicId);
            Property(m => m.TopicId);                
        }
    }
}
