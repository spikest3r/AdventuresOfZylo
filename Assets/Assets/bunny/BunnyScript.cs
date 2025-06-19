using UnityEngine;

public class BunnyScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer renderer;
    private Animator animator;
    [SerializeField] float MinimalX, MaximalX;
    [SerializeField] bool ShouldFollowBounds = false;
    [SerializeField] LayerMask wallMask;
    [SerializeField] bool PerformWallCheck = false;

    public bool Holding = false; // used for level 1-3 and further

    public float DefaultSpeed = 0.1f;
    float Speed;
    private bool collided = false;

    private void Awake()
    {
        Speed = DefaultSpeed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        TargetTime = Random.Range(1f, 2f);
    }

    private float TargetTime;
    private float LastTime = 0;
    private bool running = false;

    void Flip()
    {
        Speed = -Speed;
        renderer.flipX = !renderer.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Holding)
            {
                if (running)
                {
                    running = false;
                    animator.SetBool("run", false);
                }
                return;
            }
            if(Time.time - LastTime >= TargetTime)
            {
                RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 1f, wallMask);
                Debug.DrawRay(rb.position, Vector2.down, Color.magenta);
                if (hit.collider == null)
                {
                    running = false;
                    return; // wait until landed if no ground
                }
                LastTime = Time.time;
                TargetTime = Random.Range(1f, 2f);
                running = !running;
                animator.SetBool("run", running);
                if(Random.Range(0f,1f) > .5f) {
                    Flip();
                }
            }
            if(running)
            {
                Vector2 newPos = rb.position + new Vector2(Speed, 0);
                rb.MovePosition(newPos);
                if (!PerformWallCheck) return;
                RaycastHit2D hit = Physics2D.Raycast(rb.position, new Vector2(Mathf.Sign(Speed), 0), 2f, wallMask);
                Debug.DrawRay(rb.position, new Vector2(Mathf.Sign(Speed), 0), Color.red);
                if(hit.collider != null)
                {
                    Flip();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bunny"))
        {
            Flip();
        }
    }
}
