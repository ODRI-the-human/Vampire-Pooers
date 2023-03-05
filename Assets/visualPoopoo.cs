using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class visualPoopoo : MonoBehaviour
{
    public bool isPaused = false;
    float amounter;
    bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            EventManager.DeathEffects = null;
            LevelUp.levelEffects = null;
            //playerInstance.GetComponent<LevelUp>().levelEffects = null;
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0.05f;
                isPaused = true;
            }
        }
    }

    IEnumerator doFreeze()
    {
        float original = Time.timeScale;
        Time.timeScale = 0;
        isFrozen = true;

        yield return new WaitForSecondsRealtime(amounter);

        Time.timeScale = original;
        isFrozen = false;
    }

    public void bigHitFreeze(float amount)
    {
        if (!isFrozen)
        {
            amounter = amount;
            StartCoroutine(doFreeze());
        }
    }
}
