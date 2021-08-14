using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PocketCreeper : Monster
{

    protected override void Awake()
    {
        
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
       
        
        

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
