using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class CategoriesController : Controller
{
  private readonly ICategoryService _categoryService;

  public CategoriesController(ICategoryService categoryService)
  {
    _categoryService = categoryService;
  }

  // GET: CategoriesController
  [HttpGet]
  public async Task<ActionResult> Index()
  {
    var categories = await _categoryService.GetCategories();
    Console.WriteLine(categories);
    return View(categories);
  }

}
