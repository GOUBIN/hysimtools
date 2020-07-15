using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class HRT_AppConfig : SingletonMonoBehaviour<HRT_AppConfig>
{

    private bool LoadingComplete = false;

    public Dictionary<string, string> appconfigsDic = new Dictionary<string, string>();

    private void Awake()
    {
        Load();
    }

    void Load()
    {
        string filePath = Application.dataPath + "/07_Configs/AppConfig.txt";
        if (File.Exists(filePath) == false)
        {
            Debug.LogError(string.Format("加载主配置表失败:{0}", filePath));
            return;
        }

        string[] rows = File.ReadAllLines(filePath);

        for (int i = 0; i < rows.Length; i++)
        {
            if (string.IsNullOrEmpty(rows[i]) == false)
            {
                try
                {
                    if (rows[i][0].ToString() == "#" || !rows[i].Contains(":"))
                    {
                        continue;
                    }
                    var line = rows[i];
                    var index = line.IndexOf(':');
                    var k = line.Substring(0, index);
                    var v = line.Substring(index + 1);

                    if (appconfigsDic.ContainsKey(k))
                    {
                        //如果key重复,以最后一个为准
                        appconfigsDic[k] = v;
                        Debug.LogError("存在重复Key:" + k);
                    }
                    else
                    {
                        appconfigsDic.Add(k, v);
                    }
                }
                catch (System.Exception e)
                {
                    continue;
                }
            }
        }


        //string languagePath = Application.dataPath + "/07_Configs/"+ Get("LanguageFile");
        //LanguageManager.instance.Load(languagePath);

        LoadingComplete = true;

    }

    public string Get(string key)
    {
        if (appconfigsDic.ContainsKey(key) == false)
        {
            Debug.LogError("主配置文件中不存在该KEY:" + key);
            return "";
        }

        return appconfigsDic[key];
    }
}
