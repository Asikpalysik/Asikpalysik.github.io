using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Pages
{
    public class IndexModel(JsonFileProductService productService) : PageModel
    {
        private readonly JsonFileProductService _productService = productService;

        public IEnumerable<ProjectModel> Products { get; private set; } = Enumerable.Empty<ProjectModel>();

        public void OnGet()
        {
            Products = _productService.GetProducts();
        }
    }
}
