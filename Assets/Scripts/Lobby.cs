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

        if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(1, InputHelper.Button.START) ) )
        {
            SceneManager.LoadScene("Test");
        }
	}
}
