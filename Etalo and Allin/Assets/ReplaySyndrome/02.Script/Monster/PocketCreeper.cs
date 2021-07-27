using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PocketCreeper : Monster
{



    protected override void Awake()
    {
        speed = 5;
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        movePositions = new Vector3[positionNum];
        animator = GetComponent<Animator>();
        makePath();
        player = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    protected override void Update()
    {
       
        if(hp > 0)
        {
            MoveToPosition();
        }

     
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < movePositions.Length; ++i)
        {
            Gizmos.DrawSphere(movePositions[i], 1);
        }

        Gizmos.color = Color.red;
        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(transform.position, 30);
    }

    

}
