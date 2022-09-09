using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptions : MonoBehaviour
{
    public int itemChosen;
    public string itemDescription;

    public void getItemDescription()
    {
        switch (itemChosen)
        {
            case (int)ITEMLIST.HP25:
                itemDescription = "Increases max HP by 25 per stack.";
                break;
            case (int)ITEMLIST.HP50:
                itemDescription = "Increases max HP by 50 per stack.";
                break;
            case (int)ITEMLIST.DMGADDPT5:
                itemDescription = "Increases base damage by 25 per stack.";
                break;
            case (int)ITEMLIST.DMGMLT2:
                itemDescription = "Doubles damage. Multiplier increases by +1x per stack.";
                break;
            case (int)ITEMLIST.FIRERATE:
                itemDescription = "Decreases fire delay by 4.";
                break;
            case (int)ITEMLIST.SOY:
                itemDescription = "Per stack, increases firerate by 5x, but quarters damage and bullet knockback.";
                break;
            case (int)ITEMLIST.HOMING:
                itemDescription = "Homing shots. Stacks increase range from which bullets home.";
                break;
            case (int)ITEMLIST.ATG:
                itemDescription = "10% (+10% per stack) chance on hitting an enemy to fire a homing missile, dealing 100 damage.";
                break;
            case (int)ITEMLIST.MORESHOT:
                itemDescription = "Per stack, fire +1 projectile in an arc.";
                break;
            case (int)ITEMLIST.WAPANT:
                itemDescription = "Every 3 seconds, create a slowing puddle of creep. Size of creep increases with stacks.";
                break;
            case (int)ITEMLIST.HOLYMANTIS:
                itemDescription = "Divide the first damage you take each round by 2 (+1 per stack). Works +1 time per round, per stack.";
                break;
            case (int)ITEMLIST.CONVERTER:
                itemDescription = "At the start of each round, 10% (+10% per stack) of your missing %HP is added to your base damage.";
                break;
            case (int)ITEMLIST.EASIERTIMES:
                itemDescription = "20% chance to block damage. Chance increases with stacks.";
                break;
            case (int)ITEMLIST.STOPWATCH:
                itemDescription = "Enemies move and shoot 25% slower. Slowdown increases with stacks.";
                break;
            case (int)ITEMLIST.BOUNCY:
                itemDescription = "Your bullets bounce +1 time per stack.";
                break;
            case (int)ITEMLIST.FOURDIRMARTY:
                itemDescription = "Every 5 (-1 per stack) shots, fire 3 extra bullets around you that copy all your item effects.";
                break;
            case (int)ITEMLIST.PIERCING:
                itemDescription = "Your bullets pierce +1 enemies per stack.";
                break;
            case (int)ITEMLIST.CREEP:
                itemDescription = "Leave damaging on the ground as you move. Size of creep increases with stacks, and damage scales with your damage.";
                break;
            case (int)ITEMLIST.DODGESPLOSION:
                itemDescription = "At the end of your dodge, deal 50 damage and knock back enemies in a certain radius (radius increases with stacks).";
                break;
            case (int)ITEMLIST.BETTERDODGE:
                itemDescription = "Dodge further, faster, and get some iframes at the end of the dodge. Bonus increases with stacks.";
                break;
            case (int)ITEMLIST.ORBITAL1:
                itemDescription = "Gain a long-range orbital that blocks enemy shots and deals damage. Stacks add extra orbitals.";
                break;
            case (int)ITEMLIST.ORBITAL2:
                itemDescription = "Gain a close-range orbital that blocks enemy shots, damages enemies, and shoots, copying all your bullet effects. Deals 25% (+25%) of your damage per stack.";
                break;
            case (int)ITEMLIST.SPLIT:
                itemDescription = "Your shots split on hit. Split shots deal 30% (+30% per stack) of your damage.";
                break;
            case (int)ITEMLIST.CONTACT:
                itemDescription = "Your shots destroy enemy shots. Your bullets get destroyed after 1 (+1 per stack) collision, or 4 (+4 per stack) if using melee.";
                break;
            case (int)ITEMLIST.BLEED:
                itemDescription = "Your attacks have a 15% (+15% per stack) chance to inflict bleed for 2 seconds. New procs refill the timer and add a new stack.";
                break;
            case (int)ITEMLIST.POISONSPLOSM:
                itemDescription = "Enemies explode on death, poisoning nearby enemies for 2 seconds. Size of explosion increases with stacks.";
                break;
            case (int)ITEMLIST.ELECTRIC:
                itemDescription = "Your bullets inflict electricity. Enemies inflicted with electricity take 20 damage when your bullets hit ANY enemy.";
                break;
        }
    }
}