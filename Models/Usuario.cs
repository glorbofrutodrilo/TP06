public class Usuario{
    public string Username {get; set;}
    public string Password {get; set;}
    public int ID {get; set;}
    public string Email {get; set;}

    public Usuario() { }

    public Usuario(string email, string username, string password){
        Email = email;
        Username = username;
        Password = password;
    }
}