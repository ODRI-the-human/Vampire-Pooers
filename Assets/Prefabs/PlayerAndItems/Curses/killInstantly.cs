using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killInstantly : MonoBehaviour
{
    public int roundsLeft = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (roundsLeft <= 0)
        {
            Destroy(this);
        }
    }

    public void OnHurtEffects()
    {
        gameObject.GetComponent<HPDamageDie>().HP = 0;
    }

    public void newWaveEffects()
    {
        roundsLeft--;
    }
}
