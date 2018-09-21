using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public class MandateImportEntriesClient : ApiClientBase, IMandateImportEntriesClient
    {
        public MandateImportEntriesClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<MandateImportEntry>> AddAsync(AddMandateImportEntryRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<MandateImportEntry>>(
                "mandate_import_entries",
                new { mandate_import_entries = request }
            );
        }

        public IPagerBuilder<GetMandateImportEntriesRequest, MandateImportEntry> BuildPager()
        {
            return new Pager<GetMandateImportEntriesRequest, MandateImportEntry>(GetPageAsync);
        }

        public Task<PagedResponse<MandateImportEntry>> GetPageAsync(GetMandateImportEntriesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.MandateImport))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.MandateImport));
            }

            return GetAsync<PagedResponse<MandateImportEntry>>(
                "mandate_import_entries",
                request.ToReadOnlyDictionary()
            );
        }
    }
}