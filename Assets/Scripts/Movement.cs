using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public int playerNumber = 1;
    private bool inControl = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var pos = gameObject.transform.position;
        pos.x += 0.02f;
        gameObject.transform.position = pos;

        if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(playerNumber, InputHelper.Button.A) ) && inControl )
        {
            ObjectPool.instance.GetObject("P" + playerNumber.ToString() + " Ball", true);
            inControl = false;
        }
        Debug.Log(InputHelper.instance.GetAngle(playerNumber));
	}

    void OnBecameInvisible()
    {
        ObjectPool.instance.PoolObject(gameObject);
    }
}
