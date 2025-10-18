using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public int Health = 50;
    public InputSystem_Actions input;
    public float speed;
    public Vector2 moveInput;
    private void Awake()
    {
        input = new();
    }
    public void Start()
    {
        
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
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    private void Update()
    {
        MovementMechanics();
    }
    public void MovementMechanics()
    {
        transform.position += (Vector3)moveInput * speed * Time.deltaTime;
    }
}