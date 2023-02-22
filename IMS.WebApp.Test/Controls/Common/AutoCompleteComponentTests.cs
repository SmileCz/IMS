using Bunit;
using IMS.WebApp.Controls.Common;

namespace IMS.WebApp.Test.Controls.Common;

public class AutoCompleteComponentTests : TestContext
{
    [Fact]
    public void AutoCompleteComponentShould()
    {
        var acut = RenderComponent<AutoCompleteComponent>();

        acut.Find("input");
    }
}