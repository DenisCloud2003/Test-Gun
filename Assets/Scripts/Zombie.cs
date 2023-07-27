using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamageable {
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private float currentHealth;

    private void Start() {
        currentHealth = _enemyData.maxHealth;
    }

    public void Damage(float damage) {
        currentHealth -= damage;
        Debug.Log(damage);
    }

    public void Dead() {
        if(currentHealth<=0) {
            
        }
    }
}
