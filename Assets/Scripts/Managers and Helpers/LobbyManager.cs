using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        for( int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i )
        {
            if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.A) ) )
            {
                PlayerManager.instance.AddPlayer(i);
            }
            else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.B) ) )
            {
                PlayerManager.instance.RemovePlayer(i);
            }
        }

        for(int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i)
        {
            // must have at least 2 players
            if( PlayerManager.instance.NumberOfPlayers < 2 )
                break;
            
            // player must be playing
            if( !PlayerManager.instance.ContainsPlayer(i) )
                continue;
            
            if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.START) ) )
            {
                SceneManager.LoadScene("Test");
            }
        }
	}
}
