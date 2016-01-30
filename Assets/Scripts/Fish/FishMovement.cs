using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    //
    public float swimSpeed = 20.0f;
    public float strafeSpeed = 10.0f;

	public Vector3 direction;
    private Vector3 initDirection;
    private Vector3 initPosition;

	public int playerNumber;
	public bool inControl;

    //
    private bool hasJumped;
	private bool canJump;

    void Start()
    {
        initPosition = gameObject.transform.position;
        initDirection = direction; 

        hasJumped = false;
		canJump = false;

		direction.Normalize();
    }

    void Update()
    {
        //Time.timeScale = 0.1f;

        if (!hasJumped)
        {
            //if (Input.GetKey("a"))
            //	direction.x = -strafeSpeed;

            //if (Input.GetKey("d"))
            //	direction.x = strafeSpeed;

            if (Input.GetKeyDown(InputHelper.instance.GetInputButtonString(playerNumber, InputHelper.Button.B)) || Input.GetKeyDown("space"))
			{
				if(!hasJumped && canJump)
				{
					hasJumped = true;

                    // TODO: controller directional input
                    //float horiz = InputHelper.instance.GetHorizForController(1) * Time.deltaTime;
                    Vector3 screenWorldCoordinates = PerspectiveScreenToWorld();

                    //TODO: remove DEBUG
                    //GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = mP;

                    direction = screenWorldCoordinates - transform.position;

                    direction.Normalize();
				}
            }
        }

		this.transform.position += direction * Time.deltaTime * swimSpeed;

		if(transform.position.z <= -96)
		{
            ResetPosition();
//            GameManager.instance.SalmonFlee();
            ObjectPool.instance.PoolObject(gameObject);
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Jump Box")
		{
			canJump = true;
		}
		else if(other.tag == "Bear")
        {
            ResetPosition();
//            GameManager.instance.SalmonCaught();
            ObjectPool.instance.PoolObject(gameObject);
			Debug.Log("YAY");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Jump Box")
		{
			canJump = false;
            if( !hasJumped )
            {
//                GameManager.instance.SalmonCrash();
                ResetPosition();
                ObjectPool.instance.PoolObject(gameObject);
            }
		}
	}

    private Vector3 PerspectiveScreenToWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, -Camera.main.transform.position.y, -80.0f));

        float distance;
        xy.Raycast(ray, out distance);

        return ray.GetPoint(distance);
    }

    private void ResetPosition()
    {
        transform.position = initPosition;
        direction = initDirection;
        direction.Normalize();

        hasJumped = false;
    }
}
