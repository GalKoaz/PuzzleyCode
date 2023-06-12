using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutPuzzle : MonoBehaviour
{
    [Header("Camera")] [SerializeField] private GameObject playerCameraGameObject;
    private Camera _playerCamera;

    [Header("Raycast")] public float RaycastRange = 3;

    [Header("Drawers array")] [SerializeField]
    private GameObject[] drawers;

    [SerializeField] private int drawers_rows;
    [SerializeField] private int drawers_cols;

    [Header("Drawer animation settings")] [SerializeField]
    private float openOffset = .5f;

    private bool[,] _drawersStatus;

    // Start is called before the first frame update
    void Start()
    {
        _drawersStatus = new bool[drawers_rows, drawers_cols];
        bool[] randomBoolArray = RandomizeDrawers();
        InitDrawersStatus(randomBoolArray);
        // DEBUG
        // Debug.Log("RANDOM ARRAY: ");
        // foreach (bool cell in randomBoolArray)
        // {
        //     Debug.Log(cell);
        // }

        // Show the initial drawers position (open=1, close=0)
        SetDrawersInitPos();
    }

    public void UpdateOnMouseClick(GameObject drawer)
    {
        // Find the drawer inside the drawers array
        int drawerIndex = 0;
        for (int i = 0; i < drawers.Length; i++)
        {
            if (drawer.GetInstanceID() == drawers[i].GetInstanceID())
            {
                drawerIndex = i;
                break;
            }
        }

        // Get current drawer 2d position
        int rowPos = drawerIndex / drawers_rows;
        int colPos = drawerIndex % drawers_cols;
       
        HandleDrawerNeigh(rowPos, colPos);
        
        // Check if player solved the puzzle after current operation
        if (IsSolved())
        {
            Debug.Log("Player has solved the puzzle");
        }
    }

    // Function inverts the current given drawer position
    // and also its adjacent neighbours
    void HandleDrawerNeigh(int rowPos, int colPos)
    {
        // Handle current drawer
        HandleDrawer(rowPos, colPos);

        // Define the offsets for the neighboring positions
        int[] rowOffsets = { -1, 1, 0, 0 };
        int[] colOffsets = { 0, 0, -1, 1 };

        // Traverse all the neighbours of current drawer
        for (int i = 0; i < rowOffsets.Length; i++)
        {
            int neighborRow = rowPos + rowOffsets[i];
            int neighborCol = colPos + colOffsets[i];

            // Check if the neighbor position is within the valid range
            if (neighborRow >= 0 && neighborRow < drawers_rows && neighborCol >= 0 && neighborCol < drawers_cols)
            {
                HandleDrawer(neighborRow, neighborCol);
            }
        }
    }

    // Handles single drawer with its 2d position
    void HandleDrawer(int rowPos, int colPos)
    {
        // Check if it is opened or close and then get current drawer's status
        bool opened = _drawersStatus[rowPos, colPos];
        int drawer1dPos = GetDrawer1dPos(rowPos, colPos);
        GameObject drawer = drawers[drawer1dPos];

        // Invert the current status of the drawer (with animations and sounds)
        if (opened)
        {
            CloseDrawer(drawer);
        }
        else
        {
            OpenDrawer(drawer);
        }

        _drawersStatus[rowPos, colPos] = !_drawersStatus[rowPos, colPos];
    }

    int GetDrawer1dPos(int i, int j)
    {
        return i * drawers_cols + j;
    }
    
    int[] GetDrawer2dPos(int drawerIndex)
    {
        int rowPos = drawerIndex / drawers_rows;
        int colPos = drawerIndex % drawers_cols;
        return new int[] { rowPos, colPos };
    }
    
    
    bool[] RandomizeDrawers()
    {
        int arraySize = drawers_rows * drawers_cols;
        bool[] randomBoolArray = new bool[arraySize];
        
        //  Traverse array with 50% for true and false
        for (int i = 0; i < arraySize; i++)
        {
            randomBoolArray[i] = Random.Range(0,2) == 0;
        }

        // Count true cells in array
        int trueCellsCnt = 0;
        int falseCellsCnt = 0;
        foreach (bool cell in randomBoolArray)
        {
            if (cell)
            {
                trueCellsCnt++;
            }
            else
            {
                falseCellsCnt++;
            }
        }
        
        // Check if the array represent trivial cases
        // Then take a random cell and invert its value.
        if (trueCellsCnt == arraySize || falseCellsCnt == arraySize)
        {
            int randomIndex = Random.Range(0, arraySize);
            randomBoolArray[randomIndex] = !randomBoolArray[randomIndex];
        }

        return randomBoolArray;
    }

    void InitDrawersStatus(bool[] boolArr)
    {
        if (boolArr.Length != drawers_rows * drawers_cols) return;

        for (int i = 0; i < drawers_rows; i++)
        {
            for (int j = 0; j < drawers_cols; j++)
            {
                _drawersStatus[i,j] = boolArr[i * drawers_cols + j];
            }
        }
    }

    void OpenDrawer(GameObject drawerGameObject)
    {
        drawerGameObject.transform.position += new Vector3(openOffset,0, 0);
    }
    
    void CloseDrawer(GameObject drawerGameObject)
    {
        drawerGameObject.transform.position -= new Vector3(openOffset,0, 0);
    }

    // Function determines which one is opened and which one is close, 
    // and then fulfills the animation.
    void SetDrawersInitPos()
    {
        for (int i = 0; i < drawers_rows; i++)
        {
            for (int j = 0; j < drawers_cols; j++)
            {
                GameObject currDrawer = drawers[i * drawers_cols + j];
                if (_drawersStatus[i,j])
                {
                    OpenDrawer(currDrawer);
                }
                
            }
        }
    }

    // Function checks if the player has solved fully the puzzle
    bool IsSolved()
    {
        // Traverse the array and check that all the drawers are closed
        for (int i = 0; i < drawers_rows; i++)
        {
            for (int j = 0; j < drawers_cols; j++)
            {
                bool opened = _drawersStatus[i, j];
                if (opened)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
