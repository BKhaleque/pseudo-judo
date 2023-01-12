using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float movementUnits;
    public float forceModifier;
    public float maxVelocity;
    public float maxRotation;
    public float minRotation;
    public bool player1;
    //public bool player2;
    
    private List<GameObject> bodyParts;
    private GameObject arm1;
    private GameObject arm2;
    private GameObject leg1;
    private GameObject leg2;
    private Rigidbody rb;
    


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bodyParts = new List<GameObject>();
        if (gameObject.transform.childCount <= 0) return;
        foreach (Transform gameObj in gameObject.transform)
        {
            Searcher(bodyParts, gameObj.gameObject);
        }
    }
    //Get all children and grandchildren of current game obj recursively
    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount <= 0) return;
        foreach (Transform gameObj in root.transform)
        {
            switch (gameObj.tag)
            {
                case "arm":
                    if (arm1 == null)
                        arm1 = gameObj.gameObject;
                    else
                        arm2 = gameObj.gameObject;
                    break;
                case "leg":
                    if (leg1 == null)
                        leg1 = gameObj.gameObject;
                    else
                        leg2 = gameObj.gameObject;
                    break;
            }
            Searcher(list, gameObj.gameObject);
        }
        
        //leg1.transform.eulerAngles.z = Mathf.Clamp(leg1.transform.eulerAngles.y, -100, 100);
        //leg2.transform.eulerAngles.z = Mathf.Clamp(leg2.transform.eulerAngles.y, -100, 100);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (player1)
        {
            if (Input.GetKey(KeyCode.E))
            {
                MoveBodyPart(true, arm1);
                MoveBodyPart(true, arm2);
            }

            if (Input.GetKey(KeyCode.D))
            {
                MoveBodyPart(false, arm1);
                MoveBodyPart(false, arm2);
            }

            if (Input.GetKey(KeyCode.Q))
                MoveBodyPart(true, leg1);

            if (Input.GetKey(KeyCode.A))
                MoveBodyPart(false, leg1);

            if (Input.GetKey(KeyCode.W))
                MoveBodyPart(true, leg2);

            if (Input.GetKey(KeyCode.S))
                MoveBodyPart(false, leg2);
        }
        else
        {
            if (Input.GetKey(KeyCode.I))
            {
                MoveBodyPart(true, arm1);
                MoveBodyPart(true, arm2);
            }

            if (Input.GetKey(KeyCode.J))
            {
                MoveBodyPart(false, arm1);
                MoveBodyPart(false, arm2);
            }

            if (Input.GetKey(KeyCode.O))
                MoveBodyPart(true, leg1);

            if (Input.GetKey(KeyCode.K))
                MoveBodyPart(false, leg1);

            if (Input.GetKey(KeyCode.P))
                MoveBodyPart(true, leg2);

            if (Input.GetKey(KeyCode.L))
                MoveBodyPart(false, leg2);
        }

    }
    
    private void MoveBodyPart(bool up, GameObject bodyPart)
    {
        var localTrans = bodyPart.transform;
        var eulerAngles = bodyPart.transform.eulerAngles; //To control rotation
        //var zRotation;
        //zRotation = Mathf.Clamp(zRotation + eulerAngles.z, minRotation, maxRotation);
        var sign = 1;
        if (!up) //Move down instead
            sign *= -1;
        //localTrans.rotation = Quaternion.Euler(eulerAngles);
        var forceToAdd = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + (movementUnits*sign)); //Rotate bodypart
        bodyPart.transform.eulerAngles = forceToAdd;
        if(rb.velocity.magnitude < maxVelocity)
            rb.AddForceAtPosition(forceToAdd/forceModifier, bodyPart.transform.position);// Add force in the direction of movement at bodypart position

        
     
    }
}