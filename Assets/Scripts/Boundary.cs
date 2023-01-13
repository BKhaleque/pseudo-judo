using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public BattleManager battleManager;
    public bool roundOver = false;

    private void OnTriggerEnter(Collider other)
    {
        // Checks that both players are in the Arena at the start
//         switch (other.tag)
//         {
//             case "Player 1":
// //                Debug.Log("Player 1 is in the Arena");
//                 break;
//             case "Player 2":
//      //           Debug.Log("Player 2 is in the Arena");
//                 break;
//             default:
//                 break;
//         }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks which player was just eliminated from the Arena, to award the point to the other player      
        switch (other.tag)
        {
            case "Player 1":
                Debug.Log("Player 1 is out of the Arena");
                if (!roundOver)
                {
                    roundOver = true;
                    battleManager.PlayerEliminated(1);
                }
                break;
            case "Player 2":
                Debug.Log("Player 2 is out of the Arena");
                if (!roundOver)
                {
                    roundOver = true;
                    battleManager.PlayerEliminated(2);
                }
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        battleManager = BattleManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
