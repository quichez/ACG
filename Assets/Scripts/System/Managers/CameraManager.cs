using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Transform cameraHolder;
    public Camera mainCamera;

    Transform _target;
    InputMaster _inputs;

    [Min(1.0f)] public float zoomSpeed = 5.0f;
    [Min(1.0f)] public float zoomSmoothSpeed = 3.0f;
    [Range(-5.0f,1.0f)] public float minZoom = -2.0f;
    [Range(4.0f,10.0f)] public float maxZoom = 5.0f;
    float zoomAmount = 0.0f;

    void Awake()
    {
        Instance = this;
        _inputs = new InputMaster();
    }

    private void OnEnable()
    {
        _inputs.MapNavigation.Zoom.performed += Zoom_performed;
        _inputs.MapNavigation.Enable();
    }
    
    private void Zoom_performed(InputAction.CallbackContext obj)
    {
        zoomAmount = Mathf.Clamp(zoomAmount + obj.ReadValue<float>() * Time.deltaTime * zoomSpeed, minZoom, maxZoom);
        Vector3 zoomedCameraPos = Vector3.forward * zoomAmount;
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, zoomedCameraPos, zoomSmoothSpeed * Time.deltaTime);
    }

    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnDisable()
    {
        _inputs.MapNavigation.Zoom.performed -= Zoom_performed;
        _inputs.MapNavigation.Disable();
    }
    public IEnumerator MoveCamera(Transform targt)
    {
        _target = targt;
        Vector3 tgt = new Vector3(_target.transform.position.x, cameraHolder.transform.position.y, _target.position.z - 5.0f);
        while(!Mathf.Approximately(cameraHolder.transform.position.x, _target.transform.position.x))
        {
            yield return new WaitForEndOfFrame();

            cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position , tgt, 3.0f*Time.deltaTime);
        }
        

    }
}
