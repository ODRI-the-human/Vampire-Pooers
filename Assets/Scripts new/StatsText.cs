using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsText : MonoBehaviour
{

    public TextMeshProUGUI HPText;
    public TextMeshProUGUI HPChangeText;
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI XPChangeText;
    public TextMeshProUGUI DMGText;
    public TextMeshProUGUI DMGChangeText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI LevelChangeText;
    public TextMeshProUGUI itemScreenText;
    public TextMeshProUGUI FirerateText;
    public TextMeshProUGUI FirerateChangeText;

    public int HPChangeTimer;
    public int XPChangeTimer;
    public int DMGChangeTimer;
    public int FirerateChangeTimer;
    public int LevelChangeTimer;

    float lastMaxHP;
    float lastXP;
    float lastDMG;
    float lastFirerate;
    float lastLevel; 
    
    float lastLongMaxHP;
    float lastLongXP;
    float lastLongDMG;
    float lastLongFirerate;
    float lastLongLevel;

    GameObject Player;

    void Awake()
    {
        Player = GameObject.Find("newPlayer");
    }

    void Start()
    {
        lastMaxHP = 100;
        lastXP = 0;
        lastDMG = 50;
        lastLevel = 1;
        lastFirerate = 25;

        Color tmp = HPChangeText.color;
        tmp.a = 0;
        HPChangeText.color = tmp;
        XPChangeText.color = tmp;
        DMGChangeText.color = tmp;
        FirerateChangeText.color = tmp;
        LevelChangeText.color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastMaxHP != Player.GetComponent<HPDamageDie>().MaxHP)
        {
            HPChangeTimer = 0;
            if (lastMaxHP < Player.GetComponent<HPDamageDie>().MaxHP)
            {
                HPChangeText.text = "+" + (Player.GetComponent<HPDamageDie>().MaxHP - lastLongMaxHP).ToString();
                HPChangeText.color = Color.green;
            }
            else
            {
                HPChangeText.text = (Player.GetComponent<HPDamageDie>().MaxHP - lastLongMaxHP).ToString();
                HPChangeText.color = Color.red;
            }
        }

        if (lastXP != Player.GetComponent<LevelUp>().XP)
        {
            XPChangeTimer = 0;
            if (lastXP < Player.GetComponent<LevelUp>().XP)
            {
                XPChangeText.text = "+" + (Player.GetComponent<LevelUp>().XP - lastLongXP).ToString();
                XPChangeText.color = Color.green;
            }
        }

        if (lastDMG != Player.GetComponent<DealDamage>().finalDamageStat)
        {
            DMGChangeTimer = 0;
            if (lastDMG < Player.GetComponent<DealDamage>().finalDamageStat)
            {
                DMGChangeText.text = "+" + (Mathf.Round((Player.GetComponent<DealDamage>().finalDamageStat - lastLongDMG) * 100) / 100).ToString();
                DMGChangeText.color = Color.green;
            }
            else
            {
                DMGChangeText.text = (Mathf.Round((Player.GetComponent<DealDamage>().finalDamageStat - lastLongDMG) * 100) / 100).ToString();
                DMGChangeText.color = Color.red;
            }
        }

        if (lastLevel != Player.GetComponent<LevelUp>().level)
        {
            LevelChangeTimer = 0;
            if (lastLevel < Player.GetComponent<LevelUp>().level)
            {
                LevelChangeText.text = "+" + (Player.GetComponent<LevelUp>().level - lastLongLevel).ToString();
                LevelChangeText.color = Color.green;
            }
        }

        if (lastFirerate != Player.GetComponent<Attack>().fireTimerActualLength)
        {
            FirerateChangeTimer = 0;
            if (lastFirerate < Player.GetComponent<Attack>().fireTimerActualLength)
            {
                FirerateChangeText.text = "+" + (Mathf.Round((Player.GetComponent<Attack>().fireTimerActualLength - lastLongFirerate) * 100) / 100).ToString();
                FirerateChangeText.color = Color.red;
            }
            else
            {
                FirerateChangeText.text = (Mathf.Round((Player.GetComponent<Attack>().fireTimerActualLength - lastLongFirerate) * 100) / 100).ToString();
                FirerateChangeText.color = Color.green;
            }
        }

        HPText.text = "HP: " + Player.GetComponent<HPDamageDie>().HP.ToString() + "/" + Player.GetComponent<HPDamageDie>().MaxHP.ToString();
        XPText.text = "XP: " + Player.GetComponent<LevelUp>().XP.ToString() + "/" + (Mathf.RoundToInt(Player.GetComponent<LevelUp>().nextXP)).ToString();
        DMGText.text = "DMG: " + (Mathf.Round(Player.GetComponent<DealDamage>().finalDamageStat * 100) / 100).ToString();
        FirerateText.text = "Fire delay: " + (Mathf.Round(Player.GetComponent<Attack>().fireTimerActualLength * 100) / 100).ToString();
        LevelText.text = "Level: " + Player.GetComponent<LevelUp>().level.ToString();
        itemScreenText.text = Player.GetComponent<getItemDescription>().itemDescription;

        HPChangeTimer++;
        XPChangeTimer++;
        DMGChangeTimer++;
        LevelChangeTimer++;
        FirerateChangeTimer++;

        lastMaxHP = Player.GetComponent<HPDamageDie>().MaxHP;
        lastXP = Player.GetComponent<LevelUp>().XP;
        lastDMG = Player.GetComponent<DealDamage>().finalDamageStat;
        lastLevel = Player.GetComponent<LevelUp>().level;
        lastFirerate = Player.GetComponent<Attack>().fireTimerActualLength;

        if (HPChangeTimer > 1000)
        {
            lastLongMaxHP = lastMaxHP;
            Color tmp = HPChangeText.color;
            tmp.a -= 0.05f;
            HPChangeText.color = tmp;
        }

        if (XPChangeTimer > 1000)
        {
            lastLongXP = lastXP;
            Color tmp = XPChangeText.color;
            tmp.a -= 0.05f;
            XPChangeText.color = tmp;
        }

        if (DMGChangeTimer > 1000)
        {
            lastLongDMG = lastDMG;
            Color tmp = DMGChangeText.color;
            tmp.a -= 0.05f;
            DMGChangeText.color = tmp;
        }

        if (LevelChangeTimer > 1000)
        {
            lastLongLevel = lastLevel;
            Color tmp = LevelChangeText.color;
            tmp.a -= 0.05f;
            LevelChangeText.color = tmp;
        }

        if (FirerateChangeTimer > 1000)
        {
            lastLongFirerate = lastFirerate;
            Color tmp = FirerateChangeText.color;
            tmp.a -= 0.05f;
            FirerateChangeText.color = tmp;
        }
    }
}
