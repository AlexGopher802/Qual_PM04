using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasDeliveryApi.Models;
using GasDeliveryApi.Models.Views;

namespace GasDeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private GasDeliveryDBContext _context;

        /// <summary>
        /// Инициализация контекста данных
        /// </summary>
        /// <param name="context">контекст данных</param>
        public ProductController(GasDeliveryDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult AddProduct(Product product)
        {
            try
            {
                Product newProduct = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                };
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
