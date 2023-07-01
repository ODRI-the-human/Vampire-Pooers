using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1Moves : MonoBehaviour
{
    int timer = 0;
    public float bulletHeckAngleMult = 0;
    public GameObject sawner;

    // Start is called before the first frame update
    void Start()
    {
        ResetAll();
    }

    void SelectAttack()
    {
        int attackType = Random.Range(0, 5); // hey mesh check out this hilarious code, it's SO FUNNY!@!!!!!!!!!!!!!!!!!!!!!

        Debug.Log("lol");

        switch (attackType)
        {
            case 0:
                StartCoroutine(LazerShot());
                break;
            case 1:
                bulletHeckAngleMult = 1;
                StartCoroutine(BulletHeck());
                break;
            case 2:
                bulletHeckAngleMult = 0.4f;
                StartCoroutine(BulletHeck());
                break;
            case 3:
                StartCoroutine(BulletArc());
                break;
            case 4:
                StartCoroutine(ChainsawMan());
                break;

        }
    }

    IEnumerator LazerShot()
    {
        gameObject.GetComponent<Attack>().getEnemyPos = false;
        gameObject.GetComponent<Attack>().shotAngleCoeff = 2;
        gameObject.GetComponent<NewPlayerMovement>().speedMult = 0;

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.8f);
            gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.LAZER);
            gameObject.GetComponent<Attack>().noExtraShots++;
            gameObject.GetComponent<Attack>().UseWeapon(false);
        }

        ResetAll();
    }

    IEnumerator BulletHeck()
    {
        gameObject.GetComponent<Attack>().noExtraShots = 17;
        gameObject.GetComponent<Attack>().shotAngleCoeff = 0.5f;
        gameObject.GetComponent<Attack>().specialFireType = 1;
        gameObject.GetComponent<Attack>().doAim = false;
        gameObject.GetComponent<Attack>().shotSpeed = 4;

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.75f * bulletHeckAngleMult);
            gameObject.GetComponent<Attack>().angleAddAmount += bulletHeckAngleMult * (Mathf.PI / 18);
            gameObject.GetComponent<Attack>().UseWeapon(false);
        }

        ResetAll();
    }

    IEnumerator BulletArc()
    {
        gameObject.GetComponent<Attack>().noExtraShots = 1;
        gameObject.GetComponent<Attack>().shotAngleCoeff = 8;
        gameObject.GetComponent<Attack>().specialFireType = 0;
        gameObject.GetComponent<Attack>().getEnemyPos = false;
        gameObject.GetComponent<Attack>().shotSpeed = 8;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<Attack>().shotAngleCoeff -= 0.8f;
            gameObject.GetComponent<Attack>().UseWeapon(false);
        }

        ResetAll();
    }

    IEnumerator ChainsawMan()
    {
        GameObject target = gameObject.GetComponent<Attack>().currentTarget;
        gameObject.GetComponent<Attack>().getEnemyPos = false;
        gameObject.GetComponent<NewPlayerMovement>().speedMult = 0;
        gameObject.AddComponent<moveInDirBriefly>();
        gameObject.GetComponent<moveInDirBriefly>().owner = gameObject;
        gameObject.GetComponent<moveInDirBriefly>().target = target;

        yield return new WaitForSeconds(0.75f);

        GameObject saw = Instantiate(sawner);
        saw.GetComponent<sawRotation>().owner = gameObject;
        saw.GetComponent<sawRotation>().target = target;

        yield return new WaitForSeconds(1);

        Destroy(saw);
        ResetAll();
    }

    void ResetAll()
    {
        gameObject.GetComponent<ItemHolder>().SetWeapon((int)ITEMLIST.PISTOL);
        gameObject.GetComponent<Attack>().getEnemyPos = true;
        gameObject.GetComponent<Attack>().noExtraShots = 0;
        gameObject.GetComponent<Attack>().shotAngleCoeff = 1;
        gameObject.GetComponent<Attack>().specialFireType = 0;
        gameObject.GetComponent<Attack>().doAim = true;
        gameObject.GetComponent<Attack>().angleAddAmount = 0;
        gameObject.GetComponent<Attack>().shotSpeed = 4;
        gameObject.GetComponent<NewPlayerMovement>().speedMult = 1;
        gameObject.GetComponent<NewPlayerMovement>().recievesKnockback = false;
        bulletHeckAngleMult = 0;

        Invoke(nameof(SelectAttack), 1.5f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall" && col.gameObject.GetComponent<obstHP>() != null)
        {
            col.gameObject.GetComponent<obstHP>().HP = 0;
        }
    }
}
