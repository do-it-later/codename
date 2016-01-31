using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public GameObject animationObj;

	public AudioClip roar;

	public GameObject mainImage;
	public GameObject creditsImage;

	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		for(int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i)
		{
			if(Input.GetKeyDown(InputHelper.instance.GetInputButtonString(i, InputHelper.Button.START)) && mainImage.activeSelf)
			{
				StartCoroutine(PlayAnimation());
			}
			else if(Input.GetKeyDown(InputHelper.instance.GetInputButtonString(i, InputHelper.Button.SELECT)) && mainImage.activeSelf)
			{
				mainImage.SetActive(false);
				creditsImage.SetActive(true);
			}
			else if(Input.GetKeyDown(InputHelper.instance.GetInputButtonString(i, InputHelper.Button.B)) && creditsImage.activeSelf)
			{
				mainImage.SetActive(true);
				creditsImage.SetActive(false);
			}
		}
	}

	IEnumerator PlayAnimation()
	{
		SoundManager.instance.PlaySingleSfx(roar);
        animationObj.GetComponent<Animator>().Play("Start Screen");
		yield return 0;
		yield return new WaitForSeconds(animationObj.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
		animationObj.GetComponent<Animator>().Play("Idle");
		SceneManager.LoadScene("Lobby");
	}
}
