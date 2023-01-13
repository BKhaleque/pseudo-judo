using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public bool nowActive = false;
    private bool atMaxAlpha = false;
    public Image image;
    private float maxAlpha;
    private float startAlpha;
    public float duration;
    private float lerp = 0f;

    void SetAlpha(float alpha)
    {
        var tempColor = image.color;
        tempColor.a = alpha;
        image.color = tempColor;
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        nowActive = true;
        Debug.Log("Fading to black.");
        image = gameObject.GetComponent<Image>();
        maxAlpha = 1f;
        SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Slowly fades to black at the end of the game, as the BattleManager loads the Main Menu
        if (nowActive)
        {
            if (!atMaxAlpha)
            {
                lerp += Time.deltaTime / duration;
                float myAlpha = Mathf.Lerp(startAlpha, maxAlpha, lerp);
                SetAlpha(myAlpha);

                if (myAlpha >= maxAlpha)
                {
                    atMaxAlpha = true;
                }
            }
        }
    }
}
