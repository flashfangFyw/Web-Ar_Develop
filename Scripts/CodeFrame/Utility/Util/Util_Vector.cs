using UnityEngine;
using System.Collections;
using ffDevelopmentSpace;
using System.Collections.Generic;


/* 
    Author:     fyw 
    CreateDate: 2018-02-08 11:04:21 
    Desc:       Vector向量工具集 
*/


public class Util_Vector  
{
    /// <summary>
    /// 将 vector3转化为vector4 w轴末尾添0
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static Vector4 Vec3To4Add0(Vector3 v3)
    {
        return new Vector4(v3.x, v3.y, v3.z, 0);
    }
    public static List<Vector4> List_Vec3To4Add0(List<Vector3> lv3 )
    {
        List<Vector4> lv4 = new List<Vector4>();
        for(int i=0;i<lv3.Count;i++)
        {
            lv4.Add(Vec3To4Add0(lv3[i]));
        }
        return lv4;
    }
    /// 
    /// 判断两个向量是否平行 
    /// Lhs. 
    /// Rhs. 
    /// true:平行 false:不平行 
    /// 数学上 Mathf.Abs(value）==1才为平行，不过经测试有时数值会有偏差故用0.98近似的等于
    public static bool IsParallel(Vector3 lhs, Vector3 rhs)
    {
        float value = Vector3.Dot(lhs.normalized, rhs.normalized);
        if (Mathf.Abs(value) >= 0.98) return true;
        return false;
    }
    /// 
    /// 判断两个向量是否通向
    /// Lhs. 
    /// Rhs. 
    /// 1:通向 -1:反向 0 不平行；
    /// 
    public static float IsParallelAndDirection(Vector3 lhs, Vector3 rhs)
    {
        float value = Vector3.Dot(lhs.normalized, rhs.normalized);
        if (Mathf.Abs(value) >= 0.98) return value>0?1:-1;
        return 0;
    }
    /// <summary>
    /// 判断目标点是否位于向量的左边   
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool PointOnLeftSide(Vector3 dir, Vector3 target)
    {
        return Vector3.Cross(dir, target).y < 0;
    }
    /// <summary>
    /// 判断目标是否在自己的前方:
    /// </summary>
    /// <param name="orignio"></param>自己
    /// <param name="target"></param>目标
    /// <returns></returns>
    public static bool PointOnForwardSide(GameObject orignio, Vector3 target)
    {
        return Vector3.Dot(orignio.transform.forward, target)> 0;
    }

    /// <summary>
    /// 点到直线距离
    /// </summary>
    /// <param name="point">点坐标</param>
    /// <param name="linePoint1">直线上一个点的坐标</param>
    /// <param name="linePoint2">直线上另一个点的坐标</param>
    /// <returns></returns>
    public static float DisPoint2Line(Vector3 point, Vector3 linePoint1, Vector3 linePoint2)
    {
        Vector3 vec1 = point - linePoint1;
        Vector3 vec2 = linePoint2 - linePoint1;
        Vector3 vecProj = Vector3.Project(vec1, vec2);
        float dis = Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(vecProj), 2));
        return dis;
    }
    /// <summary>
    /// 点到平面的距离 自行推演函数
    /// </summary>
    /// <param name="point"></param>
    /// <param name="surfacePoint1"></param>
    /// <param name="surfacePoint2"></param>
    /// <param name="surfacePoint3"></param>
    /// <returns></returns>
    public static float DisPoint2Surface(Vector3 point, Vector3 surfacePoint1, Vector3 surfacePoint2, Vector3 surfacePoint3)
    {
        //空间直线一般式方程 Ax + By + Cz + D = 0;
        //假定 A = 1 ，推演B C D用A来表示，约去A，可得方程
        float BNumerator = (surfacePoint1.x - surfacePoint2.x) * (surfacePoint2.z - surfacePoint3.z) - (surfacePoint2.x - surfacePoint3.x) * (surfacePoint1.z - surfacePoint2.z);
        float BDenominator = (surfacePoint2.y - surfacePoint3.y) * (surfacePoint1.z - surfacePoint2.z) - (surfacePoint1.y - surfacePoint2.y) * (surfacePoint2.z - surfacePoint3.z);
        float B = BNumerator / BDenominator;
        float C = (B * (surfacePoint1.y - surfacePoint2.y) + (surfacePoint1.x - surfacePoint2.x)) / (surfacePoint2.z - surfacePoint1.z);
        float D = -surfacePoint1.x - B * surfacePoint1.y - C * surfacePoint1.z;

        return DisPoint2Surface(point, 1f, B, C, D);
    }

    public static float DisPoint2Surface(Vector3 point, float FactorA, float FactorB, float FactorC, float FactorD)
    {
        //点到平面的距离公式 d = |Ax + By + Cz + D|/sqrt(A2 + B2 + C2);
        float numerator = Mathf.Abs(FactorA * point.x + FactorB * point.y + FactorC * point.z + FactorD);
        float denominator = Mathf.Sqrt(Mathf.Pow(FactorA, 2) + Mathf.Pow(FactorB, 2) + Mathf.Pow(FactorC, 2));
        float dis = numerator / denominator;
        return dis;
    }
    /// <summary>
    /// 点到平面距离 调用U3D Plane类处理
    /// </summary>
    /// <param name="point"></param>
    /// <param name="surfacePoint1"></param>
    /// <param name="surfacePoint2"></param>
    /// <param name="surfacePoint3"></param>
    /// <returns></returns>
    public static float DisPoint2Surface2(Vector3 point, Vector3 surfacePoint1, Vector3 surfacePoint2, Vector3 surfacePoint3)
    {
        Plane plane = new Plane(surfacePoint1, surfacePoint2, surfacePoint3);

        return DisPoint2Surface2(point, plane);
    }

    public static float DisPoint2Surface2(Vector3 point, Plane plane)
    {
        return plane.GetDistanceToPoint(point);
    }

    /// <summary>
    /// 平面夹角
    /// </summary>
    /// <param name="surface1Point1"></param>
    /// <param name="surface1Point2"></param>
    /// <param name="surface1Point3"></param>
    /// <param name="surface2Point1"></param>
    /// <param name="surface2Point2"></param>
    /// <param name="surface2Point3"></param>
    /// <returns></returns>
    public static float SurfaceAngle(Vector3 surface1Point1, Vector3 surface1Point2, Vector3 surface1Point3, Vector3 surface2Point1, Vector3 surface2Point2, Vector3 surface2Point3)
    {
        Plane plane1 = new Plane(surface1Point1, surface1Point1, surface1Point1);
        Plane plane2 = new Plane(surface2Point1, surface2Point1, surface2Point1);
        return SurfaceAngle(plane1, plane2);
    }

    public static float SurfaceAngle(Plane plane1, Plane plane2)
    {
        return Vector3.Angle(plane1.normal, plane2.normal);
    }
}
