using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgainst : MonoBehaviour
{
    public moveBall ballScript;
    public Transform ballT;
    float dist;
    Rigidbody2D rb;
    bool hasHitGround = true;
    bool hasHitball = false;
    float jumpPower = 3;
    public Rigidbody2D oppossumRb;
    float randomBack = 0;
    public Transform oppossumT;
    float distO = 0;
    float touchCount = 1;
    public clickPlay play;
    float distY = 0;
    StateName state = StateName.idle;
    float horizSpeed = 0;
    bool hasAVal = false;
    public float jumpPowerEngineEdit = 0;
    float holdTime = 0;
    public Goalscored goalScoredScript;
    bool isTouchBorder = false;
    public moveOppossum moveO;
    public utilities Utilities;
    float timeInDist = 0;
    Distance distanceVal = Distance.none;
    float xPower = 0;
    float yPower = 0;
    float nextCheck = 0;
    float randomTime = 0;
    float rHoriz = 0;


    public enum StateName
    {
        hitBall,
        idle,
        reset,
        hitBallGround,

    }
    public enum Distance
    {
        none,
        fastLeft,
        mediumLeft,
        slowLeft,
        slowRight,
        fastRight,
        superSlowRight,
        glitchRight,
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void JumpToBall(float upPower, float sidePower)
    {
        state = StateName.idle;
        distanceVal = Distance.none;
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 2;
        }
        if (upPower != 0)
        {
            rb.velocity = new Vector3(sidePower, upPower, 0);
        }
        else
        {
            rb.velocity = new Vector3(sidePower, rb.velocity.y, 0);
        }

        CheckBallDist();
        CheckCanJump();
        CheckForHit();
        CheckForReset();
    }

    void CheckForReset()
    {
        if (goalScoredScript.reset == true)
        {
            state = StateName.reset;
        }
    }
    void CheckForHit()
    {
        if (hasHitball && distO > -3)
        {
            state = StateName.hitBall;
            xPower = -15 + (1 / distO) * 3;
            yPower = 12 + distO;
        }
        if (hasHitball && oppossumT.transform.localPosition.y > transform.localPosition.y + 2 || (distO > 2.5f && hasHitball))
        {
            state = StateName.hitBallGround;
        }

    }
    void CheckBallDist()
    {
        if (dist < -15)
        {
            timeInDist += Time.deltaTime;
            distanceVal = Distance.fastLeft;
            horizSpeed = Utilities.Blend(-4.5f, 4, timeInDist, -4.5f, 0);
        }
        else if (dist < -5)
        {
            hasAVal = true;
            distanceVal = Distance.mediumLeft;
            horizSpeed = -3.5f;
        }
        else if (dist < 0)
        {
            hasAVal = true;
            distanceVal = Distance.slowLeft;
            horizSpeed = -4f;
        }
        if (Time.time >= nextCheck)
        {
            randomTime = Time.time + .3f;
            nextCheck += 3f;
            rHoriz = Utilities.randomNum(-3, 7);
        }
        if (Time.time <= randomTime)
        {
            horizSpeed = rHoriz;
        }
        if (dist > 0)
        {
            hasAVal = true;
            distanceVal = Distance.slowRight;
            horizSpeed = 9;
        }
        if (dist > 15)
        {
            hasAVal = true;
            distanceVal = Distance.fastRight;
            horizSpeed = 13;
        }
        if ((Mathf.Abs(distO) - 2 < Mathf.Abs(dist) && oppossumRb.velocity.x > 0) || (distY > .3 && Mathf.Abs(dist) < .4f))
        {
            hasAVal = true;
            distanceVal = Distance.superSlowRight;
            horizSpeed = 3;
        }
        if (Mathf.Abs(dist) < .5f && distY > .1f)
        {
            hasAVal = true;
            distanceVal = Distance.glitchRight;
            horizSpeed = 2;
        }
        if (distanceVal == Distance.none)
        {
            horizSpeed = 0;
        }
    }

    void CheckCanJump()
    {
        if ((dist > 0 && dist < 4) && touchCount >= 1)
        {
            jumpPower = jumpPowerEngineEdit;
            rb.gravityScale = .5f;
        }
        else if ((moveO.hb.hasHitBall) && touchCount >= 1 && dist > -1)
        {
            jumpPower = 4.5f;
            rb.gravityScale = .5f;
            horizSpeed = -8;
        }
        else
        {
            jumpPower = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && play.startRound)
        {
            touchCount += 1;
        }
        if (other.gameObject.tag == "ball")
        {
            hasHitball = true;
        }
        if (other.gameObject.tag == "border")
        {
            isTouchBorder = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" && play.startRound)
        {
            touchCount -= 1;
        }
        if (other.gameObject.tag == "ball")
        {
            hasHitball = false;
        }
        if (other.gameObject.tag == "border")
        {
            isTouchBorder = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (play.startRound)
        {
            if (touchCount > 1)
            {
                touchCount = 1;
            }
            else if (touchCount < 0)
            {
                touchCount = 0;
            }
            if (hasHitball)
            {
                holdTime += Time.deltaTime;
            }
            else
            {
                holdTime = 0;
            }
            transform.localEulerAngles = new Vector3(0, 180, 0);
            rb = GetComponent<Rigidbody2D>();
            dist = Mathf.Abs(transform.localPosition.x) - Mathf.Abs(ballT.transform.localPosition.x);
            distO = oppossumT.transform.localPosition.x - ballT.transform.localPosition.x;
            distY = ballT.transform.localPosition.y - transform.localPosition.y;

            JumpToBall(jumpPower, horizSpeed);
            switch (state)
            {
                case StateName.hitBall:
                    ballScript.hitBall(xPower, yPower);
                    break;

                case StateName.reset:
                    transform.localPosition = new Vector3(-5.84f, .13f, -2.54f);
                    break;

                case StateName.hitBallGround:
                    ballScript.hitBall(-15, 0);
                    break;

            }
        }
    }
    }
