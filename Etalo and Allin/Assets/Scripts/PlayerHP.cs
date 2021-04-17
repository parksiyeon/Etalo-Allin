using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float total;



    [SerializeField] private float totalHealth;
    private float currentHealth;

    [SerializeField] private Text text_currentHealth;


    private bool isHot;
    private bool isClick = false;
    // Start is called before the first frame update

    private float currentDotTime = 0;


    private void Start()
    {
        currentDotTime = 1;
        currentHealth = totalHealth;

    }

    void Update()
    {
        Hot();
        
    }

    public void Button()
    {
        if (!isClick)
        {
            isHot = true;
            isClick = true;
        }
        else
        {

            isHot = false;
            isClick = false;
        }
    }

    private void Hot()
    {
        if (isHot)
        {
       
           // currentDotTime -= Time.deltaTime;
            
            currentDotTime -= Time.deltaTime;
            text_currentHealth.text = Mathf.RoundToInt(currentHealth).ToString();
           

            if (currentDotTime <= 0)
            {
                currentHealth -= Time.deltaTime;
                slider.value -= 0.02f;
                if (currentDotTime <= -1f)
                {
                    currentDotTime = 1;
                }
            }


        }

    }
}

