using GoCardlessApi.Http;
using System.Threading.Tasks;

namespace GoCardlessApi.Payments
{
    public interface IPaymentsClient : IPageable<GetPaymentsOptions, Payment>
    {
        Task<Response<Payment>> CancelAsync(CancelPaymentOptions options);
        Task<Response<Payment>> CreateAsync(CreatePaymentOptions options);
        Task<Response<Payment>> ForIdAsync(string id);
        Task<PagedResponse<Payment>> GetPageAsync();
        Task<PagedResponse<Payment>> GetPageAsync(GetPaymentsOptions options);
        Task<Response<Payment>> RetryAsync(RetryPaymentOptions options);
        Task<Response<Payment>> UpdateAsync(UpdatePaymentOptions options);
    }
}