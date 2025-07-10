using System;
using System.Linq;
using UserManagement.Data.Enums;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;
using UserManagement.WebMS.Controllers;

namespace LogManagement.Data.Tests;

public class LogControllerTests
{
    [Fact]
    public void List_WhenServiceReturnsLogs_ModelMustContainLogs()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var Logs = SetupLogs();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.List();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<LogListViewModel>()
            .Which.Items.Should().BeEquivalentTo(Logs, options => options
                .ExcludingMissingMembers());
    }

    [Fact]
    public void List_WhenServiceReturnsLog_ModelMustContainLog()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var logs = SetupLogs();
        var expectedLog = logs.First();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.ViewLog(logs.First().Id);

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model
            .Should().BeOfType<Log>()
            .Which.Should().BeEquivalentTo(expectedLog, options => options
                .ExcludingMissingMembers());
    }

    [Fact]
    public void List_WhenServiceReturnsCreatedLogs_OnlyCreatedLogsReturned()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var Logs = SetupLogs();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.FilterCreated();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model.Should().BeOfType<LogListViewModel>().Which.Items.Should().AllSatisfy(x => x.Type.Should().Be(LogType.Created));
    }

    [Fact]
    public void List_WhenServiceReturnsViewedLogs_OnlyViewedLogsReturned()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var Logs = SetupLogs();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.FilterModified();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model.Should().BeOfType<LogListViewModel>().Which.Items.Should().AllSatisfy(x => x.Type.Should().Be(LogType.Modified));
    }

    [Fact]
    public void List_WhenServiceReturnsDeletedLogs_OnlyDeletedLogsReturned()
    {
        // Arrange: Initializes objects and sets the value of the data that is passed to the method under test.
        var controller = CreateController();
        var Logs = SetupLogs();

        // Act: Invokes the method under test with the arranged parameters.
        var result = controller.FilterDeleted();

        // Assert: Verifies that the action of the method under test behaves as expected.
        result.Model.Should().BeOfType<LogListViewModel>().Which.Items.Should().AllSatisfy(x => x.Type.Should().Be(LogType.Deleted));
    }

    private Log[] SetupLogs()
    {
        var logs = new Log[]
        {
            new Log {
                Id = 0,
                UserName = "Mat Jones",
                Type = LogType.Created,
                Description = "Mat Jones has been created.",
                Time = DateTime.Now,
            },
            new Log {
                Id = 1,
                UserName = "Mat Jones",
                Type = LogType.Viewed,
                Description = "Mat Jones has been viewed.",
                Time = DateTime.Now,
            },
            new Log {
                Id = 2,
                UserName = "Mat Jones",
                Type = LogType.Modified,
                Description = "Mat Jones has been modified.",
                Time = DateTime.Now,
            },
            new Log {
                Id = 3,
                UserName = "Mat Jones",
                Type = LogType.Deleted,
                Description = "Mat Jones has been deleted.",
                Time = DateTime.Now,
            }
        };
        _LogService
            .Setup(s => s.GetAll())
            .Returns(logs);

        _LogService
            .Setup(s => s.GetById(It.IsAny<long>()))
            .Returns<long>(id => logs.First(l => l.Id == id));

        return logs;
    }

    private readonly Mock<ILogService> _LogService = new();
    private LogsController CreateController() => new(_LogService.Object);
}
