using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DynamicLine : MonoBehaviour
{
    public float baseWidth = 0.1f;
    public float widthMultiplier = 0.01f;
    public float pulseSpeed = 2f;

    private Transform user;
    private Transform target;
    private LineRenderer line;
    private bool initialized = false;

    public void Init(Transform userTransform, Transform targetTransform)
    {
        if (userTransform == null || targetTransform == null)
        {
            Debug.LogError("[DynamicLine.Init] Null user or target Transform passed.");
            return;
        }

        user = userTransform;
        target = targetTransform;

        if (!TryGetComponent(out line))
        {
            Debug.LogError("[DynamicLine] Missing LineRenderer component.");
            return;
        }

        line.positionCount = 2;
        line.useWorldSpace = true;
        initialized = true;

        UpdateLine();
        Debug.Log($"[Init] Line created from {user.name} to {target.name}");
    }

    void Update()
    {
        if (!initialized || user == null || target == null || line == null)
            return;

        UpdateLine();
    }

    void UpdateLine()
    {
        Vector3 userPos = user.position;
        Vector3 targetPos = target.position;

        line.SetPosition(0, userPos);
        line.SetPosition(1, targetPos);

        float distance = Vector3.Distance(userPos, targetPos);
        float width = Mathf.Clamp(baseWidth + distance * widthMultiplier, 0.05f, 1f);
        line.startWidth = width;
        line.endWidth = width;

        if (line.material != null && line.material.HasProperty("_MainTex"))
        {
            line.material.mainTextureOffset = new Vector2(Time.time * pulseSpeed % 1, 0);
        }

        Debug.DrawLine(userPos, targetPos, Color.red);
    }
}
