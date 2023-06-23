using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setWaveBarVal : MonoBehaviour
{
    GameObject master;
    public Slider slider;
    public float val;

    // Start is called before the first frame update
    void Start()
    {
        master = EntityReferencerGuy.Instance.master;
    }

    // Update is called once per frame
    void Update()
    {
        val = Mathf.Round((master.GetComponent<ThirdEnemySpawner>().spawnNumber)) / Mathf.Round((master.GetComponent<ThirdEnemySpawner>().noSpawnsBeforeNewWave + master.GetComponent<ThirdEnemySpawner>().waveNumber + 1));
        slider.value = val;
    }
}
