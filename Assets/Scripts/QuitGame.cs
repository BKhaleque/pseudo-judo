using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame: MonoBehaviour
{
    public void QuitTheGame()
    {
        Application.Quit();
        Debug.Log("The game has now quit.");
    }

    public void ChangeRoundCount(int roundCount)
    {
        PlayerCharacterHolder.pointsToWin = roundCount;
        Debug.Log("Rounds to win: " + PlayerCharacterHolder.pointsToWin.ToString());
    }
}
