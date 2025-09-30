using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
  private readonly IProductService _productsService;
  private readonly ICategoryService _categoryService;

  public ProductsController(IProductService productService, ICategoryService categoryService)
  {
    _productsService = productService;
    _categoryService = categoryService;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    var products = await _productsService.GetProducts();
    return View(products);
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Create(ProductDTO productDto)
  {
    if (ModelState.IsValid)
    {
      await _productsService.Add(productDto);
      return RedirectToAction(nameof(Index));
    }
    return View(productDto);
  }

  [HttpGet]
  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null) return NotFound();
    var product = await _productsService.GetById(id);

    if (product == null) return NotFound();

    ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name", product.CategoryId);

    return View(product);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(ProductDTO productDto)
  {
    if (ModelState.IsValid)
    {
      try
      {
        await _productsService.Update(productDto);
        return RedirectToAction(nameof(Index));
      }
      catch
      {
        return View(productDto);
      }
    }
    return View(productDto);
  }

  [HttpGet]
  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null) return NotFound();

    var product = await _productsService.GetById(id);
    if (product == null) return NotFound();

    return View(product);
  }

  [HttpPost, ActionName("Delete")]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
    await _productsService.Remove(id);
    return RedirectToAction(nameof(Index));
  }

  [HttpGet]
  public async Task<IActionResult> Details(int? id)
  {
    if (id == null) return NotFound();

    var product = await _productsService.GetById(id);
    if (product == null) return NotFound();

    return View(product);
  }
}
