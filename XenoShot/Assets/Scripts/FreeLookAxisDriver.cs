using UnityEngine;
using Cinemachine;
 
[RequireComponent(typeof(CinemachineFreeLook)), DisallowMultipleComponent]
public class FreeLookAxisDriver : MonoBehaviour
{
    public CinemachineInputAxisDriver xAxis;
    public CinemachineInputAxisDriver yAxis;

    float rotX, rotY;
    private Transform cam;
 
    private CinemachineFreeLook freeLook;

    public bool lockedTarget;

    private void Awake()
    {
        freeLook = GetComponent<CinemachineFreeLook>();
        freeLook.m_XAxis.m_MaxSpeed = freeLook.m_XAxis.m_AccelTime = freeLook.m_XAxis.m_DecelTime = 0;
        freeLook.m_XAxis.m_InputAxisName = string.Empty;
        freeLook.m_XAxis.m_InvertInput = true;
        freeLook.m_YAxis.m_MaxSpeed = freeLook.m_YAxis.m_AccelTime = freeLook.m_YAxis.m_DecelTime = 0;
        freeLook.m_YAxis.m_InputAxisName = string.Empty;
        freeLook.m_YAxis.m_InvertInput = true;
        cam = Camera.main.transform;
    }
 
    private void OnValidate()
    {
        xAxis.Validate();
        yAxis.Validate();
    }
 
    private void Reset()
    {
        xAxis = new CinemachineInputAxisDriver
        {
            multiplier = 10f,
            accelTime = 0.1f,
            decelTime = 0.1f,
            name = "Mouse X",
        };
        yAxis = new CinemachineInputAxisDriver
        {
            multiplier = -0.1f,
            accelTime = 0.1f,
            decelTime = 0.1f,
            name = "Mouse Y",

        };
    }
 
    private void Update()
    {
        if (!lockedTarget)
        {
            CamRotation();
        }
        else
        {
            LookAtTarget();
        }
    }

    private void CamRotation()
    {
        bool changed = yAxis.Update(Time.deltaTime, ref freeLook.m_YAxis);
        changed |= xAxis.Update(Time.deltaTime, ref freeLook.m_XAxis);
        if (changed)
        {
            freeLook.m_RecenterToTargetHeading.CancelRecentering();
            freeLook.m_YAxisRecentering.CancelRecentering();
        }
    }
    private void LookAtTarget()
    {
        transform.rotation = cam.rotation;
        Vector3 r = cam.eulerAngles;
        rotX = r.y;
        rotY = 1.8f;
    }
}