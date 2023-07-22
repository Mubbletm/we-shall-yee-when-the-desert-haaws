using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMovement : MonoBehaviour
{
    public float MoveLimiter = .7f;
    public float Acceleration = 10f;
    public float MaxForce = 40f;
    public float RotationSpeed = .1f;

    private Rigidbody2D rigidbody2D;
    private float Rotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // This is only effective for keyboard controls.
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= MoveLimiter;
            vertical *= MoveLimiter;
        }

        Vector2 lookDirection = new Vector2(Mathf.Min(MaxForce, horizontal * Acceleration), Mathf.Min(MaxForce, vertical * Acceleration));

        rigidbody2D.AddForce(lookDirection);

        if (lookDirection.x != 0f || lookDirection.y != 0f)
        {
            Rotation = Mathf.Atan2(-lookDirection.normalized.y, -lookDirection.normalized.x) * Mathf.Rad2Deg;
        }
        rigidbody2D.MoveRotation(Mathf.LerpAngle(rigidbody2D.rotation, Rotation + 90, RotationSpeed));

    }
}
