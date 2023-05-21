using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleSlimeMove : MonoBehaviour
{
    public Transform slime;
    public GameObject slimeGO;
    float r = 0;
    float time = 0;
    float factor = .5f;
    float m_xFactor = .1f;
    float m_speedXSize = 3;
    float m_xStartSize = 0;
    float m_xSize = 0;
    Rigidbody2D rb;
    public Tutorail tutorial;
    public SpriteRenderer sr;
    public BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        m_xStartSize = slime.transform.localScale.x;
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }
    /// ////////////////////////////////////////////////
    void MoveSlime(float speed, float rotationY)
    {
        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
        slime.transform.localEulerAngles = new Vector3(0, rotationY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial.allowForMonsters == true)
        {
            sr.enabled = true;
            bc.enabled = true;
            rb = GetComponent<Rigidbody2D>();
            time += Time.deltaTime;
            r = Random.value;
            if (Mathf.Cos(time * factor) < 0)
            {
                MoveSlime(1.5f, 0);

            }
            else
            {
                MoveSlime(-1.5f, 180);
            }
            m_xSize = Mathf.Cos(time * m_speedXSize) * m_xFactor + m_xStartSize;
            slime.transform.localScale = new Vector3(m_xSize, slime.transform.localScale.y, 0);
        }
        else
        {
            sr.enabled = false;
            bc.enabled = false;
        }
    }
}
