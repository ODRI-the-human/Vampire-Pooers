using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowBall : MonoBehaviour
{
    public int abilityIndex;
    public float coolDownFacTotal;
    public GameObject owner;
    public AbilityParams crossBowAbility;
    public Vector3 direction;
    public AudioClip sfx;

    // Start is called before the first frame update
    void Start()
    {
        coolDownFacTotal = owner.GetComponent<Attack>().GetCoolDownFac(gameObject.GetComponent<DealDamage>().abilityIndex);
        InvokeRepeating(nameof(Shootery), 1f * coolDownFacTotal, 0.15f * coolDownFacTotal);
        Invoke(nameof(Die), 4f);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Shootery()
    {
        SoundManager.Instance.PlaySound(sfx);
        for (int i = 0; i < 3; i++)
        {
            crossBowAbility.UseAttack(owner, null, transform.position, new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, 0).normalized,
                                  owner.GetComponent<Attack>().isPlayerTeam, 0, false, true, true, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += 10 * direction * Time.deltaTime;
    }

    void FixedUpdate()
    {
        direction *= 0.95f;
    }
}
