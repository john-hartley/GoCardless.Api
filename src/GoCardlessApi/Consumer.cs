using GoCardlessApi.Core;
using GoCardlessApi.Creditors;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoCardlessApi
{
    public class Consumer
    {
        public async Task DoStuff()
        {
            var client = new CreditorsClient(ClientConfiguration.ForSandbox(""));

            var request = new AllCreditorsRequest
            {
                Limit = 5
            };

            var creditors = await client.AllAfterAsync(request);
        }

        private static async Task<IEnumerable<Creditor>> Before(CreditorsClient client)
        {
            var request = new AllCreditorsRequest
            {
                Limit = 5
            };

            var response = await client.AllAsync(request);

            var results = new List<Creditor>(response.Creditors);
            while (response.Meta.Cursors.Before != null)
            {
                request.Before = response.Meta.Cursors.Before;

                response = await client.AllAsync(request);
                results.AddRange(response.Creditors);
            }

            return results;
        }

        private static async Task<IEnumerable<Creditor>> After(CreditorsClient client)
        {
            var request = new AllCreditorsRequest
            {
                Limit = 5
            };

            var response = await client.AllAsync(request);

            var results = new List<Creditor>(response.Creditors ?? Enumerable.Empty<Creditor>());
            while (response.Meta.Cursors.After != null)
            {
                request.After = response.Meta.Cursors.After;

                response = await client.AllAsync(request);
                results.AddRange(response.Creditors ?? Enumerable.Empty<Creditor>());
            }

            return results;
        }
    }

    public class ResponsePager
    {
        private readonly CreditorsClient _CreditorsClient;

        public ResponsePager(
            CreditorsClient CreditorsClient,
            AllCreditorsRequest request, 
            AllCreditorsResponse response)
        {
            _CreditorsClient = CreditorsClient;
            Request = request;
            Response = response;
        }

        public AllCreditorsRequest Request { get; private set; }
        public AllCreditorsResponse Response { get; private set; }

        public async Task<IEnumerable<Creditor>> PagesBefore()
        {
            Response = await _CreditorsClient.AllAsync(Request);

            var results = new List<Creditor>(Response.Creditors ?? Enumerable.Empty<Creditor>());
            while (Response.Meta.Cursors.Before != null)
            {
                Request.Before = Response.Meta.Cursors.Before;

                Response = await _CreditorsClient.AllAsync(Request);
                results.AddRange(Response.Creditors ?? Enumerable.Empty<Creditor>());
            }

            return results;
        }

        public async Task<IEnumerable<Creditor>> PagesAfter()
        {
            Response = await _CreditorsClient.AllAsync(Request);

            var results = new List<Creditor>(Response.Creditors ?? Enumerable.Empty<Creditor>());
            while (Response.Meta.Cursors.After != null)
            {
                Request.After = Response.Meta.Cursors.After;

                Response = await _CreditorsClient.AllAsync(Request);
                results.AddRange(Response.Creditors ?? Enumerable.Empty<Creditor>());
            }

            return results;
        }
    }
}