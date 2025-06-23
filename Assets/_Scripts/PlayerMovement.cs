using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    protected Rigidbody2D body;
    protected Animator anim;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float jumpSpeed = 10f;
    protected bool isGounded;


    private void Awake()
    {
        LoadRb();
        LoadAnim();
    }

    private void Update()
    {
        Movement();
    }

    protected virtual void LoadRb()
    {
        this.body = GetComponent<Rigidbody2D>();
    }
    protected virtual void LoadAnim()
    {
        this.anim = GetComponent<Animator>();
    }

    protected virtual void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        this.body.velocity = new Vector2(horizontalInput * this.speed, this.body.velocity.y);
                       
        //This is for flipping player when moving
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1f, 1, 1);

        //This is for jumping
        if (Input.GetKey(KeyCode.Space) && isGounded == true)
            this.Jump();

        //Add animation for running movement
        if (horizontalInput != 0)
            this.anim.SetBool("IsRun", true);
        else
            this.anim.SetBool("IsRun", false);

        //Add animation for Jumping when press jump or falling
            this.anim.SetBool("IsGrounded", isGounded);


    }

    protected virtual void Jump()
    {
        this.body.velocity = new Vector2(this.body.velocity.x, this.jumpSpeed);
        this.anim.SetTrigger("IsJump");
        this.isGounded = false;
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            this.isGounded = true;
        }
    }
}
