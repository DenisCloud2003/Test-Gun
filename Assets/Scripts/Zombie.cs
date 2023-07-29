using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Animator anim;
    [SerializeField] private float gravity;
    
    public float currentHealth;
    public float currentBodyPartHealth;

    public GameObject head;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject leftLeg;
    public GameObject rightLeg;

    private Vector3 moveDir = Vector3.zero;


    private void Start() {
        currentHealth = _enemyData.maxHealth;
        currentBodyPartHealth = _enemyData.bodyPartMaxHealth;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        moveDir.y -= gravity * Time.deltaTime;
    }

    public void Dead() {
        if(currentHealth<=0) {
            anim.SetTrigger("Dead");
        } else Debug.Log(gameObject.name + ": " + currentHealth);
    }
}
