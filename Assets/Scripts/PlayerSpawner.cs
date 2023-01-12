using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject myPlayer;
    public Material myColour;
    public bool isPlayerOne;

    public void SpawnPlayer()
    {
        GameObject Player = Instantiate(myPlayer, transform.position, transform.rotation);
        Player.GetComponent<CharacterController>().isPlayerOne = isPlayerOne;
        
        // Recolours the player character as appropriate
        Player.transform.GetChild(0).Find("Mesh").gameObject.GetComponent<SkinnedMeshRenderer>().material = myColour;

        if (isPlayerOne)
        {
            // Updates the BodyCollider hitbox to have the correct tag
            // This way, it can correctly check which player is which, even in mirror matches
            Player.tag = "Player 1";
            Player.transform.GetChild(0).GetChild(1).GetChild(0).Find("BodyCollider").tag = "Player 1";
        }
        else
        {
            Player.tag = "Player 2";
            Player.transform.GetChild(0).GetChild(1).GetChild(0).Find("BodyCollider").tag = "Player 2";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
