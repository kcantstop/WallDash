using UnityEngine;

public class GridMovement : MonoBehaviour
{
    [SerializeField] private Vector2 startingDirection = Vector2.right;
    [SerializeField] private float baseSpeed = 5.0f;

    private Vector2 _direction;
    private float _currentSpeed;

   
    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        set { _currentSpeed = value; }
    }

    public float BaseSpeed => baseSpeed;

    [SerializeField] private KeyCode UpDirection = KeyCode.W;
    [SerializeField] private KeyCode DownDirection = KeyCode.S;
    [SerializeField] private KeyCode LeftDirection = KeyCode.A;
    [SerializeField] private KeyCode RightDirection = KeyCode.D;
    [SerializeField] private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _direction = startingDirection;
        _currentSpeed = baseSpeed;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _direction * _currentSpeed;
    }

    void Update()
    {
        if (Input.GetKey(UpDirection) && _direction != Vector2.down)
        { _direction = Vector2.up;
        }

        if (Input.GetKey(DownDirection) && _direction != Vector2.up)
        { _direction = Vector2.down;
        }

        if (Input.GetKey(LeftDirection) && _direction != Vector2.right)
        { _direction = Vector2.left;
        }

        if (Input.GetKey(RightDirection) && _direction != Vector2.left)
        { _direction = Vector2.right;
        }
    }
}