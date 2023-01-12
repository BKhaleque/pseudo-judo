using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListSelector : MonoBehaviour
{
    public PlayerSpawner playerOneSpawn;
    public PlayerSpawner playerTwoSpawn;

    public string playerOneCharacterName;
    public string playerTwoCharacterName;

    public GameObject playerOneCharacter;
    public GameObject playerTwoCharacter;

    public GameObject[] characterRoster;

    GameObject SelectYourCharacter(string characterName, int playerNumber)
    {
        GameObject myCharacter;

        if(characterName == null)
        {
            characterName = "";
        }

        switch(characterName.ToLower())
        {
            case "putin":
                myCharacter = characterRoster[0];
                break;

            case "boris":
                myCharacter = characterRoster[1];
                break;

            default:
                if (playerNumber == 1)
                {
                    myCharacter = characterRoster[0];
                }
                else
                {
                    myCharacter = characterRoster[1];
                }
                break;
        }

        return myCharacter;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerOneCharacterName = PlayerCharacterHolder.playerOneCharacterName;
        playerTwoCharacterName = PlayerCharacterHolder.playerTwoCharacterName;
        playerOneCharacter = SelectYourCharacter(playerOneCharacterName, 1);
        playerTwoCharacter = SelectYourCharacter(playerTwoCharacterName, 2);
        playerOneSpawn.myPlayer = playerOneCharacter;
        playerTwoSpawn.myPlayer = playerTwoCharacter;

        playerOneSpawn.isPlayerOne = true;
        playerTwoSpawn.isPlayerOne = false;


        playerOneSpawn.SpawnPlayer();
        playerTwoSpawn.SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
