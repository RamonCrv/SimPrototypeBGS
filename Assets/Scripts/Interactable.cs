using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    [SerializeField] public bool canBeInteracted = true;
    [SerializeField] private UnityEvent OnInteract;
    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    public virtual void CallInteraction()
    {
        if (canBeInteracted == false)
        {
            return;
        }
        OnInteract?.Invoke();
    }

    public void Select()
    {
        material.SetFloat("_InnerOutlineAlpha", 0.25f);
    }

    public void Deselect()
    {
        material.SetFloat("_InnerOutlineAlpha", 0);
    }

}
