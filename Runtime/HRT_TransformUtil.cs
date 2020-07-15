using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class HRT_TransformUtil
{



    public static Matrix4x4 GetMatrix(this Transform trans)
    {
        return trans.localToWorldMatrix;
    }

    public static Matrix4x4 GetRelativeMatrix(this Transform trans, Transform relativeTo)
    {
        var m = trans.localToWorldMatrix;
        return relativeTo.worldToLocalMatrix * m;
    }

    public static Matrix4x4 GetLocalMatrix(this Transform trans)
    {
        return Matrix4x4.TRS(trans.localPosition, trans.localRotation, trans.localScale);
    }


    public static List<float> GetFloats(this Vector3 vector3)
    {
        List<float> temp = new List<float>();
        temp.Add(vector3.x);
        temp.Add(vector3.y);
        temp.Add(vector3.z);

        return temp;
    }


    public static Vector3 GetV3(this List<float> fs)
    {
       

        return new Vector3 (fs[0],fs[1],fs[2]);
    }



    #region Matrix Methods


    public static Vector3 ExtractPosition(this Matrix4x4 matrix)
    {
        return matrix.GetColumn(3);
    }

    public static Vector3 GetTranslation(this Matrix4x4 m)
    {
        var col = m.GetColumn(3);
        return new Vector3(col.x, col.y, col.z);
    }

    public static Quaternion ExtractRotation(this Matrix4x4 matrix)
    {
        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;
        forward.z = matrix.m22;

        Vector3 upwards;
        upwards.x = matrix.m01;
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;

        return Quaternion.LookRotation(forward, upwards);
    }


    public static Quaternion GetRotation(this Matrix4x4 m)
    {
        Quaternion q = new Quaternion();
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
        q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2]));
        q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0]));
        q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
        return q;
    }




    public static Vector3 GetScale(this Matrix4x4 m)
    {
        //var xs = m.GetColumn(0);
        //var ys = m.GetColumn(1);
        //var zs = m.GetColumn(2);

        //var sc = new Vector3();
        //sc.x = Vector3.Magnitude(new Vector3(xs.x, xs.y, xs.z));
        //sc.y = Vector3.Magnitude(new Vector3(ys.x, ys.y, ys.z));
        //sc.z = Vector3.Magnitude(new Vector3(zs.x, zs.y, zs.z));

        //return sc;

        return new Vector3(m.GetColumn(0).magnitude, m.GetColumn(1).magnitude, m.GetColumn(2).magnitude);
    }

    #endregion



}