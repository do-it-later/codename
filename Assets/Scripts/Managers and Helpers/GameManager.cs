using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;

    public Canvas roundCanvas;
    public Text roundText;
    public Text bearText;

    public Canvas endgameCanvas;
    public Text winnerText;

    public int numFishPerRound;
    public BearHead bear;

    private Timer timer;
    private bool paused;
    private int round;
    private Player bearPlayer;

    private int[] fishCount = new int[4];
    private float[] shootTime = new float[4];
    private bool allEmpty = false;
    public bool gameRunning = false;

    private List<int> bearTurns = new List<int>();

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start()
    {
        timer = GetComponent<Timer>();
        round = 0;
        bearTurns.Add(1);
        bearTurns.Add(2);
        bearTurns.Add(3);
        bearTurns.Add(4);

        for (int i = 0; i < bearTurns.Count; ++i)
        {
            int r = Random.Range(0, bearTurns.Count);
            int t = bearTurns[r];
            bearTurns[r] = bearTurns[i];
            bearTurns[i] = t;
        }

        PrepareNextRound();
    }

	// Update is called once per frame
	void Update()
    {
        if( gameRunning )
        {
            for(int i = 0; i < fishCount.Length; ++i)
            {
                // ignore your own player
                if(bearPlayer.PlayerNumber == i-1)
                    continue;

                if(fishCount[i] > 0)
                {
                    allEmpty = false;
                    break;
                }
                else
                {
                    allEmpty = true;
                }
            }

            if( allEmpty )
                EndRound();

            for( int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i )
            {
                //Pause
                if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.START) ) )
                {
                    if( !paused )
                        Time.timeScale = 0;
                    else
                        Time.timeScale = 1;

                    paused = !paused;
                }

                if( Input.GetKey( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.A) ) )
                {
                    ShootFish(i);
                }
            }
        }
	}

    private void PrepareNextRound()
    {
        round++;
        allEmpty = false;
        bearPlayer = PlayerManager.instance.PlayerList[bearTurns[round-1]];
        bear.playerNumber = bearPlayer.PlayerNumber;
        headSprite.color = bearPlayer.PlayerColor;
        bodySprite.color = bearPlayer.PlayerColor;
        roundCanvas.enabled = false;
        endgameCanvas.enabled = false;

        for(int i = 0; i < fishCount.Length; ++i)
        {
            fishCount[i] = numFishPerRound;
        }

        for(int i = 0; i < shootTime.Length; ++i)
        {
            shootTime[i] = 0.0f;
        }

        timer.ResetTimer();

        StartCoroutine("roundStartCoroutine");
    }

    private IEnumerator roundStartCoroutine()
    {
        //TODO: Display banner with round
        roundCanvas.enabled = true;
        roundText.text = "Round " + round;
        bearText.text = "Player " + bearPlayer.PlayerNumber.ToString() + " is the Bear!";
        yield return new WaitForSeconds(3);
        roundCanvas.enabled = false;
        StartRound();
    }

    public void StartRound()
    {
        timer.StartTimer();
        gameRunning = true;
    }

    public void EndRound()
    {
        gameRunning = false;

        timer.StopTimer();

        for(int i = 0; i < fishCount.Length; ++i)
        {
            // ignore your own player
            if(bearPlayer.PlayerNumber == i-1)
                continue;

            bearPlayer.ModifyScore(round-1, fishCount[i]/2);
        }

        if( round >= PlayerManager.MAX_PLAYERS )
            EndGame();
        else
            PrepareNextRound();
    }

    private void EndGame()
    {
        Player p = PlayerManager.instance.FindWinner();
        roundCanvas.enabled = false;
        endgameCanvas.enabled = true;

        winnerText.text = "Winner: P" + p.PlayerNumber.ToString();
    }

    public void SalmonFlee(int controller)
    {
        if( gameRunning)
        {
            var p = PlayerManager.instance.FindPlayer(controller);
            if( p != null )
            {
                p.ModifyScore(round-1, 1);
            }
        }
    }

    public void SalmonCaught(int controller)
    {
        if( gameRunning )
        {
            // Player who gets caught loses points
            var p = PlayerManager.instance.FindPlayer(controller);
            if( p != null )
            {
                p.ModifyScore(round-1, -2);
            }

            bearPlayer.ModifyScore(round-1, 10);
        }
    }

    private void ShootFish(int controller)
    {
        if( bearPlayer.PlayerNumber == controller)
            return;

        if( fishCount[controller-1] > 0 && Time.time - shootTime[controller-1] > 0.2f )
        {
            ObjectPool.instance.GetObject("P" + controller.ToString() + "_Fish");
            fishCount[controller-1]--;
            shootTime[controller-1] = Time.time;
        }
    }
}
