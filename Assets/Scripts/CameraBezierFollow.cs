using UnityEngine;

public class CameraBezierFollow : MonoBehaviour
{
    public BezierCurveRenderer bezierCurve; 
    public float speed = 5f; 
    private float t = 0f;

    private void Start()
    {
        bezierCurve = GameObject.FindObjectOfType<BezierCurveRenderer>();
    }

    private void Update()
    {
        t += Time.deltaTime * speed * bezierCurve.resolution;

        t = Mathf.Clamp01(t);

        Vector3 newPosition;
        if (bezierCurve.isCubic)
        {
            newPosition = bezierCurve.CalculateCubicBezierPoint(t, bezierCurve.point0.position, bezierCurve.point1.position, bezierCurve.point2.position, bezierCurve.point3.position);
        }
        else
        {
            newPosition = bezierCurve.CalculateQuadraticBezierPoint(t, bezierCurve.point0.position, bezierCurve.point1.position, bezierCurve.point2.position);
        }

        transform.position = newPosition;

        if (t < 1f)
        {
            Vector3 targetPoint;
            if (bezierCurve.isCubic)
            {
                targetPoint = bezierCurve.CalculateCubicBezierPoint(t + 0.01f, bezierCurve.point0.position, bezierCurve.point1.position, bezierCurve.point2.position, bezierCurve.point3.position);
            }
            else
            {
                targetPoint = bezierCurve.CalculateQuadraticBezierPoint(t + 0.01f, bezierCurve.point0.position, bezierCurve.point1.position, bezierCurve.point2.position);
            }
            transform.LookAt(targetPoint);
        }
    }
}
