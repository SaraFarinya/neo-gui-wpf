﻿using GalaSoft.MvvmLight;
using Neo.UniversalWallet.Messages;
using Neo.UniversalWallet.Views;

namespace Neo.UniversalWallet.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _pageContent;

        public object PageContent
        {
            get
            {
                return this._pageContent;
            }
            set
            {
                this._pageContent = value;
                this.RaisePropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            this.PageContent = new LoadWalletView();

            MessengerInstance.Register<NavigationMessage>(this, this.HandleNavigationMessage);
        }

        private void HandleNavigationMessage(NavigationMessage obj)
        {
            if (obj.DestinationPage == "DashboardView")
            {
                this.PageContent = new DashboardView();
            }
        }
    }
}