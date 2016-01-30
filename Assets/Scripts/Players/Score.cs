using UnityEngine;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    // SALMON GOAL: -100
    // BEAR GOAL: 100
    public int goal = 100;
    private int difference = 0;

    public enum Team
    {
        BEAR,
        SALMON,
        NONE
    }

    public void ModifyScore(Team t, int v)
    {
        if( t == Team.BEAR )
            difference += v;
        else if ( t == Team.SALMON )
            difference -= v;
    }

    public bool GoalReached()
    {
        if( difference <= goal * -1 || difference >= goal )
            return true;
        
        return false;
    }

    public Team Winner()
    {
        if( difference < 0 )
            return Team.SALMON;
        if( difference > 0)
            return Team.BEAR;

        return Team.NONE;
    }
}
