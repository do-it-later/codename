using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		StartCoroutine(delayCoroutine());
	}

	private IEnumerator delayCoroutine()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("Game");
	}
}
