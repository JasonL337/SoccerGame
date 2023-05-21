using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class utilities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float Blend(float wantVelocity, float blendSpeed, float DT, float maxVal, float startPow)
    {
        float power = 0;
        if (Mathf.Sign(startPow) == -1)
        {
            power = wantVelocity * DT * blendSpeed - startPow;
        }
        else
        {
            power = wantVelocity * DT * blendSpeed + startPow;
        }

        if (Mathf.Abs(power) >= Mathf.Abs(maxVal))
            power = maxVal;

        return power;
    }

    public float getAngleTo(Vector3 from, Vector3 to)
    {
        return Vector3.Angle(from, to);
    }

    public float getDotProduct(Vector3 firstV, Vector3 secondV)
    {
        return Vector3.Dot(firstV, secondV);
    }

    public float randomNum(float minVal, float maxVal)
    {
        float r = Random.Range(minVal, maxVal);
        return r;
    }


    /////////////////////////////////////////////////////////////////////////////
    public float MapLinearRange(float val, float in0, float in1, float out0, float out1)
    {
        if (in0 == in1)
            return val;

        float t = Mathf.Clamp01((val - in0) / (in1 - in0));

        float result = out0 + t * (out1 - out0);

        return result;
    }


    public float MinMax(float number, float minVal, float maxVal, bool isMin, bool isMax)
    {
        if (number < minVal && isMin)
        {
            number = minVal;
        }
        if (number > maxVal && isMax)
        {
            number = maxVal;
        }
        return number;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
