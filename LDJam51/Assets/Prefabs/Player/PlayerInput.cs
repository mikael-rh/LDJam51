using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // HACK: member state should be scriptable object
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float turnRate;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private Transform cameraTransform;

    public Vector3 playerVelocity;

    private void Start()
    {
        playerVelocity = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        {
            // HACK: Do not use input actions, but read the input device directly 
            var mouse = Mouse.current;
            Quaternion playerRotation = Quaternion.Euler(
                0f,
                mouse.delta.x.ReadValue() * turnRate,
                0f
            );
            transform.rotation *= playerRotation;

            Quaternion cameraRotation = Quaternion.Euler(
                mouse.delta.y.ReadValue() * -turnRate,
                0f,
                0f
            );
            cameraTransform.rotation *= cameraRotation;

            mouse.WarpCursorPosition(new Vector2(0, 0));
        }

        { 
            // HACK: Do not use input actions, but read the input device directly 
            var keyboard = Keyboard.current;
            bool groundedPlayer = controller.isGrounded;

            {
                Vector3 inputMovement = new Vector3();
                if (keyboard.wKey.isPressed)
                {
                    inputMovement += transform.forward;
                }
                if (keyboard.sKey.isPressed)
                {
                    inputMovement -= transform.forward;
                }

                if (keyboard.aKey.isPressed)
                {
                    inputMovement -= transform.right;
                }
                if (keyboard.dKey.isPressed)
                {
                    inputMovement += transform.right;
                }
                controller.Move(inputMovement * moveSpeed * Time.deltaTime);
            }

            // gravity does not affect grounded player
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            // apply jump height to player velocity 
            if (groundedPlayer && keyboard.spaceKey.wasPressedThisFrame)
            {
                // amplify by the gravity to get closer to configured jump height
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            }
            playerVelocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
