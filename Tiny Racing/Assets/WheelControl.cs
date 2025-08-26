using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform wheelModel;

    [HideInInspector] public WheelCollider WheelCollider;

    public bool steerable;
    public bool motorized;

    private Vector3 position;
    private Quaternion rotation;

    void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
    }

    void Update()
    {
        // Pega posição/rotação do collider
        WheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.position = position;

        // Aplica rotação da roda
        wheelModel.rotation = rotation;

        // Se a roda for direcional (frente), aplicar ângulo de esterçamento visual
        if (steerable)
        {
            // O eixo local da roda é geralmente o eixo Y
            wheelModel.localRotation = Quaternion.Euler(
                wheelModel.localRotation.eulerAngles.x,
                WheelCollider.steerAngle,
                wheelModel.localRotation.eulerAngles.z
            );
        }
    }
}
