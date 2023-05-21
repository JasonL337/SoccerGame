using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEagleV2 : MonoBehaviour
{
    public Transform Eagle;
    public Transform Opposum;
    float beginY;
    float beginX;
    float VertexX = 0;
    float VertexY = 0;
    float timeout = 0;
    bool left = true;
    public GameObject EagleGO;
    public Sprite ArmsUp;
    public Sprite ArmsMiddle;
    public Sprite ArmsDown;
    float flyTime = 0;
    float anim_Speed = 5;
    bool hasTimedOut = false;
    float a = 0;
    float r = 0;
    float x = 0;
    float diffInX = 0;
    float decreaseFactor = .004f;
    float vertexEagleDiff = 0;
    float speed = 0;
    bool hasHitVertex = false;
    float vertexEagleYDiff = 0;
    float m_aMin = .1f;
    float m_yOldPos = 0;
    float m_yNewPos = 0;
    bool m_doneGettingVertex = false;
    public Tutorail tutorial;
    public SpriteRenderer sr;
    public BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        beginX = Eagle.transform.localPosition.x;
        beginY = Eagle.transform.localPosition.y;
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
    void ResetSwoop(string comingFrom)
    {
        timeout = 0;
        a = 0;
        hasHitVertex = false;
        m_doneGettingVertex = false;
        hasTimedOut = false;

        if (comingFrom == "onRight")
        {
            beginX = Opposum.transform.localPosition.x + 10;
        }
        else
        {
            beginX = Opposum.transform.localPosition.x - 10;
        }

        Eagle.transform.localPosition = new Vector3(beginX, beginY, -1);
        diffInX = Eagle.transform.localPosition.x - Opposum.transform.localPosition.x;
        r = Random.value;
        x = beginX;



    }
   
    /////////////////////////////////////////////////////////////////////////////////////////
    void Swoop(float diffX, float diffY, float maxSpeed, float minSpeed)
    {

        timeout += Time.deltaTime;

        if (timeout < 3f)
        {   
            VertexX = diffX + Opposum.transform.localPosition.x;
            VertexY = diffY + Opposum.transform.localPosition.y;
            a = (Eagle.transform.localPosition.y - VertexY) / ((Eagle.transform.localPosition.x - VertexX) * (Eagle.transform.localPosition.x - VertexX));
            vertexEagleDiff = Eagle.transform.localPosition.x - VertexX;
            vertexEagleYDiff = Eagle.transform.localPosition.y - VertexY;
        }
        else
        {
            m_doneGettingVertex = true;
        }

        speed = ((Mathf.Abs(vertexEagleDiff)) * decreaseFactor);  

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed < minSpeed)
        {
            speed = minSpeed;
        }

        if ((timeout > 7.5f) || Eagle.transform.localPosition.y < -.3)
        {
            VertexX = Eagle.transform.localPosition.x;
            VertexY = Eagle.transform.localPosition.y;
            a = 10f;
            speed = .025f;
            hasTimedOut = true;
        }

        if (vertexEagleDiff > 0)
        {
            x = x - speed * timeout;
        }
        else
        {
            x = x + speed * timeout;
        }

        if (a < m_aMin)
            a = m_aMin;

        m_yOldPos = Eagle.transform.localPosition.y;

        Eagle.transform.localPosition = new Vector3(x, a * (x - VertexX) * (x - VertexX) + VertexY, -1);

        m_yNewPos = Eagle.transform.localPosition.y;

        if (m_yOldPos < m_yNewPos && m_doneGettingVertex)
        {
            hasHitVertex = true;
           // Debug.Log("OldPos" + m_yOldPos);
         //   Debug.Log("NewPos " + m_yNewPos);
        }
       // Debug.Log("beginY " + beginY);

        if ((Eagle.transform.localPosition.y > beginY - 6 && (hasHitVertex == true || hasTimedOut == true)) || Eagle.transform.localPosition.y < -1)
        {
            if (r > .5)
            {
                ResetSwoop("onRight");
            }
            else
            {
                ResetSwoop("onLeft");
            }
        }

        if (Opposum.transform.localPosition.x + diffX > Eagle.transform.localPosition.x && hasHitVertex == false)
        {
            Eagle.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (Opposum.transform.localPosition.x + diffX <= Eagle.transform.localPosition.x && hasHitVertex == false)
        {
            Eagle.transform.localEulerAngles = new Vector3(0, 0, 0);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial.allowForMonsters == true)
        {
            sr.enabled = true;
            bc.enabled = true;
            flyTime += Time.deltaTime;
            if (Mathf.Cos(flyTime * anim_Speed) >= .33)
            {
                EagleGO.gameObject.GetComponent<SpriteRenderer>().sprite = ArmsUp;
            }
            else if (Mathf.Cos(flyTime * anim_Speed) < .33 && Mathf.Cos(flyTime * anim_Speed) >= -.33)
            {
                EagleGO.gameObject.GetComponent<SpriteRenderer>().sprite = ArmsMiddle;
            }
            else
            {
                EagleGO.gameObject.GetComponent<SpriteRenderer>().sprite = ArmsDown;
            }

            Swoop(1, 1f, .025f, .0001f);
        }
        else
        {
            sr.enabled = false;
            bc.enabled = false;
        }
    }
}
