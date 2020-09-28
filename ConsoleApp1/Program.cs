using System;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;

namespace SyncSourceToTarget
{
    class FileSysInfo
    {
        static void Main()
        {
            TakeInitialDirSnapshot();
        }

        static void TakeInitialDirSnapshot()
        {
            List<Datafile> Datafiles = new List<Datafile>();

            foreach (var file in Directory.EnumerateFiles(@"/Users/moraghan/docker",
                "*.*",
                SearchOption.AllDirectories))
            {
                var fFile = new FileInfo(file);

                Datafile newSourceFile = new Datafile()
                {
                    fileName = fFile.Name,
                    lastAccessTime = fFile.LastAccessTime,
                    size = fFile.Length,
                    extension = fFile.Extension,
                    directory = fFile.DirectoryName,
                    status = "Snapshot"
                };

                Datafiles.Add(newSourceFile);
            }

            foreach (Datafile fFile in Datafiles)
            {
                Console.WriteLine("{0}: {1}: {2}: {3}: {4}: {5}", fFile.fileName, fFile.lastAccessTime, fFile.size,
                    fFile.extension, fFile.directory, fFile.status);
            }
        }
        
    }
}