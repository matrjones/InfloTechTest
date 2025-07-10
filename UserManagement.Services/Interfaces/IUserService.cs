using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService
{
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> GetAll();

    User GetById(long id);
    User Create(User user);
    User Update(User user);
    void Delete(User user);
}
