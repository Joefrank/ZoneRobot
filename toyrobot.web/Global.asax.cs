using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using toyrobot.services.Implementation;
using toyrobot.services.Interfaces;

namespace toyrobot.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = GetBuilder(typeof(MvcApplication)).Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
           

        }

        public ContainerBuilder GetBuilder(Type type)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(type.Assembly).PropertiesAutowired();

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(type.Assembly);
            builder.RegisterModelBinderProvider();


            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();
            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());
            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            //define registrations here
            builder.RegisterType<HttpCacheService>().As<ICacheService>();

            builder.Register(c => new
                RobotService(c.Resolve<ICacheService>())
            ).As<IRobotService>().InstancePerLifetimeScope();
            
            builder.RegisterType<GridService>().As<IGridService>();


            return builder;

        }
        //private ILifetimeScope GetContainer()
        //{
        //    var builder = new ContainerBuilder();

        //    // Register your MVC controllers. (MvcApplication is the name of
        //    // the class in Global.asax.)
        //    builder.RegisterControllers(typeof(MvcApplication).Assembly);
        //    // OPTIONAL: Register model binders that require DI.
        //    builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
        //    builder.RegisterModelBinderProvider();
        //    // OPTIONAL: Register web abstractions like HttpContextBase.
        //    builder.RegisterModule<AutofacWebTypesModule>();
        //    // OPTIONAL: Enable property injection in view pages.
        //    builder.RegisterSource(new ViewRegistrationSource());
        //    // OPTIONAL: Enable property injection into action filters.
        //    builder.RegisterFilterProvider();

           

        //    // Set the dependency resolver to be Autofac.
        //    return builder.Build();
        //}
    }
}
