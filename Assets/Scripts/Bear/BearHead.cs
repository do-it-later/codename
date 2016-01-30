using UnityEngine;
using System.Collections;

public class BearHead : MonoBehaviour
{
	public float shootVelocity;
	public float retractVelocity;

	private bool extending;
	private bool retracting;
	private bool canShoot;

	private float angle;
	private Vector3 direction;
	private Vector3 defaultPosition;

	// Use this for initialization
	void Start()
	{
		extending = false;
		retracting = false;
		canShoot = true;

		defaultPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("space"))
		{
			FireHead();
		}

		if(extending)
		{
			transform.Translate(direction * shootVelocity * Time.deltaTime);

			if(transform.position.x >= 5 || transform.position.x <= -5 || transform.position.y >= 5)
			{
				extending = false;
				retracting = true;
			}
		}
		else if(retracting)
		{
			transform.Translate(direction * -retractVelocity * Time.deltaTime);

			if(transform.position.y <= defaultPosition.y)
			{
				transform.position = defaultPosition;
				transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
				retracting = false;
				canShoot = true;
			}
		}

		Debug.Log(transform.up);
	}

	void FireHead()
	{
		if(canShoot)
		{
			angle = InputHelper.instance.GetAngle(1) - 90;

			if(angle > 90 && angle <= 180)
			{
				angle = 90;
			}
			else if(angle > 180 && angle < 270)
			{
				angle = 270;
			}

			direction = new Vector3(0.0f, 1.0f, 0.0f);
			direction = transform.rotation * direction;
			direction.Normalize();
			transform.Rotate(0, 0, angle);
			extending = true;
			canShoot = false;
		}
	}
}
