using GoCardlessApi.Core;
using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.Customers;
using GoCardlessApi.Mandates;
using GoCardlessApi.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.TestHelpers
{
    public class ResourceFactory
    {
        private readonly ClientConfiguration _clientConfiguration;

        public async Task<Creditor> Creditor()
        {
            var creditorsClient = new CreditorsClient(_clientConfiguration);
            return (await creditorsClient.AllAsync()).Creditors.First();
        }

        public ResourceFactory(ClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
        }

        public async Task<Customer> CreateCustomer()
        {
            var request = new CreateCustomerRequest
            {
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name",
                CountryCode = "GB",
                Email = "email@example.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                Language = "en",
                PostCode = "SW1A 1AA",
                Region = "Essex"
            };

            var customersClient = new CustomersClient(_clientConfiguration);
            return (await customersClient.CreateAsync(request)).Customer;
        }

        public async Task<CustomerBankAccount> CreateCustomerBankAccountFor(Customer customer)
        {
            var request = new CreateCustomerBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CustomerBankAccountLinks { Customer = customer.Id }
            };

            var customerBankAccountsClient = new CustomerBankAccountsClient(_clientConfiguration);
            return (await customerBankAccountsClient.CreateAsync(request)).CustomerBankAccount;
        }

        public async Task<Mandate> CreateMandate(
            Creditor creditor,
            Customer customer,
            CustomerBankAccount customerBankAccount)
        {
            var request = new CreateMandateRequest
            {
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                //Reference = "REF12345678",
                Scheme = "bacs",
                Links = new CreateMandateLinks
                {
                    Creditor = creditor.Id,
                    CustomerBankAccount = customerBankAccount.Id
                }
            };

            var mandatesClient = new MandatesClient(_clientConfiguration);
            return (await mandatesClient.CreateAsync(request)).Mandate;
        }

        public async Task<Payment> CreatePaymentFor(Mandate mandate)
        {
            var request = new CreatePaymentRequest
            {
                Amount = 500,
                //AppFee = 50,
                ChargeDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Description = "Sandbox Payment",
                Currency = "GBP",
                Links = new CreatePaymentLinks { Mandate = mandate.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Reference = "REF123456"
            };

            var paymentsClient = new PaymentsClient(_clientConfiguration);
            return (await paymentsClient.CreateAsync(request)).Payment;
        }
    }
}