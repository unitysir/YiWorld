using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlPlayer : MonoBehaviour
{
    private Animator player;
    private Rigidbody rigidbody;

    private void Awake() {
        player = transform.GetChild(0).GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.W)) {
            rigidbody.velocity = new Vector3(0, 0, 1f);
        }
    }

}
