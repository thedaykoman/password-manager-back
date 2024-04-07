using System.Collections.ObjectModel;

namespace PasswordManager.Domain.Entities;

public class User(string username, string password, string email)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = username.Trim();
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;

    public Collection<Credential> Credentials { get; } = [];

    private void AddCredential(Credential credential)
    {
        Credentials.Add(credential);
    }

    private void UpdateUser(string? username, string? passwordHash, string email)
    {
        Username = username ?? Username;
        Password = passwordHash ?? Password;
        Email = email;
    }
}