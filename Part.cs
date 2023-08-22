using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public Atom a;
    public float mass = 1;
    public Vector3 force = new Vector3(0, 0, 0);
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //GetComponent<LineRenderer>().Set
        rb.mass = mass;
        mass = Mathf.Clamp(mass, 0.1f, 100);
        rb.velocity = force / mass;
        var tx = transform.position.x;
        var ty = transform.position.y;
        var tz = transform.position.z;
        tx = Mathf.Clamp(tx, -1.7f, 1.7f);
        ty = Mathf.Clamp(ty, -1.7f, 1.7f);
        tz = Mathf.Clamp(tz, -1.7f, 1.7f);
        transform.position = new Vector3(tx, ty, tz);
    }
}
