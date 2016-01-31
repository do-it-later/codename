using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour {

    public bool debug = false;

    public List<Image> frameImages = new List<Image>();
    public List<GameObject> playerCharacters = new List<GameObject>();
    public List<Image> playerImages = new List<Image>();
    public Image startImage;
    private bool[] playersReady = new bool[] {false, false, false, false};

	public AudioClip music;
	public List<AudioClip> roarList;

    void Start()
    {
        if(debug)
        {
            playersReady[1] = true;
            playersReady[2] = true;
            playersReady[3] = true;
        }

		SoundManager.instance.PlayLoopedMusic(music);

        foreach(GameObject go in playerCharacters)
        {
            playerImages.Add( go.GetComponent<Image>() );
        }
    }

	// Update is called once per frame
	void Update () {
        for( int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i )
        {
            Player p = PlayerManager.instance.FindPlayer(i);
            if (p != null)
            {
                playerImages[i - 1].color = p.PlayerColor;
            }
        }

        for( int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i )
        {
            if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.A) ) )
            {
                // If player is not readied up yet
                if(!playersReady[i-1])
                {
                    Debug.Log("Player " + i.ToString() + " readied up.");
					SoundManager.instance.PlaySingleSfx(roarList[Random.Range(0, roarList.Count)]);

                    // Swap arrows to OK
                    frameImages[i-1].color = new Color32(133,255,183,255);
                }
                // If player is already ready, remove them
                else
                {
                    Debug.Log("Player " + i.ToString() + " unreadied.");

                    // Swap arrows to OK
                    frameImages[i-1].color = Color.white;
                }

                playersReady[i-1] = !playersReady[i-1];
            }
            else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.X) ) )
            {
                Player p = PlayerManager.instance.FindPlayer(i);
                if( p != null )
                {
                    PlayerManager.instance.SetNextPlayerColor(i);   
                    playerImages[i-1].color = p.PlayerColor;
                    playerCharacters[i-1].GetComponent<Animator>().Play("Idle");
                }
            }
        }

        for( int i = 0; i < playersReady.Length; ++i)
        {
            if(!playersReady[i])
            {
                startImage.enabled = false;
                return;
            }
        }

        startImage.enabled = true;

        for(int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i)
        {
            if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.START) ) )
            {
                SceneManager.LoadScene("Controls");
            }
        }
	}
}
