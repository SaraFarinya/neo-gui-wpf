﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Neo.Core;
using Neo.Network;
using Neo.SmartContract;
using Neo.UI.Core.Data;
using Neo.UI.Core.Transactions.Parameters;
using Neo.UI.Core.Transactions.Testing;
using Neo.Wallets;

namespace Neo.UI.Core.Wallet
{
    public interface IWalletController : IDisposable
    {
        void Initialize();

        bool WalletIsOpen { get; }

        bool WalletCanBeMigrated(string walletPath);

        /// <summary>
        /// Migrates to newer wallet format. This method does not open the migrated wallet.
        /// </summary>
        /// <returns>File path of new migrated wallet</returns>
        string MigrateWallet(string walletPath, string password);

        void CreateWallet(string walletPath, string password, bool createWithAccount = true);

        void OpenWallet(string walletPath, string password);

        void CloseWallet();

        void CreateNewAccount();

        void ImportPrivateKeys(IEnumerable<string> wifPrivateKeys);

        void ImportCertificate(X509Certificate2 certificate);

        TestForGasUsageResult TestTransactionForGasUsage(InvokeContractTransactionParameters parameters);

        void BuildSignAndRelayTransaction<TParameters>(TParameters transactionParameters) where TParameters : TransactionParameters;

        void SignAndRelay(Transaction transaction);

        bool Sign(ContractParametersContext context);

        void Relay(Transaction transaction, bool saveTransaction = true);

        void Relay(IInventory inventory);

        void SetNEP5WatchScriptHashes(IEnumerable<string> nep5WatchScriptHashesHex);

        IEnumerable<UInt160> GetNEP5WatchScriptHashes();

        /// <summary>
        /// Get all accounts addresses in the wallet.
        /// </summary>
        IEnumerable<string> GetAccountsAddresses();

        /// <summary>
        /// Gets all accounts in wallets.
        /// </summary>
        IEnumerable<WalletAccount> GetAccounts();

        /// <summary>
        /// Gets accounts that are not watch-only (i.e. standard and non-standard contract accounts).
        /// </summary>
        IEnumerable<WalletAccount> GetNonWatchOnlyAccounts();

        /// <summary>
        /// Gets standard contract accounts.
        /// </summary>
        IEnumerable<WalletAccount> GetStandardAccounts();

        IEnumerable<AssetDto> GetWalletAssets();

        IEnumerable<Coin> FindUnspentCoins();

        UInt160 GetChangeAddress();

        AccountContract GetAccountContract(string accountScriptHash);

        AccountKeyInfo GetAccountKeys(string accountScriptHash);

        Transaction GetTransaction(UInt256 hash);

        /// <summary>
        /// Gets the public keys that the specified script hash have voted for.
        /// </summary>
        /// <param name="voterScriptHash">Script hash of the account that voted</param>
        /// <returns>Enumerable collection of public keys</returns>
        IEnumerable<string> GetVotes(string voterScriptHash);

        ContractState GetContractState(UInt160 scriptHash);

        AssetStateDto GetAssetState(string assetId);

        bool CanViewCertificate(FirstClassAssetSummary assetSummary);

        string ViewCertificate(FirstClassAssetSummary assetSummary);

        Fixed8 CalculateBonus();
        
        Fixed8 CalculateUnavailableBonusGas(uint height);

        bool WalletContainsAccount(string scriptHash);

        /// <summary>
        /// NEP-5 assets
        /// </summary>
        string GetNEP5TokenAvailability(string assetId);

        /// <summary>
        /// First class assets
        /// </summary>
        string GetFirstClassTokenAvailability(string assetId);

        void ImportWatchOnlyAddress(string[] addressesToWatch);

        bool DeleteAccount(string accountScriptHash);

        Transaction MakeTransaction(Transaction transaction, UInt160 changeAddress = null, Fixed8 fee = default(Fixed8));
        
        string BytesToScriptHash(byte[] data);

        UInt160 AddressToScriptHash(string address);

        string ScriptHashToAddress(string scriptHash);

        bool AddressIsValid(string address);

        void DeleteFirstClassAsset(string assetId);

        void ClaimUtilityTokenAsset();

        void AddLockContractAccount(string publicKey, uint unlockDateTime);

        IEnumerable<string> GetPublicKeysFromStandardAccounts();

        IEnumerable<string> GetAddressesForNonWatchOnlyAccounts();

        void AddMultiSignatureContract(int minimunSignatureNumber, IEnumerable<string> publicKeys);

        void AddContractWithParameters(string reedemScript, string parameterList);
    }
}