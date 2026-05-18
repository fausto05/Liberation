using Unity.Cinemachine;
using UnityEngine;

public class DynamicCameraOffset : MonoBehaviour
{
    [Header("Target")]
    public Rigidbody2D playerRb;

    [Header("Offset")]
    public float lookAheadAmount = 2f;
    public float smoothSpeed = 5f;

    private CinemachinePositionComposer composer;

    private Vector2 currentOffset;

    void Start()
    {
        composer = GetComponent<CinemachineCamera>()
            .GetComponent<CinemachinePositionComposer>();
    }

    void Update()
    {
        Vector2 velocity = playerRb.linearVelocity;

        Vector2 targetOffset = Vector2.zero;

        if (velocity.magnitude > 0.1f)
        {
            targetOffset = velocity.normalized * lookAheadAmount;
        }

        currentOffset = Vector2.Lerp(
            currentOffset,
            targetOffset,
            smoothSpeed * Time.deltaTime
        );

        composer.TargetOffset = new Vector3(
            currentOffset.x,
            currentOffset.y,
            0
        );
    }
}
