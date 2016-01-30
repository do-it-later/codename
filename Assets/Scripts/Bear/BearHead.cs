using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	private float lastDistance;

	private List<GameObject> neckList;

	// Use this for initialization
	void Start()
	{
		extending = false;
		retracting = false;
		canShoot = true;
		flipped = !onLeft;

		lastDistance = 0.0f;

		neckList = new List<GameObject>();

//		Time.timeScale = 0.1f;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(InputHelper.instance.GetInputButtonString(playerNumber, InputHelper.Button.A)))
		{
            if( GameManager.instance.gameRunning)
			    FireHead();
		}

		if(extending)
		{
			// Head
			transform.Translate(direction * shootVelocity * Time.deltaTime);

			// Neck
			float distance = Vector3.Distance(defaultPosition, transform.position);

			if(distance - lastDistance >= 0.2)
			{
				GameObject neck = ObjectPool.instance.GetObject("Neck");
				neck.transform.position = transform.position;
				neck.transform.rotation = transform.rotation;
				lastDistance = distance;

				neckList.Add(neck);
			}
		}
		else if(retracting)
		{
			// Head
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

				foreach(GameObject neck in neckList)
				{
					ObjectPool.instance.PoolObject(neck);
				}
				neckList.Clear();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Neck" && retracting)
		{
			ObjectPool.instance.PoolObject(other.gameObject);
		}
		else if(other.tag == "Bear Wall" && extending)
		{
			extending = false;
			retracting = true;
		}
	}

	void FireHead()
	{
		if(canShoot)
		{
			angle = InputHelper.instance.GetAngle(playerNumber) - 90;

			if(angle > 90 && angle <= 180)
			{
				angle = 89;
			}
			else if(angle > 180 && angle < 270)
			{
				angle = 269;
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

			if(flipped)
				angle *=  -1;

			direction = new Vector3(0.0f, 1.0f, 0.0f);
			direction = transform.rotation * direction;
			direction.Normalize();

			transform.Rotate(0, 0, angle);
			image.transform.Rotate(0.0f, 0.0f, 20.0f);

			extending = true;
			canShoot = false;

			defaultPosition = transform.position;

			lastDistance = 0.0f;
		}
	}
}
