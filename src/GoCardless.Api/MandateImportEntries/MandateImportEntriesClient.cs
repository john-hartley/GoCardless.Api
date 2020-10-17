using Flurl.Http;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public class MandateImportEntriesClient : IMandateImportEntriesClient
    {
        private readonly ApiClient _apiClient;

        public MandateImportEntriesClient(ApiClientConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _apiClient = new ApiClient(configuration);
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