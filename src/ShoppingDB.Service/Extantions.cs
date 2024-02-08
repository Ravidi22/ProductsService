using ShoppingDB.Service.Dtos;
using ShoppingDB.Service.Entities;

namespace ShoppingDB.Service
{
    public static class Extantions
    {
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto(product.Id, product.Name, product.Price, product.Description, product.Category, product.Quantity, product.Image);
        }
    }
}
