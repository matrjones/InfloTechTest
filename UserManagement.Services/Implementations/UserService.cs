using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<User> FilterByActive(bool isActive) => _dataAccess.GetAll<User>().Where(x => x.IsActive == isActive);

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public User GetById(long id)
    {
        return _dataAccess.GetAll<User>().Include(x => x.Logs).First(x => x.Id == id);
    }

    public User Create(User user)
    {
        _dataAccess.Create(user);
        return user;
    }

    public User Update(User user)
    {
        _dataAccess.Update(user);
        return user;
    }

    public void Delete(User user)
    {
        _dataAccess.Delete(user);
    }
}
