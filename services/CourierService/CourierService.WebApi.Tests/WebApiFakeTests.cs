namespace CourierService.WebApi.Tests;

public class WebApiFakeTests
{
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
