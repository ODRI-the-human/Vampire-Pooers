using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReloadBarBehaviour : MonoBehaviour
{
    public GameObject owner;
    public GameObject barItself;
    public GameObject tick;
    public GameObject tickMask;

    public GameObject allAmmoObj;

    public GameObject ammoBar;
    public TextMeshProUGUI texterz;

    float initialAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("worldSpaceCanvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        texterz.text = owner.GetComponent<Attack>().charges[0].ToString();
        transform.position = owner.transform.position - new Vector3(0, 1.5f, 0);
        barItself.transform.position = transform.position;
        float chargeProportion = (float)owner.GetComponent<Attack>().charges[0] / (float)owner.GetComponent<Attack>().abilityTypes[0].maxCharges;
        //Debug.Log("charge proportion: " + chargeProportion.ToString());
        if (chargeProportion <= 0.4f)
        {
            ammoBar.GetComponent<Image>().color = Color.red;
        }
        else
        {
            ammoBar.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        ammoBar.GetComponent<RectTransform>().localScale = new Vector3(chargeProportion, 1, 1);

        if (owner.GetComponent<Attack>().abilityTypes[0].maxCharges == 1)
        {
            allAmmoObj.transform.localScale = Vector3.zero;
        }
        else
        {
            allAmmoObj.transform.localScale = new Vector3(1, 1, 1);
        }

        if ((!owner.GetComponent<Attack>().isHoldingAttack[0] || !owner.GetComponent<Attack>().isAttacking) && owner.GetComponent<Attack>().coolDowns[0] >= 0)
        {
            if (initialAmount == 0)
            {
                initialAmount = owner.GetComponent<Attack>().coolDowns[0];
            }

            float fracComplete = -2 * owner.GetComponent<Attack>().coolDowns[0] / initialAmount + 1;

            barItself.transform.localScale = new Vector3(1, 1, 1);
            tick.transform.position = barItself.transform.position + new Vector3(0.675f * fracComplete, 0, 0);
            tickMask.transform.position = barItself.transform.position + new Vector3(0.675f * fracComplete, 0, 0);
        }
        else
        {
            barItself.transform.localScale = new Vector3(0, 0, 0);
            initialAmount = 0;
        }
    }
}
