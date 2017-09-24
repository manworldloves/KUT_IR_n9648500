using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KUT_IR_n9648500
{
    class FileHandling
    {
        public static string ReadTextFile(String filename)
        {
            string text = "";
            try
            {
                TextReader reader = new StreamReader(filename);
                text = reader.ReadToEnd();
                reader.Close();
            }

            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            
            return text;
        }

        public static void WriteTextFile(List<string> text, string FileName, bool appendMode)
        {
            try
            {
                TextWriter writer = new System.IO.StreamWriter(FileName, appendMode);
                foreach (string line in text)
                {
                    writer.Write(line);
                }
            }
			catch (IOException e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

        }

        public static List<string> GetFileNames(String path, bool recursiveDir)
        {
            System.IO.DirectoryInfo root = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            List<string> filenames = new List<string>();

            // First, process all the files directly under this folder 
            try
            {
                // only get txt files
                files = root.GetFiles("*.txt");
            }

            catch (UnauthorizedAccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    string name = fi.FullName;
                    filenames.Add(name);
                    //OutputFileDetails(name);
                }

                if (recursiveDir)
                {
					// Now find all the subdirectories under this directory.
					subDirs = root.GetDirectories();

					foreach (System.IO.DirectoryInfo dirInfo in subDirs)
					{
						// Resursive call for each subdirectory.
						string name = dirInfo.FullName;
						filenames.AddRange(GetFileNames(name, true));
					}  
                }

            }

            return filenames;
        }
    }
}
