using Microsoft.AspNetCore.Mvc;
using TP06.Models;

namespace TP06.Controllers;

public class TareasController : Controller
{
    public IActionResult Index()
    {
        // Verificar si el usuario está logueado
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Usuario usuario = Objeto.StringToObject<Usuario>(usuarioStr);
        List<Tarea> tareas = BD.LevantarTarea();
        
        ViewBag.Usuario = usuario;
        ViewBag.Tareas = tareas;
        
        return View();
    }

    public IActionResult Agregar()
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Agregar(string titulo, string descripcion, DateTime fechaDeEntrega, string prioridad)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Tarea nuevaTarea = new Tarea(titulo, descripcion, fechaDeEntrega, prioridad);
        BD.AgregarTarea(nuevaTarea);
        
        return RedirectToAction("Index");
    }

    public IActionResult Editar(int id)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Tarea tarea = BD.LevantarTareaPorId(id);
        if (tarea == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Tarea = tarea;
        return View();
    }

    [HttpPost]
    public IActionResult Editar(int id, string titulo, string descripcion, DateTime fechaDeEntrega, string prioridad)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Tarea tarea = BD.LevantarTareaPorId(id);
        if (tarea != null)
        {
            tarea.Titulo = titulo;
            tarea.Descripcion = descripcion;
            tarea.FechaDeEntrega = fechaDeEntrega;
            tarea.Prioridad = prioridad;
            
            BD.ModificarTarea(tarea);
        }
        
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        BD.EliminarTarea(id);
        return RedirectToAction("Index");
    }

    public IActionResult Compartir(int id)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Tarea tarea = BD.LevantarTareaPorId(id);
        if (tarea == null)
        {
            return RedirectToAction("Index");
        }

        ViewBag.Tarea = tarea;
        return View();
    }

    [HttpPost]
    public IActionResult Compartir(int idTarea, string emailUsuario)
    {
        string usuarioStr = HttpContext.Session.GetString("integrante");
        if (string.IsNullOrEmpty(usuarioStr))
        {
            return RedirectToAction("Index", "Home");
        }

        Usuario usuarioDestino = BD.LevantarUsuarioPorEmail(emailUsuario);
        if (usuarioDestino != null)
        {
            BD.CompartirTarea(idTarea, usuarioDestino.ID);
        }
        
        return RedirectToAction("Index");
    }
} 