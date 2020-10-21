using GoCardlessApi.InstalmentSchedules;
using GoCardlessApi.Mandates;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class InstalmentSchedulesClientTests : IntegrationTest
    {
        private IInstalmentSchedulesClient _subject;
        private Mandate _mandate;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            _mandate = await _resourceFactory.CreateMandateFor(creditor, customerBankAccount);
        }

        [SetUp]
        public void Setup()
        {
            _subject = new InstalmentSchedulesClient(_configuration);
        }

        [Test]
        public async Task CreatesInvalidInstalmentSchedule()
        {
            // given
            var options = new CreateInstalmentScheduleWithDatesOptions
            {
                Currency = "GBP",
                Instalments = new List<InstalmentScheduleDate>
                {
                    new InstalmentScheduleDate
                    {
                        Amount = 100,
                        ChargeDate = DateTime.Now
                    },
                    new InstalmentScheduleDate
                    {
                        Amount = 800,
                        ChargeDate = DateTime.Now.AddDays(7)
                    },
                    new InstalmentScheduleDate
                    {
                        Amount = 100,
                        ChargeDate = new DateTime(2020, 12, 25)
                    },
                },
                Links = new InstalmentScheduleLinks
                {
                    Mandate = _mandate.Id
                },
                Metadata = Metadata.Initial,
                Name = "Test schedule",
                TotalAmount = 1000
            };

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.Currency, Is.EqualTo(options.Currency));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            // How do I trigger a failure?
            Assert.That(result.Item.PaymentErrors, Is.Not.Empty);
            Assert.That(result.Item.Status, Is.Not.Null);
            Assert.That(result.Item.TotalAmount, Is.EqualTo(options.TotalAmount));
        }

        [Test]
        public async Task CreatesInstalmentScheduleUsingDates()
        {
            // given
            var options = new CreateInstalmentScheduleWithDatesOptions
            {
                Currency = "GBP",
                Instalments = new List<InstalmentScheduleDate>
                {
                    new InstalmentScheduleDate
                    {
                        Amount = 100,
                        ChargeDate = DateTime.Now
                    },
                    new InstalmentScheduleDate
                    {
                        Amount = 900,
                        ChargeDate = DateTime.Now.AddDays(7)
                    },
                },
                Links = new InstalmentScheduleLinks
                {
                    Mandate = _mandate.Id
                },
                Metadata = Metadata.Initial,
                Name = "Test schedule",
                TotalAmount = 1000
            };

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.Currency, Is.EqualTo(options.Currency));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            Assert.That(result.Item.PaymentErrors, Is.Empty);
            Assert.That(result.Item.Status, Is.Not.Null);
            Assert.That(result.Item.TotalAmount, Is.EqualTo(options.TotalAmount));
        }

        [Test]
        public async Task CreatesInstalmentScheduleUsingSchedule()
        {
            // given
            var options = new CreateInstalmentScheduleWithScheduleOptions
            {
                Currency = "GBP",
                Instalments = new Schedule
                {
                    StartDate = DateTime.Now.Date,
                    Amounts = new List<int> { 100, 900 },
                    Interval = 1,
                    IntervalUnit = "monthly"
                },
                Links = new InstalmentScheduleLinks
                {
                    Mandate = _mandate.Id
                },
                Metadata = Metadata.Initial,
                Name = "Test schedule",
                TotalAmount = 1000
            };

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.Currency, Is.EqualTo(options.Currency));
            Assert.That(result.Item.Metadata, Is.EqualTo(options.Metadata));
            Assert.That(result.Item.Name, Is.EqualTo(options.Name));
            Assert.That(result.Item.PaymentErrors, Is.Empty);
            Assert.That(result.Item.Status, Is.Not.Null);
            Assert.That(result.Item.TotalAmount, Is.EqualTo(options.TotalAmount));
        }
    }
}