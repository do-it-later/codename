using UnityEngine;
using System.Collections;

public class BearNeck : MonoBehaviour
{
	public Transform bearHead;
	public Transform bearBody;

	private float factor = 1.5f;

	// Use this for initialization
	void Start()
	{
		SetPos(bearBody.position, bearHead.position);
	}
	
	// Update is called once per frame
	void Update()
	{
		SetPos(bearBody.position, bearHead.position);
	}

	void SetPos(Vector3 start, Vector3 end)
	{
		var dir = end - start;
		var mid = (dir) / 2.0f + start;
		transform.position = mid;
		transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
		Vector3 scale = transform.localScale;
		scale.y = dir.magnitude * factor;
		transform.localScale = scale;
	}
}
