﻿using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;
public interface ILogService
{
    IEnumerable<Log> GetAll();
    Log GetById(long id);

}
