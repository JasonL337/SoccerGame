using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playgamesound : MonoBehaviour
{
    public AudioSource sound;
    public utilities UtScript;
    public Transform ball;
    public Transform goodGoal;
    public Transform badGoal;
    float goodDist;
    float badDist;
    public float raiseDist = 7;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // ///////////////////////////////////////////////
    void CalcVolume(float distance, float min, float max)
    {
        float clampAmt = max - min;
        float refAmt = raiseDist - distance;
        float val = raiseDist/clampAmt;
        sound.volume = refAmt/val + min;
    }

    // Update is called once per frame
    void Update()
    {
        goodDist = Vector3.Distance(new Vector3(goodGoal.transform.localPosition.x - 3, goodGoal.transform.localPosition.y, goodGoal.transform.localPosition.z), ball.transform.localPosition);
        badDist  = Vector3.Distance(badGoal.transform.localPosition, ball.transform.localPosition);
        if (goodDist < raiseDist)
        {
            CalcVolume(goodDist, .275f, 1);
        }
        if (badDist < raiseDist)
        {
            CalcVolume(badDist, .275f, 1);
        }
    }
}
