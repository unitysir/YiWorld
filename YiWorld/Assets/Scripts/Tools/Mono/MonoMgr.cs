/****************************************************
    文件：MonoMgr.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：2020/4/18 9:32:6
	功能：Nothing
*****************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Internal;

public class MonoMgr : Single<MonoMgr>
{
    private MonoController controller;

    public MonoMgr()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }


    public void AddUpdateListener(UnityAction fun)
    {
        controller.AddUpdateListener(fun);
    }

    public void DelUpdateListener(UnityAction fun)
    {
        controller.DelUpdateListener(fun);
    }

        public void AddFixedUpdateListener (UnityAction fun) {
        controller.AddFixedUpdateListener(fun);
    }

    public void DelFixedUpdateListener (UnityAction fun) {
       controller.DelFixedUpdateListener(fun);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

}