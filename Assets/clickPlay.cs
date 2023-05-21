using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickPlay : MonoBehaviour
{
    public Tutorail tutorial;
    public Transform button;
    public GameObject startScreen;
    public GameObject playScreen;
    public bool startRound = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ClickPlay()
    {
        startRound = true;
        button.transform.localPosition = new Vector3(0, -10000, 0);
        playScreen.SetActive(true);
        startScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial.canPressPlay && startRound != true)
        {
            button.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
