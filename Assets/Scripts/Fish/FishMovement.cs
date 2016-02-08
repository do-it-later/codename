using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    //
    public float swimSpeed = 20.0f;
    public float strafeSpeed = 10.0f;

    public Vector3 direction;
    private int rotationAmount;
    private Vector3 initPosition;

    public int playerNumber;
    private bool initialPositionSet = false;

    //
    private GameObject fishPointer;
    private bool directionSet = false;

    void OnDisable()
    {
        ResetFish();
    }

    void Start()
    {
        initPosition = gameObject.transform.position;
        initialPositionSet = true;

        ResetFish();

        fishPointer = GameObject.Find("P" + playerNumber.ToString() + "_Pointer");
    }

    void Update()
    {
        if( !directionSet && fishPointer != null )
        {
            direction = fishPointer.transform.position - transform.position;
            direction.Normalize();
            directionSet = true;
        }

		this.transform.position += direction * Time.deltaTime * swimSpeed;
        transform.Rotate(0,0,rotationAmount);

		if(transform.position.z <= -88)
		{
            ResetFish();
            GameManager.instance.SalmonFlee(playerNumber);
            ObjectPool.instance.PoolObject(gameObject);
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bear")
        {
            ResetFish();
            GameManager.instance.SalmonCaught(playerNumber);
            ObjectPool.instance.PoolObject(gameObject);
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

    private void ResetFish()
    {
        if( initialPositionSet )
        {
            transform.position = initPosition + new Vector3(Random.Range(-5, 5), 0, 0);
            transform.Rotate(0,0, Random.Range(0, 360));
            rotationAmount = Random.Range(-10,10);
            directionSet = false;
        }
    }
}
