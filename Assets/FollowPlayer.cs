using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform camera;
    public Transform oppossum;
    float xDiff = 0;
    float yDiff = 0;
    float cameraShouldBePosX = 0;
    float time_Out_Of_View = 0;
    float speed = 2;
    float distanceFactor = 2.14f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// ///////////////////////////////////////////////////
    void MoveCam(float xMult)
    {
        time_Out_Of_View += Time.deltaTime;
        float catchup = Mathf.Clamp01(time_Out_Of_View / 1f);
        Vector3 vWant = new Vector3(camera.transform.localPosition.x + (xDiff + xMult * distanceFactor), camera.transform.localPosition.y, -10);

        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, vWant, catchup);
    }

    // Update is called once per frame
    void Update()
    {
        xDiff = oppossum.transform.localPosition.x - camera.transform.localPosition.x;
        yDiff = oppossum.transform.localPosition.y - camera.transform.localPosition.y;

        
            if (xDiff <= -distanceFactor)
            {
                MoveCam(1f);
            }
            else if (xDiff >= distanceFactor)
            {
                MoveCam(-1f);
            }
            else
            {
                time_Out_Of_View = 0;
            }
        
    }
}
