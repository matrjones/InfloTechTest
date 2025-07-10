using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class LogServiceTests
{
    [Fact]
    public void GetAll_WhenContextReturnsEntities_MustReturnSameEntities()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var logs = SetupLogs();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.GetAll();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(logs);
    }

    [Fact]
    public void GetById_WhenContextReturnsEntity_MustReturnSameEntity()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var service = CreateService();
        var logs = SetupLogs();
        var expectedLog = logs.First();

        // Act: Invokes the method under test with the arranged parameters.
        var result = service.GetById(expectedLog.Id);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Should().BeEquivalentTo(expectedLog);
    }

    private IQueryable<Log> SetupLogs(string forename = "Johnny", string surname = "User", string email = "juser@example.com", bool isActive = true)
    {
        var logs = new[]
        {
            new Log
            {
               Id = 0,
               UserName = "Mat Jones",
               Description = "Mat Jones has been created.",
               Type = Enums.LogType.Created,
               Time = DateTime.Now,
            },new Log
            {
               Id = 1,
               UserName = "Mat Jones",
               Description = "Mat Jones has been viewed.",
               Type = Enums.LogType.Viewed,
               Time = DateTime.Now,
            },new Log
            {
               Id = 2,
               UserName = "Mat Jones",
               Description = "Mat Jones has been modified.",
               Type = Enums.LogType.Modified,
               Time = DateTime.Now,
            },new Log
            {
               Id = 3,
               UserName = "Mat Jones",
               Description = "Mat Jones has been deleted.",
               Type = Enums.LogType.Deleted,
               Time = DateTime.Now,
            }
        }.AsQueryable();

        _dataContext
            .Setup(s => s.GetAll<Log>())
            .Returns(logs);

        return logs;
    }

    private readonly Mock<IDataContext> _dataContext = new();
    private LogService CreateService() => new(_dataContext.Object);
}
