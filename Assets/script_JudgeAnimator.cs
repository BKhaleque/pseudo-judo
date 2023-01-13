using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_JudgeAnimator : MonoBehaviour
{
    [SerializeField] private Transform leftArm;
    [SerializeField] private Transform rightArm;
    public GameObject speachBubblePrefab;

    private string[] thingsToSay = new string[]{"I love wrestling!", "One is very amused!", "Oh yeah!", "What a fight!", "Give him the chair!", "This beats solving world hunger","WHAT?", "TRANSFERENCE" ,"Boooo!"};


    private float minMoveTime = 0.1f;
    private float maxMoveTime = 0.5f;

    private float minRotation = -80f;
    private float maxRotation = 60f;

    private float maxMoveDist = 0.4f;
    private float moveTimer = 0f;
    private float startY = 0f;
    private float initialY = 0f;
    private float targetY = 0f;
    private float moveTimeLength = 0f;

    private float rightInitialRot  = 0f;
    private float rightTargetRot = 0f;

    private float leftInitialRot = 0f;
    private float leftTargetRot = 0f;

    private float rightTimer = 0f;
    private float leftTimer = 0f;

    private float rightTargetTime = 0f;
    private float leftTargetTime = 0f;

    private float minSpeachInterval = 5f;
    private float maxSpeachInterval = 60f;
    private float speachTimer = 0f;
    private float speachSpawnTarget = 0f;

    public void Start(){
        leftInitialRot = 0f;
        rightInitialRot = 0f;
        leftTargetRot = Random.Range(minRotation, maxRotation);
        rightTargetRot = Random.Range(minRotation, maxRotation);
        rightTargetTime = Random.Range(minMoveTime, maxMoveTime);
        leftTargetTime = Random.Range(minMoveTime, maxMoveTime);

        startY = this.transform.position.y;
        initialY = startY;
        targetY = Random.Range(startY-maxMoveDist, startY+maxMoveDist);

        moveTimeLength= Random.Range(0.05f, 0.2f);

        speachSpawnTarget = Random.Range(minSpeachInterval, maxSpeachInterval);
    }


    public void Update(){
        rightTimer+=Time.deltaTime;
        leftTimer+=Time.deltaTime;
        moveTimer+=Time.deltaTime;
        speachTimer+=Time.deltaTime;

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

            //Debug.Log("Initial rot, target rot, target time " + leftInitialRot+"," + leftTargetRot+","+leftTargetTime);
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

        //Movement logic
        float newY = Mathf.Lerp(initialY, targetY, moveTimer/moveTimeLength);

        Vector3 currPos = this.transform.position;

        this.transform.position = new Vector3(currPos.x, newY, currPos.z);

        if(moveTimer>moveTimeLength){
            moveTimer = 0f;
            targetY = Random.Range(startY-maxMoveDist, startY+maxMoveDist);
            initialY = this.transform.position.y;
        }

        //Speach Logic
        if(speachTimer>speachSpawnTarget){
            Vector3 judgePos = this.transform.position;
            Vector3 spawnPos = new Vector3(judgePos.x+3f, judgePos.y+8f, judgePos.z);
            GameObject speachBubObj = Instantiate(speachBubblePrefab, spawnPos, this.transform.rotation, this.transform );
            string text = thingsToSay[Random.Range(0, thingsToSay.Length)];
            speachBubObj.GetComponent<script_SpeachBubble>().Bind(text, 2f, 1f);
            speachTimer = 0f;
            speachSpawnTarget=Random.Range(minSpeachInterval, maxSpeachInterval);
            
        }


    }
}
