using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int numFishPerRound;

    private Timer timer;
    private bool paused;
    private int round;
    private Player bearPlayer;

    private int[] fishCount = new int[4];
    private bool allEmpty = false;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start()
    {
        timer = GetComponent<Timer>();
        round = 0;

        PrepareNextRound();
    }

	// Update is called once per frame
	void Update()
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

        //Add shooting logic here

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
        }
	}

    private void PrepareNextRound()
    {
        round++;
        allEmpty = false;
        bearPlayer = PlayerManager.instance.PlayerList[round-1];

        for(int i = 0; i < fishCount.Length; ++i)
        {
            fishCount[i] = numFishPerRound;
        }

        timer.ResetTimer();

        StartCoroutine("roundStartCoroutine");
    }

    private IEnumerator roundStartCoroutine()
    {
        //TODO: Display banner with round
        Debug.Log(bearPlayer.PlayerNumber.ToString() + " is bear.");
        yield return new WaitForSeconds(1);
        StartRound();
    }

    public void StartRound()
    {
        timer.StartTimer();

		ObjectPool.instance.GetObject("Fish", true);
    }

    public void EndRound()
    {
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
        
    }

    public void SalmonFlee(int controller)
    {
        var p = PlayerManager.instance.FindPlayer(controller);
        if( p != null )
        {
            p.ModifyScore(round-1, 1);
        }
    }

    public void SalmonCaught(int controller)
    {
        // Player who gets caught loses points
        var p = PlayerManager.instance.FindPlayer(controller);
        if( p != null )
        {
            p.ModifyScore(round-1, -2);
        }

        bearPlayer.ModifyScore(round-1, 10);
    }

    private void ShootFish(int controller)
    {
        if( fishCount[controller-1] > 0 )
        {
//            ObjectPool.instance.GetObject("P" + controller.ToString + "_Fish");
            fishCount[controller-1]--;
        }
    }
}
