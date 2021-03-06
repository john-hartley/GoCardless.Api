﻿using Flurl.Http.Testing;
using GoCardlessApi.MandateImports;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Unit.Clients
{
    public class MandateImportsClientTests
    {
        private IMandateImportsClient _subject;
        private HttpTest _httpTest;

        [SetUp]
        public void Setup()
        {
            var configuration = GoCardlessConfiguration.ForLive("accesstoken", false);
            _subject = new MandateImportsClient(configuration);
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
            TestDelegate test = () => new MandateImportsClient(configuration);

            // then
            var ex = Assert.Throws<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(configuration)));
        }

        [Test]
        public void throws_when_cancel_mandate_import_options_not_provided()
        {
            // given
            CancelMandateImportOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_cancel_mandate_import_id_not_provided(string id)
        {
            // given
            var options = new CancelMandateImportOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.CancelAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_cancel_mandate_import_endpoint()
        {
            // given
            var options = new CancelMandateImportOptions
            {
                Id = "IM12345678"
            };

            // when
            await _subject.CancelAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678/actions/cancel")
                .WithVerb(HttpMethod.Post);
        }

        [Test]
        public void throws_when_create_mandate_import_options_not_provided()
        {
            // given
            CreateMandateImportOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.CreateAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [Test]
        public async Task calls_create_mandate_import_endpoint()
        {
            // given
            var options = new CreateMandateImportOptions();

            // when
            await _subject.CreateAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports")
                .WithVerb(HttpMethod.Post);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_id_not_provided(string id)
        {
            // given
            // when
            AsyncTestDelegate test = () => _subject.ForIdAsync(id);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(id)));
        }

        [Test]
        public async Task calls_get_mandate_import_endpoint()
        {
            // given
            var id = "IM12345678";

            // when
            await _subject.ForIdAsync(id);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678")
                .WithVerb(HttpMethod.Get);
        }

        [Test]
        public void throws_when_submit_mandate_import_options_not_provided()
        {
            // given
            SubmitMandateImportOptions options = null;

            // when
            AsyncTestDelegate test = () => _subject.SubmitAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentNullException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("\t  ")]
        public void throws_when_submit_mandate_import_id_not_provided(string id)
        {
            // given
            var options = new SubmitMandateImportOptions
            {
                Id = id
            };

            // when
            AsyncTestDelegate test = () => _subject.SubmitAsync(options);

            // then
            var ex = Assert.ThrowsAsync<ArgumentException>(test);
            Assert.That(ex.ParamName, Is.EqualTo(nameof(options.Id)));
        }

        [Test]
        public async Task calls_submit_mandate_import_endpoint()
        {
            // given
            var options = new SubmitMandateImportOptions
            {
                Id = "IM12345678"
            };

            // when
            await _subject.SubmitAsync(options);

            // then
            _httpTest
                .ShouldHaveCalled("https://api.gocardless.com/mandate_imports/IM12345678/actions/submit")
                .WithVerb(HttpMethod.Post);
        }
    }
}