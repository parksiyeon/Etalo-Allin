using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int max_Health;
    public int cur_Health;

    Rigidbody rb;
    BoxCollider bc;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee")
        {

        }
        else if (other.tag == "Bullet")
        {

        }
    }
}
