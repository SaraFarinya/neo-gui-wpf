using Neo.UI.Core.Data;
using Neo.UI.Core.Data.Enums;

namespace Neo.Gui.ViewModels.Tests.Builders
{
    public class AccountSummaryBuilder
    {
        private AccountType accountType = AccountType.Standard;
        private string labelInternal = "accountLabel";
        private string hashInternal;
        private Fixed8 neoBalance = Fixed8.Zero;

        public AccountSummaryBuilder WithLabel(string label)
        {
            this.labelInternal = label;
            return this;
        }

        public AccountSummaryBuilder WithHash(string hash)
        {
            this.hashInternal = hash;
            return this;
        }
        
        public AccountSummaryBuilder StandardAccount()
        {
            this.accountType = AccountType.Standard;
            return this;
        }

        public AccountSummaryBuilder NonStandardAccount()
        {
            this.accountType = AccountType.NonStandard;
            return this;
        }

        public AccountSummaryBuilder WatchOnlyAccount()
        {
            this.accountType = AccountType.WatchOnly;
            return this;
        }

        public AccountSummaryBuilder AccountWithNeoBalance()
        {
            this.neoBalance = new Fixed8(1);
            return this;
        }

        public AccountSummary Build()
        {
            var account = new AccountSummary(this.labelInternal, this.hashInternal, this.accountType);

            if (this.neoBalance != Fixed8.Zero)
            {
                account.Neo = this.neoBalance;
            }

            return account;
        }
    }
}
