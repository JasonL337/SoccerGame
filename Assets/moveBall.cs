using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBall : MonoBehaviour
{
    public Goalscored goalScoredScript;
    float slopeTime = 0;
    Rigidbody2D rb;
    public Transform ballTransform;
    float a = 0;
    float b = 0;
    float yOne = 0;
    float xOne = 0;
    public float slope = 0;
    public bool goleft = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void hitBall(float powerX, float powerY)
    {
        // a = (ballTransform.transform.localPosition.y - vertexY) / Mathf.Pow(ballTransform.transform.localPosition.x - vertexX, 2);
        // b = vertexY / vertexX - a * vertexX;
        // rb.velocity = new Vector3()
        rb.velocity = new Vector3(powerX, powerY - rb.gravityScale, 0);
    }

    /// ////////////////////////////////////////////////////////////////////////
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "barrier")
        {
            rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, 0);
        }
    }

    /// ////////////////////////////////////////////////////////////////////////
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "goalLeft" || other.gameObject.tag == "goalRight")
        {
            goalScoredScript.isGoal = 0;
        }
    }

    /// ////////////////////////////////////////////////////////////////////////
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "goalLeft" && goalScoredScript.textTime == 0 && goalScoredScript.isGoal != -1)
        {
            goalScoredScript.isGoal = -1;
            goalScoredScript.pointsRight += 1;
        }
        if (other.gameObject.tag == "goalRight" && goalScoredScript.textTime == 0 && goalScoredScript.isGoal != 1)
        {
            goalScoredScript.isGoal = 1;
            goalScoredScript.pointsLeft += 1;
            Debug.Log(goalScoredScript.pointsLeft);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (goalScoredScript.reset == true)
        {
            transform.localPosition = new Vector3(-13.06f, 0, .7f);
            rb.velocity = new Vector3(0, 0, 0);
        }
        slopeTime += Time.deltaTime;
        if (slopeTime > .1)
        {
            slope = (transform.localPosition.y - yOne) / (transform.localPosition.x - xOne + .001f);
            slopeTime = 0;
        }
        yOne = transform.localPosition.y;
        xOne = transform.localPosition.x;
        if (goleft == true)
        {
            rb.velocity = new Vector3(-7, rb.velocity.y, 0);
        }
    }
}
