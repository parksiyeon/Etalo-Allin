using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{


    // 체력
    [SerializeField]
    private int hp;  // 최대 체력. 유니티 에디터 슬롯에서 지정할 것.
    private int currentHp;

    // 목마름
    [SerializeField]
    private int thirsty;  // 최대 목마름. 유니티 에디터 슬롯에서 지정할 것.
    private int currentThirsty;

    // 목마름이 줄어드는 속도
    [SerializeField]
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;


    // 필요한 이미지
    [SerializeField]
    private Image[] images_Gauge;

    // 각 상태를 대표하는 인덱스
    private const int HP = 0, THIRSTY = 1;

    void Start()
    {
 
        currentThirsty = thirsty;
    }

    // Update is called once per frame
    void Update()
    {
        Thirsty();
        GaugeUpdate();
    }

    private void GaugeUpdate()
    {
        images_Gauge[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
    }

    private void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
                currentThirstyDecreaseTime++;
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("목마름 수치가 0 이 되었습니다.");
    }


    public void IncreaseThirsty(int _count)
    {
        if (currentThirsty + _count < thirsty)
            currentThirsty += _count;
        else
            currentThirsty = thirsty;
    }

    public void DecreaseThirsty(int _count)
    {
        if (currentThirsty - _count < 0)
            currentThirsty = 0;
        else
            currentThirsty -= _count;
    }

}
