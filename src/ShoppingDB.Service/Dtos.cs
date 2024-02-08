using ShoppingDB.Service.Entities;

namespace ShoppingDB.Service.Dtos
{

    public record ProductDto(Guid Id, string Name, decimal Price, string Description, string Category, int Quantity, string Image);

    public record CreateProductDto(Guid Id, string Name, decimal Price, string Description, int Quantity, string Category, string Image);
    public record UpdateProductDto(Guid Id, string Name, decimal Price, string Description, int Quantity, string Category, string Image);

}
