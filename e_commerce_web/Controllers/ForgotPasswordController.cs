using Microsoft.AspNetCore.Mvc;
using e_commerce_web.Models;
using e_commerce_web.Data;

namespace e_commerce_web.Controllers
{
  public class ForgotPasswordController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ForgotPasswordController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Index(ForgotPasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var normalizedEmail = model.Email.Trim().ToLower();
      var user = _context.Users.FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);

      if (user == null)
      {
        ModelState.AddModelError("", "No user found with that email address.");
        return View(model);
      }

      // Here you would typically generate a password reset token and send an email
      // For now, we'll just redirect to a success page
      return RedirectToAction("Success");
    }

    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }
  }
}
