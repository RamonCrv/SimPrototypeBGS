using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableUI : MonoBehaviour
{
    [SerializeField] protected Vector2 openPosition;
    [SerializeField] protected Vector2 closePosition;
    [SerializeField] protected Vector2 openScale;

    protected float animationTime = 0.2f;
    protected bool isOpen = true;
    protected bool isAnimating = false;

    protected CanvasGroup canvasGroup;

    public virtual void Awake()
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

    public virtual void ShowUI()
    {
        isOpen = true;
        AnimatePanel(openPosition, openScale, 1);
    }

    public virtual void HideUI()
    {
        isOpen = false;
        if (canvasGroup != null)
        {
            AnimatePanel(closePosition, Vector2.zero, 0);
        }

    }

    protected void AnimatePanel(Vector2 position, Vector2 scale, float alpha)
    {
        LeanTween.moveLocal(gameObject, position, animationTime);
        LeanTween.scale(gameObject, scale, animationTime);
        LeanTween.alphaCanvas(canvasGroup, alpha, animationTime);
    }

    protected IEnumerator AnimationCouter()
    {
        isAnimating = true;
        yield return new WaitForSeconds(animationTime);
        isAnimating = false;
    }
}
