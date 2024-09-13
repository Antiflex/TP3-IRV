using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBehavior : MonoBehaviour
{
    private Light _light;
    private float _time;
    [SerializeField]
    private float _dayNightCycleTime = 120f;
    private const float MAX_UP_ANGLE = 0f;
    private const float MAX_DOWN_ANGLE = -60f;
    // Start is called before the first frame update
    void Start()
    {
       _light = GetComponent<Light>();
       _time = 0f;
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        transform.Rotate(Vector3.up, Time.deltaTime * 180 / _dayNightCycleTime);
        transform.Rotate(Vector3.right, Time.deltaTime * 180 / _dayNightCycleTime);
        _light.intensity = Mathf.Max(Mathf.Sin(_time/_dayNightCycleTime * Mathf.PI), 0);
    }
}
