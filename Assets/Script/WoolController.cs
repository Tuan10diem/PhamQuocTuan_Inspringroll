using UnityEngine;

public class WoolController : Subjects
{

    private Collider2D other;
    public Transform last;
    private Collider2D coll;

    public float jumpForce = 5f;
    public float gravity = 1f;
    public Rigidbody2D _rigidbody;
    private int health = 3;

    public SpriteRenderer _spriteRenderer;

    RaycastHit2D isBasket;
    private int layerMask = ~(1 << 3);

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        isBasket = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, layerMask);
        coll= this.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        isBasket = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, layerMask);
        //Debug.Log(_rigidbody.velocity);
        if (_rigidbody.velocity.y > 0)
        {
            _spriteRenderer.sortingOrder = 2;
            coll.enabled = false;
        }
        else
        {
            coll.enabled = true;
        }

        checkOver();
        if (this.transform.position.y - last.position.y <=5f && isBasket && isBasket.collider.CompareTag("basket") && Input.GetMouseButtonDown(0))
        {
            Jumping();
        }
        
        if (coll.enabled && isBasket && isBasket.collider.CompareTag("basket"))
        {
            _rigidbody.gravityScale = 0;
            FollowBasket(other);
        }
        else
        {
            _rigidbody.gravityScale = gravity;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.CompareTag("basket"))
        {
            other = collision;
            if (other.transform == last)
            {
                _rigidbody.gravityScale = 0;
                Debug.Log("Missed since other==last");
                Debug.Log(last.name + coll.enabled);
                Missed();
            }
            else
            {
                //last = other.transform;
                Debug.Log("Catched ");
                NotifyObservers(GameAction.catched);

            }
        }
        
    }

    private void Missed()
    {
        Debug.Log(health);
        if (health > 0)
        {
            NotifyObservers(GameAction.missed);
            this.transform.position = last.transform.position;
            _rigidbody.gravityScale = 0;
            health--;
        }
        else
        {
            NotifyObservers(GameAction.gameover);
        }
        
    }

    void Jumping()
    {
        last = other.transform;
        other = null;
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
    }

    private void FollowBasket(Collider2D collision)
    {
        if (collision == null) return;
        _spriteRenderer.sortingOrder = 0;
        this.transform.position = collision.transform.position+new Vector3(0,0.7f,0);
    }

    private void Rotating()
    {
        transform.Rotate(new Vector3(0,0,1) * 150f * Time.deltaTime);
    }

    void checkOver()
    {
        if (this.transform.position.y < last.position.y)
        {
            this.transform.position = last.transform.position;
            _rigidbody.gravityScale = 0;
        }
    }
}
