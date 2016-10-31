using Nop.Plugin.Dreamy.Forms.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Data
{
    public class FormSubmissionsRecordMap : EntityTypeConfiguration<FormSubmissionsRecord>
    {
        public FormSubmissionsRecordMap()
        {
            ToTable("FormSubmissions");
            HasKey(m => m.Id);
            Property(m => m.CustomerId);
            Property(m => m.SenderEmail);
            Property(m => m.SubmitDate);

            HasRequired(m => m.Form)
                .WithMany(f => f.Submissions)
                .HasForeignKey(m => m.FormId);
        }
    }
}
