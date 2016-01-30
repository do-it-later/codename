using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    public const int MAX_PLAYERS = 4;

    private Team team1 = new Team();
    private Team team2 = new Team();

    private List<Player> playerList = new List<Player>();
    public int NumberOfPlayers { get { return playerList.Count; } }

    private Color[] colorList = new Color[]
    {
        new Color32(255,255,255,255),
        new Color32(255,133,133,255),
        new Color32(255,242,133,255),
        new Color32(133,255,144,255),
        new Color32(133,235,255,255),
        new Color32(208,133,255,255)
    };

    private int[] colorChoices = new int[]{ -1,-1,-1,-1 };

    void Awake()
    {
        if( instance == null )
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Duplicate instance detected, destroying gameObject");
            Destroy(gameObject);
        }
    }

    public bool AddPlayer(int controller)
    {
        if( NumberOfPlayers == MAX_PLAYERS )
            return false;

        if( FindPlayer(controller) == null )
        {
            Player p;
            if( controller <= 2 )
            {
                p = new Player(controller, team1);
            }
            else
            {
                p = new Player(controller, team2);
            }

            playerList.Add(p);
            SetNextPlayerColor(controller);
        }
        else
        {
            return false;
        }

        return true;
    }

    public bool RemovePlayer(int controller)
    {
        Player p = FindPlayer(controller);
        if( p != null )
        {
            playerList.Remove(p);
            colorChoices[controller-1] = -1;

            return true;
        }
        return false;
    }

    public Player FindPlayer(int controller)
    {
        foreach(Player p in playerList)
        {
            if( p.PlayerNumber == controller )
            {
                return p;
            }
        }

        return null;
    }

    public void SetNextPlayerColor(int controller)
    {
        int newChoice;
        if( colorChoices[controller-1] == -1 )
            newChoice = controller-1;
        else
            newChoice = (colorChoices[controller-1] + 1) % colorList.Length;

        while( true )
        {
            bool found = false;
            foreach(int i in colorChoices)
            {
                if( newChoice == i )
                {
                    newChoice++;
                    found = true;
                    break;
                }
            }

            if( !found )
            {
                Player p = FindPlayer(controller);
                if( p != null )
                {
                    p.SetColor(colorList[newChoice]);
                    colorChoices[controller-1] = newChoice;
                }
                return;
            }
        }

    }
}
