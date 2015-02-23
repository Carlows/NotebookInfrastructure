[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MyNotebook.Site.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MyNotebook.Site.App_Start.NinjectWebCommon), "Stop")]

namespace MyNotebook.Site.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using MyNotebook.Domain.Entities;
    using MyNotebook.Domain.Settings;
    using MyNotebook.Site.Settings.Impl;
    using MyNotebook.Site.Settings;
    using MyNotebook.Domain.Repositories;
    using MyNotebook.Domain.Repositories.Impl;
    using MyNotebook.Domain.Caching;
    using MyNotebook.Domain.Caching.Impl;
    using MyNotebook.Site.Models;
    using System.Web.Http;
    using Ninject.WebApi.DependencyResolver;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static IKernel Kernel
        {
            get
            {
                return bootstrapper.Kernel;
            }
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                GlobalConfiguration.Configuration.DependencyResolver =
                    new NinjectDependencyResolver(kernel);

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<MyNotebookDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IDomainSettings>().To<ApplicationSettings>();
            kernel.Bind<ISiteSettings>().To<ApplicationSettings>();
            kernel.Bind<ICacheProvider>().To<AspNetCacheProvider>();
            kernel.Bind<INotesRepository>().To<NotesRepository>();
        }
    }
}
