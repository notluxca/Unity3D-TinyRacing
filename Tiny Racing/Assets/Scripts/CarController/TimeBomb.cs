using System;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    public Transform myTransform;
    public float radius = 5;
    public Color volumeColor = Color.red;
    public float explosionForce = 500;
    public float counter = 5;
    private float timer = 0;
    private bool hasExploded = false;
    

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        
        if (timer >= counter && !hasExploded)
        {
            print("Explosão aconteceu");
            hasExploded = true;
            Collider[] cols =
                Physics.OverlapSphere(
                    myTransform.position,
                    radius);

            for (int i = 0; i < cols.Length; i++)
            {
                Rigidbody rb = cols[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(
                        explosionForce,
                        myTransform.position,
                        radius);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = volumeColor;
        Gizmos.DrawSphere(myTransform.position, radius);
    }
}
