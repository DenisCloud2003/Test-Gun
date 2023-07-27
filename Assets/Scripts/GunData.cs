using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject {

    [Header("Info")]
    public GameObject prefab;
    public new string name;
    public Sprite icon;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int maxSize;
    public float fireRate;
    public float reloadTime;
    public bool isReloading;
}
