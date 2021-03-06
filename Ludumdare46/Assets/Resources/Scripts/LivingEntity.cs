﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float health;
    public Vector3 direction;

    protected SpriteRenderer sr;

    public bool flip = false;
    public Animator anim;

    public float hitstun;

    //public Alignment alignment = Alignment.NEUTRAL;

    public virtual void Awake()
    {
        
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        GetDirectionalInput();
        if (hitstun > 0)
        {
            hitstun -= Time.deltaTime;
            sr.material.color = sr.material.color == Color.red ? Color.blue : Color.red;
            return;
        }

        sr.material.color = Color.white;
        RotateSelf();
        Attack();
    }

    public virtual void FixedUpdate()
    {
        if (hitstun <= 0) Move();
    }

    public abstract void Move();

    public virtual void Attack()
    {
        if (IsAttacking())
        {
            //if (/*weaponHolder.primary.IsCD() ||*/ weaponHolder.primary.attacking) return;
            //if (!weaponHolder.primary.IsCD()) weaponHolder.primary.Attack();
            //if (weaponHolder.primary.melee) // problematic, will swing even when you're on cooldown
            //{
            //    weaponHolder.primary.attacking = true; // currently shoots at gun end rather than from middle
            //    weaponHolder.primary.hitbox.enabled = true;
            //    float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //    int f = !flip ? 1 : -1;
            //    weaponEndRotation = Quaternion.AngleAxis(angle + f * (weaponHolder.primary.swingRadius / 2), -this.transform.forward);
            //    weaponHolder.transform.rotation = Quaternion.AngleAxis(angle - f * (weaponHolder.primary.swingRadius / 2), -this.transform.forward);
            //}
        }
    }

    public abstract bool IsAttacking();

    void RotateSelf()
    {
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if ((angle > 0 && angle < 90) || (angle > -90 && angle < 0))
        {
            if (flip)
            {
                flip = !flip;
                sr.flipX = !sr.flipX;
            }
        }
        else
        {
            if (!flip)
            {
                flip = !flip;
                sr.flipX = !sr.flipX;
            }
        }

    }

    public abstract void GetDirectionalInput();

    //public virtual void RotateWeapon()
    //{
    //    float weaponAngle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    weaponHolder.transform.rotation = Quaternion.AngleAxis(weaponAngle, -this.transform.forward);
    //    //weaponHolder.rb.MoveRotation(weaponAngle); // j code
    //    if (weaponAngle > 0)
    //    {

    //        weaponHolder.weaponSR.sortingOrder = sr.sortingOrder + 1;
    //    }
    //    else
    //    {

    //        weaponHolder.weaponSR.sortingOrder = sr.sortingOrder - 1;
    //    }//if (this is Enemy) Debug.Log(this.gameObject.name + " wp flip " + ((weaponAngle > 0 && weaponAngle < 90) || (weaponAngle > -90 && weaponAngle < 0)));
    //    //Debug.Log(weaponAngle);
    //    if ((weaponAngle > 0 && weaponAngle < 90) || (weaponAngle > -90 && weaponAngle < 0))
    //    {
    //        weaponHolder.weaponSR.flipY = false;
    //        sr.flipX = false;
    //    }
    //    else
    //    {
    //        weaponHolder.weaponSR.flipY = true;
    //        sr.flipX = true;
    //    }
    //}
}