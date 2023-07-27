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
    public float speed;
    public float damage;

    [Header("BODY STATS")]
    public float bodyHealth;
    public float headHealth;
    public float armsHealth;
    public float legsHealth;

    [Header("TYPE")]
    public string type;
}
