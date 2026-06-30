using Catalog.API.DTOs;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductRepository repository) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await repository.GetAllAsync();
        var response = products.Select(p => new ProductResponse(
            p.Id, p.Name, p.Description, p.Price,
            p.ImageFile, p.Category, p.Color, p.Collection, p.AvailableSizes));
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product is null) return NotFound();

        return Ok(new ProductResponse(
            product.Id, product.Name, product.Description, product.Price,
            product.ImageFile, product.Category, product.Color,
            product.Collection, product.AvailableSizes));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var product = new Product(
            request.Name, request.Description, request.Price,
            request.ImageFile, request.Category, request.Color,
            request.Collection, request.AvailableSizes);

        await repository.AddAsync(product);
        await repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product.Id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product is null) return NotFound();

        await repository.DeleteAsync(product);
        await repository.SaveChangesAsync();
        return NoContent();
    }
}
