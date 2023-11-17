using System;
using System.Reflection;

namespace AuthoritySignClient.Utils
{
    public static class ResourcesUtil
    {
        public static byte[] GetEmbeddedResourceAsBytes(Assembly assembly, string name)
        {
            if (assembly == null)
                throw new Exception("Не указана сборка.");

            using (var stream = 
                assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{name}") as System.IO.UnmanagedMemoryStream)
            {
                if (stream == null)
                    throw new Exception("Не удалось прочитать ресурс");

                byte[] content = new byte[stream.Length];
                stream.Read(content, 0, content.Length);
                return content;
            }
        }
    }
}
