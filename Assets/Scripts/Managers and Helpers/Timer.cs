using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

    public float roundTime = 60.0f;
    public Text timerText;

    private float remainingTime;
    public float RemainingTime { get { return remainingTime;} }
    private bool isRunning;
	
	// Update is called once per frame
	void Update ()
    {
        if( isRunning )
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0)
            {
                remainingTime = 0;
                isRunning = false;
                GameManager.instance.EndRound();
            }

            timerText.text = Mathf.Ceil(remainingTime).ToString();
        }
    }

    public void ResetTimer()
    {
        remainingTime = roundTime;
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
