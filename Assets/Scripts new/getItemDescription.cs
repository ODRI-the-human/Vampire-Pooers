using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class getItemDescription : MonoBehaviour
{
    GameObject spawnedSelector;
    public bool itemsExist = false;
    int itemNearest = 0;
    public string itemDescription;
    Vector3 position;
    public GameObject itemSelector;
    public string curseDescription;

    public void InputAim(InputAction.CallbackContext context)
    {
        if (itemsExist)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("item");
            GameObject closest = null;
            float distance = Mathf.Infinity;
            position = gameObject.GetComponent<Attack>().reticle.transform.position; //new Vector3(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()).x, Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()).y, 0);
            position = new Vector3(position.x, position.y, 0);
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

            itemDescription = closest.GetComponent<itemPedestal>().description;
            curseDescription = closest.GetComponent<itemPedestal>().curseDescription;

        }
        else
        {
            itemDescription = "";
            curseDescription = "";
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        context.action.performed += ctx =>
        {
            Destroy(spawnedSelector);

            spawnedSelector = Instantiate(itemSelector, position, transform.rotation);
            spawnedSelector.transform.SetParent(gameObject.transform);
            spawnedSelector.GetComponent<mouseItemSelection>().master = gameObject;
        };
    }
}
