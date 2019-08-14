using System;
using System.IO;
using System.Linq;

namespace RecursiveRename
{
    class Program
    {
        static void Main(string[] args)
        {
            var renameFolderPath = Path.Combine("c:", "repos", "healthtracker-app", "src");
            Console.WriteLine(renameFolderPath);
            RenameFolder(renameFolderPath);
        }

        private static void RenameFolder(string renameFolderPath)
        {
            if (Directory.GetDirectories(renameFolderPath).Any())
                foreach (var folder in Directory.GetDirectories(renameFolderPath))
                {
                    if (Directory.GetDirectories(folder).Any())
                        RenameFolder(folder);
                    else
                    {
                        RenameFiles(folder);
                    }
                }
            else
            {
                RenameFiles(renameFolderPath);
            }
        }

        private static void RenameFiles(string folder)
        {
            foreach (var file in Directory.GetFiles(folder, "*.spec.ts"))
            {
                var newFileName = file.Split(".spec.ts")[0] + ".spec.old.ts";
                File.Move(file, newFileName);
                Console.WriteLine($"Renamed {file} => {newFileName}");
            }
        }
    }
}
