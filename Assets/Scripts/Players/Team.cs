using UnityEngine;
using System.Collections;

public class Team {

    public const int MAX_MEMBERS = 2;

    private int score;
    private int numMembers;
    public int NumMembers { get { return numMembers; } }

    public Team()
    {
        score = 0;
        numMembers = 0;
    }

    public void AddMember()
    {
        numMembers++;
    }

    public void RemoveMember()
    {
        numMembers--;
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int modifyAmount)
    {
        score += modifyAmount;
    }

}
