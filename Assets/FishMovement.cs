using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    private bool hasJumped;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        hasJumped = false;
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = this.transform.position;

        v.z -= 20.0f * Time.deltaTime;

        if (!hasJumped)
        { 
            if (Input.GetKey("a"))
                v.x -= 10.0f * Time.deltaTime;

            if (Input.GetKey("d"))
                v.x += 10.0f * Time.deltaTime;

            if (Input.GetKeyDown("space"))
            {
                hasJumped = true;

                rigidBody.AddForce(new Vector3(0.0f, 500.0f, 0.0f));
            }
        }

        this.transform.position = v;
    }
}
