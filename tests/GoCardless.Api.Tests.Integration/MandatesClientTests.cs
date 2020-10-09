using GoCardless.Api.Creditors;
using GoCardless.Api.CustomerBankAccounts;
using GoCardless.Api.Customers;
using GoCardless.Api.Mandates;
using GoCardless.Api.Models;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class MandatesClientTests : IntegrationTest
    {
        private Creditor _creditor;
        private Customer _customer;
        private CustomerBankAccount _customerBankAccount;

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
                Links = new CreateMandateLinks
                {
                    Creditor = _creditor.Id,
                    CustomerBankAccount = _customerBankAccount.Id
                },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Reference = DateTime.Now.ToString("yyyyMMddhhmmss"),
                Scheme = Scheme.Bacs
            };

            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);

            // when
            var creationResult = await subject.CreateAsync(createRequest);

            var cancelRequest = new CancelMandateRequest
            {
                Id = creationResult.Item.Id,
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
                Id = creationResult.Item.Id,
                Metadata = new Dictionary<string, string>
                {
                    ["Key7"] = "Value7",
                    ["Key8"] = "Value8",
                    ["Key9"] = "Value9",
                },
            };

            var reinstateResult = (await subject.ReinstateAsync(reinstateRequest));

            // then
            Assert.That(creationResult.Item, Is.Not.Null);
            Assert.That(creationResult.Item.Id, Is.Not.Null);
            Assert.That(creationResult.Item.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(creationResult.Item.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(creationResult.Item.Links.CustomerBankAccount, Is.EqualTo(_customerBankAccount.Id));
            Assert.That(creationResult.Item.Metadata, Is.EqualTo(createRequest.Metadata));
            Assert.That(creationResult.Item.NextPossibleChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(creationResult.Item.Reference, Is.Not.Null.And.EqualTo(createRequest.Reference));
            Assert.That(creationResult.Item.Scheme, Is.EqualTo(createRequest.Scheme));
            Assert.That(creationResult.Item.Status, Is.Not.Null.And.Not.EqualTo(MandateStatus.Cancelled));
            
            Assert.That(cancellationResult.Item.Status, Is.EqualTo(MandateStatus.Cancelled));

            Assert.That(reinstateResult.Item.Status, Is.Not.Null.And.Not.EqualTo(MandateStatus.Cancelled));
        }

        [Test, NonParallelizable]
        public async Task CreatesConflictingMandate()
        {
            // given
            var request = new CreateMandateRequest
            {
                Links = new CreateMandateLinks
                {
                    Creditor = _creditor.Id,
                    CustomerBankAccount = _customerBankAccount.Id
                },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Scheme = Scheme.Bacs,
            };

            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);

            // when
            await subject.CreateAsync(request);
            var result = await subject.CreateAsync(request);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(result.Item.Links.CustomerBankAccount, Is.EqualTo(_customerBankAccount.Id));
            Assert.That(result.Item.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(result.Item.NextPossibleChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result.Item.Scheme, Is.EqualTo(request.Scheme));
            Assert.That(result.Item.Status, Is.Not.Null.And.Not.EqualTo(MandateStatus.Cancelled));
        }

        [Test]
        public async Task ReturnsMandates()
        {
            // given
            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);

            // when
            var result = (await subject.GetPageAsync()).Items.ToList();

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
            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);

            var firstPageRequest = new GetMandatesRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetMandatesRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await subject.GetPageAsync(secondPageRequest);

            // then
            Assert.That(firstPageResult.Items.Count(), Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageRequest.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);

            Assert.That(secondPageResult.Items.Count(), Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageRequest.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualMandate()
        {
            // given
            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customer, _customerBankAccount);

            // when
            var result = await subject.ForIdAsync(mandate.Id);
            var actual = result.Item;

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
            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customer, _customerBankAccount);

            var request = new UpdateMandateRequest
            {
                Id = mandate.Id
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(mandate.Metadata));
        }

        [Test]
        public async Task UpdatesMandateReplacingMetadata()
        {
            // given
            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);
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
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
        }

        [Test, Explicit("Can end up performing lots of calls.")]
        public async Task PagesThroughMandates()
        {
            // given
            var subject = new MandatesClient(_apiClient, _apiClient.Configuration);
            var firstId = (await subject.GetPageAsync()).Items.First().Id;

            var initialRequest = new GetMandatesRequest
            {
                After = firstId,
                CreatedGreaterThan = new DateTimeOffset(DateTime.Now.AddDays(-1)),
                Limit = 1,
            };

            // when
            var result = await subject
                .BuildPager()
                .StartFrom(initialRequest)
                .AndGetAllAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}