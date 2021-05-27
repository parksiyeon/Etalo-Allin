using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComposedItemView : MonoBehaviour
{
    public Sprite originalImage;
    public Sprite showImage;
    public Item item;


    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        item = null;
    }

    private void OnDisable()
    {
        item = null;
    }
}
