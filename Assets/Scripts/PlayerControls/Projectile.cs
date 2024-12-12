using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;

    [Range(0f, 1f)] public float bounciness;
    public bool useGravity;
    public float maxLifetime = 7f;

    PhysicMaterial physicsMat;
    private SlenderManAI monsterObject;
    
    public void Initialize(Vector3 direction)
    {
        Setup();
        monsterObject = FindObjectOfType<SlenderManAI>();

        transform.forward = direction.normalized; // changes rotation
        rb.velocity = direction; // sends flying in the correct direction
    }

    private void Update()
    {
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        monsterObject.TryAlert(transform.position);
        Debug.Log("bro collided");
    }

    private void Setup()
    {
        physicsMat = new PhysicMaterial();
        physicsMat.bounciness = bounciness;
        physicsMat.frictionCombine = PhysicMaterialCombine.Minimum;
        physicsMat.bounceCombine = PhysicMaterialCombine.Maximum;

        rb.useGravity = useGravity;
    }
}
