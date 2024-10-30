using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using TagClass;

namespace TagClass
{
    public static class sM3U
    {
        public static string[] Load(string path)
        {
            StreamReader FS = new StreamReader(path);
            if (FS.ReadLine() != "#EXTM3U")
                return null;

            string Temp;
            ArrayList Strings = new ArrayList();
            while (!FS.EndOfStream)
            {
                Temp = FS.ReadLine();
                if (Temp.StartsWith("#EXTINF"))
                    continue;

                Strings.Add(Temp);
            }

            for (int i = 0; i < Strings.Count; i++)
                if (!(Strings[i] as string).Contains(":"))
                    Strings[i] = Path.Combine(Path.GetDirectoryName(path), Strings[i] as string);

            return (string[])Strings.ToArray(typeof(string));
        }

        public static string[] Load()
        {
            OpenFileDialog frmOpen = new OpenFileDialog();
            frmOpen.Filter = "M3U Files|*.m3u";
            frmOpen.Title = "Open M3U list";
            if (frmOpen.ShowDialog() == DialogResult.OK)
                return Load(frmOpen.FileName);
            else
                return null;
        }

        public static void Save(string path, string[] Files)
        {
            string[] F = (string[])Files.Clone();
            string Dir = Path.GetDirectoryName(path).ToLower();
            for (int i = 0; i < F.Length; i++)
                if (Path.GetDirectoryName(F[i]).ToLower().Equals(Dir))
                    F[i] = Path.GetFileName(F[i]);

            StreamWriter SW = new StreamWriter(path);
            SW.WriteLine("#EXTM3U");
            foreach (string st in F)
                SW.WriteLine(st);
            SW.Close();
        }

        public static void Save(string[] Files)
        {
            SaveFileDialog frmSave = new SaveFileDialog();
            frmSave.Filter = "M3U Files|*.m3u";
            frmSave.Title = "Open M3U list";
            if (frmSave.ShowDialog() == DialogResult.OK)
                Save(frmSave.FileName, Files);
        }

        public static void Save(ITagInfo[] Tags)
        {
            ArrayList Files = new ArrayList();
            foreach (ITagInfo var in Tags)
                Files.Add(var.FilePath);

            Save((string[])Files.ToArray(typeof(string)));
        }
    }
}