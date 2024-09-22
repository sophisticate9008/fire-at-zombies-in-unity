using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    // 存储所有子对象的 Animator
    private List<Animator> childAnimators = new List<Animator>();
    
    // Start is called before the first frame update
    void Start()
    {
        // 获取该对象下的所有子对象，并找到其中的 Animator 组件
        foreach (Transform child in transform)
        {
            Animator anim = child.GetComponent<Animator>();
            Button btn = child.GetComponent<Button>();
            
            // 如果子对象有 Animator 和 Button 组件
            if (anim != null && btn != null)
            {
                // 添加到列表
                childAnimators.Add(anim);
                // 关闭初始的动画
                anim.enabled = false;

                // 为每个按钮添加点击事件
                btn.onClick.AddListener(() => OnButtonClick(anim));
            }
        }
    }

    // 当按钮被点击时执行的逻辑
    void OnButtonClick(Animator clickedAnimator)
    {
        // 遍历所有子对象，关闭其他动画，只开启点击的那个动画
        foreach (Animator anim in childAnimators)
        {
            if (anim == clickedAnimator)
            {
                anim.enabled = true; // 开启点击的动画
            }
            else
            {
                anim.Rebind();
                anim.enabled = false; // 关闭其他动画
            }
        }
    }
}
