using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIScore : MonoBehaviour {

    private List<Player> playerList;

    public List<Image> scoreNames;
    public List<Text> scoreList;
    public List<Text> endScoreList;
    public List<Image> endScoreImages;
    public List<Image> endScoreNameImages;

    public Canvas endgameCanvas;
    private List<Player> sortedPlayers;
    private bool sorted;

	// Use this for initialization
	void Start () {
        playerList = PlayerManager.instance.PlayerList;
        sorted = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(endgameCanvas.isActiveAndEnabled)
        {
            if( !sorted )
            {
                SortScores();
                sorted = true;
            }

            for(var i = 0; i < sortedPlayers.Count; ++i)
            {
                endScoreList[i].text = sortedPlayers[i].Score.ToString();
                endScoreImages[i].color = sortedPlayers[i].PlayerColor;
                endScoreNameImages[i].sprite = GameManager.instance.PlayerImages[sortedPlayers[i].PlayerNumber-1];
            }
        }
        else
        {
            for(var i = 0; i < playerList.Count; ++i)
            {
                scoreList[i].text = playerList[i].Score.ToString();
                scoreNames[i].color = playerList[i].PlayerColor;
            }
        }
	}

    private void SortScores()
    {
        sortedPlayers = playerList.OrderByDescending(o=>o.Score).ToList();
    }
}
