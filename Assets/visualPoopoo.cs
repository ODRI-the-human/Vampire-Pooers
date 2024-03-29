using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class visualPoopoo : MonoBehaviour
{
    public bool isPaused = false;
    float amounter;
    bool isFrozen;

    public InputActionAsset actions;

    // Start is called before the first frame update
    void Start()
    {
        if (actions != null)
        {
            actions.FindActionMap("menus").Enable();
            actions.FindActionMap("menus").FindAction("restart").performed += OnRestart;
            actions.FindActionMap("menus").FindAction("slowmo").performed += OnSlowmo;
            actions.FindActionMap("menus").FindAction("nextLevel").performed += GoToNextLevel;
        }
    }

    void GoToNextLevel(InputAction.CallbackContext context)
    {
        gameObject.GetComponent<GenerateTerrain>().ProceedToNextLevel();
    }

    void OnRestart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
    }

    void OnSlowmo(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0.01f;
            isPaused = true;
        }
    }

    IEnumerator doFreeze()
    {
        float original = Time.timeScale;
        Time.timeScale = 0;
        isFrozen = true;

        yield return new WaitForSecondsRealtime(amounter);

        Time.timeScale = original;
        isFrozen = false;
    }

    public void bigHitFreeze(float amount)
    {
        if (!isFrozen)
        {
            amounter = amount;
            StartCoroutine(doFreeze());
        }
    }
}
