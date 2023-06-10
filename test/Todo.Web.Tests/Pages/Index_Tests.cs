using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Todo.Pages;

public class Index_Tests : TodoWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
