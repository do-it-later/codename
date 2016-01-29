using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;

    private const int MAX_PLAYERS = 4;
    int[] playerArray = new int[MAX_PLAYERS];

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
        // Check the player doesn't already exist
        for(int i = 0; i < MAX_PLAYERS; ++i)
        {
            if( playerArray[i] == controller )
            {
                return;
            }
        }

        //Add
        for(int i = 0; i < MAX_PLAYERS; ++i)
        {
            if( playerArray[i] == 0 )
            {
                playerArray[i] = controller;
                break;
            }
        }
    }

    public void RemovePlayer(int controller)
    {
        for(int i = 0; i < MAX_PLAYERS; ++i)
        {
            if( playerArray[i] == controller )
            {
                playerArray[i] = 0;
                break;
            }
        }
    }
}
