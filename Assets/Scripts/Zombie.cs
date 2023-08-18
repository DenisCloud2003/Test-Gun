using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    [SerializeField] private EnemyData _enemyData;
    [SerializeField] private Animator anim;
    [SerializeField] private float delayTime = 1f;

    private const string DEAD = "Dead";

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

    public void Dead() {
        if (currentHealth <= 0) StartCoroutine(StartDeadAnimation());
        else Debug.Log(gameObject.name + ": " + currentHealth);
    }

    private IEnumerator StartDeadAnimation() {
        anim.SetTrigger(DEAD);
        yield return new WaitForSeconds(delayTime);
        gameObject.SetActive(false);
    }
}
