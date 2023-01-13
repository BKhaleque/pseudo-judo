using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float movementUnits;
    public float forceModifier;
    public float maxVelocity;
    //public float maxLegRotation;
    //public float maxArmRotation;
    //public float minLegRotation; 
    //public float minArmRotation; 
    public bool isPlayerOne;
    
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
        
        // Mathf.Clamp(leg1.transform.eulerAngles.y, -100, 100);
        // Mathf.Clamp(leg2.transform.eulerAngles.y, -100, 100);
        // Mathf.Clamp(arm1.transform.eulerAngles.y, minRotation, maxRotation);
        // Mathf.Clamp(arm2.transform.eulerAngles.y, minRotation, maxRotation);
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
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(BattleManager.instance.fightBegin)
        {
            if (isPlayerOne)
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

                if (Input.GetKey(KeyCode.A))
                    MoveBodyPart(true, leg1);

                if (Input.GetKey(KeyCode.Q))
                    MoveBodyPart(false, leg1);

                if (Input.GetKey(KeyCode.S))
                    MoveBodyPart(true, leg2);

                if (Input.GetKey(KeyCode.W))
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

                if (Input.GetKey(KeyCode.K))
                    MoveBodyPart(true, leg2);

                if (Input.GetKey(KeyCode.O))
                    MoveBodyPart(false, leg2);

                if (Input.GetKey(KeyCode.L))
                    MoveBodyPart(true, leg1);

                if (Input.GetKey(KeyCode.P))
                    MoveBodyPart(false, leg1);
            }

        }

    }
    
    private void MoveBodyPart(bool up, GameObject bodyPart)
    {
        var eulerAngles = bodyPart.transform.eulerAngles; //To control rotation
        var sign = 1;
        if (!up) //Move down instead
            sign *= -1;
        
        var signedAngle = (eulerAngles.z > 180) ? eulerAngles.z - 360 : eulerAngles.z; //Get angle with +ive or -ive value

        //var forceToAdd = Vector3.zero;
        // if (signedAngle < maxLegRotation && signedAngle > minLegRotation && bodyPart.tag.Equals("leg"))
        // {
        //     AddForceToRigidBody(rb, eulerAngles, sign, bodyPart, true);
        //     //Debug.Log("In correct range");
        //     //Debug.Log(signedAngle);
        //
        // }
        // else if( bodyPart.tag.Equals("leg"))
        // {
        //     AddForceToRigidBody(rb, eulerAngles, sign, bodyPart, false);
        //     //Debug.Log(eulerAngles.z);
        //     //Debug.Log(signedAngle);
        //
        // }

        // if (bodyPart.tag.Equals("arm"))
            AddForceToRigidBody(rb, eulerAngles, sign, bodyPart, true);

    }

    void AddForceToRigidBody(Rigidbody rb, Vector3 eulerAngles, float sign,GameObject bodyPart, bool inAngleRange)
    {
        var forceToAdd = Vector3.zero;
        if (inAngleRange)
        {
            forceToAdd = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z + (movementUnits * sign));
            if (rb.velocity.magnitude < maxVelocity)
                rb.AddForceAtPosition(forceToAdd / forceModifier,
                    bodyPart.transform.position); // Add force in the direction of movement at bodypart position
        }
        else
            forceToAdd = new Vector3(eulerAngles.x, eulerAngles.y, eulerAngles.z - (movementUnits*20* sign));
        
        bodyPart.transform.eulerAngles = forceToAdd;

    }
}