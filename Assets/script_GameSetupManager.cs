using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_GameSetupManager : MonoBehaviour
{
    //GAME OBJECTS
    public GameObject leaderSelectionPanel;
    public GameObject[] toHideOnLeaderSelection;
    private script_LeaderChoosePanel leaderChoosePanel;
    public GameObject[] mainMenuObjects;

    public enum_Leaders[] leaderOptions;
    
    
    public void MoveToLeaderSelection(){
        for (int i = 0; i< toHideOnLeaderSelection.Length; i++){
            toHideOnLeaderSelection[i].SetActive(false);
        }
        leaderSelectionPanel.SetActive(true);
        leaderChoosePanel = leaderSelectionPanel.GetComponent<script_LeaderChoosePanel>();
        leaderChoosePanel.Bind(leaderOptions);
    }

    public void ActivateMainMenu(){
        for (int i = 0; i< mainMenuObjects.Length; i++){
            mainMenuObjects[i].SetActive(true);
        }
    }
}
