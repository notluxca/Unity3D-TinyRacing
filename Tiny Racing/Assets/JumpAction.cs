using UnityEngine;

public class FakeJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpHeight = 2f;       // how high it jumps
    public float jumpDuration = 1f;     // total jump time (up + down)
    public float triggerDistance = 12f;  // distance to trigger jump
    public Transform target;            // player or object to check distance

    private bool isJumping = false;
    private Vector3 startPos;
    private float elapsedTime;


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        // Check distance if not currently jumping
        if (!isJumping && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= triggerDistance)
            {
                Jump();
            }
        }

        // Perform jump
        if (isJumping)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / jumpDuration;

            // Jump curve (0 → 1 → 0)
            float heightFactor = Mathf.Sin(t * Mathf.PI);

            transform.position = startPos + Vector3.up * jumpHeight * heightFactor;

            if (t >= 1f)
            {
                isJumping = false;
                transform.position = startPos; // snap back to ground
            }
        }
    }

    private void Jump()
    {
        isJumping = true;
        elapsedTime = 0f;
        startPos = transform.position;
    }
}
