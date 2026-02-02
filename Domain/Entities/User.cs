namespace Auth.Domain.Entities;

public class User
{
    private readonly List<UserAddress> _addresses = new();

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }

    public IReadOnlyCollection<UserAddress> Addresses => _addresses.AsReadOnly();

    private User() { } 

    public User(
        string email,
        string passwordHash,
        string firstName,
        string lastName)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
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
