using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; } 
    public static Action OnStartDialogue;
    public static Action<string> OnDisplayNextSentence;
    public static Action OnFinishDialogue;

    private Queue<string> sentences;
    private Queue<string> names;
    private PlayerInput playerInput;

    public bool DialogueON { get; private set; }

    void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
        names = new Queue<string>();

        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.PlayerActions.SkipSentence.performed += SkipSentence;

    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (DialogueON == true)
        {
            return;
        }

        OnStartDialogue?.Invoke();
        DialogueON = true;

        sentences.Clear();
        names.Clear();

        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence.sentence);
            names.Enqueue(sentence.name);
  
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        string nextSentence = name + ": "+sentence;
        OnDisplayNextSentence?.Invoke(nextSentence);
        
    }

    public void EndDialogue()
    {
        DialogueON = false;
        OnFinishDialogue?.Invoke();    

    }


    private void SkipSentence(InputAction.CallbackContext context) //ON Hold
    {
        if (DialogueON == false)
        {
            return;
        }

        if (DialogueUI.FinishedDisplayingDialogue == false)
        {
            return;
        }

        DisplayNextSentence();

    }

    private void OnDisable()
    {
        playerInput.PlayerActions.SkipSentence.performed -= SkipSentence;
    }

}
