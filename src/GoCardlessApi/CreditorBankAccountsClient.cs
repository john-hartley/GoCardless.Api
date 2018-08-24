using System;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class CreditorBankAccountsClient : ApiClientBase, ICreditorBankAccounts
    {
        public CreditorBankAccountsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CreateCreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request)
        {
            var idempotencyKey = Guid.NewGuid().ToString();

            return PostAsync<CreateCreditorBankAccountRequest, CreateCreditorBankAccountResponse>(
                new { creditor_bank_accounts = request },
                idempotencyKey,
                "creditor_bank_accounts"
            );
        }
    }
}