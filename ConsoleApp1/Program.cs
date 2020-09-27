using System;
using System.IO;
using System.Data;

namespace SyncSourceToTarget
{
    
    class FileSysInfo
    {

        static void Main()
        {
            // You can also use System.Environment.GetLogicalDrives to
            // obtain names of all logical drives on the computer.
            System.IO.DriveInfo di = new System.IO.DriveInfo(@"/Users/moraghan/docker");

            // Get the root directory and print out some information about it.
            System.IO.DirectoryInfo dirInfo = di.RootDirectory;

            // Get the files in the directory and print out some information about them.
            System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

            // foreach (System.IO.FileInfo fi in fileNames)
            // {
            //     Console.WriteLine("{0}: {1}: {2}", fi.Name, fi.LastAccessTime, fi.Length);
            // }
            //
            dSearch();
        }

        static void dSearch()
        {
            DataTable table = new DataTable();
            table.Columns.Add("fileName", typeof(string));
            table.Columns.Add("lastAccessTime", typeof(string));
            table.Columns.Add("size", typeof(string));
            table.Columns.Add("extension", typeof(string));
            table.Columns.Add("directory", typeof(string));

            foreach (var file in Directory.EnumerateFiles(@"/Users/moraghan/docker",
                "*.*",
                SearchOption.AllDirectories))
            {
                // Display file path.
                var fFile = new FileInfo(file);
                table.Rows.Add(fFile.Name, fFile.LastAccessTime, fFile.Length, fFile.Extension, fFile.DirectoryName);
                Console.WriteLine("{0}: {1}: {2}: {3}: {4}", fFile.Name, fFile.LastAccessTime, fFile.Length,
                    fFile.Extension, fFile.DirectoryName);
            }

            foreach (var row in table.Rows)
            {
                Console.WriteLine(row.directory.ToString());
            }
        }
    }
}