using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (!string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Tareas");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Login(string Email, string Password)
    {
        Usuario usuario = BD.LevantarUsuario(Email, Password);
        if (usuario != null)
        {
            string usuarioStr = Objeto.ObjectToString(usuario);
            HttpContext.Session.SetString("integrante", usuarioStr);
            return RedirectToAction("Index", "Tareas");
        }
        else
        {
            ViewBag.Error = "Email o contraseña incorrectos";
            return View("Index");
        }
    }

    [HttpPost]
    public IActionResult Registrarse(string Email, string Username, string Password)
    {
        Usuario usuarioExistente = BD.LevantarUsuarioPorEmail(Email);
        if (usuarioExistente != null)
        {
            ViewBag.Error = "Ya existe un usuario con ese email";
            return View("Index");
        }

        Usuario usuario = new Usuario(Email, Username, Password);
        BD.AgregarUsuario(usuario);
        
        string usuarioStr = Objeto.ObjectToString(usuario);
        HttpContext.Session.SetString("integrante", usuarioStr);
        
        return RedirectToAction("Index", "Tareas");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
