using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyParts : MonoBehaviour, IDamageable {
    public enum ColliderBodyType { head, body, lArm, rArm, lLeg, rLeg}
    public ColliderBodyType type;

    [SerializeField] private Zombie zom;

    [SerializeField] private float bPCurrentHealth;

    private void Start() {
        zom = GetComponentInParent<Zombie>();

        bPCurrentHealth = zom.currentBodyPartHealth;
    }

    public void DestroyBodyPart() {
        gameObject.SetActive(false);

        switch (type) {
            case ColliderBodyType.head:
                zom.currentHealth = 0; Destroy(zom.head); zom.Dead(); break;
            case ColliderBodyType.lArm:
                Destroy(zom.leftArm); break;
            case ColliderBodyType.rArm:
                Destroy(zom.rightArm); break;
            case ColliderBodyType.lLeg:
                Destroy(zom.leftLeg); break;
            case ColliderBodyType.rLeg:
                Destroy(zom.rightLeg); break;
            default: break;
        }
    }

    public void Damage(float damage, bool isBodyOrHead) {
        Debug.Log(damage);
        if (isBodyOrHead) {
            zom.currentHealth -= damage;
            zom.Dead();
        } else {
            bPCurrentHealth -= damage;
            if (damage > bPCurrentHealth) zom.currentHealth -= (damage - bPCurrentHealth) / 2f;
            if (bPCurrentHealth <= 0) DestroyBodyPart();
      
            Debug.Log(bPCurrentHealth);
        }
    }
}
