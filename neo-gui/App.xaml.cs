﻿using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Neo.Controllers;
using Neo.Helpers;
using Neo.UI;
using Neo.UI.Base;
using Neo.UI.Base.Dispatching;
using Neo.UI.Base.Themes;
using Neo.UI.Home;
using Neo.UI.Updater;

namespace Neo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private IBlockChainController blockChainController;

        internal App()
        {
            this.InitializeComponent();

            this.Setup();
        }

        private void Setup()
        {
            BuildContainer();

            var dispatcher = ApplicationContext.Instance.ContainerLifetimeScope.Resolve(typeof(IDispatcher)) as IDispatcher;

            Task.Run(() =>
            {
                ThemeHelper.LoadTheme();

                NeoSplashScreen splashScreen = null;
                dispatcher?.InvokeOnMainUIThread(() =>
                {
                    splashScreen = new NeoSplashScreen();
                    splashScreen.Show();
                });

                var updateIsRequired = false;
                try
                {
                    updateIsRequired = VersionHelper.UpdateIsRequired;

                    if (updateIsRequired) return;

                    // Application is starting normally, setup blockchain
                    this.blockChainController =
                        ApplicationContext.Instance.ContainerLifetimeScope.Resolve(typeof(IBlockChainController)) as
                            IBlockChainController;

                    this.blockChainController?.Setup();
                }
                finally
                {
                    dispatcher?.InvokeOnMainUIThread(() =>
                    {
                        this.MainWindow = updateIsRequired ? (Window) new UpdateView() : new HomeView();

                        splashScreen.Close();

                        this.MainWindow?.Show();
                    });
                }
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Dispose of IBlockChainController instance
            this.blockChainController.Dispose();
            this.blockChainController = null;

            base.OnExit(e);
        }

        private static void BuildContainer()
        {
            var autoFacContainerBuilder = new ContainerBuilder();

            autoFacContainerBuilder.RegisterModule<NeoGuiRegistrationModule>();
            autoFacContainerBuilder.RegisterModule<ViewModelsRegistrationModule>();
            autoFacContainerBuilder.RegisterModule<BaseRegistrationModule>();
            autoFacContainerBuilder.RegisterModule<ControllersRegistrationModule>();
            autoFacContainerBuilder.RegisterModule<HelpersRegistrationModule>();
            autoFacContainerBuilder.RegisterModule<DialogsRegistrationModule>();

            var container = autoFacContainerBuilder.Build();

            var applicationContext = container.Resolve<IApplicationContext>();
            applicationContext.ContainerLifetimeScope = container.BeginLifetimeScope();
            ApplicationContext.Instance.ContainerLifetimeScope = container.BeginLifetimeScope();
        }
    }
}