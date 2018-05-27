using System.Windows;
using Autofac;
using Inifinal.CarPc.Bar;
using Inifinal.CarPc.View;
using Prism.Autofac;
using Prism.Regions;

namespace Infinal.CarPc.Ui
{
    public class Bootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);
            builder.RegisterType<Views.Shell>();
            builder.RegisterTypeForNavigation<MenuView>();
            builder.RegisterTypeForNavigation<MusikView>();
            builder.RegisterTypeForNavigation<NaviView>();
            builder.RegisterTypeForNavigation<OdbView>();
            builder.RegisterTypeForNavigation<RadioView>();
            builder.RegisterTypeForNavigation<SettingsView>();
            builder.RegisterTypeForNavigation<VideoView>();
            builder.RegisterTypeForNavigation<MenuBar>();
            builder.RegisterTypeForNavigation<PlayerBar>();
        }


        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Views.Shell)Shell;
            Application.Current.MainWindow?.Show();
        }
    }
}
