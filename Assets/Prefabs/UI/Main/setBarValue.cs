using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setBarValue : MonoBehaviour
{
    public float valueFac;
    public GameObject owner;
    public Slider slider;
    public int valueToTrack;

    public float timeOfChange = 0;
    [System.NonSerialized] public float decayTimeFac = 0.05f;
    public float lastVal = 0;
    public bool keepCheckingTime = true;
    public float longLastVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastVal = owner.GetComponent<HPDamageDie>().HP;
    }

    // Update is called once per frame
    void Update()
    {
        float value = 0;
        float maxValue = 0;

        switch (valueToTrack)
        {
            case 0: // HP
                value = owner.GetComponent<HPDamageDie>().HP;
                maxValue = owner.GetComponent<HPDamageDie>().MaxHP;
                break;
            case 1: // HP change
                if (lastVal != owner.GetComponent<HPDamageDie>().HP && keepCheckingTime)
                {
                    timeOfChange = Time.time;
                    keepCheckingTime = false;
                }

                if (lastVal > owner.GetComponent<HPDamageDie>().HP)
                {
                    lastVal -= decayTimeFac;
                    keepCheckingTime = true;
                }

                value = lastVal; //(lastVal - owner.GetComponent<HPDamageDie>().HP) * decayTimeFac
                maxValue = owner.GetComponent<HPDamageDie>().MaxHP;
                break;
            case 2: // XP
                int currentLevel = owner.GetComponent<LevelUp>().level;
                int nextLevel = currentLevel + 1;

                float nextXP = owner.GetComponent<LevelUp>().nextXP;
                float floorXP = (currentLevel - 1) * 40 + 10 * Mathf.Pow((currentLevel - 1), 2);
                float XP = owner.GetComponent<LevelUp>().XP;

                value = XP - floorXP;
                maxValue = nextXP - floorXP;
                break;
        }

        valueFac = value / maxValue;
        SetValue();
    }

    public void SetValue()
    {
        if (!float.IsNaN(valueFac))
        {
            slider.value = valueFac;
        }
        //if (valueToTrack == 2)
        //{
        //    Debug.Log("slider val: " + slider.value.ToString());
        //}
    }
}
