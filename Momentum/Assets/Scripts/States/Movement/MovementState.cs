using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    public new MovementSM sm;
    public MovementState(MovementSM sm) : base(sm)
    {
        this.sm = sm;
    }

    static public Quaternion targetRotation = Quaternion.identity, camRotation = Quaternion.identity;
    static Vector3 currentNormal = Vector3.up, surfaceNormal = Vector3.up, rotateVelocity = Vector3.zero;
    static float smooth = 0.1f;

    public static bool canJump = true;
    protected float acceleration;
    protected float maxSpeed;
    protected float currentSpeed;

    public Collider collider;

    public override void Enter(InputFrame input, GameObject obj)
    {
        base.Enter(input, obj);
        collider = obj.GetComponent<Collider>();
    }

    public override void Update(InputFrame input, GameObject obj)
    {
        base.Update(input, obj);

        surfaceNormal = CalculateSurfaceNormal();

        Vector3 camForward = Camera.main.gameObject.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        camRotation = Quaternion.LookRotation(camForward, Vector3.up);
        targetRotation = Quaternion.FromToRotation(Vector3.up, currentNormal.normalized);

        currentSpeed = rigid.velocity.magnitude;

        if (input.JumpPress && canJump)
        {
            sm.NextState(new JumpState(sm));
            return;
        }
    }

    public override void FixedUpdate(InputFrame input, GameObject obj)
    {
        base.FixedUpdate(input, obj);

        currentNormal = Vector3.SmoothDamp(currentNormal, surfaceNormal, ref rotateVelocity, smooth, 100f, Time.fixedDeltaTime);

        obj.transform.rotation = targetRotation * camRotation;

        if (!input.JumpHold && !isGrounded)
        {
            rc.AddForce(new Vector3(0f, Physics.gravity.y, 0f));
        }
    }

    public override void OnGroundEnter(InputFrame input, GameObject obj)
    {
        base.OnGroundEnter(input, obj);

        canJump = true;
    }

    private Vector3 CalculateBottom()
    {
        Vector3 bottom = collider.bounds.center;
        bottom.y -= collider.bounds.size.y / 2f;
        bottom = targetRotation * bottom;
        return bottom;
    }

    private Vector3 CalculateSurfaceNormal()
    {
        float radius = 7f;

        Vector3 origin = collider.bounds.center;

        Collider[] colliders = Physics.OverlapSphere(origin, radius, sm.collisionMask, QueryTriggerInteraction.Ignore);
        Vector3 surfaceNormal = Vector3.up;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider c = colliders[i];
            Vector3 closestPoint = c.ClosestPoint(origin), direction = (closestPoint - origin);


            RaycastHit hit;
            Ray ray = new Ray(origin, direction);

            if (Physics.Raycast(ray, out hit, radius, sm.collisionMask, QueryTriggerInteraction.Ignore))
            {
                surfaceNormal += hit.normal * (1f - (direction.magnitude / radius));
            }
        }

        return surfaceNormal;
    }

    public void AddMomemtum(InputFrame input, GameObject obj)
    {
        Vector3 force = targetRotation * new Vector3(input.x, 0f, input.y) * acceleration;

        if (currentSpeed > maxSpeed && Vector3.Dot(force, rigid.velocity) > 0f)
        {
            force = Vector3.Project(force, Vector3.Cross(obj.transform.up, rigid.velocity));
        }

        rc.AddForce(force);
    }
}
