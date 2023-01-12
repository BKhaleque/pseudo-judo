using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class script_SpeachBubble : MonoBehaviour
{
    [SerializeField] private TMP_Text textMesh;
    [SerializeField] private SpriteRenderer image;

    private float initialY = 0f;

    private float lifeSpan = 2f;
    private float moveDist = 0f;

    private float timer = 0f;

    public void Update(){
        timer+=Time.deltaTime;
        Vector3 currPos = this.transform.position;
        float newY = 0f;
        if(timer<lifeSpan){
            SetAlpha(Mathf.Lerp(0f, 1f, timer/lifeSpan));
            newY = Mathf.Lerp(initialY, initialY+moveDist, timer/lifeSpan);
        }
        else{
            SetAlpha(Mathf.Lerp(1f, 0f, (timer-lifeSpan)/(lifeSpan)));
            newY = Mathf.Lerp(initialY+moveDist, initialY, timer/(lifeSpan*2));
        }

        if(timer>(lifeSpan*2)){
            Debug.Log("Destroying speach bubble as existed for " + timer);
            Destroy(gameObject);
        }

        currPos = new Vector3(currPos.x, newY, currPos.z );
    }


    public void Bind(string text, float lifeSpan, float moveDist){
        textMesh.text = text;
        lifeSpan = lifeSpan;
        moveDist = moveDist;
        initialY = this.transform.position.y;
    }

    private void SetAlpha(float newAlpha){
        Color prevTmCol = textMesh.color;
        textMesh.color  = new Color(prevTmCol.r, prevTmCol.g, prevTmCol.b, newAlpha);
        Color prevImCol = image.color;
        image.color  = new Color(prevImCol.r, prevImCol.g, prevImCol.b, newAlpha);
    }
}
