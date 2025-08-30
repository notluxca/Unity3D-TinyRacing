using TMPro;
using UnityEngine;

public class SpeedUicontroller : MonoBehaviour
{
    [SerializeField] private Rigidbody targetRigidbody;
    [SerializeField] private TextMeshProUGUI speedText;

    void Update()
    {
        if (targetRigidbody != null && speedText != null)
        {
            float speed = targetRigidbody.linearVelocity.magnitude;

            // Normalize speed (0 → 30) to (0 → 1)
            float normalized = Mathf.InverseLerp(0f, 30f, speed);

            // Scale to RPM (0 → 2600)
            int rpm = Mathf.RoundToInt(normalized * 2600f);

            speedText.text = $"{rpm} RPM";
        }
    }
}
