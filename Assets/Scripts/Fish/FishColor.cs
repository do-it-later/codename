using UnityEngine;
using System.Collections;

public class FishColor : MonoBehaviour {

    public int playerNumber;    

	// Use this for initialization
	void Start ()
    {
        var sr = GetComponent<SpriteRenderer>();
        var player = PlayerManager.instance.FindPlayer(playerNumber);

        if( player != null )
        {
            sr.color = player.PlayerColor;
        }
	}
}
