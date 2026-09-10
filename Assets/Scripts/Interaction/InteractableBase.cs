using UnityEngine;

namespace LastShift
{
    [DisallowMultipleComponent]
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [Header("Interaction")]
        [SerializeField] private string promptText = "Interact";
        [SerializeField] private bool isInteractable = true;

        public string InteractionPrompt => promptText;

        public bool CanInteract => isInteractable && enabled;

        public void Interact()
        {
            if (!CanInteract)
                return;

            OnInteract();
        }

        protected abstract void OnInteract();

        protected void SetInteractable(bool value)
        {
            isInteractable = value;
        }
    }
}
