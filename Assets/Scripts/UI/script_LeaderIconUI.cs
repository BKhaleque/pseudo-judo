using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class script_LeaderIconUI : MonoBehaviour
{
    [SerializeField] private Sprite BorisFace;
    [SerializeField] private string BorisName;
    [SerializeField] private Sprite PutinFace;
    [SerializeField] private string PutinName;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text iconText;

    private script_LeaderChoosePanel leaderPanel;
    [SerializeField] GameObject selectedP1Panel;
    [SerializeField] GameObject selectedP2Panel;

    private enum_Leaders leader;


    public void Bind(enum_Leaders newLeader, script_LeaderChoosePanel lp){

        leader = newLeader;
        leaderPanel = lp;
        switch(newLeader){
            case enum_Leaders.BORIS:
                iconImage.sprite = BorisFace;
                iconText.text = BorisName;
                break;
            case enum_Leaders.PUTIN:
                iconImage.sprite = PutinFace;
                iconText.text = PutinName;
                break;
                
            default:
                iconImage.sprite = BorisFace;
                iconText.text = BorisName;
                Debug.Log("Leader selection not recognised. Giving you a default icon :(");
                break;
        }

    }

    public void SelectLeader(){
        leaderPanel.SetSelectedLeader(leader, this);
        Debug.Log("Temp leader selected " + leader);

        int playerSelected = leaderPanel.GetNextPlayerToSelect();

        if(playerSelected==1){
            selectedP1Panel.SetActive(true);
        }
        else{
            selectedP2Panel.SetActive(true);
        }

    }

    public void DeselectLeader(int i){
        if(i==1){
            selectedP1Panel.SetActive(false);

        }
        else if(i==2){
            selectedP2Panel.SetActive(false);


        }
    }
}
