using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerBehavior : MonoBehaviour
{

    private float _horizontalInput;
    private float _verticalInput;
    private float _speed;
    private float _mouseScrollInput;
    private float CAMERA_ZOOM_SENSITIVITY = 30f;
    private Vector2 _cameraRotation = Vector2.zero;
    private Vector3 _cameraPivotPoint = new Vector3(0, 1, 0);
    [SerializeField]
    private float CAMERA_ROTATION_SENSITIVITY = 300f;
    private const float MAX_FOV = 120;
    private const float MIN_FOV = 20;
    private const float PLAYER_HORIZONTAL_SPEED = 2f;
    private const float PLAYER_JUMP_SPEED = 6f;
    private const string GROUND_TAG = "Ground";
    private Rigidbody _rigidBody;
    private Collider _collider;
    private Camera _playerCamera;
    private GameObject _playerCameraControlPoint;
    private GameObject _characterModel;
    private Animator _characterModelAnimator;
    [SerializeField]
    private Animation _runningAnimation;
    [SerializeField]
    private Animation _jumpingAnimation;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _playerCameraControlPoint = transform.Find("Camera Control Point").gameObject;
        _playerCamera = _playerCameraControlPoint.transform.Find("Main Camera").GetComponent<Camera>();
        _characterModel = transform.Find("KyleRobot").gameObject;
        _characterModelAnimator = _characterModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        gameObject.transform.position += PLAYER_HORIZONTAL_SPEED * Time.deltaTime * (transform.forward * _verticalInput + transform.right * _horizontalInput);
        _speed = new Vector3(_horizontalInput, 0, _verticalInput).magnitude;


        // Vérifie si le joueur est au sol
        bool isOnGround = OnGround();

        // Mise à jour des paramètres d'animation
        _characterModelAnimator.SetFloat("Speed", _speed);
        _characterModelAnimator.SetBool("OnGround", isOnGround);


        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _rigidBody.velocity = Vector3.up * PLAYER_JUMP_SPEED;
        }


        // Gestion de l'orientation du personnage
        if (_speed > 0 && isOnGround)
        {
            Vector3 direction = _horizontalInput * _playerCamera.transform.right +
                                _verticalInput * _playerCamera.transform.forward;
            _characterModel.transform.forward = direction;
        }


        // jump movement
        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            _rigidBody.velocity = Vector3.up * PLAYER_JUMP_SPEED;
        }

        // Camera Transform

        // Zoom
        _mouseScrollInput = Input.mouseScrollDelta.y * Time.deltaTime;
        if (_mouseScrollInput != 0) { 
            _playerCamera.fieldOfView = Mathf.Clamp(_playerCamera.fieldOfView + _mouseScrollInput * CAMERA_ZOOM_SENSITIVITY, MIN_FOV, MAX_FOV);
        }

        // Rotate camera and player
        if(Input.GetKey(KeyCode.Mouse0)){
            _cameraRotation += CAMERA_ROTATION_SENSITIVITY * Time.deltaTime * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            transform.localRotation = Quaternion.Euler(0, _cameraRotation.x, 0);
            _playerCameraControlPoint.transform.localRotation = Quaternion.Euler(-_cameraRotation.y, 0, 0);
        }

        // Rotate only camera
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _cameraRotation += CAMERA_ROTATION_SENSITIVITY * Time.deltaTime * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            _playerCameraControlPoint.transform.localRotation = Quaternion.Euler(-_cameraRotation.y, _cameraRotation.x, 0);
        }

    }


    bool OnGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin: transform.position + Vector3.up * 0.05f, direction: Vector3.down, maxDistance: (float)_collider.bounds.extents.y + 0.3f, hitInfo: out hit))
        {
            return hit.transform.CompareTag(GROUND_TAG);
        }
        return false;
    }
}
