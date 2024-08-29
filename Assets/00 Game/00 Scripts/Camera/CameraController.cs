using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;
    // cinemachine
    public float _cinemachineTargetYaw;
    public float _cinemachineTargetPitch;
    private Vector2 look = Vector2.zero;
    private const float _threshold = 0.01f;

    Quaternion yaw;
    Quaternion pitch;


    public InputSO inputSO;
    public PlayerController playerController;
    public PlayerMovement playerMovement;

    private void Update()
    {
        look.x = inputSO.look.y * 70f;
        look.y = -inputSO.look.x * 70f;

        //_cinemachineTargetPitch = quaternion.eulerAngles.x;

    }

    private void LateUpdate()
    {
        if (!inputSO.isLooking)
        {
            Quaternion quaternion;
            if (inputSO.buttonJump && playerController.rb.velocity.x != 0 && playerController.rb.velocity.y != 0)
            {
                quaternion = Quaternion.LookRotation(playerController.rb.velocity, Vector3.up);
            }
            else if (!inputSO.buttonJump && playerMovement.GetVelocity().x != 0 && playerMovement.GetVelocity().y != 0)
            {
                quaternion = Quaternion.LookRotation(playerMovement.GetVelocity(), Vector3.up);
            }
            else
            {
                quaternion = Quaternion.identity;
            }

            yaw = Quaternion.Slerp(yaw, playerController.transform.rotation, Time.fixedDeltaTime);
            pitch = Quaternion.Slerp(pitch, quaternion, Time.deltaTime * 3);

            _cinemachineTargetYaw = yaw.eulerAngles.y;
            _cinemachineTargetPitch = pitch.eulerAngles.x;
        }

        CameraRotation();
    }

    private void CameraRotation()
    {
        if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            float deltaTimeMultiplier = Time.deltaTime;

            _cinemachineTargetYaw += look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += look.y * deltaTimeMultiplier;
        }

        if (!inputSO.isLooking)
        {
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, float.MinValue, float.MaxValue);
        }
        else
        {
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
        }

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
