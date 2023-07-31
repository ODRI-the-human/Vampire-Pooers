using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chargeBarAmt : MonoBehaviour
{
    public float valueFac;
    public GameObject owner;
    public int indexToTrack;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (owner == null)
        {
            Destroy(gameObject);
        }

        transform.position = owner.transform.position + new Vector3(0, 1, 0);
        valueFac = ((float)owner.GetComponent<Attack>().chargeTimers[indexToTrack]) / ((float)owner.GetComponent<Attack>().abilityTypes[indexToTrack].chargeLength * owner.GetComponent<Attack>().cooldownFac * owner.GetComponent<Attack>().cooldownFacIndiv[indexToTrack]);
        SetValue();
    }

    public void SetValue()
    {
        slider.value = valueFac;
    }
}
