using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour {
    [SerializeField] private Gun gunScript;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider coll;
    [SerializeField] private Transform player, gunContainer, cam;

    [SerializeField] private float pickUpRange;
    [SerializeField] private float dropForwardForce, dropUpwardForce;

    [SerializeField] private bool equipped;
    public static bool slotFull;

    private void Start() {
        //Setup
        if (!equipped) {
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped) {
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update() {
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "Q" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp() {
        Debug.Log("E");

        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //Add gun to List
        EvolveGames.ItemChange.Instance.Items.Add(gameObject);

        //Enable script
        gunScript.enabled = true;
    }

    private void Drop() {
        Debug.Log("Q");

        equipped = false;
        slotFull = false;

        //Set parrent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Remove gun from List
        EvolveGames.ItemChange.Instance.Items.Remove(gameObject);

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //Enable script
        gunScript.enabled = false;
    }
}
