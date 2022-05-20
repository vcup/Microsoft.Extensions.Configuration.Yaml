namespace Microsoft.Extensions.Configuration.Yaml;

public class YamlStreamConfigurationSource : StreamConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new YamlStreamConfigurationProvider(this);
    }
}