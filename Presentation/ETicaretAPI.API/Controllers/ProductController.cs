using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> Get()
        {
            return Ok(_productReadRepository.GetAll(false));
        }


        [HttpGet("{id}")]
         public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id,false));
        }




        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {

            await _productWriteRepository.AddAsync(new()
            {
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock
            });

            await _productWriteRepository.SaveAsync();

            return Ok((int)HttpStatusCode.Created);
        }


        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product =  await _productReadRepository.GetByIdAsync(model.Id);       //tracking true çünkü güncelleme yapacağız. 
            product.Stock = model.Stock;
            product.Price = model.Price;    //EntityFramework de contexten gelen bir veriyi update etmek için oluşturdugumuz update fonksiyonuna ihtiyacımız yok db den getirdiğimiz veri zaten izleniyor ve update edilecek. Biz save lememiz yeterli .
            product.Name = model.Name;

            _productWriteRepository.SaveAsync();


            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
             await _productWriteRepository.RemoveAsync(id);

            await _productWriteRepository.SaveAsync();

            return Ok();
        }





    }
}
