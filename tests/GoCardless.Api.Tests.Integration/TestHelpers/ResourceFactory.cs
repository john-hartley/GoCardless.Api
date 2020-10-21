using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.Customers;
using GoCardlessApi.MandateImportEntries;
using GoCardlessApi.MandateImports;
using GoCardlessApi.Mandates;
using GoCardlessApi.Common;
using GoCardlessApi.Payments;
using GoCardlessApi.Payouts;
using GoCardlessApi.RedirectFlows;
using GoCardlessApi.Subscriptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Customer = GoCardlessApi.Customers.Customer;

namespace GoCardlessApi.Tests.Integration.TestHelpers
{
    public class ResourceFactory
    {
        private readonly GoCardlessConfiguration _configuration;

        internal ResourceFactory(GoCardlessConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal async Task<Creditor> Creditor()
        {
            var creditorsClient = new CreditorsClient(_configuration);
            return (await creditorsClient.GetPageAsync()).Items.First();
        }

        internal Task<Customer> CreateNzCustomer()
        {
            return CreateCustomer("NZ", "en", "2205506218", "5302256218");
        }

        internal Task<Customer> CreateLocalCustomer(
            string countryCode = null, 
            string language = null,
            string region = null)
        {
            return CreateCustomer(countryCode ?? "GB", language ?? "en", region);
        }

        internal async Task<CustomerBankAccount> CreateCustomerBankAccountFor(Customer customer)
        {
            var options = new CreateCustomerBankAccountOptions
            {
                AccountHolderName = "API BANK ACCOUNT",
                AccountNumber = "55666666",
                BranchCode = "200000",
                CountryCode = "GB",
                Currency = "GBP",
                Links = new CustomerBankAccountLinks { Customer = customer.Id },
                Metadata = Metadata.Initial
            };

            var customerBankAccountsClient = new CustomerBankAccountsClient(_configuration);
            return (await customerBankAccountsClient.CreateAsync(options)).Item;
        }

        internal async Task<Mandate> CreateMandateFor(
            Creditor creditor,
            CustomerBankAccount customerBankAccount)
        {
            var options = new CreateMandateOptions
            {
                Links = new CreateMandateLinks
                {
                    Creditor = creditor.Id,
                    CustomerBankAccount = customerBankAccount.Id
                },
                Metadata = Metadata.Initial,
                Scheme = Scheme.Bacs
            };

            var mandatesClient = new MandatesClient(_configuration);
            return (await mandatesClient.CreateAsync(options)).Item;
        }

        internal async Task<MandateImport> CreateMandateImport()
        {
            var options = new CreateMandateImportOptions
            {
                Scheme = "bacs",
            };

            var mandateImportsClient = new MandateImportsClient(_configuration);
            return (await mandateImportsClient.CreateAsync(options)).Item;
        }

        internal async Task<MandateImportEntry> CreateMandateImportEntryFor(
            MandateImport mandateImport,
            string recordIdentifier)
        {
            var options = new CreateMandateImportEntryOptions
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

            var mandateImportEntriesClient = new MandateImportEntriesClient(_configuration);
            return (await mandateImportEntriesClient.CreateAsync(options)).Item;
        }

        internal async Task<Payment> CreatePaymentFor(Mandate mandate)
        {
            var options = new CreatePaymentOptions
            {
                Amount = 500,
                ChargeDate = DateTime.Now.AddMonths(1),
                Description = "Sandbox Payment",
                Currency = "GBP",
                Links = new CreatePaymentLinks { Mandate = mandate.Id },
                Metadata = Metadata.Initial,
                Reference = "REF123456"
            };

            var paymentsClient = new PaymentsClient(_configuration);
            return (await paymentsClient.CreateAsync(options)).Item;
        }

        internal async Task<Payout> Payout()
        {
            var options = new GetPayoutsOptions
            {
                PayoutType = PayoutType.Merchant
            };

            var payoutsClient = new PayoutsClient(_configuration);
            return (await payoutsClient.GetPageAsync(options)).Items.First();
        }

        internal async Task<RedirectFlow> CreateRedirectFlowFor(Creditor creditor)
        {
            var options = new CreateRedirectFlowOptions
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
            var redirectFlowsClient = new RedirectFlowsClient(_configuration);
            return (await redirectFlowsClient.CreateAsync(options)).Item;
        }

        internal async Task<Subscription> CreateSubscriptionFor(Mandate mandate, string paymentReference = "PR123456")
        {
            var options = new CreateSubscriptionOptions
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
                Metadata = Metadata.Initial,
                Name = "Test subscription",
                PaymentReference = paymentReference,
                StartDate = DateTime.Now.AddMonths(1)
            };

            var subscriptionsClient = new SubscriptionsClient(_configuration);
            return (await subscriptionsClient.CreateAsync(options)).Item;
        }

        private async Task<Customer> CreateCustomer(
            string countryCode,
            string language,
            string region = "Essex",
            string danishIdentityNumber = null,
            string swedishIdentityNumber = null)
        {
            var options = new CreateCustomerOptions
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
                Metadata = Metadata.Initial,
                PhoneNumber = "+44 1234 567890",
                PostalCode = "SW1A 1AA",
                Region = region ?? "Essex",
                SwedishIdentityNumber = swedishIdentityNumber
            };

            var customersClient = new CustomersClient(_configuration);
            return (await customersClient.CreateAsync(options)).Item;
        }
    }
}