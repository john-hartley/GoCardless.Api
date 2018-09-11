using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public class MandateImportEntriesClient : ApiClientBase, IMandateImportEntriesClient
    {
        public MandateImportEntriesClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<AddMandateImportEntriesResponse> AddAsync(AddMandateImportEntriesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<AddMandateImportEntriesResponse>(
                "mandate_import_entries",
                new { mandate_import_entries = request }
            );
        }

        public Task<AllMandateImportEntriesResponse> AllAsync(AllMandateImportEntriesRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.MandateImport))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(request.MandateImport));
            }

            return GetAsync<AllMandateImportEntriesResponse>(
                "mandate_import_entries",
                request.ToReadOnlyDictionary()
            );
        }
    }
}