using System.Collections.Generic;
using UnityEngine;

public class WormSegments : MonoBehaviour
{
    [Header("Head & Segments")]
    [Tooltip("Head Transform (the object with PlayerMovement + Rigidbody2D)")]
    public Transform head;
    [Tooltip("Segment prefab (Sprite/Collider optional).")]
    public GameObject segmentPrefab;
    [Tooltip("How many segments to spawn.")]
    public int segmentCount = 10;

    [Header("Follow Tuning")]
    [Tooltip("Desired gap between consecutive segments.")]
    public float spacing = 0.25f;
    [Tooltip("Higher = snappier position follow (6–14 typical).")]
    public float followTightness = 10f;
    [Tooltip("Higher = faster rotation alignment (8–20 typical).")]
    public float rotationLerp = 12f;

    [Header("Execution")]
    [Tooltip("If true, updates in LateUpdate (good default). If false, uses FixedUpdate (set order after PlayerMovement).")]
    public bool runInLateUpdate = true;

    private readonly List<Transform> segments = new();
    private Vector3[] velocities; // per-segment SmoothDamp velocity

    private int orderInLayer = -1;

    void Awake()
    {
        if (!head || !segmentPrefab)
        {
            Debug.LogError("[WormChain2D] Assign head + segmentPrefab.");
            enabled = false;
            return;
        }

        // Spawn body segments at head start position
        for (int i = 0; i < segmentCount; i++)
        {
            var seg = Instantiate(segmentPrefab, head.position, head.rotation, transform).transform;
            if (seg.TryGetComponent<SpriteRenderer>(out var sprite))
            {
                sprite.sortingOrder = orderInLayer;
                orderInLayer--;
            }
            segments.Add(seg);
        }
        velocities = new Vector3[segments.Count];
    }

    void LateUpdate()
    {
        if (runInLateUpdate) FollowPass(Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (!runInLateUpdate) FollowPass(Time.fixedDeltaTime);
    }

    private void FollowPass(float dt)
    {
        if (segments.Count == 0) return;

        for (int i = 0; i < segments.Count; i++)
        {
            Transform leader = (i == 0) ? head : segments[i - 1];
            Transform seg = segments[i];

            // Desired position: a bit "behind" the leader along the segment->leader direction
            Vector3 toLeader = (Vector3)leader.position - seg.position;
            Vector3 backDir = toLeader.sqrMagnitude > 1e-6f ? toLeader.normalized : Vector3.right;

            // Place target at 'spacing' behind leader
            Vector3 desired = (Vector3)leader.position - backDir * spacing;

            // SmoothDamp toward desired
            float smoothTime = 1f / Mathf.Max(0.0001f, followTightness);
            seg.position = Vector3.SmoothDamp(seg.position, desired, ref velocities[i], smoothTime, Mathf.Infinity, dt);

            // Soft constraint to keep exact spacing (prevents bunching/overshoot)
            Vector3 corr = (Vector3)seg.position - (Vector3)leader.position;
            float d = corr.magnitude;
            if (d > 1e-6f)
            {
                seg.position = (Vector3)leader.position + corr.normalized * spacing;
            }

            // Lock to 2D plane (Z=0)
            seg.position = new Vector3(seg.position.x, seg.position.y, 0f);

            // Rotate to face the leader (2D)
            Vector2 look = (Vector2)leader.position - (Vector2)seg.position;
            if (look.sqrMagnitude > 1e-6f)
            {
                float ang = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.AngleAxis(ang, Vector3.forward);
                seg.rotation = Quaternion.Slerp(seg.rotation, targetRot, rotationLerp * dt);
            }
        }
    }
}
