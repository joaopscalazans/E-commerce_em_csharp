namespace E_commerce.Service.Interface;

public interface IValidadorCnpjAsyncService
{
    Task<bool> ValidarCnpjAsync(string cnpj);
}