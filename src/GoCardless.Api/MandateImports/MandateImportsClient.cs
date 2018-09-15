using GoCardless.Api.Core;
using GoCardless.Api.Core.Configuration;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImports
{
    public class MandateImportsClient : ApiClientBase, IMandateImportsClient
    {
        public MandateImportsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<MandateImport>> CancelAsync(string mandateImportId)
        {
            if (string.IsNullOrWhiteSpace(mandateImportId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateImportId));
            }

            return PostAsync<Response<MandateImport>>(
                $"mandate_imports/{mandateImportId}/actions/cancel"
            );
        }

        public Task<Response<MandateImport>> CreateAsync(CreateMandateImportRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<Response<MandateImport>>(
                "mandate_imports",
                new { mandate_imports = request }
            );
        }

        public Task<Response<MandateImport>> ForIdAsync(string mandateImportId)
        {
            if (string.IsNullOrWhiteSpace(mandateImportId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateImportId));
            }

            return GetAsync<Response<MandateImport>>($"mandate_imports/{mandateImportId}");
        }

        public Task<Response<MandateImport>> SubmitAsync(string mandateImportId)
        {
            if (string.IsNullOrWhiteSpace(mandateImportId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateImportId));
            }

            return PostAsync<Response<MandateImport>>(
                $"mandate_imports/{mandateImportId}/actions/submit"
            );
        }
    }
}