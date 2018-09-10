using GoCardless.Api.Core;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImports
{
    public class MandateImportsClient : ApiClientBase, IMandateImportsClient
    {
        public MandateImportsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<CancelMandateImportResponse> CancelAsync(string mandateImportId)
        {
            if (string.IsNullOrWhiteSpace(mandateImportId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateImportId));
            }

            return PostAsync<CancelMandateImportResponse>(
                $"mandate_imports/{mandateImportId}/actions/cancel"
            );
        }

        public Task<CreateMandateImportResponse> CreateAsync(CreateMandateImportRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<CreateMandateImportResponse>(
                "mandate_imports",
                new { mandate_imports = request }
            );
        }

        public Task<MandateImportResponse> ForIdAsync(string mandateImportId)
        {
            if (string.IsNullOrWhiteSpace(mandateImportId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateImportId));
            }

            return GetAsync<MandateImportResponse>($"mandate_imports/{mandateImportId}");
        }

        public Task<SubmitMandateImportResponse> SubmitAsync(string mandateImportId)
        {
            if (string.IsNullOrWhiteSpace(mandateImportId))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(mandateImportId));
            }

            return PostAsync<SubmitMandateImportResponse>(
                $"mandate_imports/{mandateImportId}/actions/submit"
            );
        }
    }
}