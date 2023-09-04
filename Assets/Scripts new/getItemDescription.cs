using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class getItemDescription : MonoBehaviour
{
    GameObject spawnedSelector;
    public int itemsExist = 0;
    int itemNearest = 0;
    public string itemDescription;
    Vector3 position;
    public GameObject itemSelector;
    public string curseDescription;

    public void InputAim(InputAction.CallbackContext context)
    {
        position = gameObject.GetComponent<Attack>().reticle.transform.position; //new Vector3(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()).x, Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()).y, 0);
        position = new Vector3(position.x, position.y, 0);

        GameObject[] gos = GameObject.FindGameObjectsWithTag("item");
        if (gos.Length > 0)
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }

            if (closest.GetComponent<Pedestal>().isItemPedestal)
            {
                ItemSOInst item = closest.GetComponent<Pedestal>().chosenItem;
                itemDescription = item.name + ": " + item.description;
            }
            else
            {
                AbilityParams item = closest.GetComponent<Pedestal>().chosenAbility;
                itemDescription = item.name + ": " + item.description;
            }
        }
        else
        {
            itemDescription = "";
            curseDescription = "";
        }
    }

    public void OnSelectItem(InputAction.CallbackContext context)
    {
        context.action.performed += ctx =>
        {
            spawnedSelector = Instantiate(itemSelector, position, transform.rotation);
            spawnedSelector.transform.SetParent(gameObject.transform);
            spawnedSelector.GetComponent<mouseItemSelection>().master = gameObject;
        };

        context.action.canceled += ctx =>
        {
            Destroy(spawnedSelector);
        };
    }
}
