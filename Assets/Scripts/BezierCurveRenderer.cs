using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurveRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    public Transform point0;
    public Transform point1;
    public Transform point2;

    public Transform point3;

    [Range(0.01f, 0.5f)]
    public float resolution = 0.02f; 
    public bool isCubic = false; 

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (isCubic)
        {
            DrawCubicBezierCurve();
        }
        else
        {
            DrawQuadraticBezierCurve();
        }
    }

    public void DrawQuadraticBezierCurve()
    {
        int pointsCount = Mathf.FloorToInt(1f / resolution) + 1;
        _lineRenderer.positionCount = pointsCount;

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i * resolution;
            _lineRenderer.SetPosition(i, CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position));
        }
    }

    public void DrawCubicBezierCurve()
    {
        int pointsCount = Mathf.FloorToInt(1f / resolution) + 1;
        _lineRenderer.positionCount = pointsCount;

        for (int i = 0; i < pointsCount; i++)
        {
            float t = i * resolution;
            _lineRenderer.SetPosition(i, CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position));
        }
    }

    public Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    public Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return u * u * u * p0 + 3 * u * u * t * p1 + 3 * u * t * t * p2 + t * t * t * p3;
    }
}
