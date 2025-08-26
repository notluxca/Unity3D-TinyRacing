using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Car transform

    [Header("Smoothing")]
    public float positionSmoothTime = 0.3f;
    public float rotationSmoothSpeed = 5f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 positionOffset;
    private Quaternion rotationOffset;

    void Start()
    {
        if (target == null) return;

        // Calculate initial offsets based on current placement
        positionOffset = target.InverseTransformPoint(transform.position);
        rotationOffset = Quaternion.Inverse(target.rotation) * transform.rotation;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // --- POSITION ---
        // Desired position relative to car
        Vector3 desiredPosition = target.TransformPoint(positionOffset);
        // Smooth follow
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, positionSmoothTime);

        // --- ROTATION ---
        // Desired rotation relative to car
        Quaternion desiredRotation = target.rotation * rotationOffset;
        // Smoothly interpolate
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
