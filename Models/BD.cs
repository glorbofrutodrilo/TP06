using Microsoft.Data.SqlClient;
using Dapper;

public class BD
{
    private static string _connectionString = @"Server=localhost;Database=TP06;Integrated Security=True;TrustServerCertificate=True;";
    
    public List<Tarea> LevantarTarea(){
        List<Tarea> tareas = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Tarea";
            tareas = connection.Query<Tarea>(query).ToList();
        }
        return tareas;
    }
    public void agregarTarea(Tarea tarea)
    {
        string query = "INSERT INTO Tareas (Titulo, Descripcion, FechaDeEntrega, ID, Prioridad) VALUES @pTitulo, @pDescripcion, @pFechaDeEntrega, @pID, @pPrioridad";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { pTitulo = tarea.Titulo, pDescripcion = tarea.Descripcion, pFechaDeEntrega = tarea.FechaDeEntrega, pID = tarea.ID, pPrioridad = tarea.Prioridad});
        }
    }
}
