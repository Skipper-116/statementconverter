using System.IO;

namespace StatementConverter.Services
{
    public class FileCreater
    {
        public void WriteContent(string text, string path)
        {
            File.WriteAllText(path, text);
        }
    }
}
