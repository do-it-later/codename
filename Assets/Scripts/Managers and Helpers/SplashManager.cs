using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
	public AudioClip music;

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
		SoundManager.instance.PlaySingleMusic(music);
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("Menu");
	}
}
