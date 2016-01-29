using UnityEngine;
using System.Collections;

public class BearHead : MonoBehaviour
{
	public float angle;
	public float shootVelocity;
	public float retractVelocity;

	private bool extending;
	private bool retracting;
	private bool canShoot;

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
			transform.Translate(transform.up * shootVelocity * Time.deltaTime);

			if(transform.position.x >= 5 || transform.position.x <= -5 || transform.position.y >= 5)
			{
				extending = false;
				retracting = true;
			}
		}
		else if(retracting)
		{
			transform.Translate(transform.up * -retractVelocity * Time.deltaTime);

			if(transform.position.y <= defaultPosition.y)
			{
				transform.position = defaultPosition;
				transform.Rotate(0, 0, -angle);
				retracting = false;
				canShoot = true;
			}
		}
	}

	void FireHead()
	{
		if(canShoot)
		{
			transform.Rotate(0, 0, angle);
			extending = true;
			canShoot = false;
		}
	}
}
