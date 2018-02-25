using Neo.UI.Core.Data;

namespace Neo.UI.Core.Wallet.Messages
{
    public class AccountAddedMessage
    {
        public AccountSummary Account { get; }

        public AccountAddedMessage(AccountSummary account)
        {
            this.Account = account;
        }
    }
}