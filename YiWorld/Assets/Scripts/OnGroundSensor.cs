using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour {
    public CapsuleCollider capsule;

    private Vector3 point1;
    private Vector3 point2;

    private float radius;

    private void Start() {
        radius = capsule.radius;
        MonoMgr.Instance.AddFixedUpdateListener(OnFixedUpdate);
    }

    private void OnFixedUpdate() {

        point1 = transform.position + transform.up * radius;
        point2 = transform.position + transform.up * capsule.height - transform.up * radius;
        Collider[] outputcols = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground"));
        if (outputcols.Length != 0) {
            SendMessageUpwards("IsGround");
        } else {
            SendMessageUpwards("IsNotGround");
        }
    }

}
