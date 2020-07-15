using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HRT_FileControl 
{

    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="dir">要删除的目录</param>
    public static void DeleteFolder(string dir)
    {
        if (Directory.Exists(dir))
        {
            string[] fileSystemEntries = Directory.GetFileSystemEntries(dir);
            for (int i = 0; i < fileSystemEntries.Length; i++)
            {
                string text = fileSystemEntries[i];
                if (File.Exists(text))
                {
                    File.Delete(text);
                }
                else
                {
                    DeleteFolder(text);
                }
            }
            Directory.Delete(dir);
        }
    }

    private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
    {
        bool ret = false;
        try
        {
            SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
            DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";
            if (Directory.Exists(SourcePath))
            {
                if (Directory.Exists(DestinationPath) == false)
                    Directory.CreateDirectory(DestinationPath);
                foreach (string fls in Directory.GetFiles(SourcePath))
                {
                    FileInfo flinfo = new FileInfo(fls);
                    flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                }
                foreach (string drs in Directory.GetDirectories(SourcePath))
                {
                    DirectoryInfo drinfo = new DirectoryInfo(drs);
                    if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                        ret = false;
                }
            }
            ret = true;
        }
        catch (System.Exception ex)
        {
            ret = false;
        }
        return ret;
    }



}
