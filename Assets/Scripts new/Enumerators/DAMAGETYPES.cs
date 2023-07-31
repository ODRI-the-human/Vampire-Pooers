using System.ComponentModel;

public enum DAMAGETYPES
{
    // Types of damage
    NORMAL,
    BLEED,
    POISON,
    ELECTRIC,
    EXPLOSION,

    // Resistances to statuses (reduced chance to proc)
    BIOHAZARD,

    // Not real damage type, just for setting the proper damage number colour.
    HEAL,
}