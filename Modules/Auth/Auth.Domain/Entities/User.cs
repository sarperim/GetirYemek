namespace Auth.Domain.Entities;

public class User
{
    private readonly List<UserAddress> _addresses = new();

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }

    public IReadOnlyCollection<UserAddress> Addresses => _addresses.AsReadOnly();

    private User() { } 

    public User(
        string email,
        string firstName,
        string lastName)
    {
        Id = Guid.NewGuid();
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public void AddAddress(UserAddress address)
    {
        address.AssignToUser(Id);
        _addresses.Add(address);
    }

    public void UpdateRefreshToken(string token, DateTime expiry)
    {
        RefreshToken = token;
        RefreshTokenExpiryTime = expiry;
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    }
}
