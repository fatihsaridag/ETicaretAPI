using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }


        [HttpGet]
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            //    new() {Id = Guid.NewGuid() , Name = "Product 1" , Price = 100 , CreatedDate = DateTime.UtcNow, Stock  = 10},
            //    new() {Id = Guid.NewGuid() , Name = "Product 2" , Price = 200 , CreatedDate = DateTime.UtcNow, Stock  = 20},
            //    new() {Id = Guid.NewGuid() , Name = "Product 3" , Price = 300 , CreatedDate = DateTime.UtcNow, Stock  = 30},
            //    new() {Id = Guid.NewGuid() , Name = "Product 4" , Price = 400 , CreatedDate = DateTime.UtcNow, Stock  = 40},
            //    new() {Id = Guid.NewGuid() , Name = "Product 5" , Price = 500 , CreatedDate = DateTime.UtcNow, Stock  = 50}


            //});

           Product p= await _productReadRepository.GetByIdAsync("022e7808-7679-486e-8484-0154c4ed8920",false);
            p.Name = "Mehmet";
            await _productWriteRepository.SaveAsync();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }


    }
}
