using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;

namespace UserManagement.WebMS.Controllers;

[Route("Logs")]
public class LogsController : Controller
{
    private readonly ILogService _logService;
    public LogsController(ILogService logService) => _logService = logService;

    [HttpGet]
    [Route("List")]
    public ViewResult List()
    {
        var items = _logService.GetAll().Select(p => new LogListItemViewModel
        {
            Id = p.Id,
            UserName = p.UserName,
            Description = p.Description,
            Type = p.Type,
            Time = p.Time,
            UserId = p.UserId,
            User = p.User
        });

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet]
    [Route("view/{id:long}")]
    public ViewResult ViewLog(long id)
    {
        var log = _logService.GetById(id);
        return View(log);
    }

    [HttpGet]
    [Route("FilterCreated")]
    public ViewResult FilterCreated()
    {
        var items = _logService.GetAll().Where(x => x.Type == Data.Enums.LogType.Created).Select(p => new LogListItemViewModel
        {
            Id = p.Id,
            UserName = p.UserName,
            Description = p.Description,
            Type = p.Type,
            Time = p.Time,
            UserId = p.UserId,
            User = p.User
        });

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };

        return View("List", model);
    }

    [HttpGet]
    [Route("FilterViewed")]
    public ViewResult FilterViewed()
    {
        var items = _logService.GetAll().Where(x => x.Type == Data.Enums.LogType.Viewed).Select(p => new LogListItemViewModel
        {
            Id = p.Id,
            UserName = p.UserName,
            Description = p.Description,
            Type = p.Type,
            Time = p.Time,
            UserId = p.UserId,
            User = p.User
        });

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };

        return View("List", model);
    }

    [HttpGet]
    [Route("FilterModified")]
    public ViewResult FilterModified()
    {
        var items = _logService.GetAll().Where(x => x.Type == Data.Enums.LogType.Modified).Select(p => new LogListItemViewModel
        {
            Id = p.Id,
            UserName = p.UserName,
            Description = p.Description,
            Type = p.Type,
            Time = p.Time,
            UserId = p.UserId,
            User = p.User
        });

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };

        return View("List", model);
    }

    [HttpGet]
    [Route("FilterDeleted")]
    public ViewResult FilterDeleted()
    {
        var items = _logService.GetAll().Where(x => x.Type == Data.Enums.LogType.Deleted).Select(p => new LogListItemViewModel
        {
            Id = p.Id,
            UserName = p.UserName,
            Description = p.Description,
            Type = p.Type,
            Time = p.Time,
            UserId = p.UserId,
            User = p.User
        });

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };

        return View("List", model);
    }
}
