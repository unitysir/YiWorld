/****************************************************
    文件：SceneMgr.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：2020/4/18 10:24:18
	功能：Nothing
*****************************************************/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : BaseManager<SceneMgr>
{
    public void LoadScene(string name,Action fun)
    {
        SceneManager.LoadScene(name);
        fun();
    }

    public void LoadSceneAysnc(string name, Action fun)
    {
        MonoMgr.GetInstance().StartCoroutine(AysncScene(name, fun));
        
    }

    private IEnumerator AysncScene(string name, Action fun)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while (!ao.isDone)
        {
            EventCenter.GetInstance().EventTrigger("LoadingFun",ao.progress);
            yield return ao.progress;
        }

        fun();
    } 

}