using Unity.Cinemachine;
using UnityEngine;

public class CameraFollowX : CinemachineExtension
{
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
            pos.x = targetPos.x;
            state.RawPosition = pos;
        }
    }
}
