using UnityEngine;

public class PlayerBunnyPickUp : MonoBehaviour
{
    bool TouchingBunny = false;
    bool HoldingBunny = false;
    GameObject bunny = null; // null means no object

    SpriteRenderer rend;

    [SerializeField] Vector3 BunnyHoldingOffset;

    void SetBunnyState(bool state) // state aka is bunny enabled
    {
        if (bunny == null) return;
        bunny.GetComponent<BunnyScript>().Holding = !state;
        bunny.GetComponent<Rigidbody2D>().simulated = state; // disable physics because its bugged when interacting with player too
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && TouchingBunny)
        {
            HoldingBunny = true;
            SetBunnyState(false);
        } else if(Input.GetKeyUp(KeyCode.LeftShift) && HoldingBunny)
        {
            HoldingBunny = false;
            SetBunnyState(true);
            if(!TouchingBunny)
            {
                bunny = null; // null only after we released
            }
        }
        if(HoldingBunny)
        {
            // in this case bare transform.position is player position
            bunny.transform.position = transform.position + BunnyHoldingOffset * (rend.flipX ? -1 : 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HoldingBunny) return;
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Bunny"))
        {
            TouchingBunny = true;
            bunny = obj;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bunny"))
        {
            TouchingBunny = false;
            if(!HoldingBunny) bunny = null;
        }
    }
}
