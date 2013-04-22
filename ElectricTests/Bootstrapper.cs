using System.Web.Mvc;
using ElectricTests.Repository;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace ElectricTests {
    public static class Bootstrapper {
        public static void Initialise() {
            IUnityContainer container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer() {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();            

            container.RegisterType<IDocumentsRepository, DocumentsRepository>();
            container.RegisterType<IQuestionsRepository, QuestionsRepository>();
            container.RegisterType<ITestRepository, TestsRepository>();

            return container;
        }
    }
}