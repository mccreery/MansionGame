using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Walk : MonoBehaviour
{
    public float walkSpeed = 1.5f;

    private CharacterController characterController;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 walkDirection = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 walkVelocity = Vector3.ClampMagnitude(walkDirection, 1) * walkSpeed;

        characterController.SimpleMove(walkVelocity);
    }
}
