using ApropasTaskManager.BLL.DTO;
using ApropasTaskManager.Server;
using ApropasTaskManager.Shared;

namespace ApropasTaskManager.Test;


public class SearchOptionsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var builder = new SearchOptionsBuilder<UserDTO>();

        var result = builder.Or(u => u.Email, "asd", "dsd", "ddd")
                            .Or(u => u.Role, UserRoles.ProjectManager)
                            .BuildExpression();

        var f = result.Compile();

        var trueNeed = f(new UserDTO()
        {
            Email = "asdddd"
        });

        var falseNeed = f(new UserDTO()
        {
            Email = "sdbd",
            Role = UserRoles.Director
        });

        Assert.Multiple(() =>
        {
            Assert.That(trueNeed, Is.EqualTo(true));
            Assert.That(falseNeed, Is.EqualTo(false));
        });
    }
}