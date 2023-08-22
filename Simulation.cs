using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    public GameObject preset;
    public float accel = 1;
    public List<Part> list = new List<Part>();
    private void Start()
    {

    }
    public void CreatAtomObject(Atom atom)
    {
        var k = Instantiate(preset);
        k.GetComponent<Part>().a = atom;
        k.GetComponent<Part>().mass = atom.proton + atom.neutron;
        k.transform.localScale = new Vector3(1, 1, 1) * ((float)atom.radius);
        k.transform.position = new Vector3(Random.RandomRange(-1, 1), Random.RandomRange(-1, 1), Random.RandomRange(-1, 1));
        list.Add(k.GetComponent<Part>());
    }
    private void Update()
    {

        if (list == null) return;
        GetVector();
        if (list.Count > 0) transform.position = list[0].gameObject.transform.position;
    }

    void GetVector()
    {
        foreach (var a in list) //origin
        {
            List<Vector3> forces = new List<Vector3>();
            Vector3 EggSaltedForce = Vector3.zero;
            foreach (var b in list) //target
            {
                if (a != b)
                {
                    var rsq = System.Math.Pow(Vector3.Distance(a.transform.position, b.transform.position), 2);
                    rsq = (double)((Mathf.Clamp((float)rsq * 10, 0.1f, Mathf.Infinity)) / 10);
                    var force_scala =
accel * a.mass * b.mass / rsq;
                    var forec_vector = (float)force_scala * (b.transform.position - a.transform.position);
                    //Debug.Log(forec_vector);
                    //forces.Add(forec_vector);
                    EggSaltedForce += forec_vector;
                }
            }
            if (list.Count > 1)
            {
                a.force = EggSaltedForce;
                //Debug.Log(forces[0]);
            }
        }
    }
}
