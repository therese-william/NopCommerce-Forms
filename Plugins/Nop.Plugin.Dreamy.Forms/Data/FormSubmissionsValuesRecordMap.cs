using Nop.Plugin.Dreamy.Forms.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Data
{
    class FormSubmissionsValuesRecordMap : EntityTypeConfiguration<FormSubmissionsValuesRecord>
    {
        public FormSubmissionsValuesRecordMap()
        {
            ToTable("FormSubmissionsValues");
            HasKey(m => m.Id);
            Property(m => m.FieldValue);
            HasRequired(m => m.FormField)
                .WithMany(f => f.SubmissionValues)
                .HasForeignKey(m => m.FormFieldId);
            HasRequired(m => m.FormSubmission)
                .WithMany(s => s.SubmissionValues)
                .HasForeignKey(m => m.SubmissionId)
                .WillCascadeOnDelete(false);
        }
    }
}
