using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                         _mainCamera.transform.rotation * Vector3.up);
    }
}
