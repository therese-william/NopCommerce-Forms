using Nop.Plugin.Dreamy.Forms.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Dreamy.Forms.Data
{
    public class FormFieldsRecordMap :  EntityTypeConfiguration<FormFieldsRecord>
    {
        public FormFieldsRecordMap() 
        {
            ToTable("FormFields");
            HasKey(m => m.Id);
            Property(m => m.FieldName);
            Property(m => m.FieldType);
            Property(m => m.FieldAllowedValues);
            Property(m => m.IsRequired);
            Property(m => m.DisplayOrder);
            Property(m => m.IsForAdminOnly);

            this.HasRequired(m => m.Form)
                .WithMany(f => f.Fields)
                .HasForeignKey(m => m.FormId);

        }
    }
}
