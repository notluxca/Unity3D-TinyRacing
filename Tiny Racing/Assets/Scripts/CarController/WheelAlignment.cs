using UnityEngine;

public class WheelAlignment : MonoBehaviour
{
    public WheelCollider correspondingCollider;
    private float rotationVel = 0F;
    private Transform myTransform;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    public void Update()
    {
        RaycastHit hit;
        Vector3 colliderCenter = correspondingCollider.transform.TransformPoint(correspondingCollider.center);
        bool collided = Physics.Raycast(colliderCenter, 
                                        -correspondingCollider.transform.up,
                                        out hit,
                                        correspondingCollider.suspensionDistance + correspondingCollider.radius);
        if (collided)
        {
            myTransform.position = hit.point + (correspondingCollider.transform.up * correspondingCollider.radius);
        }
        else
        {
            myTransform.position = colliderCenter - (correspondingCollider.transform.up * correspondingCollider.suspensionDistance);
        }

        myTransform.rotation = correspondingCollider.transform.rotation * Quaternion.Euler(rotationVel, correspondingCollider.steerAngle, 0);
        rotationVel += correspondingCollider.rpm * (360 / 60) * Time.deltaTime;
    }

}
