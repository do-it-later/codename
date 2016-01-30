using UnityEngine;
using System.Collections;

public class Team {

    private int wins = 0;
    public int Wins
    {
        set { wins = value; }
        get { return wins; }
    }

    private bool areBears = false;
    public bool AreBears
    {
        set { areBears = value; }
        get { return areBears; }
    }

    private int penaltyScore = 0;
    public int PenaltyScore
    {
        set { penaltyScore = value; }
        get { return penaltyScore; }
    }

    public Team()
    {}
}
