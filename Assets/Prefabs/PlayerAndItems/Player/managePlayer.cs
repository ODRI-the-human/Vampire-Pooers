using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managePlayer : MonoBehaviour
{
    [System.NonSerialized] public int playerID = 0;
    public int trashVar; // used just to visualise playerID. if playerID is serialised it all fucks up so this is just for debug!
    public bool IDSet = false; // used just to visualise playerID. if playerID is serialised it all fucks up so this is just for debug!
    public GameObject itemsHeldVisualiser;

    public Material player2Mat;
    public Material player3Mat;
    public Material player4Mat;
    public Material chosenMaterial;

    void Start()
    {
        SetMaterial();
    }

    void FixedUpdate()
    {
        trashVar = playerID;
    }

    public void SetMaterial()
    {
        switch (playerID)
        {
            case 2:
                chosenMaterial = player2Mat;
                break;
            case 3:
                chosenMaterial = player3Mat;
                break;
            case 4:
                chosenMaterial = player4Mat;
                break;
        }

        if (playerID != 1)
        {
            GameObject model = gameObject.transform.Find("cringe").gameObject;
            MeshRenderer[] renderers = model.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer rendy in renderers)
            {
                rendy.material = chosenMaterial;
            }
        }
    }

    void itemsAdded(bool itemIsPassive)
    {
        if (itemIsPassive && itemsHeldVisualiser != null)
        {
            itemsHeldVisualiser.GetComponent<itemVisualiser>().UpdateVisual();
        }
    }
}
