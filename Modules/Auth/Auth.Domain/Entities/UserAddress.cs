namespace Auth.Domain.Entities;

public class UserAddress
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string FullAddress { get; private set; }

    private UserAddress() { }
    public UserAddress(
        string title,
        string city,
        string street,
        string fullAddress)
    {
        Id = Guid.NewGuid();
        Title = title;
        City = city;
        Street = street;
        FullAddress = fullAddress;
    }
    internal void AssignToUser(Guid userId)
    {
        UserId = userId;
    }
}
