using GoCardless.Api.MandateImports;
using GoCardless.Api.Models;
using GoCardless.Api.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Integration
{
    public class MandateImportsClientTests : IntegrationTest
    {
        [Test]
        public async Task CancelsMandateImport()
        {
            // given
            var subject = new MandateImportsClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            // when
            var result = await subject.CancelAsync(mandateImport.Id);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.EqualTo(mandateImport.CreatedAt));
            Assert.That(result.Item.Scheme, Is.Not.Null.And.EqualTo(mandateImport.Scheme));
            Assert.That(result.Item.Status, Is.Not.Null.And.Not.EqualTo(mandateImport.Status));
        }

        [Test]
        public async Task CreatesMandateImport()
        {
            // given
            var subject = new MandateImportsClient(_apiClient, _apiClient.Configuration);

            var request = new CreateMandateImportRequest
            {
                Scheme = Scheme.Bacs,
            };

            // when
            var result = await subject.CreateAsync(request);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Scheme, Is.EqualTo(request.Scheme));
            Assert.That(result.Item.Status, Is.Not.Null);
        }

        [Test]
        public async Task ReturnsIndividualMandateImport()
        {
            // given
            var subject = new MandateImportsClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            // when
            var result = await subject.ForIdAsync(mandateImport.Id);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.EqualTo(mandateImport.CreatedAt));
            Assert.That(result.Item.Scheme, Is.Not.Null.And.EqualTo(mandateImport.Scheme));
            Assert.That(result.Item.Status, Is.Not.Null.And.EqualTo(mandateImport.Status));
        }

        [Test]
        public async Task SubmitsMandateImport()
        {
            // given
            var subject = new MandateImportsClient(_apiClient, _apiClient.Configuration);
            var mandateImport = await _resourceFactory.CreateMandateImport();

            // when
            var result = await subject.SubmitAsync(mandateImport.Id);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.EqualTo(mandateImport.CreatedAt));
            Assert.That(result.Item.Scheme, Is.Not.Null.And.EqualTo(mandateImport.Scheme));
            Assert.That(result.Item.Status, Is.Not.Null.And.Not.EqualTo(mandateImport.Status));
        }
    }
}