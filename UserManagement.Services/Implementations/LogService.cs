﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;
public class LogService : ILogService
{
    private readonly IDataContext _dataAccess;
    public LogService(IDataContext dataAccess) => _dataAccess = dataAccess;
    public IEnumerable<Log> GetAll() => _dataAccess.GetAll<Log>().Include(x => x.User).OrderByDescending(l => l.Id);
    public Log GetById(long id)
    {
        return _dataAccess.GetAll<Log>().Include(x => x.User).First(x => x.Id == id);
    }
}
