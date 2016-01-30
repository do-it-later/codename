using UnityEngine;
using System.Collections;

public class Player {

    private int playerNumber;
    public int PlayerNumber { get { return playerNumber; } }

    private Team playerTeam;
    public Team PlayerTeam { get { return playerTeam; } }

    private Color playerColor;
    public Color PlayerColor { get { return playerColor; } }

    private int score;
    public int Score { get { return score; } }

    public Player(int controller, Team t)
    {
        playerNumber = controller;
        playerTeam = t;
        score = 0;
    }

    public void SetColor(Color color)
    {
        playerColor = color;
    }
}
