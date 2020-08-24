/****************************************************
    文件：InputMgr.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：2020/4/18 19:47:57
	功能：Nothing
*****************************************************/

using UnityEngine;

public class InputMgr : BaseManager<InputMgr>
{
    private bool isStart = false;

    public InputMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(MyUpdate);
    }

    public void StartOrEndCheck(bool isOpen)
    {
        isStart = isOpen;
    }

    //检测按键抬起或者按下
    public void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            EventCenter.GetInstance().EventTrigger("某键按下",key);
        }

        if (Input.GetKeyUp(key))
        {
            EventCenter.GetInstance().EventTrigger("某键抬起", key);
        }
    }

    private void MyUpdate()
    {
        if (!isStart) return;

        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.D);
    }

}