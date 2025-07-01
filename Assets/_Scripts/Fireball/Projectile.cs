using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected BoxCollider2D boxCollider;
    protected Animator anim;
    protected Rigidbody2D rb;

    protected bool hit;
    protected float direction;
    protected float lifeTime;
    [SerializeField] protected float speed = 5;

    private void Awake()
    {
        this.LoadCollider();
        this.LoadAnim();
    }

    private void Update()
    {
        this.FireballMovement();
        this.SelfDestroy();
    }

    protected virtual void LoadCollider()
    {
        this.boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void LoadAnim()
    {
        this.anim = GetComponent<Animator>();
    }

    //move the fireball forward
    protected virtual void FireballMovement()
    {
        if (hit) return;
        float movementSpeed = this.speed * Time.deltaTime * this.direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    // check if the fireball hit something
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        this.hit = true;
        this.boxCollider.enabled = false;
        //Debug.Log("Explosion Trigger Called");
        this.anim.SetTrigger("IsExplode");
    }
    public void SetDirection (float _direction)
    {
        this.lifeTime = 0;
        this.direction = _direction;
        gameObject.SetActive(true);
        this.hit = false;
        this.boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    protected virtual void Deactivate()
    {
        gameObject.SetActive(false);
    }

    protected virtual void SelfDestroy()
    {
        this.lifeTime += Time.deltaTime;
        if (this.lifeTime > 5)
            gameObject.SetActive(false);
    }
}
