using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Gun : MonoBehaviour {

    [SerializeField] private GunData gunData;
    [SerializeField] private float timeSinceLastShot;
    [SerializeField] private int waitTime = 1;

    //Subscribe event
    private void Start() {
        gunData.currentAmmo = gunData.maxSize;
    }

    private async void OnEnable() {
        await Task.Delay(waitTime);
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() {
        PlayerShoot.shootInput -= Shoot;
        PlayerShoot.reloadInput -= StartReload;
    }

    //Start reloading gun
    public void StartReload() {
        if (!gunData.isReloading) {
            StartCoroutine(Reload());
        }
    }

    //Reload logic
    private IEnumerator Reload() { 
        gunData.isReloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.maxSize;

        gunData.isReloading = false;
    }

    //Time per shot logic
    private bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    
    //Shooting logic
    public void Shoot() {
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance)) {

                    IDamageable damageable = hitInfo.collider.transform.GetComponent<IDamageable>();
                    
                    EnemyBodyParts enemyBodyParts = hitInfo.collider.gameObject.GetComponent<EnemyBodyParts>();
                    if (enemyBodyParts != null) {
                        EnemyBodyParts.ColliderBodyType bodyPart = enemyBodyParts.type;

                        Debug.Log(damageable);

                        switch (bodyPart) {
                            case EnemyBodyParts.ColliderBodyType.body:
                                damageable?.Damage(gunData.damage, true); 
                                break;
                            case EnemyBodyParts.ColliderBodyType.head: 
                                damageable?.Damage(gunData.damage * 1.5f, false); 
                                break;
                            case EnemyBodyParts.ColliderBodyType.lLeg or EnemyBodyParts.ColliderBodyType.rLeg: 
                                damageable?.Damage(gunData.damage / 2, false); 
                                break;
                            case EnemyBodyParts.ColliderBodyType.lArm or EnemyBodyParts.ColliderBodyType.rArm: 
                                damageable?.Damage(gunData.damage / 2, false); 
                                break;
                        }
                    } else Debug.Log("Miss");
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        } else {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
                StartReload();
            }
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
    }

    //Gun VFX
    private void OnGunShot() {

    }
}
