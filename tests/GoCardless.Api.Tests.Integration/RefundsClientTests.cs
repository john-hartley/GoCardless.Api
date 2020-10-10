using GoCardless.Api.Mandates;
using GoCardless.Api.Refunds;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class RefundsClientTests : IntegrationTest
    {
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(creditor, customer, customerBankAccount);
        }

        [Test, Explicit("Need to use scenario simulators to activate the mandate, and pay out the created payment, before continuing.")]
        public async Task CreatesRefund()
        {
            // given
            var payment = await _resourceFactory.CreatePaymentFor(_mandate);

            var request = new CreateRefundRequest
            {
                Amount = 100,
                Links = new CreateRefundLinks { Payment = payment.Id },
                Metadata = new Dictionary<string, string>
                {
                    ["Key1"] = "Value1",
                    ["Key2"] = "Value2",
                    ["Key3"] = "Value3",
                },
                Reference = "RF123456",
                TotalAmountConfirmation = 100
            };

            var subject = new RefundsClient(_apiClient);

            // when
            var result = await subject.CreateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Amount, Is.EqualTo(request.Amount));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(actual.Currency, Is.EqualTo(payment.Currency));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Payment, Is.EqualTo(request.Links.Payment));
            Assert.That(actual.Metadata, Is.EqualTo(request.Metadata));
            Assert.That(actual.Reference, Is.EqualTo(request.Reference));
        }

        [Test, NonParallelizable]
        public async Task ReturnsRefunds()
        {
            // given
            var subject = new RefundsClient(_apiClient);

            // when
            var result = (await subject.GetPageAsync()).Items.ToList();

            // then
            Assert.That(result.Any(), Is.True);
            Assert.That(result[0], Is.Not.Null);
            Assert.That(result[0].Id, Is.Not.Null);
            Assert.That(result[0].Amount, Is.Not.EqualTo(default(int)));
            Assert.That(result[0].Currency, Is.Not.Null);
            Assert.That(result[0].CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result[0].Links, Is.Not.Null);
            Assert.That(result[0].Links.Mandate, Is.Not.Null);
            Assert.That(result[0].Links.Payment, Is.Not.Null);
            Assert.That(result[0].Metadata, Is.Not.Null);
            Assert.That(result[0].Reference, Is.Not.Null);
        }

        [Test]
        public async Task MapsPagingProperties()
        {
            // given
            var subject = new RefundsClient(_apiClient);

            var firstPageRequest = new GetRefundsRequest
            {
                Limit = 1
            };

            // when
            var firstPageResult = await subject.GetPageAsync(firstPageRequest);

            var secondPageRequest = new GetRefundsRequest
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 1
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

        [Test, NonParallelizable]
        public async Task ReturnsIndividualRefund()
        {
            // given
            var subject = new RefundsClient(_apiClient);
            var refund = (await subject.GetPageAsync()).Items.First();

            // when
            var result = await subject.ForIdAsync(refund.Id);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(refund.Id));
            Assert.That(actual.Amount, Is.Not.Null.And.EqualTo(refund.Amount));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(refund.Currency));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(refund.CreatedAt));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Mandate, Is.Not.Null.And.EqualTo(refund.Links.Mandate));
            Assert.That(actual.Links.Payment, Is.Not.Null.And.EqualTo(refund.Links.Payment));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(refund.Metadata));
            Assert.That(actual.Reference, Is.Not.Null.And.EqualTo(refund.Reference));
        }

        [Test, NonParallelizable]
        public async Task UpdatesRefundPreservingMetadata()
        {
            // given
            var subject = new RefundsClient(_apiClient);
            var refund = (await subject.GetPageAsync()).Items.First();

            var request = new UpdateRefundRequest
            {
                Id = refund.Id
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(refund.Id));
            Assert.That(actual.Amount, Is.Not.Null.And.EqualTo(refund.Amount));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(refund.Currency));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(refund.CreatedAt));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Mandate, Is.Not.Null.And.EqualTo(refund.Links.Mandate));
            Assert.That(actual.Links.Payment, Is.Not.Null.And.EqualTo(refund.Links.Payment));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(refund.Metadata));
            Assert.That(actual.Reference, Is.Not.Null.And.EqualTo(refund.Reference));
        }

        [Test, NonParallelizable]
        public async Task UpdatesRefundReplacingMetadata()
        {
            // given
            var subject = new RefundsClient(_apiClient);
            var refund = (await subject.GetPageAsync()).Items.First();
            var now = DateTime.Now.ToString("yyyyMMdd");

            var request = new UpdateRefundRequest
            {
                Id = refund.Id,
                Metadata = new Dictionary<string, string>
                {
                    [$"Key-1-{now}"] = $"Value-1-{now}",
                    [$"Key-2-{now}"] = $"Value-2-{now}",
                    [$"Key-3-{now}"] = $"Value-3-{now}",
                },
            };

            // when
            var result = await subject.UpdateAsync(request);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null.And.EqualTo(refund.Id));
            Assert.That(actual.Amount, Is.Not.Null.And.EqualTo(refund.Amount));
            Assert.That(actual.Currency, Is.Not.Null.And.EqualTo(refund.Currency));
            Assert.That(actual.CreatedAt, Is.Not.Null.And.EqualTo(refund.CreatedAt));
            Assert.That(actual.Links, Is.Not.Null);
            Assert.That(actual.Links.Mandate, Is.Not.Null.And.EqualTo(refund.Links.Mandate));
            Assert.That(actual.Links.Payment, Is.Not.Null.And.EqualTo(refund.Links.Payment));
            Assert.That(actual.Metadata, Is.Not.Null.And.EqualTo(request.Metadata));
            Assert.That(actual.Reference, Is.Not.Null.And.EqualTo(refund.Reference));
        }

        [Test, NonParallelizable]
        public async Task PagesThroughRefunds()
        {
            // given
            var subject = new RefundsClient(_apiClient);
            var firstId = (await subject.GetPageAsync()).Items.First().Id;

            var initialRequest = new GetRefundsRequest
            {
                Limit = 1
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