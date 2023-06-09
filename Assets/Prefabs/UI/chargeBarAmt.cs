using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chargeBarAmt : MonoBehaviour
{
    public float valueFac;
    public GameObject owner;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = owner.transform.position + new Vector3(0, 1, 0);
        valueFac = (Time.time - owner.gameObject.GetComponent<Attack>().chargeTime) / (2 / owner.gameObject.GetComponent<Attack>().fireTimerActualLength);
        SetValue();
    }

    public void SetValue()
    {
        slider.value = valueFac;
    }
}
