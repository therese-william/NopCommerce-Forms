using Autofac;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Dreamy.Forms.Data;
using Nop.Plugin.Dreamy.Forms.Domain;
using Nop.Data;
using Nop.Core.Data;
using Autofac.Core;
using Nop.Plugin.Dreamy.Forms.Services;

namespace Nop.Plugin.Dreamy.Forms.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string CONTEXT_NAME = "nop_object_context_forms";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            builder.RegisterType<FormService>().As<IFormService>().InstancePerLifetimeScope();
            builder.RegisterType<PrintFormService>().As<IPrintFormService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<FormsRecordObjectContext>(builder, CONTEXT_NAME );

            //override required repository with our custom context
            builder.RegisterType<EfRepository<FormsRecord>>()
                .As<IRepository<FormsRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME ))
                .InstancePerLifetimeScope();
            
            builder.RegisterType<EfRepository<FormFieldsRecord>>()
                .As<IRepository<FormFieldsRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();
            
            builder.RegisterType<EfRepository<FormSubmissionsRecord>>()
                .As<IRepository<FormSubmissionsRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();

            builder.RegisterType<EfRepository<FormSubmissionsValuesRecord>>()
                .As<IRepository<FormSubmissionsValuesRecord>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT_NAME))
                .InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
