using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace rmcode
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: rmcode.exe inputDirectory keyword outputDirectory");
                return;
            }
            string inputpath = args[0];
            string outpath = args[2];
            string p = Path.Combine(System.Environment.CurrentDirectory, outpath);
            if (Directory.Exists(p))
            {
                // Delete the target to ensure it is not there.
                Directory.Delete(p, true);
                Thread.Sleep(1000);
                Directory.CreateDirectory(p);
            }
            else
            {
                Directory.CreateDirectory(p);
            }
            QueryFiles.outdir = outpath;
            QueryFiles.keystr = args[1];
            QueryFiles.getFiles(inputpath);
        }
    }
}
