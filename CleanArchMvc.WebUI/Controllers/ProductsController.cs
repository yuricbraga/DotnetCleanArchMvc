using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
  private readonly IProductService _productsService;

  public ProductsController(IProductService productService)
  {
    _productsService = productService;
  }

  // GET: ProductsController
  [HttpGet]
  public async Task<ActionResult> Index()
  {
    var products = await _productsService.GetProducts();
    return View(products);
  }

}
