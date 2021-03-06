﻿using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.CustomerBankAccounts
{
    public interface ICustomerBankAccountsClient : IPageable<GetCustomerBankAccountsOptions, CustomerBankAccount>
    {
        Task<Response<CustomerBankAccount>> CreateAsync(CreateCustomerBankAccountOptions options);
        Task<Response<CustomerBankAccount>> DisableAsync(DisableCustomerBankAccountOptions options);
        Task<Response<CustomerBankAccount>> ForIdAsync(string id);
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync();
        Task<PagedResponse<CustomerBankAccount>> GetPageAsync(GetCustomerBankAccountsOptions options);
        Task<Response<CustomerBankAccount>> UpdateAsync(UpdateCustomerBankAccountOptions options);
    }
}