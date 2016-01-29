using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour {

    private bool p1added = false;
    private bool p2added = false;
    private bool p3added = false;
    private bool p4added = false;
	
	// Update is called once per frame
	void Update () {
        if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.A) ) )
        {
            if( p1added )
                PlayerManager.instance.RemovePlayer(1);
            else
                PlayerManager.instance.AddPlayer(1);

            p1added = !p1added;
        }
        else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.B) ) )
        {
            if( p2added )
                PlayerManager.instance.RemovePlayer(2);
            else
                PlayerManager.instance.AddPlayer(2);

            p2added = !p2added;
        }
        else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.X) ) )
        {
            if( p3added )
                PlayerManager.instance.RemovePlayer(3);
            else
                PlayerManager.instance.AddPlayer(3);

            p3added = !p3added;
        }
        else if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.Y) ) )
        {
            if( p4added )
                PlayerManager.instance.RemovePlayer(4);
            else
                PlayerManager.instance.AddPlayer(4);

            p4added = !p4added;
        }

        for(int i = 1; i <= 4; ++i)
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
