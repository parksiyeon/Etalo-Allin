using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemImage : MonoBehaviour
{
    private Image itemImage;
    private Text itemCount;


    // Start is called before the first frame update
    void Start()
    {
        itemImage = GetComponent<Image>();
        itemCount = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
