using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
public delegate void LoadTextureCallBack(Texture2D texture);
public delegate void LoadTxtCallBack(string texture);


public class HRT_LoadAsset 
{



    public static IEnumerator IELoadText(string filePath, LoadTxtCallBack callBack = null)
    {
        string json = File.ReadAllText(filePath);
        callBack(json);
        yield return null;

        ////Debug.Log(filePath);
        //using (UnityWebRequest request = UnityWebRequest.Get(filePath))
        //{
        //    yield return request.SendWebRequest();
        //    if (request.isHttpError || request.isNetworkError)
        //    {
        //        // 下载出错
        //        UnityEngine.Debug.LogError(request.error);
        //    }
        //    else
        //    {
        //        // 下载完成
        //        string text = request.downloadHandler.text;
        //        callBack?.Invoke(text);
        //        // 优先释放request 会降低内存峰值
        //        request.Dispose();
        //    }
        //}
    }


    /// <summary>
    /// 以IO方式进行加载
    /// </summary>
    public static IEnumerator IELoadTextureByIo(string url, LoadTextureCallBack callBack = null)
    {

        //创建文件读取流
        FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
        //创建文件长度缓冲区
        byte[] bytes = new byte[fileStream.Length];
        //读取文件
        fileStream.Read(bytes, 0, (int)fileStream.Length);

        //释放文件读取流
        fileStream.Close();
        //释放本机屏幕资源
        fileStream.Dispose();
        fileStream = null;
        yield return null;
        //创建Texture
        int width = 1920;
        int height = 1080;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(bytes);
        yield return null;
        callBack?.Invoke(texture);



    }



    public static  IEnumerator IELoadTexture(string filePath, LoadTextureCallBack callBack = null)
    {
        Debug.Log("加载图片开始:" + filePath);
        Debug.Log("加载图片开始_FullPath:" + Path.GetFullPath(filePath));
        string fullPath = Path.GetFullPath(filePath);
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(fullPath))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                // 下载出错
               UnityEngine.Debug.LogError(request.error);
            }
            else
            {
                // 下载完成
                Texture2D texture = (request.downloadHandler as DownloadHandlerTexture).texture;

                //Debug.Log("加载图片完毕:" + filePath);
                // 优先释放request 会降低内存峰值
                request.Dispose();
                callBack?.Invoke(texture);
            }
        }
    }



}
