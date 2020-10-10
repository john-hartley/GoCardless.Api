using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public class MandateImportEntriesClient : IMandateImportEntriesClient
    {
        private readonly IApiClient _apiClient;

        public MandateImportEntriesClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.PostAsync<Response<MandateImportEntry>>(
                "mandate_import_entries",
                new { mandate_import_entries = options },
                request =>
                {
                    request.AppendPathSegment("mandate_import_entries");
                });
        }

        public IPagerBuilder<GetMandateImportEntriesRequest, MandateImportEntry> BuildPager()
        {
            return new Pager<GetMandateImportEntriesRequest, MandateImportEntry>(GetPageAsync);
        }

        public async Task<PagedResponse<MandateImportEntry>> GetPageAsync(GetMandateImportEntriesRequest options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.MandateImport))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(options.MandateImport));
            }

            return await _apiClient.GetAsync<PagedResponse<MandateImportEntry>>(request =>
            {
                request
                    .AppendPathSegment("mandate_import_entries")
                    .SetQueryParams(options.ToReadOnlyDictionary());
            });
        }
    }
}