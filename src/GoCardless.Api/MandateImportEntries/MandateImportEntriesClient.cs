﻿using Flurl.Http;
using GoCardlessApi.Http;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.MandateImportEntries
{
    public class MandateImportEntriesClient : IMandateImportEntriesClient
    {
        private readonly ApiClient _apiClient;

        public MandateImportEntriesClient(GoCardlessConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
        }

        public async Task<Response<MandateImportEntry>> CreateAsync(CreateMandateImportEntryOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("mandate_import_entries")
                    .PostJsonAsync(new { mandate_import_entries = options })
                    .ReceiveJson<Response<MandateImportEntry>>();
            });
        }

        public async Task<PagedResponse<MandateImportEntry>> GetPageAsync(GetMandateImportEntriesOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.MandateImport))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.MandateImport));
            }

            return await _apiClient.RequestAsync(async request =>
            {
                return await request
                    .AppendPathSegment("mandate_import_entries")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<MandateImportEntry>>();
            });
        }

        public IPager<GetMandateImportEntriesOptions, MandateImportEntry> PageUsing(GetMandateImportEntriesOptions options)
        {
            return new Pager<GetMandateImportEntriesOptions, MandateImportEntry>(GetPageAsync, options);
        }
    }
}