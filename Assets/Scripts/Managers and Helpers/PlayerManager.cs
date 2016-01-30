using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    public const int MAX_PLAYERS = 4;

    private List<Player> playerList = new List<Player>();
    public int NumberOfPlayers { get { return playerList.Count; } }

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

    public void AddPlayer(int controller)
    {
        if( NumberOfPlayers == MAX_PLAYERS )
            return;

        if( FindPlayer(controller) == null )
        {
            Player p = new Player(controller);
            playerList.Add(p);
        }
    }

    public void RemovePlayer(int controller)
    {
        Player p = FindPlayer(controller);
        if( p != null )
            playerList.Remove(p);
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
}
