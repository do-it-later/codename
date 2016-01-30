using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    //
    float SwimSpeed = 20.0f;
    float StrafeSpeed = 10.0f;
    public float JumpForce = -2000.0f;

    //
    private bool hasJumped;
    private Rigidbody rigidBody;

    void Start()
    {
        hasJumped = false;
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 v = this.transform.position;

        v.z -= SwimSpeed * Time.deltaTime;

        if (!hasJumped)
        { 
            if (Input.GetKey("a"))
                v.x -= StrafeSpeed * Time.deltaTime;

            if (Input.GetKey("d"))
                v.x += StrafeSpeed * Time.deltaTime;

            if (Input.GetKeyDown("space"))
            {
                hasJumped = true;

				rigidBody.useGravity = false;
                rigidBody.AddForce(new Vector3(0.0f, 0.0f, JumpForce));
            }
        }

        this.transform.position = v;
    }
}
