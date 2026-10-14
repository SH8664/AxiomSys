using AxiomSysPortal.Models;

namespace AxiomSysPortal.Services
{
    public class SaDeliveryTermService
    {
        private readonly HttpClient _http;
        public SaDeliveryTermService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseModel<List<SaDeliveryTermModel>>> GetAllAsync() =>
            await ReadAsync<List<SaDeliveryTermModel>>(await _http.GetAsync("api/Sales/SaDeliveryTerm/GetAllSaDeliveryTerms"));

        public async Task<ResponseModel<SaDeliveryTermModel>> GetByCodeAsync(short code) =>
            await ReadAsync<SaDeliveryTermModel>(await _http.GetAsync($"api/Sales/SaDeliveryTerm/GetSaDeliveryTermByCode/{code}"));

        public async Task<ResponseModel<SaDeliveryTermModel>> CreateAsync(SaDeliveryTermModel m) =>
            await ReadAsync<SaDeliveryTermModel>(await _http.PostAsJsonAsync("api/Sales/SaDeliveryTerm/AddSaDeliveryTerm", m));

        public async Task<ResponseModel<SaDeliveryTermModel>> UpdateAsync(SaDeliveryTermModel m) =>
            await ReadAsync<SaDeliveryTermModel>(await _http.PostAsJsonAsync($"api/Sales/SaDeliveryTerm/UpdateSaDeliveryTerm", m));

        public async Task<ResponseModel<object>> DeleteAsync(short code) =>
            await ReadAsync<object>(await _http.DeleteAsync($"api/Sales/SaDeliveryTerm/DeleteSaDeliveryTerm/{code}"));


        private static async Task<ResponseModel<T>> ReadAsync<T>(HttpResponseMessage res)
        {
            try
            {
                return await res.Content.ReadFromJsonAsync<ResponseModel<T>>()
                       ?? new ResponseModel<T> { messageEn = "Empty response" };
            }
            catch
            {
                return new ResponseModel<T>
                {
                    messageEn = $"Request failed ({(int)res.StatusCode})"

                };
            }
        }

    }
}
