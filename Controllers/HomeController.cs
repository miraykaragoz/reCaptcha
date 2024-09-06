using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using reCaptcha.Models;
using RestSharp;

namespace reCaptcha.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new FormModel());
    }
    
    [HttpPost]
    public IActionResult Index(FormModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model);
        }

        var captchaToken = Request.Form["g-recaptcha-response"];
            
        if(!VerifyCaptcha(captchaToken))
        {
            ViewBag.CaptchaError = true;
            return View(model);
        }

        return View("Sonuc");
    }
    
    public bool VerifyCaptcha(string captchaToken)
    {
        var client = new RestClient("https://www.google.com/recaptcha");
        var request = new RestRequest("api/siteverify", Method.Post);
        request.AddParameter("secret", "");
        request.AddParameter("response", captchaToken);

        var response = client.Execute<CaptchaResponse>(request);

        if(response.Data.Success && response.Data.Score > 0.6)
        {
            return true;
        }
        return false;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}