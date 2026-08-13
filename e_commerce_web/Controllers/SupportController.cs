using e_commerce_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web.Controllers
{
    public class SupportController : Controller
    {
        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Here you would typically save the message to a database or send an email.
                // For now, we just show a success message via TempData.
                TempData["SuccessMessage"] = "Thank you for reaching out! Your message has been received, and our support team will get back to you shortly.";
                return RedirectToAction(nameof(ContactUs));
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TermsOfService()
        {
            return View();
        }

        public IActionResult ShippingPolicy()
        {
            return View();
        }

        public IActionResult ReturnPolicy()
        {
            return View();
        }
    }
}
