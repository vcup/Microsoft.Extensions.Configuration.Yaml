using System.IO;

namespace Microsoft.Extensions.Configuration.Yaml.Tests.Common;

internal static class TestHelpers
{
    public static Stream ToStream(this string str)
    {
        var memoryStream = new MemoryStream();
        var writer = new StreamWriter(memoryStream);
        writer.Write(str);
        writer.Flush();

        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }
}