using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
public enum AnimationState
{
    IdleRun,
    Attack,
    LanzarBomba,
    Dead,
    None 
}
public class Player : Entity,IAttack,IDamage
{
    [SerializeField] private AnimationState StateAnimation;
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
        if (StateAnimation == AnimationState.Dead)
        {
            return;
        }
      
        switch (StateAnimation)
        {
            case AnimationState.IdleRun:
                MovementMechanics();
                if (Input.GetMouseButtonDown(0))
                    
                {
                    StateAnimation = AnimationState.LanzarBomba;
                    Debug.Log("Press");
                }
                break;

            case AnimationState.LanzarBomba:
                LanzarBomba();
                StateAnimation = GetStateAnimation();
                break;
            case
            AnimationState.Attack:
                Attack();

                break;
        
        }
        
       
        currentTime += Time.deltaTime;
        /*
        MovementMechanics();
        
        LanzarBomba();
        */
        Attack();
     }
    public void MovementMechanics()
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("IdleRun"))
        {
            if (moveInput != Vector2.zero)
            {
                transform.position += (Vector3)moveInput * speed * Time.deltaTime;
                animator.SetFloat("Horizontal", moveInput.x);
                animator.SetFloat("Vertical", moveInput.y);

            }
            else
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 0);

            }
            if (moveInput.magnitude > 0)
            {
                animator.SetFloat("Speed", moveInput.magnitude);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }

        }

     
    }
    private void LanzarBomba() 
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("IdleRun"))
        { 
           animator.SetTrigger("LanzarBomba");

        }

    }
    private AnimationState GetStateAnimation()
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("IdleRun"))
        {
            return  AnimationState.IdleRun;
        }
        else
        if (state.IsName("LanzarBomba"))
        {
            return AnimationState.LanzarBomba;
        }
        else
        if (state.IsName("Atacar"))
        {
            return AnimationState.Attack;
        }

        return AnimationState.None ;
    }
    private void UpdateStateAnimation()
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("IdleRun"))
        {
            StateAnimation = AnimationState.IdleRun;
        }
        else
        if (state.IsName("LanzarBomba"))
        {
            StateAnimation = AnimationState.LanzarBomba;
        }
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