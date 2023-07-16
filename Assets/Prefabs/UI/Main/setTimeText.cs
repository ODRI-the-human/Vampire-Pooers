using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setTimeText : MonoBehaviour
{
    GameObject master;
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = master.GetComponent<EntityReferencerGuy>().time;
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
