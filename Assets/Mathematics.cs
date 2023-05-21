using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mathematics : MonoBehaviour
{
    public utilities Utilities;
    public float constant = 1;
    public Transform barSizeT;
    float shotTime = 0;
    float lastSize;
    Vector3 barSizeV;
    float speedTime = 0;
    float startVal = 0;
    float difference = 0;
    float requiredTime = 0;
    public float yConstant = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 Shot(Vector2 rbVel, Vector2 bVel, Vector2 Pos, Vector2 bPos)
    {
        Pos = new Vector2(Pos.x + 100, Pos.y);
        bPos = new Vector2(bPos.x + 100, bPos.y);

        Vector2 oldPos = new Vector2(Pos.x - rbVel.x, Pos.y - rbVel.y);
        Vector2 oldBPos = new Vector2(bPos.x -bVel.x, bPos.y - bVel.y);


        float xPower = Mathf.Abs((oldBPos.x - oldPos.x) - (bPos.x - Pos.x)) * constant;

        xPower = Utilities.MinMax(xPower, 12, 30, true, true);

        barSizeV = new Vector3((Utilities.MapLinearRange(xPower, 12, 30, .02f, .9425f)), barSizeT.localScale.y, barSizeT.localScale.z);
        ComputeSpeed(barSizeV.x);



        Vector3 newToOld = new Vector3(bPos.x - oldPos.x, bPos.y - oldPos.y, 0);
        Vector3 newToNew = new Vector3(bPos.x - Pos.x, bPos.y - Pos.y, 0);
        float yPower = Utilities.getAngleTo(newToOld, newToNew)/yConstant - rbVel.x / 3 + 10;

        return new Vector2(xPower, yPower);
    }

    public void ComputeSpeed(float xSize)
    {
        difference = xSize - barSizeT.localScale.x;
        requiredTime = (xSize / .46f) * 2.5f;
        speedTime += Time.deltaTime;
        if (speedTime >= requiredTime)
        {
            speedTime = 0;
            startVal = barSizeT.localScale.x;
        }
            barSizeT.localScale = new Vector3(Utilities.MinMax(startVal, .02f, .9425f, true, true) + (difference * speedTime/requiredTime), barSizeT.localScale.y, barSizeT.localScale.z);




    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}


