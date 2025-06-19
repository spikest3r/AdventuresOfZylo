using UnityEngine;

public class OpossumScript : MonoBehaviour
{
    public float X1, X2;
    public float Speed = 0.1f;
    private float direction = -1f;
    private bool flip = false;
    private SpriteRenderer renderer;
    private Animator animator;

    private float X()
    {
        return transform.position.x;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("Kill") && Time.timeScale == 1)
        {
            if (X() <= X1 || X() >= X2)
            {
                direction = direction == 1f ? -1f : 1f;
                flip = !flip;
                renderer.flipX = flip;
            }
            transform.position += new Vector3(Speed * direction * Time.deltaTime, 0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            direction = direction == 1f ? -1f : 1f;
            flip = !flip;
            renderer.flipX = flip;
            PlayerScript script = collision.gameObject.GetComponent<PlayerScript>();
            script.Health -= script.HeartScore; // harm one heart
        }
    }
}
