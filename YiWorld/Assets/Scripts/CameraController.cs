using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float horizontalSpeed = 100f;
    public float verticalSpeed = 80f;

    private GameObject player;
    private GameObject cameraHandle;

    private GameObject model;
    private GameObject camera;

    private float tempEulerX;

    private Vector3 cameraDampVelocity;

    private void Awake() {
        cameraHandle = transform.parent.gameObject;
        player = cameraHandle.transform.parent.gameObject;
        camera = Camera.main.gameObject;
    }

    private void Start() {
        model = player.GetComponent<PlayerCtrl>().m_model;
        MonoMgr.Instance.AddFixedUpdateListener(OnFixedUpdate);
    }

    private void OnFixedUpdate() {

        Vector3 tempModelEuler = model.transform.eulerAngles;

        player.transform.Rotate(Vector3.up, PlayerCtrl.Instance.jRight * horizontalSpeed * Time.deltaTime);

        tempEulerX -= PlayerCtrl.Instance.jUp * -verticalSpeed * Time.deltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -20f, 30f);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        model.transform.eulerAngles = tempModelEuler;

        camera.transform.localPosition = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, 0.1f);

        camera.transform.eulerAngles = transform.eulerAngles;
    }

}
