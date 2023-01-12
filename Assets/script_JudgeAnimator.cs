using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_JudgeAnimator : MonoBehaviour
{
    [SerializeField] private Transform leftArm;
    [SerializeField] private Transform rightArm;


    private float minMoveTime = 0.1f;
    private float maxMoveTime = 0.5f;

    private float minRotation = -80f;
    private float maxRotation = 60f;

    private float rightInitialRot  = 0f;
    private float rightTargetRot = 0f;

    private float leftInitialRot = 0f;
    private float leftTargetRot = 0f;

    private float rightTimer = 0f;
    private float leftTimer = 0f;

    private float rightTargetTime = 0f;
    private float leftTargetTime = 0f;

    public void Start(){
        leftInitialRot = 0f;
        rightInitialRot = 0f;
        leftTargetRot = Random.Range(minRotation, maxRotation);
        rightTargetRot = Random.Range(minRotation, maxRotation);
        rightTargetTime = Random.Range(minMoveTime, maxMoveTime);
        leftTargetTime = Random.Range(minMoveTime, maxMoveTime);
    }


    public void Update(){
        rightTimer+=Time.deltaTime;
        leftTimer+=Time.deltaTime;

        //left arm logic
        float newLeftRot = Mathf.Lerp(leftInitialRot, leftTargetRot, (leftTimer/leftTargetTime));
        Vector3 currLeftRot = leftArm.localEulerAngles;
        leftArm.localEulerAngles = new Vector3(currLeftRot.x, currLeftRot.y, newLeftRot);

        if(leftTimer>leftTargetTime){
            leftInitialRot = leftArm.localEulerAngles.z;
            leftInitialRot = (leftInitialRot > 180) ? leftInitialRot - 360 : leftInitialRot; //Get angle with +ive or -ive value
            leftTargetRot = Random.Range(minRotation, maxRotation);
            leftTargetTime = Random.Range(minMoveTime, maxMoveTime);
            leftTimer = 0f;

            Debug.Log("Initial rot, target rot, target time " + leftInitialRot+"," + leftTargetRot+","+leftTargetTime);
        }

        //right arm logic
        float newrightRot = Mathf.Lerp(rightInitialRot, rightTargetRot, (rightTimer/rightTargetTime));
        Vector3 currRightRot = rightArm.localEulerAngles;
        rightArm.localEulerAngles = new Vector3(currRightRot.x, currRightRot.y, newrightRot);

        if(rightTimer>rightTargetTime){
            rightInitialRot = rightArm.localEulerAngles.z;
            rightInitialRot = (rightInitialRot > 180) ? rightInitialRot - 360 : rightInitialRot; //Get angle with +ive or -ive value
            rightTargetRot = Random.Range(minRotation, maxRotation);
            rightTargetTime = Random.Range(minMoveTime, maxMoveTime);
            rightTimer = 0f;
        }
    }
}
