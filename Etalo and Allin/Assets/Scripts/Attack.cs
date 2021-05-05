using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack : MonoBehaviourPunCallbacks
{
    public Transform HandTransform;
    public ParticleSystem ps;

    PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //shoot
                photonView.RPC("RPC_Shoot", RpcTarget.All);
            }
        }
    
    }

    //get called on all instances if the viewID
    [PunRPC]
    void RPC_Shoot()
    {
        //start particle
        ps.Play();
        Ray ray = new Ray(HandTransform.position, HandTransform.forward);
      //  if (Physics.Raycast(ray, out RaycastHit, 50f)) ;

    }
}
