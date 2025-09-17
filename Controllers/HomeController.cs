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

    public IActionResult ConfigurarJuego(){

        //los viewbags
        
        return View("ConfigurarJuego");
    }
    public IActionResult Comenzar(string Username, int Dificultad, int Categoria){
        Juego.CargarPartida(Username, Dificultad, Categoria);
        return RedirectToAction("Jugar");
    }
    public IActionResult Jugar(){
        
    }
}
