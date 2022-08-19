using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private Transform goTransform;
    private float fallTime = 0.8f;
    private float normalRepeatRate = 0.8f;
    private float fastRepeatRate = 0.08f;
    public Vector3 rotationPoint;
    private int rotateAngle = 90;
    private SpawnManager spawnManager;   
    
    private static int height = 20;
    private static int width = 10;
    public static Transform[,] grid = new Transform[width,height];

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        goTransform = GetComponent<Transform>();
        InvokeRepeating(nameof(VerticalMovement), fallTime, fallTime);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) // move left
        {
            goTransform.position += Vector3.left;
            if (!ValidMove())
            {
                goTransform.position -= Vector3.left;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) // move right
        {
            goTransform.position += Vector3.right;
            if (!ValidMove())
            {
                goTransform.position -= Vector3.right;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) // rotate
        {
            goTransform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, rotateAngle);
            if (!ValidMove())
            {
                goTransform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -rotateAngle);
            }
        }

        if(Input.GetKey(KeyCode.DownArrow) && fallTime != fastRepeatRate) // fall fast
        {
            InvokeVerticalMovement(fastRepeatRate);
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && fallTime != normalRepeatRate) // fall normal
        {
            InvokeVerticalMovement(normalRepeatRate);
        }
    }
    void VerticalMovement() // falling down method
    {
        goTransform.position += Vector3.down;
        if (!ValidMove())
        {
            goTransform.position -= Vector3.down;
            CancelInvoke(nameof(VerticalMovement));
            AddToGrid();
            CheckForLines();
            FindObjectOfType<SpawnManager>().SpawnTetromino();
            enabled = false;
        }
    }
    void InvokeVerticalMovement(float repeatRate) // invoke vertical movement with repeat rate which you want
    {
        CancelInvoke(nameof(VerticalMovement));
        fallTime = repeatRate;
        InvokeRepeating(nameof(VerticalMovement), 0, fallTime);
    }

    bool ValidMove() // check movement and if it is not correct, move back
    {
        foreach (Transform child in goTransform)
        {
            int roundedX = Mathf.RoundToInt(child.position.x);
            int roundedY = Mathf.RoundToInt(child.position.y);

            if (roundedX < 0 || roundedY < 0 || roundedX >= width || roundedY >= height)
            {
                return false;
            }
            if (grid[roundedX, roundedY] != null )
            {
                return false;
            }
        }
        return true;
    }

    void AddToGrid() // save tetromino position to the grids
    {
        foreach (Transform child in goTransform)
        {
            int roundedX = Mathf.RoundToInt(child.position.x);
            int roundedY = Mathf.RoundToInt(child.position.y);

            grid[roundedX,roundedY] = child;
        }
    }

    void CheckForLines() // check grids in the lines
    {
        for (int i = height - 1; i >=  0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i) // Has Line ?
    {
        for (int j = 0; j < width; j++)
        {
            if(grid[j,i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i) // delete line
    {
        for (int j = 0; j< width;j++)
        {
            Destroy(grid[j,i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width;j++)
            {
                if (grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position += Vector3.down;
                }
            }
        }
    }
}
