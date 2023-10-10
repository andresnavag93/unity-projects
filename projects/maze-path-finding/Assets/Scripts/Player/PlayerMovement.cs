using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Player Movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";
    const string DESTINATION_TAG = "Destination";
    const string MAZE_CELL_TAG = "Maze Cell";
    const string BANANA_TAG = "Banana";

    public bool isOnUiButtonPress = false;
    private bool walking = false;
    private Rigidbody2D playerRigidbody;
    public float speed = 1.0f;
    public Vector2 movement = Vector2.zero;

    [SerializeField] MazeCell curCell;
    public MazeCell CurrentCell { 
        get 
        { 
            return curCell; 
        } 
    }

    #region Mono
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isOnUiButtonPress)
        {
            walking = false;
            if (Mathf.Abs(Input.GetAxisRaw(HORIZONTAL)) > 0.5f ||
                    Mathf.Abs(Input.GetAxisRaw(VERTICAL)) > 0.5)
            {
                walking = true;
                movement = new Vector2(Input.GetAxisRaw(HORIZONTAL), 
                    Input.GetAxisRaw(VERTICAL));
                movement = movement * speed;
            }
            if (!walking)
            {
                movement = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        playerRigidbody.velocity = movement;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == DESTINATION_TAG)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.CompareTag(MAZE_CELL_TAG))
        {
            SetCell(collision.GetComponent<MazeCell>());
        }
        if (collision.CompareTag(BANANA_TAG))
        {
            collision.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(BANANA_TAG))
        {
            collision.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void MoveForward ()
    {
        walking = true;
    }

    public void SetCell(MazeCell nextCell)
    {
        curCell = nextCell;
    }
}
