using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nexto.Selenium.Fixtures
{
    [CollectionDefinition("Chrome Driver")]
    public class CollectionFixture : ICollectionFixture<OpenFixture>
    {
    }
}
