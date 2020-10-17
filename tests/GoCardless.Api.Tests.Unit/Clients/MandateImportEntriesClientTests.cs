﻿using Flurl.Http.Testing;
using GoCardless.Api.MandateImportEntries;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardless.Api.Tests.Unit.Clients
{
    public class MandateImportEntriesClientTests
    {
        private IMandateImportEntriesClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new MandateImportEntriesClient(configuration);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void ConfigurationIsNullThrows()
        {
            // given
            GoCardlessConfiguration configuration = null;

            // when
            TestDelegate test = () => new MandateImportEntriesClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void AddMandateImportEntryOptionsIsNullThrows()
        {
            // given
            AddMandateImportEntryOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.AddAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task CallsAddMandateImportEntryEndpoint()
        {
            // given
            var options = new AddMandateImportEntryOptions();

            // when
            await _subject.AddAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void GetMandateImportEntriesOptionsIsNullThrows()
        {
            // given
            GetMandateImportEntriesOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void GetMandateImportEntriesOptionsMandateImportIsNullOrWhiteSpaceThrows(string mandateImport)
        {
            // given
            var options = new GetMandateImportEntriesOptions
            {
                MandateImport = mandateImport
            };

            // when
            AsyncTestDelegate test = () => _subject.GetPageAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.MandateImport)));
        }

        [Test]
        public async Task CallsGetMandateImportEntriesEndpoint()
        {
            // given
            var options = new GetMandateImportEntriesOptions
            {
                MandateImport = "IM12345678"
            };

            // when
            await _subject.GetPageAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_import_entries?mandate_import=IM12345678")
                .WithVerb(HttpMethod.Get);
        }
    }
}