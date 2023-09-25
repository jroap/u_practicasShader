using UnityEngine;

public class FoxController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    private Rigidbody rb;
    private Animator animator;

    
    private enum FoxState
    {
        Idle,
        Walking
    }
    private FoxState currentState = FoxState.Idle;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckWantToMove();
        switch (currentState)
        {
            case FoxState.Idle:
                
                animator.SetBool("IsWalking", false); 
                Debug.Log("Estoy en estado Idle");
                break;

            case FoxState.Walking:
                
                Debug.Log("Estoy en estado Walking");
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                
                Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
                rb.velocity = movement.normalized * moveSpeed;
                
                if (movement != Vector3.zero)
                {
                    transform.forward = movement;
                }
                animator.SetBool("IsWalking", true); 
                break;
        }
       
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public void CheckWantToMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if ((horizontalInput != 0) || (verticalInput != 0))
            SetWalkingState();
        else
            SetIdleState();
    }
    public void SetWalkingState()
    {
        currentState = FoxState.Walking;
        Debug.Log("Cambiando a estado Walking---");
    }
    public void SetIdleState()
    {
        currentState = FoxState.Idle;
        rb.velocity = Vector3.zero;
        Debug.Log("Estoy en el Idle del método");
    }
    private void Jump()
    {
        if (currentState == FoxState.Walking || currentState == FoxState.Idle)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("salto en cualquier estado");
        }
    }
}
