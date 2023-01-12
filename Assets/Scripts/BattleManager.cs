using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public int pointsToVictory = 1;
    public static int playerOnePoints = 0;
    public static int playerTwoPoints = 0;

    public static bool firstRound = true;
    public static int currentRound = 1;

    public TMP_Text playerOneScore;
    public TMP_Text playerTwoScore;
    public TMP_Text maxScore;
    public TMP_Text roundStartPrompt;
    public FadeToBlack fadeToBlack;

    public bool fightBegin = false;

    void UpdateScoreUI()
    {
        playerOneScore.text = playerOnePoints.ToString();
        playerTwoScore.text = playerTwoPoints.ToString();
        maxScore.text = pointsToVictory.ToString();
        return;
    }

    void UpdateScore(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                playerOnePoints++;
                roundStartPrompt.text = "Player 1 wins the round point!";
                if (playerOnePoints >= pointsToVictory)
                {
                    DeclareVictory(playerNumber);
                }
                else
                {
                    StartCoroutine(RoundEndWaitForRestart());
                }
                UpdateScoreUI();
                break;
            case 2:
                playerTwoPoints++;
                roundStartPrompt.text = "Player 2 wins the round point.";
                if (playerTwoPoints >= pointsToVictory)
                {
                    DeclareVictory(playerNumber);
                }
                else
                {
                    StartCoroutine(RoundEndWaitForRestart());
                }
                UpdateScoreUI();
                break;
            default:
                StartCoroutine(RoundEndWaitForRestart());
                break;
        }
        return;
    }

    // Waits for a cooldown before everyone can fight
    IEnumerator StartFight()
    {
        int duration = 3;
        yield return new WaitForSeconds(duration);
        fightBegin = true;
        roundStartPrompt.text = "Fight!";
        yield return new WaitForSeconds(1);
        roundStartPrompt.text = "";
    }

    IEnumerator RoundEndWaitForRestart()
    {
        yield return new WaitForSeconds(3);
        Restart();
    }

    IEnumerator ReturnToMainMenu()
    {
        int duration = 3;

        yield return new WaitForSeconds(3);

        fadeToBlack.nowActive = true;
        fadeToBlack.duration = duration;

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene("StartMenuScene");
    }

    void Restart()
    {
        currentRound++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void DeclareVictory(int playerNumber)
    {
        roundStartPrompt.text = "Player " + playerNumber.ToString() + " Wins!";
        firstRound = true;
        StartCoroutine(ReturnToMainMenu());
        return;
    }

    public void PlayerEliminated(int playerNumber)
    {
        // Gets the opposite player number to the one who was eliminated
        int winner = (3 - playerNumber);
        UpdateScore(winner);
    }

    private void Awake()
    {
        if (instance != null)
        {
            // More than one Battle Manager in a scene
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sets default scores every time a new battle begins
        if (firstRound == true)
        {
            playerOnePoints = 0;
            playerTwoPoints = 0;
            firstRound = false;
            currentRound = 1;
        }

        UpdateScoreUI();
        StartCoroutine(StartFight());

        // The number of points to win is achieved in a maximum of its triangle number of rounds
        int maxRounds = (pointsToVictory * 2) - 1;

        if (currentRound < maxRounds)
        {
            Debug.Log("Round " + currentRound);
            if (playerOnePoints == pointsToVictory - 1)
            {
                roundStartPrompt.text = "Round " + currentRound.ToString() + "\nMatch Point: Player 1!";
            }
            else if (playerTwoPoints == pointsToVictory - 1)
            {
                roundStartPrompt.text = "Round " + currentRound.ToString() + "\nMatch Point: Player 2!";
            }
            else
            {
                roundStartPrompt.text = "Round " + currentRound.ToString();
            }
        }

        else
        {
            roundStartPrompt.text = "Final Round!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey("t"))
        {
            Restart();
        }
        */
    }
}
