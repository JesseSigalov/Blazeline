using UnityEngine;

// GLOBAL SETTINGS

[CreateAssetMenu()]
public class Settings : ScriptableObject
{
    [Header("Player Data")]
    public float horizontalSpeed = 10f;
    public float jumpForce = 12f;
    public float groundDetectionRadius = 0.25f;
    public float defaultGravityScale = 3f;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 8f;
    public LayerMask groundLayer;

    [Header("Rope")]
    public int segments = 35;
    public float segmentLength = 0.25f;
    public float lineWidth = 0.1f;
    public float shrinkSpeed = 1f;
    public int maxRadius = 10;

    [Header("Ice")]
    public float meltTime = 10f;

    [Header("TNT")]
    public float explosionTime = 1.5f;
    public float explosionRadius = 2.5f;

    [Header("Axe/Sword")]
    public float throwForce = 250f;

    [Header("Camera Settings")]
    [Range(0, 1)]public float smoothingAmount = 0.1f;


}
