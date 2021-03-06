﻿using GoCardlessApi.MandateImports;
using GoCardlessApi.Common;
using GoCardlessApi.Tests.Integration.TestHelpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.Tests.Integration.Clients
{
    public class MandateImportsClientTests : IntegrationTest
    {
        private IMandateImportsClient _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new MandateImportsClient(_configuration);
        }

        [Test]
        public async Task cancels_mandate_import()
        {
            // given
            var mandateImport = await _resourceFactory.CreateMandateImport();
            var options = new CancelMandateImportOptions
            {
                Id = mandateImport.Id
            };

            // when
            var result = await _subject.CancelAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.EqualTo(mandateImport.CreatedAt));
            Assert.That(result.Item.Scheme, Is.Not.Null.And.EqualTo(mandateImport.Scheme));
            Assert.That(result.Item.Status, Is.EqualTo(MandateImportStatus.Cancelled));
        }

        [Test]
        public async Task creates_mandate_import()
        {
            // given
            var options = new CreateMandateImportOptions
            {
                Scheme = Scheme.Bacs,
            };

            // when
            var result = await _subject.CreateAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null);
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.Not.EqualTo(default(DateTimeOffset)));
            Assert.That(result.Item.Scheme, Is.EqualTo(options.Scheme));
            Assert.That(result.Item.Status, Is.EqualTo(MandateImportStatus.Created));
        }

        [Test]
        public async Task returns_mandate_import()
        {
            // given
            var mandateImport = await _resourceFactory.CreateMandateImport();

            // when
            var result = await _subject.ForIdAsync(mandateImport.Id);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.EqualTo(mandateImport.CreatedAt));
            Assert.That(result.Item.Scheme, Is.Not.Null.And.EqualTo(mandateImport.Scheme));
            Assert.That(result.Item.Status, Is.EqualTo(MandateImportStatus.Created));
        }

        [Test]
        public async Task submits_mandate_import()
        {
            // given
            var mandateImport = await _resourceFactory.CreateMandateImport();
            var options = new SubmitMandateImportOptions
            {
                Id = mandateImport.Id
            };

            // when
            var result = await _subject.SubmitAsync(options);

            // then
            Assert.That(result.Item, Is.Not.Null);
            Assert.That(result.Item.Id, Is.Not.Null.And.EqualTo(mandateImport.Id));
            Assert.That(result.Item.CreatedAt, Is.Not.Null.And.EqualTo(mandateImport.CreatedAt));
            Assert.That(result.Item.Scheme, Is.Not.Null.And.EqualTo(mandateImport.Scheme));
            Assert.That(result.Item.Status, Is.EqualTo(MandateImportStatus.Submitted));
        }
    }
}