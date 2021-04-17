using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Bar : MonoBehaviour
{
    [SerializeField] GameObject Prefab = null;
    [SerializeField] Camera camera;

    List<Transform> ObjectList = new List<Transform>();
    List<GameObject> HpBarList = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
       

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster");
        for(int i=0; i<objects.Length; i++)
        {
            
            ObjectList.Add(objects[i].transform);
            GameObject hpbar = Instantiate(Prefab, objects[i].transform.position, Quaternion.identity, transform);
            HpBarList.Add(hpbar);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        for (int i=0; i<ObjectList.Count; i++)
        {
            
            HpBarList[i].transform.position = camera.WorldToScreenPoint(ObjectList[i].position + new Vector3(0, 1, 0));
            
        }
        
    }
}
