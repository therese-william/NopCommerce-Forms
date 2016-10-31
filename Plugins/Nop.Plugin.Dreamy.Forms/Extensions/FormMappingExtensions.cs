using Nop.Plugin.Dreamy.Forms.Domain;
using Nop.Plugin.Dreamy.Forms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Dreamy.Forms.Infrastructure;

namespace Nop.Plugin.Dreamy.Forms.Extensions
{
    public static class FormMappingExtensions
    {
        #region Forms
        public static FormModel ToModel(this FormsRecord entity)
        {
            if (entity == null)
                return null;

            var model = new FormModel
            {
                Id = entity.Id,
                FormName = entity.FormName,
                Published = entity.Published,
                AdminEmails = entity.AdminEmails
            };
            return model;
        }
        public static FormsRecord ToEntity(this FormModel model)
        {
            if (model == null)
                return null;

            var entity = new FormsRecord
            {
                Id = model.Id,
                FormName = model.FormName,
                Published = model.Published,
                AdminEmails = model.AdminEmails
            };
            return entity;
        }
        #endregion

        #region FormFields
        public static FormFieldsModel ToModel(this FormFieldsRecord entity)
        {
            if (entity == null)
                return null;

            var model = new FormFieldsModel
            {
                Id = entity.Id,
                DisplayOrder = entity.DisplayOrder,
                FieldAllowedValues = entity.FieldAllowedValues,
                FieldName = entity.FieldName,
                FieldType = entity.FieldType,
                FormId = entity.FormId,
                IsRequired = entity.IsRequired,
                IsForAdminOnly = entity.IsForAdminOnly
            };
            return model;
        }
        public static FormFieldsRecord ToEntity(this FormFieldsModel model)
        {
            if (model == null)
                return null;

            var entity = new FormFieldsRecord
            {
                Id = model.Id,
                DisplayOrder = model.DisplayOrder,
                FieldAllowedValues = model.FieldAllowedValues,
                FieldName = model.FieldName,
                FieldType = model.FieldType,
                FormId = model.FormId,
                IsRequired = model.IsRequired,
                IsForAdminOnly = model.IsForAdminOnly
            };
            return entity;
        }
        #endregion
    }
}
