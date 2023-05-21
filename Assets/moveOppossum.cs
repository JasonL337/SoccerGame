using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class moveOppossum : MonoBehaviour
{
    public Goalscored goalScoredScript;
    Rigidbody2D rb;
    public Transform Opposum;
    public GameObject OpposumGO;
    float jumpDeltaTime = 0;
    bool isJumpPress = false;
    bool isContact = false;
    Vector3 oldOPos;
    bool isJumping = false;
    float walkTime = 0;
    public Sprite fullExtension;
    public Sprite together;
    float jumpPressTime = 0;
    bool MoreUp = true;
    bool touchingGround = true;
    bool hasPressedSpace = false;
    int touchCount = 0;
    public moveBall ball;
    public Transform ballT;
    public hitBall hb;
    public utilities Utilities;
    float holdTime = 0;
    bool left = false;
    public Rigidbody2D ballRB;
    bool canJump = true;
    bool pressedSpace = false;
    bool resetJump;
    StateName state = StateName.idle;
    float rotY = 0;
    float stateTime = 0;
    public Transform barT;
    public float maxHoldTime = 0;
    public GameObject bigPaw;
    public GameObject frontPaw;
    public hitBall bigHit;
    float noMoreDribble = 1;
    bool canDribble = false;
    Vector3 ballVelocity;
    int direction = 0;
    public Mathematics mathematics = new Mathematics();
    Vector2 shotV;
    Vector3 fourSPos;
    public Animator backInTime;
    public Material twistMat;
    public Material regularMat;
    public GameObject emptyBar;
    public Transform hologram;
    public ChoosePower choosePowerScript;
    public String abilityFinder;
    float speed = 10;
    public float speedBonus = 10;
    public float coolDownTime = 0;
    public List<float> coolDown = new List<float>();
    public Transform coolDownPanel;
    public TimeCooldown timeCScript;
    public Ability abilityScript;
    public Ability[] abilities;
    public GameObject[] abilityGO;
    public bool ispressingR = false;




    /// ////////////////////////////////////////////////////////////////////
    void Start()
    {
        emptyBar.GetComponent<SpriteRenderer>().material = regularMat;
        Opposum.GetComponent<SpriteRenderer>().material = regularMat;
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        ballRB = ballRB.GetComponent<Rigidbody2D>();
        bigPaw.SetActive(false);
        coolDown.Add(1);
    }

    public enum StateName
    {
        hold,
        idle,
        shoot,
        ability,
    }

    void SwitchState(StateName nextState)
    {
        stateTime = 0;
        state = nextState;
    }

    /// ///////////////////////////////////////////////////////////////////////
    void MoveLeftRight(string direction, float speed)
    {
        float speedOfWalk = 15f;

        if (direction != "Don't Move")
        {
            walkTime += Time.deltaTime;
            if (Mathf.Cos(walkTime * speedOfWalk) > 0)
            {
                OpposumGO.gameObject.GetComponent<SpriteRenderer>().sprite = fullExtension;
            }
            else if (Mathf.Cos(walkTime * speedOfWalk) < 0)
            {
                OpposumGO.gameObject.GetComponent<SpriteRenderer>().sprite = together;
            }
        }
        else
        {
            walkTime = 0;
            OpposumGO.gameObject.GetComponent<SpriteRenderer>().sprite = together;
        }

       /* if (direction == "left")
        {
            speed = -1 * Mathf.Abs(speed);
            Opposum.localEulerAngles = new Vector3 (0, 0, 0);
        }

        if (direction == "right")
        {
            speed = Mathf.Abs(speed);
            Opposum.localEulerAngles = new Vector3(0, 180, 0);
        }

        if (direction == "Don't Move")
            speed = 0;

        rb.velocity = new Vector3(speed, rb.velocity.y - 1, 0);
        */
    }

    /// /////////////////////////////////////////////////////////////////////////
    void ResetJump()
    {
        MoreUp = true;
        hasPressedSpace = false;
        isJumpPress = false;
        jumpDeltaTime = 0;
        jumpPressTime = 0;
        isJumping = false;
        isContact = false;
        canJump = true;
        resetJump = true;
    }

    /// ////////////////////////////////////////////////////////////////////////
    void Jump (float bonusPower)
    {
        jumpDeltaTime += Time.deltaTime;

        rb.velocity = new Vector3(rb.velocity.x, bonusPower, 0);

            isJumping = true;
        

        if (isContact == true)
        {
            ResetJump();
        }

    }

    /// ////////////////////////////////////////////////////////////////////////
    public void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag != "barrier" && other.gameObject.tag != "ball") && isJumping == true)
        {
            isContact = true;
        }
        if (other.gameObject.tag != "barrier" || other.gameObject.tag != "ball")
        {
            touchCount += 1;
        }



    }


    /// ////////////////////////////////////////////////////////////////////////
    public void OnCollisionExit2D(Collision2D other)
    {
        if (isJumping == true)
        {
            isContact = false;
        }
        if (other.gameObject.tag != "barrier" || other.gameObject.tag != "ball")
        {
            touchCount -= 1;
        }
    }

    /// ////////////////////////////////////////////////////////////////////////
    public void HoldBall(float yRotation, float holdPos)
    {
        barT.transform.localScale = new Vector3(stateTime * (1/maxHoldTime) * .01f , barT.transform.localScale.y, barT.transform.localScale.z);
        ballT.transform.localPosition = new Vector3(transform.localPosition.x + holdPos, ballT.transform.localPosition.y, ballT.transform.localPosition.z);
        Opposum.transform.localEulerAngles = new Vector3(0, yRotation, 0);
    }

    /// ////////////////////////////////////////////////////////////////////////
    public void Idle()
    {
        barT.transform.localScale = new Vector3(0, barT.transform.localScale.y, barT.transform.localScale.z);
    }

    /// ////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (choosePowerScript.ability.Count >= 1)
          abilityFinder = choosePowerScript.ability[0];

        // Direction
        if (transform.localEulerAngles.y == 180)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        // Touching Ground?

        if (goalScoredScript.reset == true)
        {
            transform.localPosition = new Vector3(-17.57f, 0, 0);
        }
        if (touchCount >= 1)
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }





        // Moving


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (left)
            {
                holdTime += Time.deltaTime;
            }
            else
                holdTime = 0;

            rb.velocity = new Vector3(Utilities.Blend(-10 - speedBonus, 3, holdTime, -speed - speedBonus, -1), rb.velocity.y, 0);
            MoveLeftRight("left", 0);
            transform.localEulerAngles = new Vector3(0, 0, 0);
            left = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!left)
            {
                holdTime += Time.deltaTime;
            }
            else
                holdTime = 0;

            rb.velocity = new Vector3(Utilities.Blend(speed + speedBonus, 3, holdTime, speed + speedBonus, 1), rb.velocity.y, 0);
            MoveLeftRight("right", 0);
            transform.localEulerAngles = new Vector3(0, 180, 0);
            left = false;
        }
        else
        {
            holdTime = 0;
            rb.velocity = new Vector3(Utilities.Blend(0,0,0,0,0), rb.velocity.y, 0);
        }


        //Dribbling
        if (Input.GetKey(KeyCode.W) && (hb.hasHitBall || bigHit.hasHitBall) && stateTime <= maxHoldTime && canDribble)
        {
            if (stateTime == 0)
            {
                noMoreDribble = 0;
                SwitchState(StateName.hold);
                bigPaw.SetActive(true);
                rotY = transform.localEulerAngles.y;
                frontPaw.SetActive(false);
            }
            ball.hitBall(0, 0);
        }
        else
        {
            noMoreDribble += Time.deltaTime;
            if (noMoreDribble >= 1)
            {
                canDribble = true;
            }
            else
            {
                canDribble = false;
            }
            bigPaw.SetActive(false);
            frontPaw.SetActive(true);
            SwitchState(StateName.idle);
        }



        // Jumping

        if (touchingGround)
        {
            resetJump = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (resetJump == true)
            {
                jumpPressTime += Time.deltaTime;
                if (jumpPressTime >= .25)
                {
                    jumpPressTime = .25f;
                    resetJump = false;
                    canJump = false;
                    isJumpPress = false;
                }
                else
                {
                    isJumpPress = true;
                    canJump = true;
                }
            }

        }
        else if (!Input.GetKey(KeyCode.UpArrow))
        {
            jumpPressTime = 0;
            resetJump = false;
            canJump = false;
            isJumpPress = false;
        }


        if (isJumpPress == true && canJump == true)
            Jump(10);



        // Shooting


        if (Input.GetKey(KeyCode.Space) && hb.hasHitBall && Opposum.transform.localEulerAngles.y == 0)
        {
            state = StateName.shoot;
        }
        else if (Input.GetKey(KeyCode.Space) && hb.hasHitBall && Opposum.transform.localEulerAngles.y == 180)
        {
            state = StateName.shoot;
        }
        else
        {
            ballVelocity = new Vector3(ballRB.velocity.x, ballRB.velocity.y, 0);
        }



        // Rotation

        Opposum.transform.localEulerAngles = new Vector3(0, Opposum.transform.localEulerAngles.y, 0);

        // HasHitBall Check

        if (!hb.hasHitBall)
        {
            shotV = mathematics.Shot(rb.velocity, ballRB.velocity, transform.localPosition, ballT.transform.localPosition);
        }

        // Playing Ability

        switch (abilityFinder)
        {
            case "Speed":
                abilityScript = abilities[0];
                for (int i = 0; i < abilityGO.Length; i++)
                {
                    if (i != 0)
                    abilityGO[i].SetActive(false);
                }
                break;
            case "Object Phase":
                abilityScript = abilities[1];
                for (int i = 0; i < abilityGO.Length; i++)
                {
                    if (i != 1)
                    abilityGO[i].SetActive(false);
                }
                break;
            case "Time Traveler":
                abilityScript = abilities[2];
                for (int i = 0; i < abilityGO.Length; i++)
                {
                    if (i != 2)
                    abilityGO[i].SetActive(false);
                }
                break;

        }

        if (Input.GetKey(KeyCode.R))
        {
            ispressingR = true;
            abilityScript.AbilityPressed();
            speedBonus = abilityScript.m_speedBonus;
        }
        else
        {
            ispressingR = false;
            speedBonus = 0;
        }

        // State
        switch (state)
        {
            case StateName.hold:
                if (rotY == 180)
                {
                    HoldBall(rotY, 1.32f);
                }
                else
                {
                    HoldBall(rotY, -1.32f);
                }
                stateTime += Time.deltaTime;
                break;
            case StateName.idle:
                Idle();
                break;
            case StateName.shoot:
                ball.hitBall(shotV.x, shotV.y) ;
                break;
            case StateName.ability:
                
                break;
        }



    }
}







