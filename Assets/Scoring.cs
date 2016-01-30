using UnityEngine;
using System.Collections.Generic;

public class Scoring : MonoBehaviour
{
    Dictionary<string, int> scores;

	// Use this for initialization
	void Start()
    {
        scores.Add("Bear", 0);
        scores.Add("Salmon", 0);
    }
	
	// Update is called once per frame
	void Update()
    {
	}

    public void AddScore(string teamName, int v)
    {
        if(scores.ContainsKey(teamName))
            scores[teamName] += v;


    }

    public void RemoveScore(string teamName, int v)
    {
        if (scores.ContainsKey(teamName))
            scores[teamName] -= v;
    }
}
