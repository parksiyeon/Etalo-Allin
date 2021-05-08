using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    Rigidbody rigid;
    public int nextMoveX;
    public int nextMoveZ;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        moving();

        Invoke("moving", 3);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector3(nextMoveX, rigid.velocity.y, nextMoveZ);

    }

    void moving()
    {
        nextMoveX = Random.Range(-1, 2);
        nextMoveZ = Random.Range(-1, 2);

        Invoke("moving", 3);
    }
}
