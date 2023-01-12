using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private List<GameObject> bodyParts;
    private GameObject arms;
    private GameObject legs;


    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        bodyParts = new List<GameObject>();
        if (gameObject.transform.childCount <= 0) return;
        foreach (Transform gameObj in gameObject.transform)
        {
            Searcher(bodyParts, gameObj.gameObject);
        }
    }

    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount <= 0) return;
        foreach (Transform gameObj in root.transform)
        {
            switch (gameObj.tag)
            {
                case "arm":
                    arms = gameObj.gameObject;
                    break;
                case "leg":
                    legs = gameObj.gameObject;
                    break;
            }

            Searcher(list, gameObj.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            MoveArms(true);
        
        if (Input.GetKeyDown(KeyCode.K))
            MoveArms(false);
        
        if (Input.GetKeyDown(KeyCode.S))
            MoveLegs(true);
        
        if (Input.GetKeyDown(KeyCode.W))
            MoveLegs(false);
        


        
        
    }

    void MoveArms(bool up)
    {
        var eulerAngles = arms.transform.eulerAngles;
        var val = 10f;
        if (!up)
            val *= -1;
        
        arms.transform.eulerAngles = new Vector3(eulerAngles.x,
            eulerAngles.y, eulerAngles.z + val);
    }

    void MoveLegs(bool up)
    {
        var eulerAngles = legs.transform.eulerAngles;
        var val = 10f;
        if (!up)
            val *= -1;
        
        legs.transform.eulerAngles = new Vector3(eulerAngles.x,
            eulerAngles.y, eulerAngles.z + val);
    }
}