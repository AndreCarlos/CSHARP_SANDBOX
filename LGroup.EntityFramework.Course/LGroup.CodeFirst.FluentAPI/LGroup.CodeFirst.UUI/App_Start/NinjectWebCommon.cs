//O Ninject eh tipo um parasita (carrapato), assim que o projeto subir para memoria
//ele sobe junto (gruda) e quando o projeto sair da memoria ele desgruda
//quando baixamos o ninject veio de brinde o componente WebActivator eh ele quem fica monitorando
//o porjeto para saber a hora de acionar o ninject
//WebActicvator -> Ninject -> MVC
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LGroup.CodeFirst.UUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LGroup.CodeFirst.UUI.App_Start.NinjectWebCommon), "Stop")]

//Pra fazer a injeçao de dependencia (inicializaçoes) temos que utilizar alguma
//biblioteca injetora : Object builder, Unity, Structure Map, Auto Fac
//a mais fácil de utilizar eh o Ninject
namespace LGroup.CodeFirst.UUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using LGroup.CodeFirst.Repository.Contracts;
    using LGroup.CodeFirst.Repository;

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
            //É aqui dentro desse comando que fazemos as inicializações, os NEWS enrustidos...
            //isso eh a injecao == NEW
            //onde ele encontrar um parametro do tipo IClienteREP, ele vai dar um new
            //na classe ClienteRepository

            //atraves de injecao de dependencia, podemos injetar uma classe mock(duble == fake) que simula
            //os comportamentos de uma classe original
            kernel.Bind<IClienteRepository>().To<ClienteRepository>();        //classe original
            //kernel.Bind<IClienteRepository>().To<ClienteMockRepository>();  //classe mock


            //injetamos estes tres repositorios por causa da UOW, ela precisa
            //de todos os repositorios envolvidos no pedido
            kernel.Bind<IPedidoRepository>().To<PedidoRepository>();
            kernel.Bind<IProdutoRepository>().To<ProdutoRepository>();
            kernel.Bind<IItemPedidoRepository>().To<IItemPedidoRepository>();
        }        
    }
}
