using Microsoft.Data.SqlClient;
using Dapper;

public class BD
{
    private static string _connectionString = @"Server=localhost;Database=TP06;Integrated Security=True;TrustServerCertificate=True;";
    
    public static List<Tarea> LevantarTarea(){
        List<Tarea> tareas = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Tarea";
            tareas = connection.Query<Tarea>(query).ToList();
        }
        return tareas;
    }

    public static Tarea LevantarTareaPorId(int id)
    {
        Tarea tarea = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Tarea WHERE ID = @TId";
            tarea = connection.QueryFirstOrDefault<Tarea>(query, new { TId = id });
        }
        return tarea;
    }

    public static void AgregarTarea(Tarea tarea)
    {
        string query = "INSERT INTO Tarea (Titulo, Descripcion, FechaDeEntrega, Prioridad) VALUES (@TTitulo, @TDescripcion, @TFechaDeEntrega, @TPrioridad)";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { TTitulo = tarea.Titulo, TDescripcion = tarea.Descripcion, TFechaDeEntrega = tarea.FechaDeEntrega, TPrioridad = tarea.Prioridad});
        }
    }

    public static void ModificarTarea(Tarea tarea)
    {
        string query = "UPDATE Tarea SET Titulo = @TTitulo, Descripcion = @TDescripcion, FechaDeEntrega = @TFechaDeEntrega, Prioridad = @TPrioridad WHERE ID = @TId";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { TTitulo = tarea.Titulo, TDescripcion = tarea.Descripcion, TFechaDeEntrega = tarea.FechaDeEntrega, TPrioridad = tarea.Prioridad, TId = tarea.ID });
        }
    }

    public static void EliminarTarea(int id)
    {
        string query = "EXEC EliminarTarea @TId;";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { TId = id });
        }
    }

    public static Usuario LevantarUsuario(string Email, string Password)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Email = @UEmail AND Password = @UPassword";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { UEmail = Email, UPassword = Password });
        }
        return usuario;
    }

    public static Usuario LevantarUsuarioPorEmail(string Email)
    {
        Usuario usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Email = @UEmail";
            usuario = connection.QueryFirstOrDefault<Usuario>(query, new { UEmail = Email });
        }
        return usuario;
    }

    public static void AgregarUsuario(Usuario usuario){
        string query = "INSERT INTO Usuario (Username, Password, Email) VALUES (@UUsername, @UPassword, @UEmail)";
         using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { UUsername = usuario.Username, UPassword = usuario.Password, UEmail=usuario.Email});
        }
    }

    public static void CompartirTarea(int idTarea, int idUsuario)
    {
        string query = "INSERT INTO UsuarioxTarea (IDTarea, IDUsuario) VALUES (@TIdTarea, @TIdUsuario)";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { TIdTarea = idTarea, TIdUsuario = idUsuario });
        }
    }

    public static List<Tarea> LevantarTareasPorUsuario(int idUsuario)
    {
        List<Tarea> tareas = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = @"SELECT t.* FROM Tarea t 
                           INNER JOIN UsuarioxTarea uxt ON t.ID = uxt.IDTarea 
                           WHERE uxt.IDUsuario = @TIdUsuario";
            tareas = connection.Query<Tarea>(query, new { TIdUsuario = idUsuario }).ToList();
        }
        return tareas;
    }
}
