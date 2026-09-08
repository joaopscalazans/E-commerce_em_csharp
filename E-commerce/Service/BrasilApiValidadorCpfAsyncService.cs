using System.Text.Json;
using E_commerce.Service.Interface;

namespace E_commerce.Service;

public class BrasilApiValidadorCpfAsyncService : IValidadorCpfAsyncService
{


    private readonly HttpClient _httpClient;

    public BrasilApiValidadorCpfAsyncService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ValidarCpfAsync(string cpf)
    {
        var cpfLimpo = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpfLimpo.Length != 11)
            return false;

        try
        {
            var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cpf/v1/{cpfLimpo}");
            string jsonString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonString);


            if (doc.RootElement.TryGetProperty("isValid", out var isValidProp))
            {
                bool isValid = isValidProp.GetBoolean();
                return isValid;
            }
            return true;
        }
        catch (HttpRequestException)
        {
            return false;
        }
    }

}