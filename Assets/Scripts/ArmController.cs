using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    //protected Animator animator;

    public bool ikActive;

    private List<GameObject> bodyParts;
    // public Transform rightHandObj = null;
    //public Transform lookObj = null;
    private GameObject arm;


    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        bodyParts = new List<GameObject>();
        if (gameObject.transform.childCount > 0)
        {
            foreach (Transform gameObj in gameObject.transform)
            {
                Searcher(bodyParts, gameObj.gameObject);
            }
        }
    }

    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount <= 0) return;
        foreach (Transform gameObj in root.transform)
        {
            if (gameObj.tag.Equals("arm"))
                arm = gameObj.gameObject;
            Searcher(list, gameObj.gameObject);
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // bodyPart.transform.position = new Vector3(bodyPart.transform.position.x+0.01f,bodyPart.transform.position.y+0.03f,bodyPart.transform.position.z);
                    var eulerAngles = arm.transform.eulerAngles;
                    arm.transform.eulerAngles = new Vector3(eulerAngles.x,
                        eulerAngles.y, eulerAngles.z + 10f);
        }

        
    if (Input.GetKeyDown(KeyCode.K))
        {
            // bodyPart.transform.position = new Vector3(bodyPart.transform.position.x+0.01f,bodyPart.transform.position.y+0.03f,bodyPart.transform.position.z);
                    var eulerAngles = arm.transform.eulerAngles;
                    arm.transform.eulerAngles = new Vector3(eulerAngles.x,
                        eulerAngles.y, eulerAngles.z - 10f);

        }
        
    }
}