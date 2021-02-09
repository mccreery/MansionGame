using UnityEngine;

public class PointAtReticle : MonoBehaviour
{
    public Reticle reticle;

    public float maxRotateSpeed = 360;
    public float angleAtMaxSpeed = 20;

    public float maxAngleFromForward = 45;

    private void Update()
    {
        Quaternion targetRotation;
        if (reticle.Hit)
        {
            targetRotation = Quaternion.LookRotation(reticle.RaycastHit.point - transform.position);

            float angleFromForward = Quaternion.Angle(targetRotation, transform.parent.rotation);
            if (angleFromForward > maxAngleFromForward)
            {
                targetRotation = Quaternion.Slerp(transform.parent.rotation, targetRotation, maxAngleFromForward / angleFromForward);
            }
        }
        else
        {
            targetRotation = transform.parent.rotation;
        }

        float angle = Quaternion.Angle(targetRotation, transform.rotation);
        float speed = Mathf.InverseLerp(0, angleAtMaxSpeed, angle) * maxRotateSpeed;

        // Multiply speed by remaining angle to slow down when closer
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
