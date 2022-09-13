using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Reflection;

namespace OrchardCoreContrib.Tests.Common;

internal class EmbeddedResourceReader
{
    public EmbeddedResourceReader() : this(typeof(EmbeddedResourceReader).Assembly)
    {

    }
    
    public EmbeddedResourceReader(Assembly assembly)
    {
        FileProvider = new EmbeddedFileProvider(assembly);
    }

    public EmbeddedFileProvider FileProvider { init; get; }

    public Stream Read(string resource) => FileProvider.GetFileInfo(resource).CreateReadStream();
}
