using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class setSpawnNumber : MonoBehaviour
{
    GameObject master;
    public TextMeshProUGUI spawnText;

    // Start is called before the first frame update
    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
    }

    // Update is called once per frame
    void Update()
    {
        if (master.GetComponent<ThirdEnemySpawner>().spawnNumber - master.GetComponent<ThirdEnemySpawner>().waveNumber != master.GetComponent<ThirdEnemySpawner>().noSpawnsBeforeNewWave + 1)
        {
            spawnText.text = (master.GetComponent<ThirdEnemySpawner>().spawnNumber).ToString() + "/" + (master.GetComponent<ThirdEnemySpawner>().noSpawnsBeforeNewWave + master.GetComponent<ThirdEnemySpawner>().waveNumber + 1).ToString();
        }
        else
        {
            spawnText.text = "Pick an item!";
        }
    }
}
