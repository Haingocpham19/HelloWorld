using Extension.Domain.Entities;
using Extension.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using PCS.Extension.Services;
using PCS.Extension.Services.interfaces;
using System;

namespace PCS.Extension.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseCustomController<Products>
    {
        private readonly IProductService _productService;
        public ProductController(IBaseService<Products> baseService, IProductService productService) : base(baseService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductsByIdClient/{id}")]
        public IActionResult GetProductsByIdClient(Guid id)
        {
            var result = _productService.GetProductsByIdClient(id);
            return Ok(result);
        }
    }
}
