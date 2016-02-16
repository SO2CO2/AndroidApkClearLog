using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace rmcode
{
    class QueryFiles
    {
        public static string outdir = "";
        public static string keystr = "";
        public static Stack st = new Stack();
        public static void getFiles(string path)
        {
            string[] strFileNames = Directory.GetFiles(path);
            string[] strDirectories = Directory.GetDirectories(path);

            foreach (string filename in strFileNames)
            {
                if (filename.EndsWith(".java"))
                {
                    QueryFileApi(filename);
                }            
            }
            foreach (string dir in strDirectories)
            {
                Console.WriteLine("DirectoriesName:{0}", dir);
                getFiles(dir);
            }
        }
        public static void QueryFileApi(string fileName)
        {
            Console.WriteLine(fileName);
            Directory.CreateDirectory(Path.Combine(outdir, Path.GetDirectoryName(fileName)));
            st.Clear();
            try
            {
                FileStream aFile = new FileStream(fileName, FileMode.Open);
                StreamReader sr = new StreamReader(aFile);

                var utf8WithoutBom = new System.Text.UTF8Encoding(false);
                StreamWriter sw = new StreamWriter(Path.Combine(outdir, fileName), false, utf8WithoutBom);
                string line = null;
                string rmemptyline = null;
                while ((line = sr.ReadLine()) != null)
                {
                    rmemptyline = line.Trim();
                    if (rmemptyline.StartsWith(keystr))
                    {
                        st.Clear();
                        while (true)
                        {
                            StOp(rmemptyline);
                            if (st.Count == 0 && rmemptyline.EndsWith(";"))
                            {
                                //说明这一句是一个完整的语法, 什么也不做，不再把这句写到新文件.
                                break;
                            }
                            else
                            {
                                //说明换行了，再重新读一行
                                line = sr.ReadLine();
                                if (line != null)
                                {
                                    rmemptyline = line.Trim();
                                }
                                else
                                {
                                    rmemptyline = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        sw.WriteLine(line);
                        sw.Flush();
                    }
                }
                sr.Close();
                aFile.Close();
                sw.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(fileName + ex.Message);
            }
        }
        public static void StOp(string rmemptyline)
        {
            if (rmemptyline == null) return;
            int i = 0;
            for (i = 0; i < rmemptyline.Length; i++)
            {
                if (rmemptyline[i] == '(')
                {
                    if (st.Count == 0 || st.Peek().Equals("("))
                    {
                        st.Push("(");
                    }
                }
                else if (rmemptyline[i] == '\"')
                {
                    if (i == 0)
                    {
                        if (st.Peek().Equals("\""))
                        {
                            st.Pop();
                        }
                        else
                        {
                            st.Push("\"");
                        }
                    }
                    else
                    {
                        if (rmemptyline[i - 1] != ('\\'))
                        {
                            if (st.Peek().Equals("\""))
                            {
                                st.Pop();
                            }
                            else
                            {
                                st.Push("\"");
                            }
                        }
                    }

                }
                else if (rmemptyline[i] == ')')
                {
                    if (st.Peek().Equals("("))
                    {
                        st.Pop();
                    }
                }
            }
        }
    }
}
