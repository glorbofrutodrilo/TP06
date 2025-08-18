public class Tarea{
    public string Titulo {get; set;}
    public string Descripcion {get; set;}
    public DateTime FechaDeEntrega {get; set;}
    public int ID {get; set;}
    public string Prioridad {get; set;}
    public bool CreeLaTarea {get; set;}

    public Tarea() { }

    public Tarea(string titulo, string descripcion, DateTime fechaDeEntrega, string prioridad)
    {
        Titulo = titulo;
        Descripcion = descripcion;
        FechaDeEntrega = fechaDeEntrega;
        Prioridad = prioridad;
        CreeLaTarea = false;
    }
}