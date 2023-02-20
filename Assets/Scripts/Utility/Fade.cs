using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fade : MonoBehaviour
{  
    public static Fade Instance { get; private set; }
    [SerializeField] private bool FadeInOnStart;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (FadeInOnStart == true)
        {
            FadeIn(4);
        }

    }

    public void FadeIn(float fadeTime = 2)
    {
        LeanTween.cancel(gameObject);
        LeanTween.alphaCanvas(canvasGroup, 0, fadeTime).setEaseInOutSine();
        
        
    }

    public void FadeOut(float fadeTime = 2)
    {
        LeanTween.cancel(gameObject);
        LeanTween.alphaCanvas(canvasGroup, 1, fadeTime).setEaseInOutSine();

    }

 
}
