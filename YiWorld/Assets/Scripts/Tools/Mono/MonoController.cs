/****************************************************
    文件：MonoController.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：2020/4/18 9:31:56
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour {
    private UnityAction action;
    private UnityAction actionFixedUpdate;
    public void AddUpdateListener (UnityAction fun) {
        action += fun;
    }

    public void DelUpdateListener (UnityAction fun) {
        action -= fun;
    }

    public void AddFixedUpdateListener (UnityAction fun) {
        actionFixedUpdate += fun;
    }

    public void DelFixedUpdateListener (UnityAction fun) {
        actionFixedUpdate -= fun;
    }

    private void Start () {
        DontDestroyOnLoad (this);
    }

    private void Update () {
        if (action != null)
            action ();
    }

    private void FixedUpdate() {
         if (actionFixedUpdate != null)
            actionFixedUpdate ();
    }

}