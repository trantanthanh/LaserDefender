using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    public bool isProjectile = false;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        if (isProjectile)
        {
            Destroy(gameObject);
        }
    }
}
