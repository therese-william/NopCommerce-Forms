using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Plugins;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Plugin.Dreamy.Forms.Data;
using Nop.Plugin.Dreamy.Forms.Domain;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Dreamy.Forms
{
    public class FormsPlugin : BasePlugin, IAdminMenuPlugin
    {
        private FormsRecordObjectContext _context;
        private IRepository<FormsRecord> _formsrepo;
        public FormsPlugin(FormsRecordObjectContext context, IRepository<FormsRecord> formsrepo ) 
        {
            _context = context;
            _formsrepo = formsrepo;

        }
        public void ManageSiteMap(SiteMapNode rootNode)
        {
            var menuItem = new SiteMapNode()
            {
                Title = "Forms Creator",
                ControllerName="Forms",
                ActionName="Manage",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "Area", "Admin" } }
            };
            var menuItem1 = new SiteMapNode()
            {
                Title = "Forms Submissions",
                ControllerName = "Forms",
                ActionName = "Submissions",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "Area", "Admin" } }
            };
            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
            {
                pluginNode.ChildNodes.Add(menuItem);
                pluginNode.ChildNodes.Add(menuItem1);
            }
            else
            {
                rootNode.ChildNodes.Add(menuItem);
                rootNode.ChildNodes.Add(menuItem1);
            }
        }

        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms", "Forms");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.List.SearchFormName", "Form Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields.FormName", "Form Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields.FormName.Hint", "The form's Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields.Published", "Published");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields.Published.Hint", "Check to publish this form (visible in store), Uncheck to unpublish (form not available).");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields.AdminEmails", "Admin Emails");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields.AdminEmails.Hint", "Enter all admin emails comma separated");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Added", "The new form has been added successfully.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Updated", "The form has been updated successfully.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Deleted", "The form has been deleted successfully.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.AddNew", "Add a new form");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.EditFormDetails", "Edit form details");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.BackToList", "back to form list");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Info", "Form Info");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields", "Form Fields");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.SaveBeforeEdit", "You need to save the form before adding fields");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Forms.Fields.AddNew", "Add a new field");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.FieldName", "Field Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.IsRequired", "Required");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.DisplayOrder", "Display Order");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.FieldType", "Field Type");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.FieldAllowedValues", "Allowed Values (comma separated)");
            this.AddOrUpdatePluginLocaleResource("plugins.dreamy.formfields.fields.IsForAdminOnly", "For admin use only");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Submissions", "Form Submissions");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Submissions.SubmitDate", "Submit Date");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Submissions.CustomerName", "Customer Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.SubmissionDetails", "Submission Details");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.SubmissionDetails.Edit", "Edit Form Submission");
            this.AddOrUpdatePluginLocaleResource("Plugins.Dreamy.Submissions.BackToList", "Back to form submissions list");
            this.AddOrUpdatePluginLocaleResource("Admin.Common.SavePrint", "Save & Print");
                        
            _context.Install();
            base.Install();
        }
        public override void Uninstall()
        {
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.AdminEmails.Hint");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.AdminEmails");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.AdminEmails.Hint");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.Published");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.Published.Hint");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.FormName");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.FormName.Hint");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.List.SearchFormName");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Added");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Updated");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Deleted");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.AddNew");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.EditFormDetails");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.BackToList");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Info");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.SaveBeforeEdit");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Forms.Fields.AddNew");
            this.DeletePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.FieldName");
            this.DeletePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.IsRequired");
            this.DeletePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.DisplayOrder");
            this.DeletePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.FieldType");
            this.DeletePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.FieldAllowedValues");
            this.DeletePluginLocaleResource("Plugins.Dreamy.FormFields.Fields.IsForAdminOnly");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Submissions");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Submissions.SubmitDate");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Submissions.CustomerName");
            this.DeletePluginLocaleResource("Plugins.Dreamy.SubmissionDetails");
            this.DeletePluginLocaleResource("Plugins.Dreamy.SubmissionDetails.Edit");
            this.DeletePluginLocaleResource("Plugins.Dreamy.Submissions.BackToList");
            this.DeletePluginLocaleResource("Admin.Common.SavePrint");

            _context.Uninstall();
            base.Uninstall();
        }
    }
}
