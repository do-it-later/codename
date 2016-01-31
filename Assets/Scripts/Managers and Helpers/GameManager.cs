using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;
    public List<GameObject> cursors;

    public List<Text> FishPlayerText;
    public List<Text> FishRemainingTexts;
    public List<Image> RoarImages;

	public Canvas pausedCanvas;
    public Canvas UICanvas;
    public Canvas roundCanvas;
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
    public bool gameRunning = false;
    private bool gameEnded = false;
    private int totalFishLeft = 0;

    private List<int> bearTurns = new List<int>();

	public AudioClip music;
	public List<AudioClip> chompAudio;
	public AudioClip gameoverAudio;
	public List<AudioClip> playerWinsAudio;
	public List<AudioClip> roundNumberAudio;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start()
    {
        // Setup cursor colors
        for(int i = 0; i < cursors.Count; ++i)
        {
            var sr = cursors[i].GetComponent<SpriteRenderer>();
            sr.color = PlayerManager.instance.FindPlayer(i+1).PlayerColor;
        }

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

        for(var i = 0; i < PlayerManager.MAX_PLAYERS; ++i)
        {
            Player p = PlayerManager.instance.FindPlayer(i+1);

            if( p != null )
                FishPlayerText[i].color = p.PlayerColor;
        }

        PrepareNextRound();

		SoundManager.instance.PlayLoopedMusic(music);
    }

	// Update is called once per frame
	void Update()
    {
        for(int i = 0; i < PlayerManager.MAX_PLAYERS; ++i)
        {
            if( bear.playerNumber - 1 == i)
                FishRemainingTexts[i].text = "";
            else
                FishRemainingTexts[i].text = fishCount[i].ToString();
        }

        if( gameRunning || gameEnded )
        {
            for( int i = 1; i <= PlayerManager.MAX_PLAYERS; ++i )
            {
                //Pause
                if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.START) ) )
                {
                    if( gameEnded )
                    {
                        SceneManager.LoadScene("Lobby");
                    }
                    else
                    {
                        if( !paused )
						{
                            Time.timeScale = 0;
							pausedCanvas.enabled = true;
						}
                        else
						{
                            Time.timeScale = 1;
							pausedCanvas.enabled = false;
						}

                        paused = !paused;
                    }
                }

                if( Input.GetKeyDown( InputHelper.instance.GetInputButtonString(i, InputHelper.Button.A) ) )
                {
                    ShootFish(i);
                }
            }
        }
	}

    private void PrepareNextRound()
    {
		SoundManager.instance.PlaySingleSfx(roundNumberAudio[round]);

        round++;
        bearPlayer = PlayerManager.instance.FindPlayer(bearTurns[round-1]);
        bear.playerNumber = bearPlayer.PlayerNumber;
        headSprite.color = bearPlayer.PlayerColor;
        bodySprite.color = bearPlayer.PlayerColor;
        roundCanvas.enabled = false;
        endgameCanvas.enabled = false;
        UICanvas.enabled = false;
		pausedCanvas.enabled = false;
        totalFishLeft = numFishPerRound * 3;

        for(int i = 0; i < PlayerManager.MAX_PLAYERS; ++i)
        {
            fishCount[i] = numFishPerRound;
            shootTime[i] = 0.0f;

            if( bear.playerNumber - 1 == i)
            {
                cursors[i].SetActive(false);
                RoarImages[i].enabled = true;
            }
            else
            {
                cursors[i].SetActive(true);
                RoarImages[i].enabled = false;
            }
        }

        timer.ResetTimer();

        StartCoroutine("roundStartCoroutine");
    }

    private IEnumerator roundStartCoroutine()
    {
        //TODO: Display banner with round
        roundCanvas.enabled = true;
        bearText.text = "Player " + bearPlayer.PlayerNumber.ToString() + " is the Bear!";
        yield return new WaitForSeconds(4);
        roundCanvas.enabled = false;
        StartRound();
    }

    public void StartRound()
    {
        timer.StartTimer();
        gameRunning = true;
        UICanvas.enabled = true;
    }

    public void EndRound()
    {
        gameRunning = false;

        timer.StopTimer();

        for(int i = 0; i < fishCount.Length; ++i)
        {
            // ignore your own player
            if(bearPlayer.PlayerNumber == i+1)
                continue;

            bearPlayer.ModifyScore(round-1, fishCount[i]/2);
        }

        if( round >= PlayerManager.MAX_PLAYERS )
			StartCoroutine(EndGame());
        else
            PrepareNextRound();
    }

	private IEnumerator EndGame()
    {
		SoundManager.instance.PlaySingleSfx(gameoverAudio);
		yield return new WaitForSeconds(2);

        Player p = PlayerManager.instance.FindWinner();
        roundCanvas.enabled = false;
        endgameCanvas.enabled = true;
        UICanvas.enabled = false;
        gameEnded = true;

		SoundManager.instance.PlaySingleSfx(playerWinsAudio[p.PlayerNumber - 1]);

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
			SoundManager.instance.PlaySingleSfx(chompAudio[Random.Range(0, chompAudio.Count)]);

            // Player who gets caught loses points
            var p = PlayerManager.instance.FindPlayer(controller);
            if( p != null )
            {
                p.ModifyScore(round-1, -2);
            }

            bearPlayer.ModifyScore(round-1, 2);
        }
    }

    private void ShootFish(int controller)
    {
        if( bearPlayer.PlayerNumber == controller)
            return;

        if( fishCount[controller-1] > 0 )
        {
            ObjectPool.instance.GetObject("P" + controller.ToString() + "_Fish");
            fishCount[controller-1]--;
            shootTime[controller-1] = Time.time;
            totalFishLeft--;

            if( totalFishLeft <= 0 )
            {
                EndRound();
            }
        }
    }
}
