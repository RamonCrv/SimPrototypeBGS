using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private bool _enableButtonOnAnimationEnds = true;
    [SerializeField] private UnityEvent OnClick;

    private Vector2 buttonDefaultSize;
    private Button button;

    private float growAmount = 0.8f;
    private float animationTime = 0.1f;
    private void Awake()
    {
        buttonDefaultSize = gameObject.transform.localScale;
        button = GetComponent<Button>();
    }
    public void AnimateOnClick()
    {
        button.interactable = false;
        StartCoroutine(WaitToInvokeOnClickEvent());
        LeanTween.cancel(gameObject);

        float ajustedAnimationTime = animationTime * Time.timeScale;
        LeanTween.scale(gameObject, buttonDefaultSize * growAmount, ajustedAnimationTime).setEaseOutCubic().setLoopPingPong(1).setOnComplete(EnableButton);
    }

    private void EnableButton()
    {
        if (_enableButtonOnAnimationEnds == false) { return; }
        button.interactable = true;

    }

    private IEnumerator WaitToInvokeOnClickEvent()
    {
        yield return new WaitForSecondsRealtime(animationTime / 2);
        OnClick?.Invoke();
    }

}
