using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMoveUI : MonoBehaviour
{
    private RectTransform minimapRT;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        minimapRT = GetComponent<RectTransform>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        minimapRT.anchoredPosition = new Vector2( -playerTransform.position.x + 500  ,  -playerTransform.position.z + 500  );

        
    }
}
