﻿namespace Neo.UI.Messages
{
    // TODO Remove class and replace with getters in OpenWalletView
    public class OpenWalletMessage
    {
        public OpenWalletMessage(string walletPath, string password, bool repairMode)
        {
            this.WalletPath = walletPath;
            this.Password = password;
            this.RepairMode = repairMode;
        }

        public string WalletPath { get; }

        public string Password { get; }

        public bool RepairMode { get; }
    }
}