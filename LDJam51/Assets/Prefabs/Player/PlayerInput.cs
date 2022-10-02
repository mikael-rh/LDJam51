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
    private float sprintSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float turnRate;

#if DEBUG
    [Tooltip("disable player grabbing mouse :)")]
    [SerializeField]
    private bool disableMouseGrab;
#endif // DEBUG

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Vector3 cameraRotationBounds;

    [SerializeField]
    private PlayerState playerState;

    private Vector3 playerVelocity;

    private void Start()
    {
        Debug.Assert(moveSpeed < sprintSpeed);
        Debug.Assert(playerState != null);

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
            cameraTransform.localRotation = ClampRotation(cameraTransform.localRotation * cameraRotation, cameraRotationBounds);

            if (mouse.leftButton.wasPressedThisFrame)
            {
                playerState.Dagger.Fire(cameraTransform.forward);
            }

#if DEBUG
            if (disableMouseGrab == false)
            {
                mouse.WarpCursorPosition(new Vector2(0, 0));
            }
#else
            mouse.WarpCursorPosition(new Vector2(0, 0));
#endif // DEBUG

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

                // either use movement speed, or sprint speed
                float speedModifer;
                if (keyboard.shiftKey.isPressed && playerState.DrainStamina(Time.deltaTime))
                {
                    speedModifer = sprintSpeed;
                } else
                {
                    speedModifer = moveSpeed;
                }

                controller.Move(inputMovement * speedModifer * Time.deltaTime);
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

            playerState.LookingForGoal = keyboard.tabKey.isPressed;
        }
    }

    private static Quaternion ClampRotation(Quaternion q, Vector3 bounds)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -bounds.x, bounds.x);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);
        angleY = Mathf.Clamp(angleY, -bounds.y, bounds.y);
        q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

        float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
        angleZ = Mathf.Clamp(angleZ, -bounds.z, bounds.z);
        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

        return q.normalized;
    }
}
