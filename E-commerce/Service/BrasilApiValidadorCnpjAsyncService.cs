using E_commerce.Service.Interface;

namespace E_commerce.Service;

public class BrasilApiValidadorCnpjAsyncService: IValidadorCnpjAsyncService
{ 
    private readonly HttpClient _httpClient;

        public BrasilApiValidadorCnpjAsyncService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidarCnpjAsync(string cnpj)
        {
   
            var cnpjLimpo = new string(cnpj.Where(char.IsDigit).ToArray());

            if (cnpjLimpo.Length != 14)
                return false;

            try
            {
    
                var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cnpj/v1/{cnpjLimpo}");

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
