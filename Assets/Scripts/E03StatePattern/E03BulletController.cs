using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E03BulletController : MonoBehaviour
{
    private Rigidbody rb;
    private float force = 500f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
