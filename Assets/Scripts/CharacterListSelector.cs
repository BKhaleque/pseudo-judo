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

    public Material playerOneColour;
    public Material playerTwoColour;

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
            // Roster Slot 0 and 1 are reserved for Default characters
            case "putin":
                myCharacter = characterRoster[2];
                break;

            case "boris":
                myCharacter = characterRoster[3];
                break;

            case "biden":
                myCharacter = characterRoster[4];
                break;

            case "macron":
                myCharacter = characterRoster[5];
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

        playerOneSpawn.myColour = playerOneColour;
        playerTwoSpawn.myColour = playerTwoColour;

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
