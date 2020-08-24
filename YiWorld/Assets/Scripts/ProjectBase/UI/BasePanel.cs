/****************************************************
    文件：BasePanel.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：2020/4/18 22:43:19
	功能：Nothing
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour 
{
    private Dictionary<string,List<UIBehaviour>> controlsDic = new Dictionary<string, List<UIBehaviour>>();

    private void Start()
    {
        FindChild<Button>();
        FindChild<Image>();
    }

    private void FindChild<T>() where T:UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();
        string objName="";
        for (int i = 0; i < controls.Length; i++)
        {
            objName = controls[i].gameObject.name;
            if (controlsDic.ContainsKey(objName))
                controlsDic[objName].Add(controls[i]);
            else
                controlsDic.Add(controls[i].gameObject.name,new List<UIBehaviour>{ controls[i] });
        }
    }
}