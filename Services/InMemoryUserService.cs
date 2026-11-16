using System.Collections.Concurrent;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

// Demo storage only: data is lost when the application stops.
// Replace this service with a database-backed implementation for production.
public sealed class InMemoryUserService : IUserService
{
    private readonly ConcurrentDictionary<int, User> _users = new();
    private int _nextId = 0;

    public InMemoryUserService()
    {
        Create(new User { Name = "Aarav Sharma", Email = "aarav@example.com", Age = 22 });
        Create(new User { Name = "Maya Patel", Email = "maya@example.com", Age = 28 });
    }

    public IEnumerable<User> GetAll() =>
        _users.Values.OrderBy(user => user.Id).ToList();

    public User? GetById(int id) =>
        _users.TryGetValue(id, out var user) ? user : null;

    public User Create(User user)
    {
        var created = Copy(user);
        created.Id = Interlocked.Increment(ref _nextId);
        _users[created.Id] = created;
        return Copy(created);
    }

    public bool Update(int id, User user)
    {
        if (!_users.ContainsKey(id))
            return false;

        var updated = Copy(user);
        updated.Id = id;
        _users[id] = updated;
        return true;
    }

    public bool Delete(int id) => _users.TryRemove(id, out _);

    public bool EmailExists(string email, int? exceptUserId = null) =>
        _users.Values.Any(user =>
            user.Id != exceptUserId &&
            string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase));

    private static User Copy(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Age = user.Age
    };
}
