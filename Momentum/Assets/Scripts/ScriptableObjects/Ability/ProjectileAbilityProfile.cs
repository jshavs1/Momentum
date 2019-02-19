using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Ability/Projectile")]
public class ProjectileAbilityProfile : AbilityProfile
{
    public GameObject projectilePrefab;
    public int ammo;
    public float speed = 1f;
    public float distance = 1f;
    public float gravityMultiplier = 1f;
    public bool firstShotDelay;

    protected override Type AbilityStateType { get { return typeof(ProjectileState); } }

    protected override void Initialize(AbilityState state)
    {
        ProjectileState projectileState = state as ProjectileState;
        projectileState.ability = this;
    }

    public int SolveBallisticArc(Vector3 pos, Vector3 target, out Vector3 sol)
    {
        sol = Vector3.zero;

        Vector3 diff = target - pos;
        Vector3 diffXZ = new Vector3(diff.x, 0f, diff.z);
        float groundDist = diffXZ.magnitude;

        float gravity = Mathf.Abs(Physics.gravity.y) * gravityMultiplier;
        float speed2 = speed*speed;
        float speed4 = speed*speed*speed*speed;
        float y = diff.y;
        float x = groundDist;
        float gx = gravity * x;

        float root = speed4 - gravity * (gravity * x * x + 2 * y * speed2);

        if (root < 0)
        {
            sol = diff.normalized * Mathf.Sqrt(2 * gravity * diff.y);
            return 0;
        }

        root = Mathf.Sqrt(root);

        float angle = Mathf.Atan2(speed2 - root, gx);
        if (float.IsNaN(angle))
            angle = Mathf.Atan2(speed2 + root, gx);

        Vector3 groundDir = diffXZ.normalized;
        sol = groundDir * Mathf.Cos(angle) * speed + Vector3.up * Mathf.Sin(angle) * speed;

        return 1;
    }
}
