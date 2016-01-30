using UnityEngine;
using System.Collections;

public class BearHead : MonoBehaviour
{
	public float shootVelocity;
	public float retractVelocity;

	public bool onLeft;

	public int playerNumber;

	public GameObject image;

	private bool extending;
	private bool retracting;
	private bool canShoot;
	private bool flipped;

	private float angle;
	private Vector3 direction;
	private Vector3 defaultPosition;

	// Use this for initialization
	void Start()
	{
		extending = false;
		retracting = false;
		canShoot = true;
		flipped = !onLeft;

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
				if(flipped)
				{
					transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
				}
				else
				{
					transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
				}
				transform.position = defaultPosition;
				image.transform.Rotate(0.0f, 0.0f, -20.0f);
				retracting = false;
				canShoot = true;
			}
		}
	}

	void FireHead()
	{
		if(canShoot)
		{
			angle = InputHelper.instance.GetAngle(playerNumber) - 90;

			if(angle > 90 && angle <= 180)
			{
				angle = 90;
			}
			else if(angle > 180 && angle < 270)
			{
				angle = 270;
			}

			if(angle > 0 && angle <= 90)
			{
				if(onLeft)
				{
					transform.parent.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					flipped = true;
				}
				else
				{
					transform.parent.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
					flipped = false;
				}
			}
			else
			{
				if(onLeft)
				{
					transform.parent.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
					flipped = false;
				}
				else
				{
					transform.parent.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
					flipped = true;
				}
			}

			Debug.Log(angle);

			direction = new Vector3(0.0f, 1.0f, 0.0f);
			direction = transform.rotation * direction;
			if(flipped)
				angle *=  -1;
			direction.Normalize();
			transform.Rotate(0, 0, angle);
			image.transform.Rotate(0.0f, 0.0f, 20.0f);
			extending = true;
			canShoot = false;
		}
	}
}
