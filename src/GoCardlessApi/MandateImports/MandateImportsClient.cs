using GoCardlessApi.Core;
using System;
using System.Threading.Tasks;

namespace GoCardlessApi.MandateImports
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

            return PostAsync<string, CancelMandateImportResponse>(
                $"mandate_imports/{mandateImportId}/actions/cancel"
            );
        }

        public Task<CreateMandateImportResponse> CreateAsync(CreateMandateImportRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return PostAsync<CreateMandateImportRequest, CreateMandateImportResponse>(
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

            return PostAsync<string, SubmitMandateImportResponse>(
                $"mandate_imports/{mandateImportId}/actions/submit"
            );
        }
    }
}