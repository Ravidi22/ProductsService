using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using ShoppingDB.Service.Dtos;
using ShoppingDB.Service.Entities;
using ShoppingDB.Service.Repositories;

namespace ShoppingDB.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository productRepository = new();

    [HttpGet]
    public async Task<IEnumerable<ProductDto>> Get()
    {
        var products = (await productRepository.GetAllAsync()).Select(product => product.AsDto());
        return products;
    }

    [HttpGet("id")]
    public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetAsync(id);

        if (product == null) return NotFound();

        return product.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostAsync(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            Description = createProductDto.Description,
            Quantity = createProductDto.Quantity,
            Image = createProductDto.Image,
        };

        await productRepository.CreateAsync(product);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDto>> PutAsync(Guid id, UpdateProductDto updateProductDto)
    {
        var existingEntity = await productRepository.GetAsync(id);

        if (existingEntity == null) return NotFound();

        existingEntity.Name = updateProductDto.Name;
        existingEntity.Description = updateProductDto.Description;

        existingEntity.Price = updateProductDto.Price;
        existingEntity.Quantity = updateProductDto.Quantity;

        await productRepository.UpdateAsync(existingEntity);

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult<ProductDto>> DeleteAsync(Guid id, UpdateProductDto updateProductDto)
    {
        var product = await productRepository.GetAsync(id);

        if (product == null) return NotFound();

        await productRepository.DeleteAsync(product.Id);

        return NoContent();
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> PostBulkAsync([FromBody] List<CreateProductDto> createProductDtos)
    {
        var products = new List<Product>();

        foreach (var dto in createProductDtos)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Quantity = dto.Quantity,
                Image = dto.Image,
                Category = dto.Category
            };

            products.Add(product);
        }

        await productRepository.CreateBulkAsync(products);

        return CreatedAtAction(nameof(Get), new { }, products);
    }
    
}


