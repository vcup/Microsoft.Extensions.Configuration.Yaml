using YamlDotNet.RepresentationModel;

namespace Microsoft.Extensions.Configuration.Yaml;

public class YamlConfigureProvider : FileConfigurationProvider
{
    public YamlConfigureProvider(YamlConfigurationSource source)
        : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        Data = YamlConfigurationFileParser.ParseStream(stream);
    }
}