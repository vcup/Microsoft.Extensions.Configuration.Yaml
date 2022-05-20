using YamlDotNet.RepresentationModel;

namespace Microsoft.Extensions.Configuration.Yaml;

internal sealed class YamlConfigurationFileParser
{
    private readonly Dictionary<string, string?> _result = new();
    private readonly Stack<string> _paths = new();

    public static IDictionary<string, string?> ParseStream(Stream input)
    {
        return new YamlConfigurationFileParser().Parse(input);
    }

    private IDictionary<string, string?> Parse(Stream input)
    {
        var yaml = new YamlStream();
        using var reader = new StreamReader(input);
        yaml.Load(reader);

        foreach (var document in yaml.Documents)
        {
            BuildNodeToResult(document.RootNode);
        }
        
        return _result;
    }

    private void BuildNodeToResult(YamlNode node)
    {
        switch (node.NodeType)
        {
            case YamlNodeType.Mapping:
                foreach (var (key,value) in (YamlMappingNode) node)
                {
                    EnterContext(key.ToString());
                    BuildNodeToResult(value);
                    ExitContext();
                }
                break;
            case YamlNodeType.Sequence:
                var index = 0;
                foreach (var subNode in (YamlSequenceNode)node)
                {
                    EnterContext(index.ToString());
                    BuildNodeToResult(subNode);
                    ExitContext();
                    index++;
                }
                break;
            case YamlNodeType.Scalar:
                _result.Add(_paths.Peek(), node.ToString());
                break;
            default:
            case YamlNodeType.Alias:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void EnterContext(string context)
    {
        _paths.Push(_paths.Count > 0 ? _paths.Peek() + ConfigurationPath.KeyDelimiter + context : context);
    }

    private void ExitContext() => _paths.Pop();
}