using UnityEngine;
using System.Collections;

public class FishCursor : MonoBehaviour
{
    public int ControllerNumber = 1;
    public float FishPointerSpeed = 25.0f;
    public float ControllerErrorThreshold = 0.15f;

    private Transform fishPointer;
    private Vector3 originalPosition;
    private Vector3 cameraDimensions;

    private float frustumHeight;
    private float frustumWidth;

    private float distanceToPlayArea = 20.0f;

    // Use this for initialization
    void Start()
    {
        fishPointer = transform;
        originalPosition = fishPointer.position;

        cameraDimensions = new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0.0f);
        //cameraDimensions = new Vector3(Screen.width, Screen.height, 0.0f);

        // Distance between bear and camera
		distanceToPlayArea = Vector3.Distance(GameObject.Find("Bear Left").transform.position, Camera.main.transform.position);
		distanceToPlayArea = 16.0f; // TODO: remove hardcoded values

        frustumHeight = 2.0f * distanceToPlayArea * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        // TODO: ofset accounting for rotation of camera
        float offset = Mathf.Tan(Camera.main.transform.rotation.eulerAngles.x * Mathf.Deg2Rad) * distanceToPlayArea;
        Debug.Log(">>>" + offset);
        frustumHeight -= offset * 2.0f;


        frustumWidth = frustumHeight * Camera.main.aspect;
    }
	
    // Update is called once per frame
    void Update()
    {
        Vector3 w = fishPointer.position;
        float horizontal = InputHelper.instance.GetHorizForController(ControllerNumber);
        float verticle = InputHelper.instance.GetVertForController(ControllerNumber);


        Debug.Log(horizontal + " : " + verticle);

        if (!(horizontal <= ControllerErrorThreshold && horizontal >= -ControllerErrorThreshold))
        {
            w.x += horizontal * FishPointerSpeed * Time.deltaTime;

            if(w.x > originalPosition.x + (frustumWidth / 2.0f) - 3.5f)
				w.x = originalPosition.x + (frustumWidth / 2.0f) - 3.5f;

             if(w.x <= originalPosition.x - (frustumWidth / 2.0f) + 3.5f)
                w.x = originalPosition.x - (frustumWidth / 2.0f) + 3.5f;
        }

        if (!(verticle <= ControllerErrorThreshold && verticle >= -ControllerErrorThreshold))
        {
            w.y += verticle * FishPointerSpeed * Time.deltaTime;

            if (w.y > originalPosition.y + (frustumHeight / 2.0f) - 0.5f)
                w.y = originalPosition.y + (frustumHeight / 2.0f) - 0.5f;

            if (w.y <= originalPosition.y - (frustumHeight / 2.0f) + 5.5f)
                w.y = originalPosition.y - (frustumHeight / 2.0f) + 5.5f;
        }
            
        fishPointer.transform.position = w;
    }
}
