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
  public class CartController : Controller
  {
    private readonly ApplicationDbContext _context;

    public CartController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var cartItems = await _context.CartItems.Include(c => c.Product).ToListAsync();
      var viewModels = cartItems.Select(item => new CartItemViewModel
      {
        ProductId = item.ProductId,
        ProductName = item.Product?.Name ?? "Unknown Product",
        Quantity = item.Quantity,
        Price = item.Product?.BasePrice ?? 0,
        TotalPrice = item.Quantity * (item.Product?.BasePrice ?? 0)
      }).ToList();

      return View(viewModels);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId, int quantity = 1)
    {
      if (quantity <= 0)
      {
        return BadRequest("Invalid quantity.");
      }

      // Check product exist
      var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
      if (product == null)
      {
        return BadRequest("Product not found.");
      }

      // Check if the product is already in cart
      // Note: normally will filter by user id or session id here
      var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.ProductId == productId);

      if (existingCartItem != null)
      {
        // if already in cart, increase quantity
        existingCartItem.Quantity += quantity;
      }
      else
      {
        // if not in cart, add to cart
        var newCartItem = new CartItem
        {
          ProductId = productId,
          Quantity = quantity,
          // Note: need a cartid 
        };
        _context.CartItems.Add(newCartItem);
      }

      // save to database
      await _context.SaveChangesAsync();

      // back to shop page
      return RedirectToAction("Index", "Product");
    }
  }
}
