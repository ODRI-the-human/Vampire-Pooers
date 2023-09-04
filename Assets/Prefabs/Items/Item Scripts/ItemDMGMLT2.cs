using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDMGMLT2 : ItemScript
{
    public override float DamageMult()
    {
        return 1f + instances * 0.5f;
    }
}
