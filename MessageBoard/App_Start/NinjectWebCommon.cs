using System;
using System.Web;
using System.Web.Http;
using MessageBoard.App_Start;
using MessageBoard.Services;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Persistance.Data;
using WebActivator;
using WebApiContrib.IoC.Ninject;

[assembly: WebActivator.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectWebCommon), "Stop")]

namespace MessageBoard.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            //Telling WebAPI when you resolved dependencies, this is the resolver you should use, the one from ninject
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);


            return kernel;
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
#if DEBUG
            kernel.Bind<IMailService>().To<MockMailService>().InRequestScope();
#else
      kernel.Bind<IMailService>().To<MailService>().InRequestScope();
#endif
            kernel.Bind<MessageBoardContext>().To<MessageBoardContext>().InRequestScope();
            kernel.Bind<IMessageBoardRepository>().To<MessageBoardRepository>().InRequestScope();

        }
    }
}