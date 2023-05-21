using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goalscored : MonoBehaviour
{
    public int isGoal = 0;
    public Text goalText;
    public float textTime = 0;
    public bool reset = false;
    public GameObject goalLeft;
    public GameObject goalRight;
    public int pointsLeft = -1;
    public int pointsRight = -1;
    public GameObject[] ballsLeft;
    public GameObject[] ballsRight;


    // Start is called before the first frame update 
    void Start()
    {
        
    }

    //////////////////////////////////////////////////////////////////
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ball")
        {
            //isGoal = true;
        }
    }

    //////////////////////////////////////////////////////////////////
    void OnTriggerExit2D(Collider2D other)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoal == -1)
        {
            goalText.text = "Enemy Scored!";
            goalText.color = new Color(255, 0, 0);
            textTime += Time.deltaTime;
        }
        if (isGoal == 1)
        {
            goalText.text = "You Scored!";
            goalText.color = new Color(0, 255, 0);
            textTime += Time.deltaTime;
        }
        if (textTime >= 2.5f)
        {
            goalText.text = "";
            textTime = 0;
            reset = true;
            isGoal = 0;
        }
        else
        {
            reset = false;
        }
        if (pointsLeft != -1 && pointsLeft < 3)
        {
            ballsLeft[pointsLeft].SetActive(true);
        }
        if (pointsRight != -1 && pointsLeft < 3)
        {
            ballsRight[pointsRight].SetActive(true);
        }
    }
}
