using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public int caughtScore = 10;
    public int passedScore = 1;
    private Team team1 = new Team();
    private Team team2 = new Team();

    private Timer timer;
    private Score score;
    private int round;
    private Team[] roundWinners;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start()
    {
        timer = GetComponent<Timer>();
        score = GetComponent<Score>();
        roundWinners = new Team[3];
        round = 0;

        int firstTeam = Random.Range(1,2);

        if( firstTeam == 1 )
            team1.AreBears = true;
        else
            team2.AreBears = true;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public void StartRound()
    {
        round++;

        timer.ResetTimer();
        timer.StartTimer();
    }

    public void EndRound()
    {
        //Check for a winner
//        Team winner = score.Winner();
//        roundWinners[round-1] = winner;
    }

    public void SalmonCaught()
    {
//        score.ModifyScore(1);
    }
}
