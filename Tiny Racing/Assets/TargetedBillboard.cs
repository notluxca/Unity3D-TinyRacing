using UnityEngine;

public class TargetedBillboard : MonoBehaviour
{

    private Transform target;
    public bool lockX;
    public bool lockY;
    public bool lockZ;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target == null)
            target = Camera.main?.transform;

        if (target == null) return;

        // direção para o alvo
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 euler = lookRotation.eulerAngles;
        Vector3 current = transform.rotation.eulerAngles;

        // trava os eixos escolhidos
        if (lockX) euler.x = current.x;
        if (lockY) euler.y = current.y;
        if (lockZ) euler.z = current.z;

        transform.rotation = Quaternion.Euler(euler);
    }
}
