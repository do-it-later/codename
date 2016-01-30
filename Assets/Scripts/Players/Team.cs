using UnityEngine;
using System.Collections;

public class Team {

    private int score;

    public Team()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public int ModifyScore(int modifyAmount)
    {
        score += modifyAmount;
    }

}
