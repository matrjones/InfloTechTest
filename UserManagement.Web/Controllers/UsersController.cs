using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    [Route("index")]
    public ViewResult List()
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet]
    [Route("active")]
    public ViewResult Active()
    {
        var items = _userService.FilterByActive(true).Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View("List", model);
    }

    [HttpGet]
    [Route("inactive")]
    public ViewResult Inactive()
    {
        var items = _userService.FilterByActive(false).Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth,
            Email = p.Email,
            IsActive = p.IsActive
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View("List", model);
    }

    [HttpGet]
    [Route("add")]
    public ViewResult AddUser()
    {
        return View();
    }

    [HttpGet]
    [Route("view/{id:long}")]
    public ViewResult ViewUser(long id)
    {
        var user = _userService.GetById(id);
        user.Logs.Add(new Log
        {
            UserName = $"{user.Forename} {user.Surname}",
            Description = $"{user.Forename} {user.Surname} has been viewed.",
            Type = Data.Enums.LogType.Viewed,
            Time = DateTime.UtcNow,
            UserId = user.Id
        });
        _userService.Update(user);
        return View(user);
    }

    [HttpGet]
    [Route("edit/{id:long}")]
    public ViewResult EditUser(long id)
    {
        var user = _userService.GetById(id);
        return View(user);
    }

    [HttpGet]
    [Route("delete/{id:long}")]
    public IActionResult DeleteUser(long id)
    {
        var user = _userService.GetById(id);
        user.Logs.Add(new Log
        {
            UserName = $"{user.Forename} {user.Surname}",
            Description = $"{user.Forename} {user.Surname} has been deleted.",
            Type = Data.Enums.LogType.Deleted,
            Time = DateTime.UtcNow,
            UserId = user.Id
        });
        _userService.Update(user);
        _userService.Delete(user);
        return RedirectToAction("List");
    }

    [HttpPost]
    [Route("AddNewUser")]
    public IActionResult AddNewUser(User user)
    {
        user.Logs.Add(new Log
        {
            UserName = $"{user.Forename} {user.Surname}",
            Description = $"{user.Forename} {user.Surname} has been created.",
            Type = Data.Enums.LogType.Created,
            Time = DateTime.UtcNow,
            UserId = user.Id
        });
        _userService.Create(user);
        return RedirectToAction("List");
    }

    [HttpPost]
    [Route("ModifyUser")]
    public IActionResult ModifyUser(User user)
    {
        user.Logs.Add(new Log
        {
            UserName = $"{user.Forename} {user.Surname}",
            Description = $"{user.Forename} {user.Surname} has been modified.",
            Type = Data.Enums.LogType.Modified,
            Time = DateTime.UtcNow,
            UserId = user.Id
        });
        _userService.Update(user);
        return RedirectToAction("List");
    }
}
