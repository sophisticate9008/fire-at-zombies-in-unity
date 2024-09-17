using UnityEngine;

public class ReboundComponent : ComponentBase, IReboundable
{
    private int reboundCount = 3;
    private float edgeBuffer = 0.01f; //设置屏幕边缘的缓冲区，当接近该缓冲区时会反弹
    public ReboundComponent(string componentName, string type, GameObject selfObj) : base(componentName, type, selfObj)
    {
    }

    public int ReboundCount { get => reboundCount; set => reboundCount = value; }

    public void Rebound()
    {
        if (reboundCount <= 0) return;  // 反弹次数用完则不再反弹

        Vector3 position = SelfObj.transform.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);
        Vector2 direction = SelfObj.GetComponent<IArmChild>().Direction;

        bool rebounded = false;

        // 检查是否接近屏幕边缘并反转方向
        if (viewportPos.x < edgeBuffer || viewportPos.x > (1 - edgeBuffer))
        {
            SelfObj.GetComponent<IArmChild>().Direction = new Vector2(-direction.x, direction.y);
            rebounded = true;
        }
        if (viewportPos.y < edgeBuffer || viewportPos.y > (1 - edgeBuffer))
        {
            SelfObj.GetComponent<IArmChild>().Direction = new Vector2(direction.x, -direction.y);
            rebounded = true;
        }

        // 如果发生了反弹，则减少反弹次数
        if (rebounded)
        {
            reboundCount--;
        }
    }

    public override void TriggerExec(GameObject enemyObj)
    {
        Rebound();
    }
}