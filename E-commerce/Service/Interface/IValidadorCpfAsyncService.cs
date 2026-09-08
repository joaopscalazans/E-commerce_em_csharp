namespace E_commerce.Service.Interface;

public interface IValidadorCpfAsyncService
{
    
    Task<bool> ValidarCpfAsync(string cpf);
    
}