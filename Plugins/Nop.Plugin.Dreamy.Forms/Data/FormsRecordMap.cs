using Nop.Plugin.Dreamy.Forms.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Data
{
    public class FormsRecordMap : EntityTypeConfiguration<FormsRecord>
    {
        public FormsRecordMap() 
        {
            ToTable("Forms");
            HasKey(m => m.Id);

            Property(m => m.FormName);
            Property(m => m.AdminEmails);
            Property(m => m.Published);
        }
    }
}
