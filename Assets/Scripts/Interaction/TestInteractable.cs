using UnityEngine;

namespace LastShift
{
    public class TestInteractable : InteractableBase
    {
        [SerializeField] private string message = "Phase 3B interaction test triggered.";

        protected override void OnInteract()
        {
            Debug.Log($"[InteractionTest] {message}");
        }
    }
}
