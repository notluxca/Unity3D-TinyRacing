using UnityEngine;

public class TestCollision : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Target"))
        {
            print("TriggerEnter chamado");
        }
    }
    
    void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.CompareTag("Target"))
        {
            print("       TriggerStay chamado");
        }
        
    }
    
    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Target"))
        {
            print("                 TriggerExit chamado");
        }
        
    }
    
    
    
}
