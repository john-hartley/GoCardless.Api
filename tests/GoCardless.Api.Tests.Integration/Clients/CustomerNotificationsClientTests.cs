using GoCardlessApi.CustomerNotifications;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class CustomerNotificationsClientTests : IntegrationTest
    {
        [Test, Explicit("The id for the notification to handle comes via a webhook, so a mandate or payment must be created prior to use.")]
        [Category(TestCategory.NeedsManualIntervention)]
        public async Task handles_customer_notification()
        {
            // given
            var creditor = await _resourceFactory.Creditor();
            var customer = await _resourceFactory.CreateLocalCustomer();
            var customerBankAccount = await _resourceFactory.CreateCustomerBankAccountFor(customer);
            await _resourceFactory.CreateMandateFor(creditor, customerBankAccount);
            var subject = new CustomerNotificationsClient(_configuration);

            var options = new HandleCustomerNotificationOptions
            {
                Id = "PCN000MD59PMGWZ"
            };

            // when
            var result = await subject.HandleAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(options.Id));
            Assert.That(result.Item.ActionTaken, Is.Not.Null.And.EqualTo(ActionTaken.Handled));
            Assert.That(result.Item.ActionTakenAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.ActionTakenBy, Is.Not.Null);
            Assert.That(result.Item.Links, Is.Not.Null);
            Assert.That(result.Item.Links.Customer, Is.Not.Null);
            Assert.That(result.Item.Links.Event, Is.Not.Null);
            Assert.That(result.Item.Links.Mandate, Is.Not.Null);
            Assert.That(result.Item.Type, Is.Not.Null);
        }
    }
}