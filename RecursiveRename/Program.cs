using System;
using System.IO;
using System.Linq;

namespace RecursiveRename
{
    class Program
    {
        static int _count = 0;
        
        static void Main(string[] args)
        {
            var renameFolderPath = Path.Combine("c:", "repos", "healthtracker-app", "src");

            Console.WriteLine(renameFolderPath);

            RenameFilesInFolder(renameFolderPath);

            Console.WriteLine($"");
            Console.WriteLine($"Total file(s) renamed: {_count}");
        }

        private static void RenameFilesInFolder(string renameFolderPath)
        {
            RenameFiles(renameFolderPath, ".spec.ts", ".spec.ts.old");

            if (Directory.GetDirectories(renameFolderPath).Any())
            { 
                foreach (var folder in Directory.GetDirectories(renameFolderPath))
                    RenameFilesInFolder(folder);
            }
        }
        
        private static void RenameFiles(string folder, string matchText, string replaceText)
        {
            foreach (var file in Directory.GetFiles(folder, $"*{matchText}"))
            {
                var splitText = file.Split(matchText);
                if (splitText.Any())
                {
                    var newFileName = splitText[0] + replaceText;
                    File.Move(file, newFileName); // the actual renaming
                    _count += 1;
                    Console.WriteLine($"Renamed {file} => {newFileName}");
                }
            }
        }
    }
}
