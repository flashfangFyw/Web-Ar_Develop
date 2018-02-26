
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class AbFileInfo
{
    private string fileName;
    public string FileName
    {
        get { return fileName; }
    }

    private string relativePath;
    public string RelativePath
    {
        get { return relativePath; }
    }

    private long crc;
    public long Crc
    {
        get { return crc; }
    }

    public AbFileInfo(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return;
        }
        string[] strs = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        this.fileName = strs[0];
        this.relativePath = strs[0];
        this.crc = long.Parse(strs[1]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(RelativePath);
        sb.Append("  ");
        sb.Append(crc);
        return sb.ToString();
    }

    public static Dictionary<string, AbFileInfo> DeCode(string data)
    {
        Dictionary<string, AbFileInfo> totals = new Dictionary<string, AbFileInfo>();
        if (string.IsNullOrEmpty(data)) return totals;
        StringReader textReader = new StringReader(data);

        string line;
        while (!string.IsNullOrEmpty(line = textReader.ReadLine()))
        {
            AbFileInfo abFileInfo = new AbFileInfo(line);
            totals[abFileInfo.FileName] = abFileInfo;
        }
        return totals;
    }
    public static void FileWrite(string path, string abFileText)
    {
        File.WriteAllText(path, abFileText, Encoding.UTF8);
    }

    public static void FileWrite(string path, Dictionary<string, AbFileInfo> data)
    {
        Dictionary<string, AbFileInfo>.Enumerator en = data.GetEnumerator();
        StringBuilder sb = new StringBuilder();
        while (en.MoveNext())
        {
            sb.AppendLine(en.Current.Value.ToString());
        }
        FileWrite(path, sb.ToString());
    }

    public static void FileAppend(string path, string abFileText)
    {
        File.AppendAllText(path, abFileText, Encoding.UTF8);
    }
    public static void FileAppendLine(string path, AbFileInfo data)
    {
        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine(data.ToString());
            sw.Close();
        }
    }
}
