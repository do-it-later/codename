using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour {
	
    public Text statusText;

	// Update is called once per frame
	void Update () {
        for( int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i )
        {
            if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.A) ) )
            {
                Debug.Log("Player " + i.ToString() + " added.");
                PlayerManager.instance.AddPlayer(i);
            }
            else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.B) ) )
            {
                PlayerManager.instance.RemovePlayer(i);
            }
            Debug.Log(PlayerManager.instance.NumberOfPlayers);
        }

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
