using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //public int accelerationFrames = 5;
    public float speed = 0.5f;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;


    //private float actualSpeed = 0f;


    // Update is called once per frame
    void Update()
    {
        bool moving = false;
        Vector3 movement = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(left))
        {
            moving = true;
            movement += new Vector3(-1f, 0f, 0f);
        }
        if (Input.GetKey(right))
        {
            moving = true;
            movement += new Vector3(1f, 0f, 0f);
        }
        if (Input.GetKey(down))
        {
            moving = true;
            movement += new Vector3(0f, -1f, 0f);
        }
        if (Input.GetKey(up))
        {
            moving = true;
            movement += new Vector3(0f, 1f, 0f);
        }

        if (moving)
        {
            //if (actualSpeed == 0f)
            //{
            //    actualSpeed = speed / accelerationFrames;
            //}
            //else if (actualSpeed < speed)
            //{
            //    actualSpeed += speed / accelerationFrames;
            //}

            //Debug.Log(actualSpeed);
            
            //movement.Scale(new Vector3(actualSpeed, actualSpeed, actualSpeed));
            movement.Scale(new Vector3(speed, speed, speed));
            transform.Translate(movement);
        }
        //else
        //{
        //    actualSpeed = 0f;
        //}
    }
}
