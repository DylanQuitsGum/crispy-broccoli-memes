using System;
using System.IO;
using System.IO.Compression;

namespace gsm.IO
{
    public class FileHandler : IFileHandler
    {
        public void CreateFile(string path) => this.CreateFile(path, false);

        public void CreateFile(string path, bool overwrite) => this.CreateFile(path, overwrite, false);

        public void CreateFile(string path, bool overwrite, bool createDirectoryStructure)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException(nameof(path));
            if (File.Exists(path) && !overwrite)
                return;
            string directoryName = Path.GetDirectoryName(path);
            if (string.IsNullOrWhiteSpace(Path.GetFileName(path)))
                throw new ArgumentException("FilePath must be a full path.", "filePath");
            if (!Directory.Exists(directoryName))
            {
                if (!createDirectoryStructure)
                    throw new IOException("FilePath does not exist.");
                this.CreateFolder(directoryName);
            }
            using (FileStream fileStream = File.Create(path))
                fileStream.Close();
        }

        public string CreateTempFile() => this.CreateTempFile(this.CreateTempFolder());

        public string CreateTempFile(string directory)
        {
            string randomFileName = Path.GetRandomFileName();
            string path = Path.Combine(directory, randomFileName);
            this.CreateFile(path);
            return path;
        }

        public void CreateFolder(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));
            if (Directory.Exists(path))
                return;
            Directory.CreateDirectory(path);
        }

        public string CreateTempFolder()
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            this.CreateFolder(path);
            return path;
        }

        public void Compress(string destination, params string[] sourceFiles)
        {
            string tempFolder = this.CreateTempFolder();
            foreach (string sourceFile in sourceFiles)
                File.Copy(sourceFile, Path.Combine(tempFolder, Path.GetFileName(sourceFile)));
            ZipFile.CreateFromDirectory(tempFolder, destination, CompressionLevel.Optimal, false);
        }

        public void Decompress(string sourceFile, string destination) => ZipFile.ExtractToDirectory(sourceFile, destination);
    }

}
