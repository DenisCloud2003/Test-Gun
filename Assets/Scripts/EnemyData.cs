using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy")]
public class EnemyData : ScriptableObject {

    [Header("ENEMY INFO")]
    public new string name;
    public GameObject prefab;

    [Header("STATS")]
    public float maxHealth;
    public float bodyPartMaxHealth;
    public float walkSpeed;
    public float runSpeed;
    public float damage;
    public float attackRange;

    [Header("TYPE")]
    public string type;
}
