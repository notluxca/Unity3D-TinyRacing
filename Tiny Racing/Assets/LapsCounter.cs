using System;
using UnityEngine;

public class LapsCounter : MonoBehaviour
{

    public static Action OnLapCompleted;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnLapCompleted?.Invoke();
        }
    }
}
