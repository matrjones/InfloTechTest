using System;
using UserManagement.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;
public class Log
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string UserName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public LogType Type { get; set; } = default!;
    public DateTime Time { get; set; } = default!;
    public long? UserId { get; set; }
    public virtual User? User { get; set; }
}
