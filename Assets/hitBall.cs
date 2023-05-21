using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBall : MonoBehaviour
{
    public bool hasHitBall = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// ////////////////////////////////////////////////////////////////////////
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ball")
        {
            hasHitBall = true;
        }
    }

    /// ////////////////////////////////////////////////////////////////////////
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ball")
        {
            hasHitBall = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
