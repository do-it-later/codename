using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour {

    public List<Image> playerImages = new List<Image>();

    void Start()
    {
        foreach(Image i in playerImages)
        {
            i.enabled = false;
        }
    }

	// Update is called once per frame
	void Update () {
        for( int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i )
        {
            if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.A) ) )
            {
                // If player is not readied up yet
                if(!PlayerManager.instance.IsPlayerAdded(i))
                {
                    Debug.Log("Player " + i.ToString() + " readied up.");
                    if (PlayerManager.instance.AddPlayer(i))
                    {
                        playerImages[i - 1].enabled = true;

                        Player p = PlayerManager.instance.FindPlayer(i);
                        if (p != null)
                        {
                            playerImages[i - 1].color = p.PlayerColor;
                        }
                    }
                }
                // If player is already ready, remove them
                else
                {
                    Debug.Log("Player " + i.ToString() + " unreadied.");
                    if (PlayerManager.instance.RemovePlayer(i))
                    {
                        playerImages[i - 1].enabled = false;
                        playerImages[i - 1].color = Color.white;
                    }
                }
            }
            /*else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.B) ) )
            {
                if( PlayerManager.instance.RemovePlayer(i) )
                {
                    playerImages[i-1].enabled = false;
                    playerImages[i-1].color = Color.white;
                }
            }*/
            else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.X) ) )
            {
                Player p = PlayerManager.instance.FindPlayer(i);
                if( p != null )
                {
                    PlayerManager.instance.SetNextPlayerColor(i);   
                    playerImages[i-1].color = p.PlayerColor;
                }
            }
        }

//        if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.A) ) )
//        {
//            if( PlayerManager.instance.AddPlayer(1) )
//            {
//                playerImages[0].enabled = true;
//
//                Player p = PlayerManager.instance.FindPlayer(1);
//                if( p != null )
//                {
//                    playerImages[0].color = p.PlayerColor;
//                }
//            }
//        }
//        if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.B) ) )
//        {
//            if( PlayerManager.instance.AddPlayer(2) )
//            {
//                playerImages[1].enabled = true;
//
//                Player p = PlayerManager.instance.FindPlayer(2);
//                if( p != null )
//                {
//                    playerImages[1].color = p.PlayerColor;
//                }
//            }
//        }
//        if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.X) ) )
//        {
//            if( PlayerManager.instance.AddPlayer(3) )
//            {
//                playerImages[2].enabled = true;
//
//                Player p = PlayerManager.instance.FindPlayer(3);
//                if( p != null )
//                {
//                    playerImages[2].color = p.PlayerColor;
//                }
//            }
//        }
//        if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.Y) ) )
//        {
//            if( PlayerManager.instance.AddPlayer(4) )
//            {
//                playerImages[3].enabled = true;
//
//                Player p = PlayerManager.instance.FindPlayer(4);
//                if( p != null )
//                {
//                    playerImages[3].color = p.PlayerColor;
//                }
//            }
//        }

        for(int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i)
        {
            // must have exactly 4 players
            if( PlayerManager.instance.NumberOfPlayers != PlayerManager.MAX_PLAYERS )
                break;
            
            // player must be playing
            if( PlayerManager.instance.FindPlayer(i) == null )
                continue;
            
            if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.START) ) )
            {
                SceneManager.LoadScene("Test");
            }
        }
	}
}
