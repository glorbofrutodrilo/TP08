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
        
        ViewBag.Categorias = Juego.ObtenerCategorias();
        ViewBag.Dificultades = Juego.ObtenerDificultades();
        
        return View("ConfigurarJuego");
    }
    public IActionResult Comenzar(string Username, int Dificultad, int Categoria){
        Juego.CargarPartida(Username, Dificultad, Categoria);
        HttpContext.Session.SetString("Username", Username);
        
        return RedirectToAction("Jugar");
    }
    public IActionResult Jugar(){
        Preguntas pregunta = Juego.ObtenerProximaPregunta();
        
        if (pregunta == null)
        {
            return RedirectToAction("Fin");
        }
        
        List<Respuestas> respuestas = Juego.ObtenerProximasRespuestas(pregunta.IDPregunta);
        respuestas = respuestas.OrderBy(_ => Random.Shared.Next()).ToList();
        Juego.ListaRespuestas = respuestas;
        
        ViewBag.Username = Juego.username;
        ViewBag.PuntajeActual = Juego.PuntajeActual;
        ViewBag.Pregunta = pregunta;
        ViewBag.Respuestas = respuestas;
        ViewBag.NumeroPregunta = Juego.ContadorNroPreguntaActual;
        ViewBag.TotalPreguntas = Juego.ListaPreguntas?.Count ?? 0;
        
        return View("Juego");
    }
    
    public IActionResult Fin()
    {
        ViewBag.Username = Juego.username;
        ViewBag.PuntajeFinal = Juego.PuntajeActual;
        
        return View("Fin");
    }
    
    public IActionResult Integrantes()
    {
        return View("Integrantes");
    }
    
    

    [HttpPost] 
    public IActionResult VerificarRespuesta(int IDPregunta, int IDRespuesta){
        bool EsCorrecta = Juego.VerificarRespuesta(IDRespuesta);
        return  Json(new { esCorrecta = EsCorrecta });
    }
}
