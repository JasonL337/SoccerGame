using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTravel : Ability
{
    int tCount = 1;
    public Transform oppossumT;
    public Transform hologram;
    float animTime = 0;
    public Material timeMat;
    public float maxTime = 2;
    public float timeCoolDown;
    // Start is called before the first frame update
    void Start()
    {

    }

    //////////////////////////////////////////////////////////
    public override void AbilityPressed()
    {
        timeFactor = 1;
        cd = 10;
        shownCd = cd;
        base.AbilityPressed();
        if (canActivate)
        {
            totalSpellTime = 0;
            if (tCount % 2 == 1)
            {
                mat = defaultMat;
                key = "";
                animPlay();
                setMat();

                //    This is the cutoff for material/anim (stuff for superclass) (above) and stuff for time travel specifically (below)

                cdt = cd - 1;
                hologram.transform.localPosition = oppossumT.transform.localPosition;
                hologram.transform.localRotation = oppossumT.transform.localRotation;
            }
            else
            {
                pauseRb = true;
                mat = timeMat;
                key = "play";
                animPlay();
                setMat();

                //    This is the cutoff for material/anim (stuff for superclass) (above) and stuff for time travel specifically (below)

                cdt = 0;
            }
            tCount += 1;
            base.AbilityPressed();
        }
    }


    // ///////////////////////////////////////////////
    void doAnimMath()
    {
        totalSpellTime += Time.deltaTime;
        if (totalSpellTime >= 1f)
        {
            oppossumT.transform.localPosition = hologram.transform.localPosition;
            oppossumT.transform.localRotation = hologram.transform.localRotation;
        }
        if (totalSpellTime >= maxTime)
        {
            hologram.transform.localPosition = new Vector3(0, 100, 0);
            mat = defaultMat;
            setMat();
            oppossum.gameObject.GetComponent<SpriteRenderer>().material = mat;
            tCount = 1;
            pauseRb = false;
            base.AbilityPressed();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (tCount % 2 == 1 && tCount != 1) 
        doAnimMath();



    }
}
