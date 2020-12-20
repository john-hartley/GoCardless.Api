using GoCardlessApi.Creditors;
using GoCardlessApi.CustomerBankAccounts;
using GoCardlessApi.Customers;
using GoCardlessApi.Mandates;
using GoCardlessApi.Common;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class MandatesClientTests : IntegrationTest
    {
        private IMandatesClient _subject;
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

        [SetUp]
        public void Setup()
        {
            _subject = new MandatesClient(_configuration);
        }

        [Test, NonParallelizable]
        public async Task creates_cancels_and_reinstates_mandate()
        {
            // given
            var createOptions = new CreateMandateOptions
            {
                Links = new CreateMandateLinks
                {
                    Creditor = _creditor.Id,
                    CustomerBankAccount = _customerBankAccount.Id
                },
                Metadata = Metadata.Initial,
                Reference = DateTime.Now.ToString("yyyyMMddhhmmss"),
                Scheme = Scheme.Bacs
            };

            // when
            var createResult = await _subject.CreateAsync(createOptions);

            var cancelOptions = new CancelMandateOptions
            {
                Id = createResult.Item.Id,
                Metadata = Metadata.Updated
            };

            var cancelResult = await _subject.CancelAsync(cancelOptions);

            var reinstateOptions = new ReinstateMandateOptions
            {
                Id = createResult.Item.Id,
                Metadata = Metadata.Initial
            };

            var reinstateResult = (await _subject.ReinstateAsync(reinstateOptions));

            // then
            Assert.That(createResult.Item, Is.Not.Null);
            Assert.That(createResult.Item.Id, Is.Not.Null);
            Assert.That(createResult.Item.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(createResult.Item.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(createResult.Item.Links.CustomerBankAccount, Is.EqualTo(_customerBankAccount.Id));
            Assert.That(createResult.Item.Metadata, Is.EqualTo(createOptions.Metadata));
            Assert.That(createResult.Item.NextPossibleChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(createResult.Item.Reference, Is.Not.Null.And.EqualTo(createOptions.Reference));
            Assert.That(createResult.Item.Scheme, Is.EqualTo(createOptions.Scheme));
            Assert.That(createResult.Item.Status, Is.Not.Null.And.Not.EqualTo(MandateStatus.Cancelled));
            
            Assert.That(cancelResult.Item.Status, Is.EqualTo(MandateStatus.Cancelled));

            Assert.That(reinstateResult.Item.Status, Is.Not.Null.And.Not.EqualTo(MandateStatus.Cancelled));
        }

        [Test, NonParallelizable]
        public async Task creates_conflicting_mandate()
        {
            // given
            var options = new CreateMandateOptions
            {
                Links = new CreateMandateLinks
                {
                    Creditor = _creditor.Id,
                    CustomerBankAccount = _customerBankAccount.Id
                },
                Metadata = Metadata.Initial,
                Scheme = Scheme.Bacs,
            };

            // when
            await _subject.CreateAsync(options);
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.CreatedAt, Is.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Links.Creditor, Is.EqualTo(_creditor.Id));
            Assert.That(result.Item.Links.CustomerBankAccount, Is.EqualTo(_customerBankAccount.Id));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.NextPossibleChargeDate, Is.Not.Null.And.Not.EqualTo(default(DateTime)));
            Assert.That(result.Item.Scheme, Is.EqualTo(options.Scheme));
            Assert.That(result.Item.Status, Is.Not.Null.And.Not.EqualTo(MandateStatus.Cancelled));
        }

        [Test]
        public async Task returns_mandates()
        {
            // given
            // when
            var result = (await _subject.GetPageAsync()).Items.ToList();

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
        public async Task maps_paging_properties()
        {
            // given
            var firstPageOptions = new GetMandatesOptions
            {
                Limit = 1
            };

            // when
            var firstPageResult = await _subject.GetPageAsync(firstPageOptions);

            var secondPageOptions = new GetMandatesOptions
            {
                After = firstPageResult.Meta.Cursors.After,
                Limit = 2
            };

            var secondPageResult = await _subject.GetPageAsync(secondPageOptions);

            // then
            Assert.That(firstPageResult.Items.Count(), Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Limit, Is.EqualTo(firstPageOptions.Limit));
            Assert.That(firstPageResult.Meta.Cursors.Before, Is.Null);
            Assert.That(firstPageResult.Meta.Cursors.After, Is.Not.Null);

            Assert.That(secondPageResult.Items.Count(), Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Limit, Is.EqualTo(secondPageOptions.Limit));
            Assert.That(secondPageResult.Meta.Cursors.Before, Is.Not.Null);
            Assert.That(secondPageResult.Meta.Cursors.After, Is.Not.Null);
        }

        [Test]
        public async Task returns_mandate()
        {
            // given
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customerBankAccount);

            // when
            var result = await _subject.ForIdAsync(mandate.Id);
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
        public async Task updates_mandate_preserving_metadata()
        {
            // given
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customerBankAccount);

            var options = new UpdateMandateOptions
            {
                Id = mandate.Id
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(mandate.Metadata));
        }

        [Test]
        public async Task updates_mandate_replacing_metadata()
        {
            // given
            var mandate = await _resourceFactory.CreateMandateFor(_creditor, _customerBankAccount);

            var options = new UpdateMandateOptions
            {
                Id = mandate.Id,
                Metadata = Metadata.Updated
            };

            // when
            var result = await _subject.UpdateAsync(options);
            var actual = result.Item;

            // then
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.Not.Null);
            Assert.That(actual.Metadata, Is.EqualTo(options.Metadata));
        }

        [Test]
        [Category(TestCategory.Paging)]
        public async Task pages_through_mandates()
        {
            // given
            var firstId = (await _subject.GetPageAsync()).Items.First().Id;

            var options = new GetMandatesOptions
            {
                After = firstId,
                CreatedGreaterThan = new DateTimeOffset(DateTime.Now.AddDays(-1))
            };

            // when
            var result = await _subject
                .PageUsing(options)
                .GetItemsAfterAsync();

            // then
            Assert.That(result.Count, Is.GreaterThan(1));
            Assert.That(result[0].Id, Is.Not.Null.And.Not.EqualTo(result[1].Id));
            Assert.That(result[1].Id, Is.Not.Null.And.Not.EqualTo(result[0].Id));
        }
    }
}