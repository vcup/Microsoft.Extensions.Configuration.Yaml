namespace Microsoft.Extensions.Configuration.Yaml;

public class YamlStreamConfigurationProvider : StreamConfigurationProvider
{
    public YamlStreamConfigurationProvider(StreamConfigurationSource source) : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        Data = YamlConfigurationFileParser.ParseStream(stream);
    }
}