using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEagle : MonoBehaviour
{
    public Transform Eagle;
    public Transform Opposum;
    float beginY;
    float beginX;
    float diffTime = 0;
    float VertexX = 0;
    float VertexY = 0;
    float timeout = 0;
    float beginRightX = 0;
    bool left = true;
    public GameObject EagleGO;
    public Sprite ArmsUp;
    public Sprite ArmsMiddle;
    public Sprite ArmsDown;
    float flyTime = 0;
    float anim_Speed = 5;
    float a = 0;
    float r = 0;
    float lr = 0;
    // Start is called before the first frame update
    void Start()
    {
        beginX = Eagle.transform.localPosition.x;
        beginY = Eagle.transform.localPosition.y;
        beginRightX = beginX + 30;
        Debug.Log(Eagle.transform.localPosition.y);
    }
    void Swoop(float diffX, float diffY, float speed)
    {
        timeout += Time.deltaTime;
        diffTime += Time.deltaTime;
        float x = 0;
        if (timeout < 2f)
        {
            VertexX = diffX + Opposum.transform.localPosition.x;
            VertexY = diffY + Opposum.transform.localPosition.y;
            a = (Eagle.transform.localPosition.y - VertexY) / ((Eagle.transform.localPosition.x - VertexX) * (Eagle.transform.localPosition.x - VertexX));
        }
        else if (timeout >= 7 && timeout <= 7.5f || Eagle.transform.localPosition.y < -.3)
        {
            if (Eagle.transform.localEulerAngles.y == 180)
            {
                VertexX = Eagle.transform.localPosition.x;
                VertexY = Eagle.transform.localPosition.y;
                a = .15f;
            }
            else
            {
                VertexX = Eagle.transform.localPosition.x;
                VertexY = Eagle.transform.localPosition.y;
                a =.15f;
            }
            Debug.Log("a" + a);
            Debug.Log("VertexX" + VertexX);
        }

        if (Mathf.Abs(VertexX - beginX) < 3 && left == true)
            speed = (VertexX - beginX)/3 * speed;
        else if (left == false && Mathf.Abs(VertexX - beginRightX) < 3)
        x = beginRightX - diffTime * speed;
        else
        x = beginX + diffTime * speed;

        Debug.Log("x" + x);

        Eagle.transform.localPosition = new Vector3(x, a*(x - VertexX)*(x - VertexX) + VertexY, 0);

        if (Eagle.transform.localPosition.y > beginY)
        {
            r = Random.value;
            if (r < .5)
            {
                diffTime = 0;
                Eagle.transform.localPosition = new Vector3(beginX, beginY, 0);

                if (Eagle.transform.localPosition.x < Opposum.transform.localPosition.x)
                {
                    Eagle.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    Eagle.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                    timeout = 0;
                left = true;
            }
            else
            {
                if (Eagle.transform.localPosition.x < Opposum.transform.localPosition.x)
                {
                    Eagle.transform.localEulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    Eagle.transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                diffTime = 0;
                Eagle.transform.localPosition = new Vector3(beginRightX, beginY, 0);
                timeout = 0;
                left = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
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
        
        Swoop(1, .5f, 6);
    }
}
