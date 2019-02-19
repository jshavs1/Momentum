using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    protected Rigidbody rigid;
    protected ProjectileAbilityProfile ability;
    public bool isLocal;

    float gravity = Mathf.Abs(Physics.gravity.y);
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (ability != null)
            rigid.AddForce(Vector3.down * gravity * (ability.gravityMultiplier - 1f));
    }

    public void Launch(ProjectileAbilityProfile ability, Vector3 vel)
    {
        this.ability = ability;
        rigid.velocity = vel;
    }

    public void Launch(ProjectileAbilityProfile ability, Vector3 vel, float gravity)
    {
        this.gravity = gravity;
        Launch(ability, vel);
    }
}
