using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Walk : MonoBehaviour
{
    public float jumpVelocity = 1f;
    public float gravity = 1f;

    public string jumpButton = "Jump";
    public string crouchButton = "Crouch";

    public float walkSpeed = 1.5f;

    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private float yVelocity;

    private void Update()
    {
        Vector3 walkDirection = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 walkVelocity = Vector3.ClampMagnitude(walkDirection, 1) * walkSpeed;

        if (characterController.isGrounded && Input.GetButtonDown(jumpButton))
        {
            yVelocity = jumpVelocity;
        }
        else if ((characterController.collisionFlags & CollisionFlags.Above) != 0 && yVelocity > 0)
        {
            yVelocity = 0;
        }
        else
        {
            yVelocity -= gravity * Time.deltaTime;
        }

        characterController.Move((walkVelocity + Vector3.up * yVelocity) * Time.deltaTime);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, 5, 100, 100), characterController.isGrounded.ToString());
    }
}
