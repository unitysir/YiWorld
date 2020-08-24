/****************************************************
    文件：ResMgr.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：2020/4/18 15:44:52
	功能：Nothing
*****************************************************/

using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResMgr : BaseManager<ResMgr>
{
    public T Load<T>(string name) where T:Object
    {
        T res = Resources.Load<T>(name);
        if (res is GameObject)
        {
            return GameObject.Instantiate(res);
        }
        return res;
        
    }

    public void LoadAsync<T>(string name, Action<T> action) where T:Object
    {
        MonoMgr.GetInstance().StartCoroutine(IELoadAsync<T>(name, action));
    }

    private IEnumerator IELoadAsync<T>(string name, Action<T> action) where T:Object
    {
        ResourceRequest res = Resources.LoadAsync<T>(name);
        yield return res;

        if (res.asset is GameObject)
            action(GameObject.Instantiate(res.asset) as T);
        else
            action(res.asset as T);
    }

}