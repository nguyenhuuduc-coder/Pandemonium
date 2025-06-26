using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Animator anim;
    protected BoxCollider2D boxCollider;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float jumpSpeed = 20f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    protected float wallJumpCooldown;
    protected float horizontalInput;


    private void Awake()
    {
        this.LoadRb();
        this.LoadAnim();
        this.LoadCollider();
    }

    private void Update()
    {
        this.Movement();
    }

    protected virtual void LoadRb()
    {
        this.body = GetComponent<Rigidbody2D>();
    }
    protected virtual void LoadAnim()
    {
        this.anim = GetComponent<Animator>();
    }
    protected virtual void LoadCollider()
    {
        this.boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Movement()
    {
        //basic left right movement
        this.horizontalInput = Input.GetAxis("Horizontal");        
                       
        //This is for flipping player when moving
        if (this.horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (this.horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        //Add animation for running movement
        if (this.horizontalInput != 0)
            this.anim.SetBool("IsRun", true);
        else
            this.anim.SetBool("IsRun", false);

        //Add animation for Jumping when press jump or falling
            this.anim.SetBool("IsGrounded", this.IsGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            this.body.velocity = new Vector2(this.horizontalInput * this.speed, this.body.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7f;

            //This is for jumping
            if (Input.GetKey(KeyCode.Space))
                this.Jump();
        }
        else
            this.wallJumpCooldown += Time.deltaTime;
    }

    protected virtual void Jump()
    {
        if(this.IsGrounded())
        {
            this.body.velocity = new Vector2(this.body.velocity.x, this.jumpSpeed);
            this.anim.SetTrigger("IsJump");
        }
        else if(this.OnWall() && !this.IsGrounded())
        {
            if(this.horizontalInput == 0)
            {
                this.body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10f, 0);
                this.body.transform.localScale = new Vector3(-Mathf.Sign(body.transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                this.body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3f, 6f);

            this.wallJumpCooldown = 0;

        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {

    }

    protected virtual bool IsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, this.groundLayer);        
        return rayCastHit.collider != null;
    }

    protected virtual bool OnWall()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, this.wallLayer);
        return rayCastHit.collider != null;
    }
}
