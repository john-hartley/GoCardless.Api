using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImportEntries
{
    public class MandateImportEntriesClient : ApiClientBase
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
    }
}