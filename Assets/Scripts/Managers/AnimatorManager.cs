using System;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private static AnimatorManager _instance;

    public static AnimatorManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    // 执行在主线程的包裹方法

    // 播放动画anim(速度为speed)
    public void PlayAnim(Animator anim, float speed)
    {

        if (anim != null)
        {
            anim.enabled = true;
            anim.speed = 1;
        }

    }

    // 暂停动画anim
    public void StopAnim(Animator anim)
    {


        if (anim != null)
        {
            anim.speed = 0;
        }

    }
}
