namespace AuthorizationServer.Infrastructure.Tests;

public class InfrastructureFakeTests
{
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    [Trait("Category", "Unit")]
    public void FakeTest()
    {
        // Arrange.
        var value = 1;

        // Act.
        var result = value + 1;

        // Assert.
        result.Should()
              .Be(2);
    }
}
