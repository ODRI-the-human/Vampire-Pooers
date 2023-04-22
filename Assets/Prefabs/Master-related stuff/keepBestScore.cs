using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keepBestScore : MonoBehaviour
{
    public GameObject master;

    public float bestWave = -5;
    public float bestLevel = 0;
    public float bestDamage = 0;
    public float bestFirerate = 0;
    public float bestHP = 0;

    public float worstWave = 100000;
    public float worstLevel = 0;
    public float worstDamage = 0;
    public float worstFirerate = 0;
    public float worstHP = 0;

    //public GameObject endRunText;
    public TextMeshProUGUI bestText;
    public GameObject bestTextObj;
    public TextMeshProUGUI worstText;
    public GameObject worstTextObj;
    public TextMeshProUGUI bullyText;
    public GameObject bullyTextObj;
    public TextMeshProUGUI resetText;
    public GameObject resetTextObj;

    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] splingks = GameObject.FindGameObjectsWithTag("keepBestScore");
        if (splingks.Length > 1)
        {
            Destroy(gameObject);
        }

        bestTextObj.SetActive(false);
        worstTextObj.SetActive(false);
        bullyTextObj.SetActive(false);
        resetTextObj.SetActive(false);
    }

    void Update()
    {
        if (master == null)
        {
            bestTextObj.SetActive(false);
            worstTextObj.SetActive(false);
            bullyTextObj.SetActive(false);
            resetTextObj.SetActive(false);
            master = EntityReferencerGuy.Instance.master;
            Player = EntityReferencerGuy.Instance.playerInstance;
            EntityReferencerGuy.Instance.keepBestStatObj = gameObject;
        }
    }

    public void ShowStats()
    {
        if (master.GetComponent<ThirdEnemySpawner>().totalSpawnsSurvived > bestWave)
        {
            bestWave = master.GetComponent<ThirdEnemySpawner>().totalSpawnsSurvived;
            bestLevel = master.GetComponent<StatsText>().lastLevel;
            bestDamage = Mathf.Round(master.GetComponent<StatsText>().lastDMG * 100) / 100;
            bestFirerate = Mathf.Round(master.GetComponent<StatsText>().lastFirerate * 100) / 100;
            bestHP = master.GetComponent<StatsText>().lastMaxHP;
        }

        if (master.GetComponent<ThirdEnemySpawner>().totalSpawnsSurvived < worstWave)
        {
            worstWave = master.GetComponent<ThirdEnemySpawner>().totalSpawnsSurvived;
            worstLevel = master.GetComponent<StatsText>().lastLevel;
            worstDamage = Mathf.Round(master.GetComponent<StatsText>().lastDMG * 100) / 100;
            worstFirerate = Mathf.Round(master.GetComponent<StatsText>().lastFirerate * 100) / 100;
            worstHP = master.GetComponent<StatsText>().lastMaxHP;
        }

        bestTextObj.SetActive(true);
        worstTextObj.SetActive(true);
        bullyTextObj.SetActive(true);
        resetTextObj.SetActive(true);

        bestText.text = "BEST RUN:\n" + "Wave: " + (bestWave).ToString() + "\nLevel: " + bestLevel.ToString() + "\nDMG: " + bestDamage.ToString() + "\nFire rate: " + bestFirerate.ToString() + "\nHP: " + bestHP.ToString();
        worstText.text = "WORST RUN:\n" + "Wave: " + (worstWave).ToString() + "\nLevel: " + worstLevel.ToString() + "\nDMG: " + worstDamage.ToString() + "\nFire rate: " + worstFirerate.ToString() + "\nHP: " + worstHP.ToString();

        int textToDisplay = Random.Range(0, 5);
        switch (textToDisplay)
        {
            case 0:
                bullyText.text = "If you had dodged some of those attacks you might've survived...";
                break;
            case 1:
                bullyText.text = "Unfortunate, guess you ran out of luck - remember your luck decreases at an exponential rate! (not really!)";
                break;
            case 2:
                bullyText.text = "Remember: oh the treachery, every single brightly coloured circle is an enemy. Well, most of them. Some aren't brightly coloured.";
                break;
            case 3:
                bullyText.text = "The Boberman is particularly susceptible to death - he can be killed by LITERALLY one single death. Be careful.";
                break;
            case 4:
                bullyText.text = "Tip: press 'Q' to enter turn-based RPG mode. It'll give you time to plan all your moves! Pressing it again toggles it off. This is definitely NOT a debug tool.";
                break;
        }

        resetText.text = "Press R to restart";
    }
}
