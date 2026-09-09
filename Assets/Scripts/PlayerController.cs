using UnityEngine;
using UnityEngine.InputSystem;

namespace LastShift
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 3.5f;
        [SerializeField] private float acceleration = 12f;
        [SerializeField] private float gravity = -18f;

        [Header("Look")]
        [SerializeField] private float lookSensitivity = 1.5f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;
        [SerializeField] private Transform cameraPivot;

        private CharacterController controller;
        private Vector3 moveVelocity;
        private Vector3 verticalVelocity;
        private float pitch;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();

            if (cameraPivot == null && transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                if (child.GetComponent<Camera>() != null)
                    cameraPivot = child;
            }

            if (cameraPivot == null)
                cameraPivot = transform;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleLook();
            HandleMovement();
            HandleCursor();
        }

        private void HandleLook()
        {
            Mouse mouse = Mouse.current;
            if (mouse == null)
                return;

            Vector2 delta = mouse.delta.ReadValue() * lookSensitivity;

            transform.Rotate(Vector3.up, delta.x);
            pitch = Mathf.Clamp(pitch - delta.y, minPitch, maxPitch);
            cameraPivot.localEulerAngles = new Vector3(pitch, 0f, 0f);
        }

        private void HandleMovement()
        {
            Keyboard keyboard = Keyboard.current;
            Vector2 input = Vector2.zero;

            if (keyboard != null)
            {
                if (keyboard.wKey.isPressed) input.y += 1f;
                if (keyboard.sKey.isPressed) input.y -= 1f;
                if (keyboard.aKey.isPressed) input.x -= 1f;
                if (keyboard.dKey.isPressed) input.x += 1f;
            }

            Vector3 targetVelocity = transform.forward * input.y + transform.right * input.x;
            if (targetVelocity.sqrMagnitude > 1f)
                targetVelocity.Normalize();
            targetVelocity *= walkSpeed;

            float blend = 1f - Mathf.Exp(-acceleration * Time.deltaTime);
            moveVelocity.x = Mathf.Lerp(moveVelocity.x, targetVelocity.x, blend);
            moveVelocity.z = Mathf.Lerp(moveVelocity.z, targetVelocity.z, blend);

            if (controller.isGrounded && verticalVelocity.y < 0f)
                verticalVelocity.y = -2f;

            verticalVelocity.y += gravity * Time.deltaTime;

            controller.Move((moveVelocity + verticalVelocity) * Time.deltaTime);
        }

        private void HandleCursor()
        {
            Keyboard keyboard = Keyboard.current;
            Mouse mouse = Mouse.current;

            if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (Cursor.lockState == CursorLockMode.None
                && mouse != null && mouse.leftButton.wasPressedThisFrame)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}