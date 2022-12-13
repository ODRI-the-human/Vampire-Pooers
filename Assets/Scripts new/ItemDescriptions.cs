using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptions : MonoBehaviour
{
    public int itemChosen;
    public bool enemiesCanUse = false;
    public string itemDescription;

    public void getItemDescription()
    {
        switch (itemChosen)
        {
            case (int)ITEMLIST.HP25:
                itemDescription = "Increases max HP by 25 per stack.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.HP50:
                itemDescription = "Increases max HP by 50 per stack.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.DMGADDPT5:
                itemDescription = "Increases base damage by 25 per stack.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.DMGMLT2:
                itemDescription = "Doubles damage. Multiplier increases by +1x per stack.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.FIRERATE:
                itemDescription = "Decreases fire delay by 4.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.SOY:
                itemDescription = "Per stack, increases firerate by 5x, but quarters damage and bullet knockback.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.HOMING:
                itemDescription = "Homing shots. Stacks increase range from which bullets home.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.ATG:
                itemDescription = "10% chance on hitting an enemy to fire a homing missile, dealing 3x (+3x per stack) of the damage that procced it.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.MORESHOT:
                itemDescription = "Per stack, fire +1 projectile in an arc.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.WAPANT:
                itemDescription = "Every 3 seconds, create a slowing puddle of creep. Size of creep increases with stacks.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.HOLYMANTIS:
                itemDescription = "Divide the first damage you take each round by 2 (+1 per stack). Works +1 time per round, per stack.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.CONVERTER:
                itemDescription = "At the start of each round, 10% (+10% per stack) of your missing %HP is added to your base damage.";
                break;
            case (int)ITEMLIST.EASIERTIMES:
                itemDescription = "20% chance to block damage. Chance increases with stacks.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.STOPWATCH:
                itemDescription = "Enemies move and shoot 25% slower. Slowdown increases with stacks.";
                break;
            case (int)ITEMLIST.BOUNCY:
                itemDescription = "Your bullets bounce +1 time per stack.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.FOURDIRMARTY:
                itemDescription = "Every 5 (-1 per stack) shots, fire 3 extra bullets around you that copy all your item effects.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.PIERCING:
                itemDescription = "Your bullets pierce +1 enemies per stack.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.CREEP:
                itemDescription = "Leave creep on the ground that deals 20% of your damage per tick. Size of creep increases with stacks.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.DODGESPLOSION:
                itemDescription = "At the end of your dodge, deal 50 damage and knock back enemies in a certain radius (radius increases with stacks).";
                break;
            case (int)ITEMLIST.BETTERDODGE:
                itemDescription = "Dodge further, faster, and get some iframes at the end of the dodge. Bonus increases with stacks.";
                break;
            case (int)ITEMLIST.ORBITAL1:
                itemDescription = "Gain a long-range orbital that blocks enemy shots and deals damage. Stacks add extra orbitals.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.ORBITAL2:
                itemDescription = "Gain a close-range orbital that blocks enemy shots, damages enemies, and shoots, copying all your bullet effects and dealing 25% of your damage. Stacks add extra orbitals.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.SPLIT:
                itemDescription = "Your shots split on hit. Split shots deal 30% (+30% per stack) of your damage.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.CONTACT:
                itemDescription = "Enemy bullets bounce off your bullets and now damage enemies rather than you. Your bullets are destroyed after 2 (+2 per stack) collisions.";
                
                break;
            case (int)ITEMLIST.BLEED:
                itemDescription = "Your attacks have a 15% (+15% per stack) chance to inflict bleed for 2 seconds. New procs refill the timer and add a new stack.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.POISONSPLOSM:
                itemDescription = "Enemies explode on death, poisoning nearby enemies for 2 seconds for 10% of your damage. Size of explosion increases with stacks.";
                break;
            case (int)ITEMLIST.ELECTRIC:
                itemDescription = "Your bullets inflict electricity. Enemies inflicted with electricity take 10 damage (+10 per stack) when your bullets hit any other enemy.";
                
                break;
            case (int)ITEMLIST.BERSERK:
                itemDescription = "Upon levelling up to an even level, your weapon is swapped with a melee knife that blocks all enemy shots for 3 (+3 per stack) seconds. Hits restore 5HP and kills restore 1 second on the timer.";
                break;
            case (int)ITEMLIST.REROLL:
                itemDescription = "Reroll all your items.";
                break;
            case (int)ITEMLIST.PERFECTHEAL:
                itemDescription = "After completing 2 waves without taking damage, heal to full HP and increase max HP by 10 (+10 per stack).";
                break;
            case (int)ITEMLIST.HEALMLT:
                itemDescription = "All healing sources are double as effective. Bonus increases linearly with stacks.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.BRICK:
                itemDescription = "10% chance (+10% per stack) of shooting a bullet that deals 4x damage and infinitely pierces enemies.";
                enemiesCanUse = true;
                
                break;
            case (int)ITEMLIST.BETTERLEVEL:
                itemDescription = "Recieve +2x the stat bonuses upon levelling up per stack.";
                break;
            case (int)ITEMLIST.EXTRAITEMLEVEL:
                itemDescription = "When levelling to a level divisible by 4, your next item gets applied an extra time. Items get applied an extra time per level, per stack.";
                break;
            case (int)ITEMLIST.MORELEVELSTATS:
                itemDescription = "Level ups also increase your damage, iframe length after getting hit, and bullet size. Bonus increases with stacks.";
                break;
            case (int)ITEMLIST.HEALTHXP:
                itemDescription = "XP drops give you extra XP depending on how much missing health you have (+1 XP per 5% missing HP).";
                break;
            case (int)ITEMLIST.LEVELHEAL:
                itemDescription = "Levelling up heals 25 HP (+25 HP per stack).";
                break;
            case (int)ITEMLIST.DAGGERTHROW:
                itemDescription = "Every 3rd shot, fire 3 (+2 per stack) daggers in an arc that deal 10 damage and inflict bleed.";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.MOREXP:
                itemDescription = "Gain 30% extra XP from XP drops.";
                break;
            case (int)ITEMLIST.FAMILIAR:
                itemDescription = "Gain a follower that shoots 20 damage bullets. All followers deal 1.5x damage (+0.5x per stack).";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.HOMINGFAMILIAR:
                itemDescription = "Gain a follower that shoots 20 damage bullets. All followers get one instance of homing (+1 instance per stack).";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.AUTOFAMILIAR:
                itemDescription = "Gain a follower that automatically shoots 15 damage bullets. All familiars shoot 1.5x faster (+0.5x per stack)";
                enemiesCanUse = true;
                break;
            case (int)ITEMLIST.SAWSHOT:
                itemDescription = "10% chance for bullets to stick to enemies, dealing 20% of your bullet damage 10 (+10 per stack) times over 2 (+2 per stack) seconds and creating a small creep puddle. Can proc on-hit effects.";
                enemiesCanUse = true;
                
                break;
        }
    }
}