using System.Diagnostics;
using System.Reflection;

namespace CLIDictionaryHelper.Utils;

public static class ResourcesLoader
{
    public static string ReadFile(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames()
                                   .FirstOrDefault(str => str.EndsWith(name, StringComparison.OrdinalIgnoreCase));

        if (resourceName == null)
            throw new FileNotFoundException($"Resource {name} not found.");

        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        Debug.Assert(stream != null, nameof(stream) + " != null");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}