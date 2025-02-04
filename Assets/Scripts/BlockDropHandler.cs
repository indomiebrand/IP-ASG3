using UnityEngine;


public class BlockDropHandler : MonoBehaviour
{
    public char blockLetter; //set the correct letter for the block (e.g. 'l', 'i', 'o', 'n')
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private CorrectLetterSlot targetSlot;

    private Vector3 originalPosition; //to store the original position
    private Quaternion originalRotation; //to store the original rotation

    private void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        //store the original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    //method to assign the correct slot based on the block's letter
    public void SetTargetSlot(CorrectLetterSlot slot)
    {
        targetSlot = slot;
    }

    private void OnSelectExited(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        //check if the block's letter matches the target slot's expected letter
        if (targetSlot != null && blockLetter == targetSlot.correctLetter)
        {
            //snap the block to the slot position
            transform.position = targetSlot.transform.position;
            transform.rotation = targetSlot.transform.rotation;

            //optionally, disable grabbing after placement
            grabInteractable.enabled = false;
            GetComponent<Collider>().enabled = false; //disable the collider so the block doesn't move anymore
        }
        else
        {
            //if it's not the correct slot, return the block to its original position
            Debug.Log("incorrect slot, returning to original position!");
            transform.position = originalPosition;
            transform.rotation = originalRotation;

            //optionally, you can add feedback like a sound or visual effect here
        }
    }
}
