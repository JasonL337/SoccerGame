using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Ability
{
    public float timeCoolDown;
    public moveOppossum mo;
    public Material speedMat;
    // Start is called before the first frame update
    void Start()
    {

    }

    //////////////////////////////////////////////////////////
    public override void AbilityPressed()
    {
        if (cdt <= 0)
        {
            cdt = -2;
        }
        timeFactor = -1;
        cd = 0;
        shownCd = timeCoolDown;
        base.AbilityPressed();

        if (canActivate)
        {
            mat = speedMat;
            m_speedBonus = 5;
            setMat();
        }
        else
        {
            setMat();
            mat = defaultMat;
            m_speedBonus = 0;
        }

    }
   
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (mo.ispressingR)
        {
            if (cdt >= 5)
            {
                cdt = 5;
            }
            timeFactor = 1;
        }
        else
        {
            mat = defaultMat;
            setMat();
        }

       
    }
}
