using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.Customers;
using GoCardless.Api.MandateImportEntries;
using GoCardless.Api.MandateImports;
using GoCardless.Api.Mandates;
using GoCardless.Api.Models;
using GoCardless.Api.Payments;
using GoCardless.Api.Payouts;
using GoCardless.Api.RedirectFlows;
using GoCardless.Api.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customer = GoCardless.Api.Customers.Customer;

namespace GoCardless.Api.Tests.Integration.TestHelpers
{
    public class ResourceFactory
    {
        private readonly IApiClient _apiClient;

        internal ResourceFactory(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        internal async Task<Creditor> Creditor()
        {
            var creditorsClient = new CreditorsClient(_apiClient);
            return (await creditorsClient.GetPageAsync()).Items.First();
        }

        internal Task<Customer> CreateForeignCustomer()
        {
            return CreateCustomer("NZ", "en", "2205506218", "5302256218");
        }

        internal Task<Customer> CreateLocalCustomer()
        {
            return CreateCustomer("GB", "en");
        }

        internal async Task<CustomerBankAccount> CreateCustomerBankAccountFor(Customer customer)
        {
            var request = new CreateCustomerBankAccountRequest
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CustomerBankAccountLinks { Customer = customer.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                }
            };

            var customerBankAccountsClient = new CustomerBankAccountsClient(_apiClient);
            return (await customerBankAccountsClient.CreateAsync(request)).Item;
        }

        internal async Task<Mandate> CreateMandateFor(
            Creditor creditor,
            Customer customer,
            CustomerBankAccount customerBankAccount)
        {
            var request = new CreateMandateRequest
            {
                Links = new CreateMandateLinks
                {
                    Creditor = creditor.Id,
                    CustomerBankAccount = customerBankAccount.Id
                },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Scheme = Scheme.Bacs
            };

            var mandatesClient = new MandatesClient(_apiClient);
            return (await mandatesClient.CreateAsync(request)).Item;
        }

        internal async Task<MandateImport> CreateMandateImport()
        {
            var request = new CreateMandateImportRequest
            {
                Scheme = "bacs",
            };

            var mandateImportsClient = new MandateImportsClient(_apiClient);
            return (await mandateImportsClient.CreateAsync(request)).Item;
        }

        internal async Task<MandateImportEntry> CreateMandateImportEntryFor(
            MandateImport mandateImport,
            string recordIdentifier)
        {
            var request = new AddMandateImportEntryRequest
            {
                BankAccount = new BankAccount
                {
                    AccountHolderName = "Joe Bloggs",
                    AccountNumber = "55666666",
                    BranchCode = "200000",
                    CountryCode = "GB"
                },
                Customer = new MandateImportEntries.Customer
                {
                    AddressLine1 = "Address Line 1",
                    AddressLine2 = "Address Line 2",
                    AddressLine3 = "Address Line 3",
                    City = "London",
                    CompanyName = "Company Name",
                    CountryCode = "DK",
                    DanishIdentityNumber = "2205506218",
                    Email = "email@example.com",
                    FamilyName = "Family Name",
                    GivenName = "Given Name",
                    Language = "da",
                    PostalCode = "SW1A 1AA",
                    Region = "Essex",
                    SwedishIdentityNumber = "5302256218",
                },
                Links = new AddMandateImportEntryLinks
                {
                    MandateImport = mandateImport.Id
                },
                RecordIdentifier = recordIdentifier
            };

            var mandateImportEntriesClient = new MandateImportEntriesClient(_apiClient);
            return (await mandateImportEntriesClient.AddAsync(request)).Item;
        }

        internal async Task<Payment> CreatePaymentFor(Mandate mandate)
        {
            var request = new CreatePaymentRequest
            {
                Amount = 500,
                ChargeDate = DateTime.Now.AddMonths(1),
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

            var paymentsClient = new PaymentsClient(_apiClient);
            return (await paymentsClient.CreateAsync(request)).Item;
        }

        internal async Task<Payout> Payout()
        {
            var request = new GetPayoutsRequest
            {
                PayoutType = PayoutType.Merchant
            };

            var payoutsClient = new PayoutsClient(_apiClient);
            return (await payoutsClient.GetPageAsync(request)).Items.First();
        }

        internal async Task<RedirectFlow> CreateRedirectFlowFor(Creditor creditor)
        {
            var request = new CreateRedirectFlowRequest
            {
                Description = "First redirect flow",
                Links = new CreateRedirectFlowLinks
                {
                    Creditor = creditor.Id
                },
                PrefilledCustomer = new PrefilledCustomer
                {
                    AddressLine1 = "Address Line 1",
                    AddressLine2 = "Address Line 2",
                    AddressLine3 = "Address Line 3",
                    City = "London",
                    CompanyName = "Company Name",
                    CountryCode = "GB",
                    DanishIdentityNumber = "2205506218",
                    Email = "email@example.com",
                    FamilyName = "Family Name",
                    GivenName = "Given Name",
                    Language = "en",
                    PostalCode = "SW1A 1AA",
                    Region = "Essex",
                    SwedishIdentityNumber = "5302256218",
                },
                Scheme = Scheme.Bacs,
                SessionToken = Guid.NewGuid().ToString(),
                SuccessRedirectUrl = "https://localhost",
            };

            // when
            var redirectFlowsClient = new RedirectFlowsClient(_apiClient);
            return (await redirectFlowsClient.CreateAsync(request)).Item;
        }

        internal async Task<Subscription> CreateSubscriptionFor(Mandate mandate, string paymentReference = "PR123456")
        {
            var request = new CreateSubscriptionRequest
            {
                Amount = 123,
                Currency = "GBP",
                Count = 5,
                Interval = 3,
                IntervalUnit = IntervalUnit.Weekly,
                Links = new SubscriptionLinks
                {
                    Mandate = mandate.Id
                },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Name = "Test subscription",
                PaymentReference = paymentReference,
                StartDate = DateTime.Now.AddMonths(1)
            };

            var subscriptionsClient = new SubscriptionsClient(_apiClient);
            return (await subscriptionsClient.CreateAsync(request)).Item;
        }

        private async Task<Customer> CreateCustomer(
            string countryCode,
            string language,
            string danishIdentityNumber = null,
            string swedishIdentityNumber = null)
        {
            var request = new CreateCustomerRequest
            {
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                AddressLine3 = "Address Line 3",
                City = "London",
                CompanyName = "Company Name",
                CountryCode = countryCode,
                DanishIdentityNumber = danishIdentityNumber,
                Email = "email@example.com",
                FamilyName = "Family Name",
                GivenName = "Given Name",
                Language = language,
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                PhoneNumber = "+44 1234 567890",
                PostalCode = "SW1A 1AA",
                Region = "Essex",
                SwedishIdentityNumber = swedishIdentityNumber
            };

            var customersClient = new CustomersClient(_apiClient);
            return (await customersClient.CreateAsync(request)).Item;
        }
    }
}