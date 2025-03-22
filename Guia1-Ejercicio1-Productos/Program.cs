var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Crear una lista para almacenar objetos tipo Product (Producto)
var products = new List<Product>();

//Configurar una ruta GET para obtener todos los productos
app.MapGet("/products", () =>
{
return products; //Retorna o devuelve la lista de los productos
});

//Configura una ruta HTTP GET para obtener un producto especifico por si ID
app.MapGet("/products/{id}", (int id) =>
{

//Busca un producto en la lista con el id especificado
var product = products.FirstOrDefault(p => p.Id == id);
return product; //Devuelve el producto encontrado o null si no se encuentra
});

//Configura una ruta HTTP Post para agregar un nuevo producto ala lista 
app.MapPost("/products", (Product product) =>
{
products.Add(product); //Añade un nuevo producto ala lista
return Results.Ok(); //Devuelve una respuesta HTTP 200 OK  
});

//Configurar una ruta HTTP PUT para poder actualizar un producto segun su ID de la lista creada
app.MapPut("/products/{id}", (int id, Product product) =>
{
//Busca un producto en la lista con el id que se le proporcionara
var existingProduct = products.FirstOrDefault(p => p.Id == id);
if (existingProduct != null)
{
// Actualiza los datos del producto existente con los datos proporcionados
existingProduct.Name = product.Name;
existingProduct.Quantity = product.Quantity;
return Results.Ok(); //Esto devuelve una respuesta HTTP 200 OK
}
else
{
return Results.NotFound(); //Devuelve una respuesta HTTP 404 Not Fount si el cliente no existe en la lista
}
});

//Confgurar una ruta DELETE para eliminar un producto de la lista segun su ID
app.MapDelete("/products/{id}", (int id) =>
{
//Busca un producto en la lista por su ID
var existingProduct = products.FirstOrDefault(p => p.Id == id);
if (existingProduct != null)
{
//Elimina el producto segun el id encontrado
products.Remove(existingProduct);
return Results.Ok(); //Devuelve una respuesta HTTP 200 OK
}

else
{
return Results.NotFound(); //Retorna el error 404 Not Found si el cliente no existe
}
});

//Ejecutar la aplicacion
app.Run();

//Definicion de la clase Client que representa la estructura de un cliente o toda su informacion
internal class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
}
