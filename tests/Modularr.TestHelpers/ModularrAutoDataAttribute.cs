using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Modularr.TestHelpers;

public class ModularrAutoDataAttribute : AutoDataAttribute
{
    public ModularrAutoDataAttribute()
        : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
    }
}
