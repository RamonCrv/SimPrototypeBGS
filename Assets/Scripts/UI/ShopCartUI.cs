using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCartUI : MonoBehaviour
{
    private Vector2 openPosition = new Vector2(700, -100);
    private Vector2 closePosition = new Vector2(915, -360);
    private Vector2 openScale = new Vector2(1.2f, 1.2f);
    private CanvasGroup canvasGroup;   
    private float animationTime = 0.2f;
    private bool isOpen = true;
    private bool isAnimating = false;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HideUI();

    }

    public void ShowOrCloseUI()
    {
        if (isAnimating == true)
        {
            return;

        }
        
        StartCoroutine(AnimationCouter());
        LeanTween.cancel(gameObject);
        if (isOpen == true)
        {
            HideUI();
        }
        else
        {           
            ShowUI();

        }
    }

    private void ShowUI()
    {
        isOpen = true;
        AnimatePanel(openPosition, openScale, 1);
    }

    private void HideUI()
    {
        isOpen = false;
        AnimatePanel(closePosition, Vector2.zero, 0);
    }

    private void AnimatePanel(Vector2 position, Vector2 scale, float alpha)
    {
        LeanTween.moveLocal(gameObject, position, animationTime);
        LeanTween.scale(gameObject, scale, animationTime);
        LeanTween.alphaCanvas(canvasGroup, alpha, animationTime);
    }

    private IEnumerator AnimationCouter()
    {
        isAnimating = true;
        yield return new WaitForSeconds(animationTime);
        isAnimating = false;
    }

}
