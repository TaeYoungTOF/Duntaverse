using UnityEngine;

public class Plane : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D _rigidbody = null;

    [SerializeField] private float flapForce = 6f;
    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private bool isDead = false;
    private float deathCooldown = 0f;
    
    private bool isFlap = false;

    [SerializeField] private bool godMode = false;

    FlappyPlaneManager flappyPlaneManager;
    
    void Start()
    {
        flappyPlaneManager = FlappyPlaneManager.Instance;

        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogError("Not Founded Animator");
        }

        if (_rigidbody == null)
        {
            Debug.LogError("Not Founded Rigidbody");
        }
    }

    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                // 게임 재시작
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isDead)
            return;
        
        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }
        
        _rigidbody.velocity = velocity;
        
        float angle = Mathf.Clamp(_rigidbody.velocity.y * 10f, -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode)
            return;
            
	      if (isDead)
            return;

        animator.SetInteger("IsDie", 1);
        isDead = true;
        deathCooldown = 1f;
        flappyPlaneManager.GameOver();
    }
}