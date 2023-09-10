using ETicaretAPI.Application.Repositories;
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

        public ProductController(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }


        [HttpGet]
        public async void Get()
        {
           await _productWriteRepository.AddRangeAsync(new()
            {
                new() {Id = Guid.NewGuid() , Name = "Product 1" , Price = 100 , CreatedDate = DateTime.UtcNow, Stock  = 10},
                new() {Id = Guid.NewGuid() , Name = "Product 2" , Price = 200 , CreatedDate = DateTime.UtcNow, Stock  = 20},
                new() {Id = Guid.NewGuid() , Name = "Product 3" , Price = 300 , CreatedDate = DateTime.UtcNow, Stock  = 30},
                new() {Id = Guid.NewGuid() , Name = "Product 4" , Price = 400 , CreatedDate = DateTime.UtcNow, Stock  = 40},
                new() {Id = Guid.NewGuid() , Name = "Product 5" , Price = 500 , CreatedDate = DateTime.UtcNow, Stock  = 50}
                
             
            });
            var count = await _productWriteRepository.SaveAsync();
        }
    }
}
