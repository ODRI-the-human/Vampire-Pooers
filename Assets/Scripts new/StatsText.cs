using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsText : MonoBehaviour
{

    public TextMeshProUGUI HPText;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI DMGText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI itemScreenText;
    public TextMeshProUGUI FirerateText;
    GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("newPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        HPText.text = "HP: " + Player.GetComponent<HPDamageDie>().HP.ToString() + "/" + Player.GetComponent<HPDamageDie>().MaxHP.ToString();
        XPText.text = "XP: " + 0.ToString();
        DMGText.text = "DMG: " + Player.GetComponent<DealDamage>().finalDamageStat.ToString();
        FirerateText.text = "Fire delay: " + Player.GetComponent<Attack>().fireTimerLength.ToString();
        LevelText.text = "Level: " + 0.ToString();
    }
}
