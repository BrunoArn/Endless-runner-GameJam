using Unity.Cinemachine;
using UnityEngine;


public class CameraFollowX : CinemachineExtension
{
    [SerializeField] private float offsetX = 5f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.RawPosition;
            Vector3 targetPos = vcam.Follow != null ? vcam.Follow.position : pos;

            // Only follow X, keep Y and Z from the current camera position
            pos.x = targetPos.x + offsetX;
            state.RawPosition = pos;
        }
    }
}
