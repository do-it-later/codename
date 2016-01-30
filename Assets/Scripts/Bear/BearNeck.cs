using UnityEngine;
using System.Collections;

public class BearNeck : MonoBehaviour
{
	public Transform bearHead;
	public Transform bearBody;

	private float factor = 1.5f;

	private Vector3 body;
	private Vector3 head;

	// Use this for initialization
	void Start()
	{
		body = new Vector3(bearBody.position.x + 0.65f, bearBody.position.y + 1.0f, bearBody.position.z);
		head = new Vector3(bearHead.position.x, bearHead.position.y - 0.1f, bearHead.position.z);
		transform.position = body;
		SetPos(body, head);
	}
	
	// Update is called once per frame
	void Update()
	{
		head = new Vector3(bearHead.position.x, bearHead.position.y - 0.1f, bearHead.position.z);
		SetPos(body, head);
	}

	void SetPos(Vector3 start, Vector3 end)
	{
		var dir = end - start;
//		var mid = (dir) / 2.0f + start;
//		transform.position = mid;
		transform.rotation = Quaternion.FromToRotation(bearBody.up, dir);
		Vector3 scale = transform.localScale;
		scale.y = dir.magnitude / 2.0f * factor;
		transform.localScale = scale;
	}
}
