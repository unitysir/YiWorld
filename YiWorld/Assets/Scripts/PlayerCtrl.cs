using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {



    #region Input
    /// <summary>
    /// 输入开关
    /// </summary>
    public bool isInput = true;
    /// <summary>
    /// 前进或后退
    /// </summary>
    public float UD;
    /// <summary>
    /// 向左或向右
    /// </summary>
    public float LR;
    /// <summary>
    /// 旋转速度
    /// </summary>
    public float velocity;
    /// <summary>
    /// 方向
    /// </summary>
    public Vector3 dir;

    public bool isRun;//是否跑步

    public bool isJump;//是否跳跃
    private bool lastJump;//是否正在跳跃

    public KeyCode keyUp = KeyCode.W;
    public KeyCode keyDown = KeyCode.S;
    public KeyCode keyLeft = KeyCode.A;
    public KeyCode keyRight = KeyCode.D;

    public float walkSpeed = 1.45f;
    public float runMultiplier = 6f;
    /// <summary>
    /// 跳跃向量
    /// </summary>
    public float jumpVelocity = 6f;
    /// <summary>
    /// 等待跳跃事件
    /// </summary>
    public float jumpStaySeconds=0.5f;

    [Header("Other Key")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public KeyCode keyJump = KeyCode.Space;

    /// <summary>
    /// 前进或后退
    /// </summary>
    private float targetUD;
    /// <summary>
    /// 向左或向右
    /// </summary>
    private float targetLR;
    private float velocityUD;
    private float velocityLR;

    #endregion

    private GameObject m_model;
    private Rigidbody m_rigidbody;
    private Animator m_animator;

    /// <summary>
    /// 移动方向
    /// </summary>
    private Vector3 m_planeDir;
    /// <summary>
    /// 冲量方向
    /// </summary>
    private Vector3 m_thrustDir;
    /// <summary>
    /// 是否锁死平面移动
    /// </summary>
    private bool isLockPlane = false;//

    private void Awake() {
        m_rigidbody = GetComponent<Rigidbody>();
        m_model = transform.GetChild(0).gameObject;
        m_animator = m_model.GetComponent<Animator>();
    }

    private void Start() {
        MonoMgr.Instance.AddUpdateListener(OnUpdate);
        MonoMgr.Instance.AddFixedUpdateListener(OnFixedUpdate);
    }

    private void OnUpdate() {
        if (!isInput) return;

        // 从键盘获取输入的方向
        targetUD = ((Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0));
        targetLR = ((Input.GetKey(keyLeft) ? 1.0f : 0) - (Input.GetKey(keyRight) ? 1.0f : 0));

        // 缓缓朝向目标
        UD = Mathf.SmoothDamp(UD, targetUD, ref velocityUD, 0.1f);
        LR = Mathf.SmoothDamp(LR, targetLR, ref velocityLR, 0.1f);

        Vector2 tempAxis = SquareToCircle(new Vector2(LR, UD));
        float UD2 = tempAxis.y;
        float LR2 = tempAxis.x;
        velocity = Mathf.Sqrt(UD2 * UD2 + LR2 * LR2);
        dir = UD2 * transform.forward + LR2 * transform.right;

        // 是否奔跑
        isRun = Input.GetKey(keyRun);
        //是否跳跃
        bool newJump = Input.GetKey(keyJump);
        if (newJump != lastJump && newJump == true) isJump = true;
        else isJump = false;
        lastJump = newJump;

        float targetRunMulti = (isRun ? 2.0f : 1.0f);
        m_animator.SetFloat("move", velocity * Mathf.Lerp(m_animator.GetFloat("move"), targetRunMulti, 0.5f));     //动画控制
        if (isJump) {
            m_animator.SetTrigger("jump");
        }
        if (velocity > 0.1f) {
            Vector3 targetForward = Vector3.Slerp(m_model.transform.forward, dir, 0.4f);  //做差值，增加切换旋转动画时的平滑度
            m_model.transform.forward = targetForward;      //旋转
        }

        if (!isLockPlane) {
            m_planeDir = velocity * m_model.transform.forward * walkSpeed * (isRun ? runMultiplier : 1.0f);
        }

    }

    private void OnFixedUpdate() {
        m_rigidbody.velocity = new Vector3(m_planeDir.x, m_rigidbody.velocity.y, m_planeDir.z) + m_thrustDir;
        m_thrustDir = Vector3.zero;
    }

    private Vector2 SquareToCircle(Vector2 input) {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }

    ///
    /////////////////////////////////////////////////////////////////////////////////
    ///

    /// <summary>
    /// 当进入跳跃
    /// </summary>
    public void OnJumpEnter() {
        isInput = false;
        isLockPlane = true;
        m_thrustDir = new Vector3(0, jumpVelocity, 0);


        //print("On Jump Enter!!");
    }

    /// <summary>
    /// 退出跳跃
    /// </summary>
    public void OnJumpExit() {
        isInput = true;
        isLockPlane = false;
        //print("On Jump Exit");
    }

    public void IsGround() {
        m_animator.SetBool("isGround", true);
    }

    public void IsNotGround() {
        m_animator.SetBool("isGround", false);
    }

}
