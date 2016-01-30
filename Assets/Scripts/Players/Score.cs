using UnityEngine;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
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

    public Team GoalReached()
    {
        if( difference <= goal * -1 )
            return Team.SALMON;
        else if( difference >= goal )
            return Team.BEAR;
        
        return Team.NONE;
    }
}
