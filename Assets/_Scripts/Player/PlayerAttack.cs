using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    protected Animator anim;
    protected PlayerMovement playerMovement;
    [SerializeField] protected float attackCoolDown = 0.2f;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject[] fireballs;


    protected float cooldowntimer = Mathf.Infinity;
    private void Awake()
    {
        this.LoadAnim();
        this.LoadPlayerMovement();
        this.LoadFirepoint();
    }

    private void Update()
    {
        this.Attack();       
    }

    protected virtual void LoadAnim()
    {
        this.anim = GetComponent<Animator>();
    }
    protected virtual void LoadPlayerMovement()
    {
        this.playerMovement = GetComponent<PlayerMovement>();
    }
    protected virtual void LoadFirepoint()
    {
        this.firePoint = transform.Find("Firepoint");
    }

    protected virtual void Attack()
    {
        if (Input.GetMouseButton(0) && this.cooldowntimer > this.attackCoolDown && this.playerMovement.CanAttack())
        {
            this.anim.SetTrigger("IsAttack");
            this.cooldowntimer = 0;

            this.FireballAction();            
        }

        this.cooldowntimer += Time.deltaTime;   
    }

    protected virtual void FireballAction()
    {
        this.fireballs[FindFireball()].transform.position = this.firePoint.transform.position;
        this.fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    protected virtual int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
