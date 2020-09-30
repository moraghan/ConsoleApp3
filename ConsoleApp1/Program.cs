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
            List<Datafile> sourceSnapshot = TakeFileSnapshot(@"/Users/moraghan/docker");
            
            List<Datafile> filesProcessed = CopySourceFilesToTarget(sourceSnapshot);

            foreach (Datafile dFile in filesProcessed)
            {
                Console.WriteLine("{0}: {1}: {2}: {3}: {4}: {5}", dFile.fileName, dFile.lastAccessTime, dFile.size,
                    dFile.extension, dFile.directory, dFile.status);
            }
        }

        static List<Datafile> TakeFileSnapshot(string sourceDirName)
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

        static List<Datafile>  CopySourceFilesToTarget(List<Datafile> DataFilesToBeCopied)
        {
            foreach (Datafile dFile in DataFilesToBeCopied)
            {
                var fFile = new FileInfo(dFile.directory + '/' + dFile.fileName);
                // If current file size and last access time same as snapshot then we know file has not changed
                // and so can be copied to destination
                if ((fFile.Length == dFile.size) && (fFile.LastAccessTime == dFile.lastAccessTime))
                {
                    Console.WriteLine("{0}: {1}: {2}: {3}: {4}: {5}", dFile.fileName, dFile.lastAccessTime, dFile.size,
                        dFile.extension, dFile.directory, dFile.status);
                    dFile.status = "Copied";
                }
            }

            return DataFilesToBeCopied;
        }
        
    }
}