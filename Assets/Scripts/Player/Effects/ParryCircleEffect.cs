using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ParryCircleEffect : MonoBehaviour
{
    public int segments = 64;
    public float radius = 1.5f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        Color skyBlue = new Color(0.5f, 0.8f, 1f, 1f);
        lineRenderer.startColor = skyBlue;
        lineRenderer.endColor = skyBlue;

        DrawCircle(radius);
    }

    void DrawCircle(float radius)
    {
        for (int i = 0; i <= segments; i++)
        {
            float angle = 2f * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
    private IEnumerator VanishAfterTime(float vanishTime)
    {
        yield return new WaitForSeconds(vanishTime);
        Destroy(gameObject);
    }

    public void SetDeleteTime(float vanishTime)
    {
        StartCoroutine(VanishAfterTime(vanishTime));
    }
}
