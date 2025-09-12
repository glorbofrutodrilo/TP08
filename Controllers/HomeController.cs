using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP08.Models;

namespace TP08.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
