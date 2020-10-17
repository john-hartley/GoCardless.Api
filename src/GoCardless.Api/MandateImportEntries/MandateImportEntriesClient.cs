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
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public MandateImportEntriesClient(ApiClientConfiguration apiClientConfiguration)
        {
            if (apiClientConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiClientConfiguration));
            }

            _apiClient = new ApiClient(apiClientConfiguration);
        }

        public async Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return await _apiClient.RequestAsync(
                request =>
                {
                    return request
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

            return await _apiClient.RequestAsync(request =>
            {
                return request
                    .AppendPathSegment("mandate_import_entries")
                    .SetQueryParams(options.ToReadOnlyDictionary())
                    .GetJsonAsync<PagedResponse<MandateImportEntry>>();
            });
        }

        public IPager<GetMandateImportEntriesOptions, MandateImportEntry> PageFrom(GetMandateImportEntriesOptions options)
        {
            return new Pager<GetMandateImportEntriesOptions, MandateImportEntry>(GetPageAsync, options);
        }
    }
}