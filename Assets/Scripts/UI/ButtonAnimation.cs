using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
     

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private UnityEvent OnClick;

    private Vector2 buttonDefaultSize;
    private Button button;

    private float growAmount = 1.2f;
    private float animationTime = 0.05f;
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
        LeanTween.scale(gameObject, buttonDefaultSize * growAmount, animationTime).setEaseOutCubic().setLoopPingPong(1).setOnComplete(() => 
        {          
            button.interactable = true;

        });
    }

    private IEnumerator WaitToInvokeOnClickEvent()
    {
        yield return new WaitForSeconds(animationTime/4);
        OnClick?.Invoke();
    }
   
}
