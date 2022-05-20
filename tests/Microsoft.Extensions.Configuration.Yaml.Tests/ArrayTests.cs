using Microsoft.Extensions.Configuration.Yaml.Tests.Common;
using NUnit.Framework;

namespace Microsoft.Extensions.Configuration.Yaml.Tests;

public class ArrayTests
{
    [Test]
    public void YamlConfigurationFileParser_Parse()
    {
        const string yamlDoc = "ip:\n" +
                      "  - 1.1.1.1\n" +
                      "  - 1.2.3.4\n" +
                      "  - 8.8.8.8\n";
        using var provider = new YamlConfigureProvider(new YamlConfigurationSource());

        provider.Load(yamlDoc.ToStream());

        provider.TryGet("ip:0", out var value);
        Assert.AreEqual("1.1.1.1", value);
        provider.TryGet("ip:1", out value);
        Assert.AreEqual("1.2.3.4", value);
        provider.TryGet("ip:2", out value);
        Assert.AreEqual("8.8.8.8", value);
    }
}