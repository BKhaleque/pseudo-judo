using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_LeaderChoosePanel : MonoBehaviour
{

    //GAME OBJECTS
    public GameObject leaderIconPrefab;
    public Transform leaderIconGroup;
    private List<GameObject> leaderIcons = new List<GameObject>();
    public GameObject selectButton;
    public GameObject fightButton;

    //DYNAMIC VARIABLES
    private enum_Leaders tempSelectedLeader;
    private enum_Leaders p1Leader;
    private bool p1Selected = false;
    private script_LeaderIconUI p1Icon;
    private enum_Leaders p2Leader;
    private bool p2Selected = false;
    private script_LeaderIconUI p2Icon;



    public void Update(){
        //Present players with the select button if conditions are met
        if(tempSelectedLeader!=enum_Leaders.NONE&&(!p1Selected||!p2Selected)&&!selectButton.activeSelf){
            selectButton.SetActive(true);
        }

        //Present players with the select button if conditions are met
        if((p1Selected&&p2Selected)&&!fightButton.activeSelf){
            fightButton.SetActive(true);
        }

    }



    public void Bind(enum_Leaders[] leaders){   

        for (int i =0; i<leaders.Length; i++){
            GameObject newIcon = Instantiate(leaderIconPrefab, leaderIconGroup.position, leaderIconGroup.rotation, leaderIconGroup );
            newIcon.GetComponent<script_LeaderIconUI>().Bind(leaders[i], this);
            leaderIcons.Add(newIcon);

        }
    }

    public void ClearAllIcons(){

        foreach(GameObject icon in leaderIcons)
        {  
            Destroy(icon);
        }
        leaderIcons = new List<GameObject>();
        p1Selected=false;
        p2Selected = false;
        p1Leader = enum_Leaders.NONE;
        p2Leader = enum_Leaders.NONE;
        selectButton.SetActive(false);
        fightButton.SetActive(false);

    }

    public void SetSelectedLeader(enum_Leaders selected, script_LeaderIconUI icon){
        tempSelectedLeader = selected;
        //Remove previous Selection UI if present
        if(!p1Selected&&p1Icon!=icon&&p1Icon!=null){
            p1Icon.DeselectLeader(1);
        }
        else if(p1Selected&&p2Icon!=icon&&p2Icon!=null){
            p2Icon.DeselectLeader(2);
        }

        //Update selected icon
        if(!p1Selected){
            p1Icon=icon;
        }
        else{
            p2Icon=icon;
        }
    }

    public void ConfirmSelection(){
        if(tempSelectedLeader!=enum_Leaders.NONE){
            if(!p1Selected){
                p1Leader = tempSelectedLeader;
                tempSelectedLeader=enum_Leaders.NONE;
                p1Selected = true;
            }
            else if(!p2Selected){
                p2Leader = tempSelectedLeader;
                tempSelectedLeader=enum_Leaders.NONE;
                p2Selected = true;
            }

        }
        selectButton.SetActive(false);
    }

    public void StartFight(){
        Debug.Log("P1 Leader: " + p1Leader + " P2 Leader: " +p2Leader);
        PlayerCharacterHolder.playerOneCharacterName = p1Leader.ToString();
        PlayerCharacterHolder.playerTwoCharacterName = p2Leader.ToString();
        SceneManager.LoadScene("FightScene");
    }

    public int GetNextPlayerToSelect(){
        if(!p1Selected){
            return 1;
        }
        else if(!p2Selected){
            return 2;
        }
        //This is a lazy hack, but 3 indicates that both players have seleted and no more input should be allowed
        else{
            return 3;
        }
    }
    

}
