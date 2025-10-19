using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity,IAttack,IDamage
{
    private Animator animator;
    [SerializeField] private float currentTime;
    [SerializeField] private float TimeSpawnBullet;
    public int Health = 50;
    public InputSystem_Actions input;
    public float speed;
    public Vector2 moveInput;
    [SerializeField] private Transform PistolTransform;
    public GameObject BulletPrefab;
    private void Awake()
    {
        input = new();
    }
    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Move.started += OnMove;
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += OnMove;

      
    }
    private void OnDisable()
    {
        input.Disable();

        input.Player.Move.started -= OnMove;
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= OnMove;

        animator.SetFloat("Horizontal", 3f);
        animator.SetFloat("Vertical", 3f);
        animator.SetFloat("Speed", 3f);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
     void Update()
    {
        currentTime += Time.deltaTime;
        MovementMechanics();
        Attack();
    }
    public void MovementMechanics()
    {
        transform.position += (Vector3)moveInput * speed * Time.deltaTime;
     
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetFloat("Speed", moveInput.magnitude);
    }
    
    

    public void Attack()
    { currentTime += Time.deltaTime;
        if ((Input.GetMouseButtonDown(0) && currentTime >= TimeSpawnBullet))
        {
          Vector2 mousePos = Input.mousePosition;
            Vector3 GamePos = Camera.main.ScreenToWorldPoint(mousePos);      
            GamePos.z = 0; 
            Vector3 dir = (GamePos - PistolTransform.position).normalized;
            GameObject bullet = Instantiate(BulletPrefab, PistolTransform.position, Quaternion.identity);
            bullet.transform.up = dir;
            currentTime = 0;
        }     
        return;
    }
    public void ReceiveDamage(int damage)
    {
        damage = 10;
        Health -= damage;
        if (Health <= 0) 
        { 
            Destroy(gameObject);
        }
    }
}