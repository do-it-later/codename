using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishUI : MonoBehaviour {

    public int playerNumber;
    public List<Sprite> sprites;

	// Use this for initialization
	void Start ()
    {
        var sr = GetComponent<SpriteRenderer>();
        var player = PlayerManager.instance.FindPlayer(playerNumber);

        if( player != null )
        {
            sr.color = player.PlayerColor;
            sr.sprite = sprites[Random.Range(0,sprites.Count)];
        }
	}
}
