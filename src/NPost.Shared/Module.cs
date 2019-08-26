using System.Reflection;

namespace NPost.Shared
{
    public class Module
    {
        public Assembly Assembly { get; }
        public string BaseNamespace { get; }

        public Module(Assembly assembly, string baseNamespace)
        {
            Assembly = assembly;
            BaseNamespace = baseNamespace;
        }
    }
}