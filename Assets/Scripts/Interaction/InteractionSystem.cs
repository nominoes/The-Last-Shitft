using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LastShift
{
    [DefaultExecutionOrder(100)]
    public class InteractionSystem : MonoBehaviour
    {
        [Header("Detection")]
        [SerializeField] private float interactionDistance = 3f;
        [SerializeField] private LayerMask interactionMask = ~0;
        [SerializeField] private QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore;

        [Header("Input")]
        [SerializeField] private Key interactionKey = Key.E;

        [Header("Prompt")]
        [SerializeField] private InteractionPrompt prompt;

        private Camera playerCamera;
        private IInteractable currentTarget;

        public IInteractable CurrentTarget => currentTarget;

        public event Action<IInteractable> FocusChanged;

        private void Awake()
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
                playerCamera = GetComponentInChildren<Camera>();

            if (prompt == null)
                prompt = CreatePrompt();
        }

        private void Update()
        {
            UpdateFocus();

            Keyboard keyboard = Keyboard.current;
            if (keyboard != null && keyboard[interactionKey].wasPressedThisFrame)
                TryInteract();
        }

        private void UpdateFocus()
        {
            IInteractable target = null;

            if (playerCamera != null)
            {
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

                if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactionMask, triggerInteraction))
                    target = hit.collider.GetComponentInParent<IInteractable>();
            }

            if (target != currentTarget)
            {
                currentTarget = target;
                FocusChanged?.Invoke(currentTarget);
            }

            if (currentTarget != null && currentTarget.CanInteract)
                prompt.Show(currentTarget.InteractionPrompt);
            else
                prompt.Hide();
        }

        private void TryInteract()
        {
            if (currentTarget == null || !currentTarget.CanInteract)
                return;

            currentTarget.Interact();
        }

        private InteractionPrompt CreatePrompt()
        {
            GameObject promptGameObject = new GameObject("InteractionPrompt");
            promptGameObject.transform.SetParent(transform, false);

            return promptGameObject.AddComponent<InteractionPrompt>();
        }
    }
}
