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
}
