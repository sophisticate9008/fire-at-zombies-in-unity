using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// 为 Transform 扩展的递归查找方法，查找指定名称的子物体。
    /// </summary>
    /// <param name="parent">要在其下查找的父物体的 Transform。</param>
    /// <param name="name">要查找的子物体的名称。</param>
    /// <returns>如果找到匹配名称的 Transform，则返回该 Transform；否则返回 null。</returns>
    public static Transform RecursiveFind(this Transform parent, string name)
    {
        // 如果当前物体的名称匹配，返回当前 Transform
        if (parent.name == name)
            return parent;

        // 遍历当前物体的所有子物体
        foreach (Transform child in parent)
        {
            // 递归调用，在每个子物体下继续查找
            Transform result = RecursiveFind(child, name);
            // 如果找到匹配的 Transform，立即返回
            if (result != null)
            {
                return result;
            }


        }

        // 如果在所有子物体中都未找到，返回 null
        return null;
    }
    /// <summary>
    /// 复制原 RectTransform 的布局属性到新 RectTransform 上
    /// </summary>
    /// <param name="newTransform">新创建的 Transform（目标 Transform）</param>
    /// <param name="originalTransform">原始 Transform（源 Transform）</param>
    public static void CopyRectTransform(this Transform newTransform, Transform originalTransform)
    {
        RectTransform originalRectTransform = originalTransform as RectTransform;
        RectTransform newRectTransform = newTransform as RectTransform;

        if (originalRectTransform != null && newRectTransform != null)
        {
            newRectTransform.anchorMin = originalRectTransform.anchorMin;
            newRectTransform.anchorMax = originalRectTransform.anchorMax;
            newRectTransform.pivot = originalRectTransform.pivot;
            newRectTransform.sizeDelta = originalRectTransform.sizeDelta;
            newRectTransform.localPosition = originalRectTransform.localPosition;
            newRectTransform.localScale = originalRectTransform.localScale;
            newRectTransform.localRotation = originalRectTransform.localRotation;
        }
        else
        {
            Debug.LogWarning("Both original and new Transform must be RectTransforms.");
        }
    }
}
