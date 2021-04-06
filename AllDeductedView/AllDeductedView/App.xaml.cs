using AllDeductedBusinessLogic.BusinessLogics;
using AllDeductedBusinessLogic.Interfaces;
using AllDeductedBusinessLogic.ViewModels;
using AllDeductedDatabaseImplement.Implements;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace AllDeductedView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ProviderViewModel SelectProvider { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = BuildUnityContainer();
            var loginWindow = container.Resolve<LoginWindow>();
            loginWindow.ShowDialog();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentStorage, StudentStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudyingStatusStorage, StudyingStatusStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProviderStorage, ProviderStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGroupStorage, GroupStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<StudentLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<StudyingStatusLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ProviderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<GroupLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
