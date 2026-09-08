using E_commerce.Infa;
using E_commerce.Repository;
using E_commerce.Repository.Interface;
using E_commerce.Service;
using E_commerce.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Conexão Banco
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// HttpClients e Serviços Externos
builder.Services.AddHttpClient<IValidadorCpfAsyncService, BrasilApiValidadorCpfAsyncService>();

// ADICIONADO: Registro do validador de CNPJ (assumindo a classe BrasilApiValidadorCnpjAsyncService)
builder.Services.AddHttpClient<IValidadorCnpjAsyncService, BrasilApiValidadorCnpjAsyncService>();

// Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

// ADICIONADO: Repositório de Itens do Pedido
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();

// Serviços de Domínio
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVendedorService, VendedorService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();