using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setWaveText : MonoBehaviour
{
    GameObject master;
    public TextMeshProUGUI waveText;

    // Start is called before the first frame update
    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = "Wave " + (master.GetComponent<ThirdEnemySpawner>().waveNumber + 1).ToString();
    }
}
