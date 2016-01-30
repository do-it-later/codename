using UnityEngine;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    // SALMON GOAL: -100
    // BEAR GOAL: 100
    public int goal = 100;
    private int difference = 0;

    public enum AnimalTeam
    {
        BEAR,
        SALMON,
        BOTH
    }

    public void ModifyScore(AnimalTeam t, int v)
    {
        if( t == AnimalTeam.BEAR )
            difference += v;
        else if ( t == AnimalTeam.SALMON )
            difference -= v;
    }

    public bool GoalReached()
    {
        if( difference <= goal * -1 || difference >= goal )
            return true;
        
        return false;
    }

    public AnimalTeam Winner()
    {
        if( difference < 0 )
            return AnimalTeam.SALMON;
        if( difference > 0)
            return AnimalTeam.BEAR;

        return AnimalTeam.BOTH;
    }
}
