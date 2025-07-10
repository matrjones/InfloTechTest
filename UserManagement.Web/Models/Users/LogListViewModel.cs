using System;
using UserManagement.Data.Enums;
using UserManagement.Models;

namespace UserManagement.Web.Models.Logs;

public class LogListViewModel
{
    public List<LogListItemViewModel> Items { get; set; } = new();
}

public class LogListItemViewModel
{
    public long Id { get; set; }
    public string? UserName { get; set; }
    public string? Description { get; set; }
    public LogType Type { get; set; }
    public DateTime Time { get; set; }
    public long? UserId { get; set; }
    public virtual User? User { get; set; }
}
