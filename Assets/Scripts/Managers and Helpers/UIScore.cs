using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIScore : MonoBehaviour {

    private List<Player> playerList;

    public List<Text> scoreList;
    public List<Text> endScoreList;

	// Use this for initialization
	void Start () {
        playerList = PlayerManager.instance.PlayerList;
	}
	
	// Update is called once per frame
	void Update () {
        for(var i = 0; i < playerList.Count; ++i)
        {
            scoreList[i].text = "P" + (i+1).ToString() + " Score: " + playerList[i].Score.ToString();
            scoreList[i].color = playerList[i].PlayerColor;

            endScoreList[i].text = playerList[i].Score.ToString();
            endScoreList[i].color = playerList[i].PlayerColor;
        }
	}
}
