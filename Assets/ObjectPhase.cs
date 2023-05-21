using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPhase : Ability
{
    float timeCoolDown = 5;
    public Material opMat;
    public moveOppossum mo;
    public bool phasingOn = false;
    public BoxCollider2D bc;
    public CircleCollider2D ballCol;
    public PolygonCollider2D slimeCol;

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
            mat = opMat;
            m_speedBonus = 0;
            Physics2D.IgnoreCollision(slimeCol, bc);
            Physics2D.IgnoreCollision(ballCol, bc);
            setMat();
        }
        else
        {
            mat = defaultMat;
            Physics2D.IgnoreCollision(slimeCol, bc, false);
            Physics2D.IgnoreCollision(ballCol, bc, false);
            setMat();
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
            Physics2D.IgnoreCollision(slimeCol, bc, false);
            Physics2D.IgnoreCollision(ballCol, bc, false);
        }
    }
}