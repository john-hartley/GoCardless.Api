using System.Threading.Tasks;

namespace GoCardlessApi
{
    public interface ICreditorBankAccounts
    {
        Task<CreateCreditorBankAccountResponse> CreateAsync(CreateCreditorBankAccountRequest request);
        Task<DisableCreditorBankAccountResponse> DisableAsync(DisableCreditorBankAccountRequest request);
    }
}