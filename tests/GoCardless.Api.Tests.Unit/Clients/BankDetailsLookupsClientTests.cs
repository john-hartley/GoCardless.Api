﻿using Flurl.Http.Testing;
using GoCardlessApi.BankDetailsLookups;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class BankDetailsLookupsClientTests
    {
        private IBankDetailsLookupsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new BankDetailsLookupsClient(configuration);
            _httpTest = new HttpTest();
        }

        [TearDown]
        public void TearDown()
        {
            _httpTest.Dispose();
        }

        [Test]
        public void throws_when_configuration_not_provided()
        {
            // given
            GoCardlessConfiguration configuration = null;

            // when
            TestDelegate test = () => new BankDetailsLookupsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_bank_details_lookup_options_not_provided()
        {
            // given
            BankDetailsLookupOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.LookupAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_bank_details_lookup_endpoint()
        {
            // given
            var options = new BankDetailsLookupOptions();

            // when
            await _subject.LookupAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/bank_details_lookups")
                .WithVerb(HttpMethod.Post);
        }
    }
}