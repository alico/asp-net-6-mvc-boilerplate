using NUnit.Framework;
using NUnit;

namespace BoilerPlate.Test
{
    [TestFixtureSource(nameof(Sites))]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public abstract class TestBase
    {
        protected static readonly TestConfig[] Sites =
        {
             new TestConfig("UK", "localhost:44369"),
        };

        protected TestConfig Config;

        protected TestBase(TestConfig config)
        {
            Config = (TestConfig)config.Clone();
        }
    }
}