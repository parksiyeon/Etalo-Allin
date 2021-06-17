using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PocketCreeper : Monster
{
    NavMeshAgent agent;





    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        hp = 300;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
