using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Animator anim;
    protected BoxCollider2D boxCollider;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float jumpSpeed = 7f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;


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
        float horizontalInput = Input.GetAxis("Horizontal");
        this.body.velocity = new Vector2(horizontalInput * this.speed, this.body.velocity.y);
                       
        //This is for flipping player when moving
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        //This is for jumping
        if (Input.GetKey(KeyCode.Space))
            this.Jump();

        //Add animation for running movement
        if (horizontalInput != 0)
            this.anim.SetBool("IsRun", true);
        else
            this.anim.SetBool("IsRun", false);

        //Add animation for Jumping when press jump or falling
            this.anim.SetBool("IsGrounded", this.IsGrounded());





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
