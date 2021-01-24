namespace WebApi.Entities
{
  public class User
  {
    public int Id { get; set; }
    // public string FirstName { get; set; } //Optional?
    // public string LastName { get; set; } //Optional?
    public string Username { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
  }
}