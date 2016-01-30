using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public GameObject animation;

	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("space"))
		{
			StartCoroutine(PlayAnimation());
		}

		for(int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i)
		{
			if(Input.GetKeyDown(InputHelper.instance.GetInputButtonString(i, InputHelper.Button.START)))
			{
				StartCoroutine(PlayAnimation());
			}
		}
	}

	IEnumerator PlayAnimation()
	{
		animation.GetComponent<Animator>().Play("Start Screen");
		yield return 0;
		yield return new WaitForSeconds(animation.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
		animation.GetComponent<Animator>().Play("Idle");
		SceneManager.LoadScene("Lobby");
	}
}
