using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBoxUI;
    [SerializeField] private TextMeshProUGUI DialogueTextUI;
    [SerializeField] private GameObject NextSentenceArrow;

    private float openPosition = -365f;
    private float closedPosition = -750;
    private float animationTime = 0.4f;
    private float typingSpeed = 0.025f;

    public static bool FinishedDisplayingDialogue { get; private set; }

    private void Awake()
    {
        DialogueManager.OnStartDialogue += ShowDialogueBox;
        DialogueManager.OnFinishDialogue += HideDialogueBox;
        DialogueManager.OnDisplayNextSentence += DisplayDilogueText;
        FinishedDisplayingDialogue = true;
    }

    private void DisplayDilogueText(string sentence)
    {
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        FinishedDisplayingDialogue = false;
        NextSentenceArrow.SetActive(false);
        DialogueTextUI.text = "";
        foreach (char letter in sentence.ToCharArray())
        {

            DialogueTextUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        NextSentenceArrow.SetActive(true);
        FinishedDisplayingDialogue = true;
    }

    private void ShowDialogueBox()
    {
        LeanTween.moveLocalY(gameObject, openPosition, animationTime).setEaseOutBack();
    }

    private void HideDialogueBox()
    {
        LeanTween.moveLocalY(gameObject, closedPosition, animationTime).setEaseInBack();
    }

    private void OnDisable()
    {
        DialogueManager.OnStartDialogue -= ShowDialogueBox;
        DialogueManager.OnFinishDialogue -= HideDialogueBox;
        DialogueManager.OnDisplayNextSentence -= DisplayDilogueText;
    }

}
