using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private Interactable selectedInteractable;

    private bool canInteract = true;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.PlayerActions.Interact.performed += Interact;
        PlayerController.OnReciveWalkInput += IdentifyInteractables;
    }

    public void IdentifyInteractables(Vector2 direction) //Indentify and interactable object in front of player
    {
        if (direction == Vector2.zero)
        {
            return;
        }

        Vector2 detectorPosition = transform.position;
        RaycastHit2D hit = new RaycastHit2D();
        float offSetAmout = 0.6f;
        float rayRange = offSetAmout * 1.25f;


        if (direction.x != 0 ) //Verify if player moving horizontaly or verticaly.
        {
            Vector2 rayDirection = transform.right * direction.x; //Verify if Ray goes Right or Left
            float rayOffSet = detectorPosition.x + (offSetAmout * direction.x); //Defines rayOrigin based on the direction
            hit = Physics2D.Raycast(new Vector2(rayOffSet, detectorPosition.y), rayDirection, rayRange);
            Debug.DrawRay(new Vector2(rayOffSet, detectorPosition.y), rayDirection * offSetAmout, Color.red) ;
        }

        if (direction.y != 0)
        {
            Vector2 rayDirection = transform.up * direction.y; //Verify if Ray goes Up or Down
            float rayOffSet =detectorPosition.y + (offSetAmout * direction.y); //Defines rayOrigin based on the direction
            hit = Physics2D.Raycast(new Vector2(detectorPosition.x, rayOffSet), rayDirection, rayRange);
            Debug.DrawRay(new Vector2(detectorPosition.x, rayOffSet), rayDirection * offSetAmout, Color.red);
        }

        

        if (hit == false) //If raycast dont find anything
        {
            DeselectIntractable();
            return;

        }

        Debug.Log(hit.transform.name);
        Interactable newInteractable = hit.transform.gameObject.GetComponent<Interactable>();

        if (newInteractable == null) //If found object is not interactable
        {
            DeselectIntractable();
            return;
        }

        if (newInteractable.canBeInteracted == false) //If found interactable cannot be interacted
        {
            DeselectIntractable();
            return;
        }

        if (selectedInteractable == null) 
        {
            SelectIntractable(newInteractable);

            return;
        }

        if (selectedInteractable == newInteractable)
        {
            return;
        }

        SelectIntractable(newInteractable);

    }

    private void SelectIntractable(Interactable newInteractable)
    {
        DeselectIntractable();
        selectedInteractable = newInteractable;
        selectedInteractable.Select();
    }

    private void DeselectIntractable()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Deselect();
        }

        selectedInteractable = null;
    }

    public void EnableInteraction()
    {
        canInteract = true;
    }

    public void DisableInteraction()
    {
        canInteract = false;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (canInteract == false)
        {
            return;
        }

        if (selectedInteractable == null)
        {
            return;
        }

        selectedInteractable.CallInteraction();
    }

    private void OnDisable()
    {
        playerInput.PlayerActions.Interact.performed -= Interact;
        PlayerController.OnReciveWalkInput -= IdentifyInteractables;
    }

}
