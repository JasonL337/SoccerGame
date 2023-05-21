using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float m_speedBonus = 0;
    public Material mat;
    public Animator animate;
    public GameObject oppossum;
    public Material defaultMat;
    public string key = "";
    public float cdt;
    public float cd;
    public float shownCd;
    public bool canActivate = false;
    public float totalSpellTime = 0;
    public Rigidbody2D[] allRB;
    public Vector2[] rbNum;
    public GameObject[] allGO;
    public bool pauseRb;
    string lastAt = "Wake";
    public RectTransform rBox;
    public utilities Ut;
    public int timeFactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        animate = oppossum.GetComponent<Animator>();
    }

    //////////////////////////////////////////////////////////
    public void animPlay()
    {
        if (key != "")
        {
            animate.SetBool(key, true);
        }
    }

    //////////////////////////////////////////////////////////
    public void setMat()
    {
        oppossum.gameObject.GetComponent<SpriteRenderer>().material = mat;
    }


    //////////////////////////////////////////////////////////
    public virtual void AbilityPressed()
    {


        if (cdt >= cd)
        {
            canActivate = true;
        }
        else
        {
            canActivate = false;
            key = "";
        }

        if (pauseRb == true && lastAt == "Wake")
        {
            for (int i = 0; i < allRB.Length; i++)
            {
                allRB[i] = allGO[i].GetComponent<Rigidbody2D>();
                rbNum[i] = allRB[i].velocity;
                allRB[i].constraints = RigidbodyConstraints2D.FreezePosition;
                lastAt = "Sleep";
            }

        }
        else if (pauseRb == false && totalSpellTime > 0 && lastAt == "Sleep")
        {
            for (int i = 0; i < allRB.Length; i++)
            {
                allRB[i] = allGO[i].GetComponent<Rigidbody2D>();
                allRB[i].constraints = RigidbodyConstraints2D.None;
                allRB[i].velocity = rbNum[i];
                lastAt = "Wake";
            }
        }

        


    }



    // Update is called once per frame
    public virtual void Update()
    {
        if (cdt <= shownCd)
        {
            rBox.transform.localScale = new Vector3(rBox.transform.localScale.x, Ut.MapLinearRange(cdt, 0, shownCd, 0, .1f), rBox.transform.localScale.z);
        }
        cdt += Time.deltaTime * timeFactor;
    }
}


