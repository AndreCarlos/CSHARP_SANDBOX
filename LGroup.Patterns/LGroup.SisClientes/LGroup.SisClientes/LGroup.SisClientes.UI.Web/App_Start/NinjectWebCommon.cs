using LGroup.SisClientes.DataAccessObject;
using LGroup.SisClientes.DataAccessObject.Contracts;
using LGroup.SisClientes.DataAccessObject.Implementation;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LGroup.SisClientes.UI.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LGroup.SisClientes.UI.Web.App_Start.NinjectWebCommon), "Stop")]

///Para fazer a tecnica de inje��o de dependencia (DAR NEWS), temos que usar algum
///framework injetor (inicializa�ao) : UNITY, SIMPLE INJECTOR, AUTOFAC E O MAIS SIMPLES E CONHECIDO � O NINJECT
///
///Quando baixamos o Ninject, ele criou um modulo de inicializa��o, configura��o
///
namespace LGroup.SisClientes.UI.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

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
            ///� o unico local desse arquivo que temos que alterar
            ///� aqui dentro que faremos as inicializa�oes
            ///as inje��es de depend�ncia (Classes)
            kernel.Bind<Conexao>().To<Conexao>();
            kernel.Bind<ClienteDAO>().To<ClienteDAO>();
        }        
    }
}
