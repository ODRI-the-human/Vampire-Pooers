using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptions : MonoBehaviour
{
    public int itemChosen;
    public bool addNewScriptForNewInstance; // Used to determine whether to add a new script instance or not when adding a new copy of the item.
    public int whatUses; // Used to determine whether to attach to a bullet or player (or both)
    public int quality; // Each item has a quality.
    public string itemDescription;
    public string curseDescription;

    void Start()
    {
        if (gameObject.GetComponent<itemPedestal>() != null)
        {
            Invoke(nameof(LateStart), 0.02f);
            itemChosen = gameObject.GetComponent<itemPedestal>().itemChosen;
            getItemDescription();
        }
    }

    void LateStart()
    {
        GameObject backGround = gameObject.transform.Find("backgroundSprite").gameObject;
        backGround.GetComponent<setBackgroundColor>().qualityChosen = quality;
        backGround.GetComponent<setBackgroundColor>().SetColour();
    }

    public void GetCurseDescription(int curseType)
    {
        //Debug.Log("Curse Type: " + curseType.ToString());
        switch (curseType)
        {
            case -2:
                curseDescription = "";
                break;
            case 0: // Gives player three of the item, gives enemies one.
                curseDescription = "Gain three of this item, but all enemies gain one instance.";
                break;
            case 1: // Get 1 of this item every third time you pick up an item, lose 2 items on hit (perm)
                curseDescription = "Gain one of this item every third time you pick up an item, but lose 2 random items every time you take damage.";
                break;
            case 2: // Get 3 of the item, lose 5 random items (ONCE) if you get hit in the next 2 rounds.
                curseDescription = "Gain three of this item, but if you get hit in the next 2 rounds, you lose 5 random items (only happens once).";
                break;
            case 3: // Give enemies one of the item, if an enemy dies in the next 2 rounds they can drop an item they hold.
                curseDescription = "Enemies permanently gain one of this item, but have a 5% chance to drop a random item they hold on death for the next two rounds.";
                break;
            case 4: // Get five of the item, but can't heal ever again.
                curseDescription = "Gain five of this item, but you can never heal again.";
                break;
            case 5: // Get three of the item, but die instantly if hit in the next 2 rounds.
                curseDescription = "Gain three of this item, but taking any damage in the next two rounds instantly kills you";
                break;
            case 6: // gives 3 of the item.
                curseDescription = "Gain two of this item - no downside!";
                break;
            case 7: // gives 10 of the item.
                curseDescription = "Gain five of this item - no downside!";
                break;
        }
    }

    public void getItemDescription()
    {
        switch (itemChosen)
        {
            // Regular passive items
            case (int)ITEMLIST.HP25:
                itemDescription = "Increases max HP by 25 per stack.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = true; 
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.HP50:
                itemDescription = "Increases max HP by 50 per stack.";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.DMGADDPT5:
                itemDescription = "Increases base damage by 25 per stack.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.ALL;
                break;
            case (int)ITEMLIST.DMGMLT2:
                itemDescription = "Doubles damage. Multiplier increases by +1x per stack.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.ALL;
                break;
            case (int)ITEMLIST.FIRERATE:
                itemDescription = "Decreases fire delay by 4.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.CANSHOOT;
                break;
            case (int)ITEMLIST.SOY:
                itemDescription = "Per stack, increases firerate by 5x, but quarters damage and bullet knockback.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.ALL;
                break;
            case (int)ITEMLIST.HOMING:
                itemDescription = "Homing shots. Stacks increase range from which bullets home.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
            case (int)ITEMLIST.ATG:
                itemDescription = "10% chance on hitting an enemy to fire a homing missile, dealing 3x (+3x per stack) of the damage that procced it.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.CANSHOOT;
                break;
            case (int)ITEMLIST.MORESHOT:
                itemDescription = "Per stack, fire +1 projectile in an arc.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.CANSHOOT;
                break;
            case (int)ITEMLIST.WAPANT:
                itemDescription = "Every 3 seconds, create a slowing puddle of creep. Size of creep increases with stacks.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.HOLYMANTIS:
                itemDescription = "Divide the first damage you take each round by 2 (+1 per stack). Works +1 time per round, per stack.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.CONVERTER:
                itemDescription = "At the end of each round, 10% (+10% per stack) of your missing %HP is added to your base damage.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.EASIERTIMES:
                itemDescription = "20% chance to block damage. Chance increases with stacks.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.STOPWATCH:
                itemDescription = "Enemies move and shoot 25% slower. Slowdown increases with stacks.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.MASTERANDPLAYER;
                break;
            case (int)ITEMLIST.BOUNCY:
                itemDescription = "Your bullets bounce +1 time per stack.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
            case (int)ITEMLIST.FOURDIRMARTY:
                itemDescription = "Every 4 shots, fire a number of bullets in a circle around you.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.CANSHOOT;
                break;
            case (int)ITEMLIST.PIERCING:
                itemDescription = "Your bullets pierce +1 enemies per stack and deal 1.2x (0.2x per stack) damage after piercing each enemy.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
            case (int)ITEMLIST.CREEP:
                itemDescription = "Leave creep on the ground that deals 10% of your damage per tick. Size of creep increases with stacks.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.DODGESPLOSION:
                itemDescription = "At the end of your dodge, deal 50 damage to enemies and deflect bullets within a radius. Radius increases with stacks.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.BETTERDODGE:
                itemDescription = "Dodge further and faster.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.ORBITAL1:
                itemDescription = "Gain a long-range orbital that blocks enemy shots and deals damage. Stacks add extra orbitals.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.ORBITAL2:
                itemDescription = "Gain a close-range orbital that blocks enemy shots, damages enemies, and shoots, copying all your bullet effects and dealing 25% of your damage. Stacks add extra orbitals.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.SPLIT:
                itemDescription = "Your shots split on hit. Split shots deal 30% (+30% per stack) of your damage.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
            case (int)ITEMLIST.CONTACT:
                itemDescription = "Enemy bullets bounce off your bullets and now damage enemies rather than you. Your bullets are destroyed after 2 (+2 per stack) collisions.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
            case (int)ITEMLIST.BLEED:
                itemDescription = "Your attacks have a 15% (+15% per stack) chance to inflict bleed for 2 seconds. New procs refill the timer and add a new stack.";
                quality = (int)ITEMTIERS.COMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.CANSHOOT;
                break;
            case (int)ITEMLIST.POISONSPLOSM:
                itemDescription = "Enemies explode into poison on death, dealing 10% of their max health to nearby enemies over time. Size of explosion increases with stacks.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.MASTERANDPLAYER;
                break;
            case (int)ITEMLIST.ELECTRIC:
                itemDescription = "Your bullets inflict electricity. Enemies inflicted with electricity take 10 damage (+10 per stack) when your bullets hit any other enemy.";
                quality = (int)ITEMTIERS.UNCOMMON;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.ALL;
                break;
            case (int)ITEMLIST.BERSERK:
                itemDescription = "Upon levelling up to an even level, your weapon is swapped with a melee dagger that blocks all enemy shots for 3 (+3 per stack) seconds. Hits restore 5HP and kills restore 1 second on the timer.";
                quality = (int)ITEMTIERS.LEGENDARY;
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.ALL;
                break;
            case (int)ITEMLIST.REROLL:
                quality = (int)ITEMTIERS.UNCOMMON;
                itemDescription = "Reroll all your items.";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.MASTERANDPLAYER;
                break;
            case (int)ITEMLIST.PERFECTHEAL:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "After completing 2 waves without taking damage, heal to full HP and increase max HP by 10 (+10 per stack).";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.HEALMLT:
                quality = (int)ITEMTIERS.UNCOMMON;
                itemDescription = "All healing sources are double as effective. Bonus increases linearly with stacks.";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.BRICK:
                quality = (int)ITEMTIERS.UNCOMMON;
                itemDescription = "10% chance of shooting a bullet that deals 4x damage per stack and infinitely pierces enemies.";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
            case (int)ITEMLIST.BETTERLEVEL:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Recieve +100% the stat bonuses upon levelling up per stack.";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.EXTRAITEMLEVEL:
                quality = (int)ITEMTIERS.UNCOMMON;
                itemDescription = "When levelling to a level divisible by 4, your next item gets applied an extra time. Items get applied an extra time per level, per stack.";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.MORELEVELSTATS:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Level ups also increase your damage, iframe length after getting hit, and bullet size. Bonus increases with stacks.";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.HEALTHXP:
                quality = (int)ITEMTIERS.UNCOMMON;
                itemDescription = "XP drops give you extra XP depending on how much missing health you have (+1 XP per 5% missing HP).";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.LEVELHEAL:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Levelling up heals 25 HP (+25 HP per stack).";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.DAGGERTHROW:
                quality = (int)ITEMTIERS.UNCOMMON;
                itemDescription = "Every 3rd shot, fire 3 (+2 per stack) daggers in an arc that deal 10 damage and inflict bleed.";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.CANSHOOT;
                break;
            case (int)ITEMLIST.MOREXP:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Gain 30% extra XP from XP drops.";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.FAMILIAR:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Gain a gunner that shoots 20 damage bullets. All gunners deal 1.5x damage (+0.5x per stack).";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.HOMINGFAMILIAR:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Gain a gunner that shoots 20 damage bullets. All gunners get one instance of homing (+1 instance per stack).";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.AUTOFAMILIAR:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Gain a gunner that automatically shoots 15 damage bullets. All gunners shoot 1.5x faster (+0.5x per stack)";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.SAWSHOT:
                quality = (int)ITEMTIERS.UNCOMMON;
                itemDescription = "20% chance for bullets to stick to enemies, dealing 20% of your bullet damage 10 (+10 per stack) times over 2 (+2 per stack) seconds and creating a small creep puddle. Can proc on-hit effects.";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
            case (int)ITEMLIST.LUCKIER:
                quality = (int)ITEMTIERS.LEGENDARY;
                itemDescription = "All luck-based events are more likely to occur";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;
            case (int)ITEMLIST.MORECRITS:
                quality = (int)ITEMTIERS.COMMON;
                itemDescription = "Crit chance increased by 20% per stack";
                addNewScriptForNewInstance = true;
                whatUses = (int)ITEMOWNERS.ALL;
                break;
            case (int)ITEMLIST.MARCEL:
                quality = (int)ITEMTIERS.LEGENDARY;
                itemDescription = "hey guys its teh epic marcelageloo";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BEING;
                break;


            // For items enemies use.
            case (int)ITEMLIST.CREEPSHOT:
                quality = (int)ITEMTIERS.NULL;
                itemDescription = "On collision, your shots leave creep that deals 10% (+10% per stack) of your bullet damage per tick";
                addNewScriptForNewInstance = false;
                whatUses = (int)ITEMOWNERS.BULLET;
                break;
        }
    }
}