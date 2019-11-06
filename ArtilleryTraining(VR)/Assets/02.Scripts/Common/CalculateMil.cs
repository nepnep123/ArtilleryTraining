using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateMil : MonoBehaviour
{
    
    public static int DegreeToMil(float degree)
    {
        degree = degree / 360;
        int mil = Mathf.RoundToInt(degree * 6400);

        while(mil < 0){
            mil += 6400;
        }
        return mil;
    }

    public static float MilToDegree(int mil)
    {
        float degree = 1.0f / 6400.0f * 360.0f;

        return mil * degree;
    }

    // 편각 계산
    public static int CalcuateAzimuth(Vector3 fromPos, Vector3 toPos)
    {
        float z = fromPos.z - toPos.z;
        float x = fromPos.x - toPos.x;

        double degree = Mathf.Rad2Deg * System.Math.Atan(x / z) - 180;

        return DegreeToMil((float)degree);
    }

    // 사각 계산
    public static int CalcuateElevationAngle(Vector3 fromPos, Vector3 toPos)
    {
        float mag = Vector3.Magnitude(new Vector3(fromPos.x, 0, fromPos.z) - new Vector3(toPos.x, 0, toPos.z));
        float high = Mathf.Abs(toPos.y - fromPos.y);

        double degree = Mathf.Rad2Deg * System.Math.Atan(high / mag);

        return DegreeToMil((float)degree);
    }



    public static int FixAzimuth(Vector3 firePos, Vector3 targetPos, Vector3 hitPos)
    {
        Vector3 fireToTarget = new Vector3(targetPos.x, 0, targetPos.z) - new Vector3(firePos.x, 0, firePos.z);
        Vector3 fireToHit = new Vector3(hitPos.x, 0, hitPos.z) - new Vector3(firePos.x, 0, firePos.z);

        double degree = Mathf.Rad2Deg * System.Math.Acos(Vector3.Dot(fireToHit.normalized, fireToTarget.normalized));

        return DegreeToMil(Mathf.Abs((float)degree));
    }

    public static int FixElevationAngle(Vector3 firePos, Vector3 targetPos, Vector3 hitPos)
    {
        float magToTarget = Vector3.Magnitude(new Vector3(firePos.x, 0, firePos.z) - new Vector3(targetPos.x, 0, targetPos.z));
        float magToHit = Vector3.Magnitude(new Vector3(firePos.x, 0, firePos.z) - new Vector3(hitPos.x, 0, hitPos.z));

        float highToTarget = Mathf.Abs(targetPos.y - firePos.y);
        float highToHit = Mathf.Abs(hitPos.y - firePos.y);

        double degreeToTarget = Mathf.Rad2Deg * System.Math.Atan(highToTarget / magToTarget);
        double degreeToHit = Mathf.Rad2Deg * System.Math.Atan(highToHit / magToHit);

        double degree = degreeToHit - degreeToTarget;

        return DegreeToMil(Mathf.Abs((float)degree));
    }
}