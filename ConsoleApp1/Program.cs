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
            List<datafile> Datafiles = new List<datafile>();

            foreach (var file in Directory.EnumerateFiles(@"/Users/moraghan/docker",
                "*.*",
                SearchOption.AllDirectories))
            {
                var fFile = new FileInfo(file);

                datafile newSourceFile = new datafile()
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

            foreach (datafile fFile in Datafiles)
            {
                Console.WriteLine("{0}: {1}: {2}: {3}: {4}: {5}", fFile.fileName, fFile.lastAccessTime, fFile.size,
                    fFile.extension, fFile.directory, fFile.status);
            }
        }

        class datafile
        {
            public string fileName { get; set; }
            public DateTime lastAccessTime { get; set; }
            public long size { get; set; }
            public string extension { get; set; }
            public string directory { get; set; }
            public string status { get; set; }
            
        }
    }
}