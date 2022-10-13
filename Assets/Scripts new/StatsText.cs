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
        XPText.text = "XP: " + Player.GetComponent<LevelUp>().XP.ToString() + "/" + (Mathf.RoundToInt(Player.GetComponent<LevelUp>().nextXP)).ToString();
        DMGText.text = "DMG: " + Player.GetComponent<DealDamage>().finalDamageStat.ToString();
        FirerateText.text = "Fire delay: " + (Player.GetComponent<Attack>().fireTimerLength + Player.GetComponent<Attack>().fireTimerLength * (1 - Player.GetComponent<Attack>().fireTimerDIV) / Player.GetComponent<Attack>().fireTimerDIV + Player.GetComponent<Attack>().fireTimerLength * (Player.GetComponent<Attack>().fireTimerLengthMLT - 1)).ToString();
        LevelText.text = "Level: " + Player.GetComponent<LevelUp>().level.ToString();
        itemScreenText.text = Player.GetComponent<getItemDescription>().itemDescription;
    }
}
