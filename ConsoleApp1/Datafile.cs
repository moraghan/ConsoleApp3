using System;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;

namespace SyncSourceToTarget
{
    class Datafile
    {
        public string fileName { get; set; }
        public DateTime lastAccessTime { get; set; }
        public long size { get; set; }
        public string extension { get; set; }
        public string directory { get; set; }
        public string status { get; set; }

    }
}