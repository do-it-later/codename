using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    public const int MAX_PLAYERS = 4;

    private List<int> players = new List<int>();
    public int NumberOfPlayers { get { return players.Count; } }

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddPlayer(int controller)
    {
        if( NumberOfPlayers == MAX_PLAYERS )
            return;

        if( !players.Contains(controller) )
            players.Add(controller);
    }

    public void RemovePlayer(int controller)
    {
        players.Remove(controller);
    }

    public bool ContainsPlayer(int controller)
    {
        return players.Contains(controller);
    }
}
