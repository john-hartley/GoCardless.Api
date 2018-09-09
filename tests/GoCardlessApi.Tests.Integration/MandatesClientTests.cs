using GoCardlessApi.Core;
using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.Customers;
using GoCardlessApi.Mandates;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration
{
    public class MandatesClientTests : IntegrationTest
    {
        private readonly ClientConfiguration _configuration;
        private readonly ResourceFactory _resourceFactory;

        private Creditor _creditor;
        private Customer _customer;
        private CustomerBankAccount _customerBankAccount;

        public MandatesClientTests()
        {
            _configuration = ClientConfiguration.ForSandbox(_accessToken);
            _resourceFactory = new ResourceFactory(_configuration);
        }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _creditor = await _resourceFactory.Creditor();
            _customer = await _resourceFactory.CreateLocalCustomer();
            _customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(_customer);
        }

        [Test, NonParallelizable]
        public async Task CreatesCancelsAndReinstatesMandate()
        {
            // given
            var createRequest = new CreateMandateRequest
            {
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Reference = DateTime.Now.ToString("yyyyMMddhhmmss"),
                Scheme = "bacs",
                Links = new CreateMandateLinks
                {
                    Creditor = _creditor.Id,
                    CustomerBankAccount = _customerBankAccount.Id
                }
            };

            var subject = new MandatesClient(_configuration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var cancelRequest = new CancelMandateRequest
            {
                Id = creationResult.Mandate.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            var cancellationResult = await subject.CancelAsync(cancelRequest);

            var reinstateRequest = new ReinstateMandateRequest
            {
                Id = creationResult.Mandate.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key7"] = "Value7",
                    ["Key8"] = "Value8",
                    ["Key9"] = "Value9",
                },
            };

            var reinstateResult = (await subject.ReinstateAsync(reinstateRequest));

            // then
            Assert.That(creationResult.Mandate, Is.Not.Null);
            Assert.That(creationResult.Mandate.Id, Is.Not.Null);
            Assert.That(creationResult.Mandate.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(creationResult.Mandate.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(creationResult.Mandate.Links.CustomerBankAccount, Is.EqualTo(_customerBankAccount.Id));
            Assert.That(creationResult.Mandate.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Mandate.NextPossibleChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(creationResult.Mandate.Reference, Is.Not.Null.And.EqualTo(createRequest.Reference));
            Assert.That(creationResult.Mandate.Scheme, Is.EqualTo(createRequest.Scheme));
            Assert.That(creationResult.Mandate.Status, Is.Not.Null.And.Not.EqualTo("cancelled"));
            
            Assert.That(cancellationResult.Mandate.Status, Is.EqualTo("cancelled"));

            Assert.That(reinstateResult.Mandate.Status, Is.Not.Null.And.Not.EqualTo("cancelled"));
        }

        [Test]
        public async Task ReturnsMandates()
        {
            // given
            var subject = new MandatesClient(_configuration);

            // when
            var result = (await subject.AllAsync()).Mandates.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Links.Creditor, Is.Not.Null);
            Assert.That(result[0].Links.CustomerBankAccount, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].NextPossibleChargeDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(result[0].Reference, Is.Not.Null);
            Assert.That(result[0].Scheme, Is.Not.Null);
            Assert.That(result[0].Status, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new MandatesClient(_configuration);

            var firstPageRequest = new AllMandatesRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.AllAsync(firstPageRequest);

            var secondPageRequest = new AllMandatesRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await subject.AllAsync(secondPageRequest);

            // then
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(firstPageResult.Mandates.Count(), Is.EqualTo(firstPageRequest.Limit));

            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
            Assert.That(secondPageResult.Mandates.Count(), Is.EqualTo(secondPageRequest.Limit));
        }

        [Test]
        public async Task ReturnsIndividualMandate()
        {
            // given
            var subject = new MandatesClient(_configuration);
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customer, _customerBankAccount);

            // when
            var result = await subject.ForIdAsync(mandate.Id);
            var actual = result.Mandate;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(mandate.Id));
            Assert.That(actual.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Links.Creditor, Is.Not.Null.And.EqualTo(mandate.Links.Creditor));
            Assert.That(actual.Links.Customer, Is.Not.Null.And.EqualTo(mandate.Links.Customer));
            Assert.That(actual.Links.CustomerBankAccount, Is.Not.Null.And.EqualTo(mandate.Links.CustomerBankAccount));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(mandate.Metadata));
            Assert.That(actual.NextPossibleChargeDate, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Reference, Is.Not.Null);
            Assert.That(actual.Scheme, Is.Not.Null.And.EqualTo(mandate.Scheme));
            Assert.That(actual.Status, Is.Not.Null.And.EqualTo(mandate.Status));
        }

        [Test]
        public async Task UpdatesMandatePreservingMetadata()
        {
            // given
            var subject = new MandatesClient(_configuration);
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customer, _customerBankAccount);

            var request = new UpdateMandateRequest
            {
                Id = mandate.Id
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Mandate;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(mandate.Metadata));
        }

        [Test]
        public async Task UpdatesMandateReplacingMetadata()
        {
            // given
            var subject = new MandatesClient(_configuration);
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customer, _customerBankAccount);

            var request = new UpdateMandateRequest
            {
                Id = mandate.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key4"] = "Value4",
                    ["Key5"] = "Value5",
                    ["Key6"] = "Value6",
                },
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Mandate;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }
    }
}