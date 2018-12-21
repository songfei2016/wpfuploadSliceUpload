using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Prism.Autofac;
using Prism.Modularity;
using Serilog;
using St.Common;
using St.Common.Contract;
using St.Upload.View;
using St.Upload.ViewModel;

namespace St.Upload
{
    public class DevBootstrapper : AutofacBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            //RegisterSignoutHandler();
            Log.Logger.Debug("【account startup】");
            var loginView = Container.Resolve<LoginView>();
            loginView.ShowDialog();
            var loginViewModel = loginView.DataContext as LoginViewModel;
            if ((loginViewModel != null) && !loginViewModel.IsLoginSucceeded)
            {
                Application.Current.Shutdown();
                return;
            }
            var mainView = Container.Resolve<MainWindow>();
            mainView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainView?.Show();
        }

        protected override IContainer CreateContainer(ContainerBuilder containerBuilder)
        {
            var container = base.CreateContainer(containerBuilder);
            DependencyResolver.SetContainer(container);

            return container;
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            builder.RegisterInstance(new UserInfo()).SingleInstance();
            builder.RegisterInstance(new List<UserInfo>()).SingleInstance();
            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();
            RegisterAutofacModules(builder);
        }

        private void RegisterAutofacModules(ContainerBuilder builder)
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            var assemblies = new List<Assembly>();
            foreach (var module in moduleCatalog.Items)
            {
                var type = Type.GetType(((ModuleInfo)module).ModuleType);
                if (type != null) assemblies.Add(type.Assembly);
            }
            builder.RegisterAssemblyModules(assemblies.ToArray());
        }

        protected override void ConfigureModuleCatalog()
        {
            //ModuleCatalog catalog = (ModuleCatalog)ModuleCatalog;

            //catalog.AddModule(typeof(CoreModule));
            //catalog.AddModule(typeof(MeetingModule));
            //catalog.AddModule(typeof(CollaborativeInfoModule));
            //catalog.AddModule(typeof(InteractiveModule));
            //catalog.AddModule(typeof(InteractiveWithoutLiveModule));
            //catalog.AddModule(typeof(DiscussionModule));
            //catalog.AddModule(typeof(InstantMeetingModule));
            //catalog.AddModule(typeof(SettingModule));
            //catalog.AddModule(typeof(ProfileModule));
            //catalog.AddModule(typeof(RtClientModule));
        }
    }
}
