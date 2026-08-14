using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_commerce_web.Models;
using e_commerce_web.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace e_commerce_web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper method to fetch from DB and map to ViewModel
        private async Task<List<ProductViewModel>> GetProductsFromDB()
        {
            // Fetch products from database
            var allProducts = await _context.Products.Where(p => p.IsActive).ToListAsync();

            // Map database model to our frontend ViewModel
            return allProducts.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description ?? "No description available.",
                Price = p.BasePrice,
                OriginalPrice = p.BasePrice + 20m, // Just as a mockup for the frontend UI
                Category = "Shop", // You will need to join with Categories table to get real name
                ImageUrl = "/images/product_sneakers_1786514012011.png", // Mocking image until you add an ImageUrl column
                StockQuantity = 10, // Mocking stock until you join with ProductVariants table
                Rating = 4.5,
                ReviewCount = 10,
                IsNewArrival = p.CreatedAt > DateTime.UtcNow.AddDays(-30),
                IsBestSeller = false
            }).ToList();
        }

        public async Task<IActionResult> Index(string category = null, string sortOrder = null)
        {
            // Await the database call
            var productsList = await GetProductsFromDB();
            var products = productsList.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
            }

            // Simple sorting
            switch (sortOrder)
            {
                case "price_asc":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "newest":
                    products = products.OrderByDescending(p => p.IsNewArrival).ThenByDescending(p => p.Id);
                    break;
                default:
                    products = products.OrderBy(p => p.Id);
                    break;
            }

            ViewBag.CurrentCategory = category;
            ViewBag.CurrentSort = sortOrder;
            
            // Get unique categories for the sidebar
            ViewBag.Categories = productsList.Select(p => p.Category).Distinct().ToList();

            return View(products.ToList());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var productsList = await GetProductsFromDB();
            var product = productsList.FirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }

            // Get related products for the PDP
            ViewBag.RelatedProducts = productsList
                .Where(p => p.Category == product.Category && p.Id != product.Id)
                .Take(3)
                .ToList();

            return View(product);
        }

        public async Task<IActionResult> Search(string query)
        {
            ViewBag.Query = query;

            if (string.IsNullOrWhiteSpace(query))
            {
                return View(new List<ProductViewModel>());
            }

            var productsList = await GetProductsFromDB();
            var results = productsList
                .Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                            p.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            p.Category.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return View(results);
        }
    }
}
