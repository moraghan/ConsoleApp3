using System;
using System.Collections.Generic;
using System.IO;
using SyncSourceToTarget;

namespace ConsoleApp1
{
    static class FileSysInfo
    {
        static void Main()
        {
            List<Datafile> sourceSnapshot = TakeInitialDirSnapshot(@"/Users/moraghan/docker");
            foreach (Datafile fFile in sourceSnapshot)
            {
                Console.WriteLine("{0}: {1}: {2}: {3}: {4}: {5}", fFile.fileName, fFile.lastAccessTime, fFile.size,
                    fFile.extension, fFile.directory, fFile.status);
            }
            
        }

        static List<Datafile> TakeInitialDirSnapshot(string sourceDirName)
        {
            List<string> validExtensions = new List<string>() {".txt", ".bak", ".sql"};
            var minModifiedDate = new DateTime(2019,12,18);
            
            List<Datafile> Datafiles = new List<Datafile>() ;

            foreach (var file in Directory.EnumerateFiles(sourceDirName,
                "*.*",
                SearchOption.AllDirectories))
            {
                var fFile = new FileInfo(file);

                if ((validExtensions.Contains(fFile.Extension)) && (minModifiedDate <= fFile.LastAccessTime))
                {
                    var newSourceFile = new Datafile();

                    newSourceFile.fileName = fFile.Name;
                    newSourceFile.lastAccessTime = fFile.LastAccessTime;
                    newSourceFile.size = fFile.Length;
                    newSourceFile.extension = fFile.Extension;
                    newSourceFile.directory = fFile.DirectoryName;
                    newSourceFile.status = "Snapshot";

                    Datafiles.Add(newSourceFile);
                }
            }

            return Datafiles;
            
        }
        
    }
}