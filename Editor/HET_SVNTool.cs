using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.IO;
public class HET_SVNTool : MonoBehaviour
{

    [MenuItem("Assets/SVN-更新")]
    public static void  SVNUpdate()
    {
        string str = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(Selection.activeObject);
        Process.Start("TortoiseProc.exe", "/command:update /path:" + str+ " /closeonend:4");
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/SVN-提交并打包")]
    public static void SVNCommitAndBuild()
    {
        string str = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(Selection.activeObject);
        Process.Start("TortoiseProc.exe", "/command:commit /logmsg:release /path:" + str + " /closeonend:0");
        AssetDatabase.Refresh();
    }


    
    [MenuItem("Assets/SVN-提交")]
    public static void SVNCommit()
    {
        string str = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')) + "/" + AssetDatabase.GetAssetPath(Selection.activeObject);
        Process.Start("TortoiseProc.exe", "/command:commit /path:" + str + " /closeonend:0");
        AssetDatabase.Refresh();
    }


    [MenuItem("Assets/创建文本")]
    public static void CreateTXT()
    {

        string fileName = "";
        for (int i = 0; i < 100; i++)
        {
            fileName = "NewTxt_" + i.ToString() + ".txt";
            if (File.Exists(AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + fileName) ==false)
            {
                break;
            }
        }
        File.WriteAllText(AssetDatabase.GetAssetPath(Selection.activeObject) + "/" + fileName, "");
        AssetDatabase.Refresh();
    }



}
