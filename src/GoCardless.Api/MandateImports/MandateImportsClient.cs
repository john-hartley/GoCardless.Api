using GoCardless.Api.Core.Configuration;
using GoCardless.Api.Core.Http;
using System;
using System.Threading.Tasks;

namespace GoCardless.Api.MandateImports
{
    public class MandateImportsClient : ApiClient, IMandateImportsClient
    {
        public MandateImportsClient(ClientConfiguration configuration) : base(configuration) { }

        public Task<Response<MandateImport>> CancelAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return PostAsync<Response<MandateImport>>(
                $"mandate_imports/{id}/actions/cancel"
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

        public Task<Response<MandateImport>> ForIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return GetAsync<Response<MandateImport>>($"mandate_imports/{id}");
        }

        public Task<Response<MandateImport>> SubmitAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value is null, empty or whitespace.", nameof(id));
            }

            return PostAsync<Response<MandateImport>>(
                $"mandate_imports/{id}/actions/submit"
            );
        }
    }
}