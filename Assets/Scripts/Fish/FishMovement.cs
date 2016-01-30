using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    //
    public float swimSpeed = 20.0f;
    public float strafeSpeed = 10.0f;

    public Vector3 direction;
    private Vector3 initPosition;

    public int playerNumber;

    //
    private GameObject fishPointer;
    private bool directionSet = false;


    void Start()
    {
        initPosition = gameObject.transform.position;

        fishPointer = GameObject.Find("P" + playerNumber.ToString() + "_Pointer");
    }

    void Update()
    {
        if( !directionSet )
        {
            direction = fishPointer.transform.position - transform.position;
            direction.Normalize();
            directionSet = true;
        }

		this.transform.position += direction * Time.deltaTime * swimSpeed;

		if(transform.position.z <= -88)
		{
            transform.position = initPosition;
            directionSet = false;
            GameManager.instance.SalmonFlee(playerNumber);
            ObjectPool.instance.PoolObject(gameObject);
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bear")
        {
            transform.position = initPosition;
            directionSet = false;
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
}
