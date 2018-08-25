﻿using System.Threading.Tasks;

namespace GoCardlessApi.Customers
{
    public interface ICustomersClient
    {
        Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request);
        Task<AllCustomersResponse> AllAsync();
        Task<CustomersResponse> ForIdAsync(string customerId);
        Task<UpdateCustomerResponse> UpdateAsync(UpdateCustomerRequest request);
    }
}