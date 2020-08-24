﻿/****************************************************
    文件：BaseManager.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/

using UnityEngine;

public class BaseManager <T> where T:new()
{
    private static T instance;

    public static T GetInstance()
    {
        if(instance==null) instance = new T();
        return instance;
    }
}