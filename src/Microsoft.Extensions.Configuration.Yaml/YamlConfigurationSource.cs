﻿namespace Microsoft.Extensions.Configuration.Yaml;

public class YamlConfigurationSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        EnsureDefaults(builder);
        return new YamlConfigureProvider(this);
    }
}