using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tutorail : MonoBehaviour
{
    float time = 0;
    bool hasUsedArrows = false;
    bool hasEndedArrows = false;
    bool hasUsedSpace = false;
    public Text theText;
    public bool skipTutorial = false;
    public bool allowForMonsters = false;
    public bool canPressPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void SaySomething(string text)
    {
        theText.text = text.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (skipTutorial == false)
        {
            if (time >= 4 && hasUsedArrows == false)
            {
                SaySomething("Use the arrow keys to move!");
            }
            if (time >= 4 && hasUsedArrows == true)
            {
                hasEndedArrows = true;
                SaySomething("Use space to jump!");
            }
            if (time >= 4 && hasUsedSpace == true)
            {
                allowForMonsters = true;
                SaySomething("Avoid the eagle, slime, and other monsters!");
                if (time >= 8)
                {
                    SaySomething("Have Fun!!!");
                    if (time >= 10)
                    {
                        canPressPlay = true;
                    }
                }
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && time >= 4 && !hasUsedArrows)
            {
                hasUsedArrows = true;
                time = 0;
            }
            if (Input.GetKey(KeyCode.Space) && hasEndedArrows == true && hasUsedSpace == false)
            {
                hasUsedSpace = true;
                time = 0;
            }
        }
        else
        {
            allowForMonsters = true;
            SaySomething("");
            canPressPlay = true;
        }

    }
}
