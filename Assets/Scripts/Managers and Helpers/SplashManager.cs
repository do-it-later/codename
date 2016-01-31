using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		StartCoroutine(roundStartCoroutine());
	}

	// Update is called once per frame
	void Update()
	{
	}

	private IEnumerator roundStartCoroutine()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("Menu");
	}
}
