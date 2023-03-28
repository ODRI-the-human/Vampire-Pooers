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
    public TextMeshProUGUI curseText;
    public TextMeshProUGUI FirerateText;
    public TextMeshProUGUI FirerateChangeText;

    public TextMeshProUGUI timeText;

    public int HPChangeTimer = 100;
    public int XPChangeTimer = 100;
    public int DMGChangeTimer = 100;
    public int FirerateChangeTimer = 100;
    public int LevelChangeTimer = 100;
    int totalTime = 0;

    public float lastMaxHP;
    public float lastXP;
    public float lastDMG;
    public float lastFirerate;
    public float lastLevel; 
    
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
        lastFirerate = 2;

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
        float timeLeft = gameObject.GetComponent<EntityReferencerGuy>().time;
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        timeText.text = "boss(es) spawn in:" + "\n" + string.Format("{0:00} : {1:00}", minutes, seconds);

        if (!gameObject.GetComponent<ThirdEnemySpawner>().enemiesAreSpawning)
        {
            timeText.text += ", paused right now lol";
        }



        if (totalTime > 5)
        {
            if (lastMaxHP != Player.GetComponent<HPDamageDie>().MaxHP)
            {
                HPChangeTimer = 0;
                if (lastMaxHP < Player.GetComponent<HPDamageDie>().MaxHP)
                {
                    HPChangeText.text = "+" + (Mathf.Round((Player.GetComponent<HPDamageDie>().MaxHP - lastLongMaxHP))).ToString();
                    HPChangeText.color = Color.green;
                }
                else
                {
                    HPChangeText.text = (Mathf.Round((Player.GetComponent<HPDamageDie>().MaxHP - lastLongMaxHP))).ToString();
                    HPChangeText.color = Color.red;
                }
            }

            if (lastXP != Player.GetComponent<LevelUp>().XP)
            {
                XPChangeTimer = 0;
                if (lastXP < Player.GetComponent<LevelUp>().XP)
                {
                    XPChangeText.text = "+" + (Mathf.Round((Player.GetComponent<LevelUp>().XP - lastLongXP))).ToString();
                    XPChangeText.color = Color.green;
                }
            }

            if (lastDMG != Player.GetComponent<DealDamage>().damageToPresent)
            {
                DMGChangeTimer = 0;
                if (lastDMG < Player.GetComponent<DealDamage>().damageToPresent)
                {
                    DMGChangeText.text = "+" + (Mathf.Round((Player.GetComponent<DealDamage>().damageToPresent - lastLongDMG) * 100) / 100).ToString();
                    DMGChangeText.color = Color.green;
                }
                else
                {
                    DMGChangeText.text = (Mathf.Round((Player.GetComponent<DealDamage>().damageToPresent - lastLongDMG) * 100) / 100).ToString();
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
                    FirerateChangeText.color = Color.green;
                }
                else
                {
                    FirerateChangeText.text = (Mathf.Round((Player.GetComponent<Attack>().fireTimerActualLength - lastLongFirerate) * 100) / 100).ToString();
                    FirerateChangeText.color = Color.red;
                }
            }
        }

        HPText.text = "HP: " + (Mathf.Round(Player.GetComponent<HPDamageDie>().HP)).ToString() + "/" + (Mathf.Round(Player.GetComponent<HPDamageDie>().MaxHP)).ToString();
        XPText.text = "XP: " + (Mathf.Round(Player.GetComponent<LevelUp>().XP)).ToString() + "/" + (Mathf.RoundToInt(Player.GetComponent<LevelUp>().nextXP)).ToString();
        DMGText.text = "DMG: " + (Mathf.Round(Player.GetComponent<DealDamage>().damageToPresent * 100) / 100).ToString();
        FirerateText.text = "Fire rate: " + (Mathf.Round(Player.GetComponent<Attack>().fireTimerActualLength * 100) / 100).ToString();
        LevelText.text = "Level: " + Player.GetComponent<LevelUp>().level.ToString();
        itemScreenText.text = Player.GetComponent<getItemDescription>().itemDescription;
        curseText.text = Player.GetComponent<getItemDescription>().curseDescription;

        lastMaxHP = Player.GetComponent<HPDamageDie>().MaxHP;
        lastXP = Player.GetComponent<LevelUp>().XP;
        lastDMG = Player.GetComponent<DealDamage>().damageToPresent;
        lastLevel = Player.GetComponent<LevelUp>().level;
        lastFirerate = Player.GetComponent<Attack>().fireTimerActualLength;

        if (HPChangeTimer > 100)
        {
            lastLongMaxHP = lastMaxHP;
            Color tmp = HPChangeText.color;
            tmp.a -= 0.02f;
            HPChangeText.color = tmp;
        }

        if (XPChangeTimer > 100)
        {
            lastLongXP = lastXP;
            Color tmp = XPChangeText.color;
            tmp.a -= 0.02f;
            XPChangeText.color = tmp;
        }

        if (DMGChangeTimer > 100)
        {
            lastLongDMG = lastDMG;
            Color tmp = DMGChangeText.color;
            tmp.a -= 0.02f;
            DMGChangeText.color = tmp;
        }

        if (LevelChangeTimer > 100)
        {
            lastLongLevel = lastLevel;
            Color tmp = LevelChangeText.color;
            tmp.a -= 0.02f;
            LevelChangeText.color = tmp;
        }

        if (FirerateChangeTimer > 100)
        {
            lastLongFirerate = lastFirerate;
            Color tmp = FirerateChangeText.color;
            tmp.a -= 0.02f;
            FirerateChangeText.color = tmp;
        }
    }

    void FixedUpdate()
    {
        totalTime++;
        HPChangeTimer++;
        XPChangeTimer++;
        DMGChangeTimer++;
        LevelChangeTimer++;
        FirerateChangeTimer++;
    }
}
