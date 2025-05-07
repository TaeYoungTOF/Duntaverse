using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Job
    {
        None,
        Warrior,
        Archer,
        Wizard
    }
    
    public static Player Instance { get; private set; }

    [SerializeField] private string playerName;
    public string PlayerName { get => playerName ; set => playerName = value; }
    [SerializeField] private Job playerJob;
    public Job PlayerJob { get => playerJob ; set => playerJob = value; }
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D _rigidbody;
    private AnimationHandler animationHandler;
    private SpriteRenderer spriteRenderer;
    private Vector2 directionVector = Vector2.right;
    private Vector2 movementVector = Vector2.zero;
    private bool isHorizonDirection = true;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        InitPlayer();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!UIManager.Instance.IsMainPanelOpen) { return; }

        Move();
        spriteRenderer.flipX = directionVector.x < 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScanObject();
        }
    }

    private void FixedUpdate()
    {
        if (UIManager.Instance.IsMainPanelOpen) { _rigidbody.velocity = movementVector * speed; }
        else { _rigidbody.velocity = Vector2.zero; }

        
        if (UIManager.Instance.IsMainPanelOpen) { _rigidbody.velocity = movementVector * speed; }
        else { _rigidbody.velocity = Vector2.zero; }
    }

    private void InitPlayer()
    {
        playerName = "던삣삐";
        playerJob = Job.None;
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 플레이어의 대각선 움직임을 제한
        bool horizontalKeyDown = Input.GetButtonDown("Horizontal");
        bool horizontalKeyUp = Input.GetButtonUp("Horizontal");
        bool verticalKeyDown = Input.GetButtonDown("Vertical");
        bool verticalKeyUp = Input.GetButtonUp("Vertical");

        if (horizontalKeyDown) { isHorizonDirection = true; }
        else if (verticalKeyDown) { isHorizonDirection = false; }
        else if (horizontalKeyUp || verticalKeyUp) { isHorizonDirection = horizontal != 0; }

        Vector2 inputDirection = isHorizonDirection ? new Vector2(horizontal, 0) : new Vector2 (0, vertical);

        if (inputDirection != Vector2.zero)
        {
            movementVector = inputDirection.normalized;
            directionVector = movementVector;
        }
        else
        {
            movementVector = Vector2.zero;
        }
        animationHandler.Move(movementVector);
    }

    private void ScanObject()
    {
        Debug.DrawRay(_rigidbody.position, directionVector * 1.5f, Color.red, 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, directionVector, 1.5f, layerMask);

        if (hit.collider)
        {
            Debug.Log($"{hit.collider.name}을(를) 발견했담");

            if (hit.collider.TryGetComponent<NPC>(out var NPC))
            {
                NPC.StartInteraction(() => Debug.Log("대화 종료"));
            }
        }
        else
        {
            Debug.Log("아무것도 없담");
        }
    }
}
