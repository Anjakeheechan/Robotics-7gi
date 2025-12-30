using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width = 10;
    public int length = 10;
    public int height = 4;
    public int towerHeight = 2; // Extra height for towers

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuildCastle();
    }

    void BuildCastle()
    {
        // Adjust center to be at (0,0,0) roughly or start from (0,0,0)
        Vector3 startPos = Vector3.zero;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                // Is this a border position?
                bool isBorder = (x == 0 || x == width - 1 || z == 0 || z == length - 1);
                
                if (isBorder)
                {
                    // Is this a corner (Tower)?
                    bool isCorner = (x == 0 && z == 0) || (x == width - 1 && z == 0) || 
                                    (x == 0 && z == length - 1) || (x == width - 1 && z == length - 1);

                    // Is this the Gate (middle of front wall)?
                    // Assuming Front is where z == 0
                    bool isGate = (z == 0 && x > width / 3 && x < width * 2 / 3);

                    if (isGate) continue; // Skip creating cubes for the gate

                    int currentHeight = height;
                    if (isCorner)
                    {
                        currentHeight += towerHeight;
                    }

                    // Build column
                    for (int y = 0; y < currentHeight; y++)
                    {
                        CreateCube(new Vector3(x, y, z));
                        
                        // Add some battlements on top of walls (not towers)
                        if (!isCorner && y == currentHeight - 1 && (x + z) % 2 == 0)
                        {
                            CreateCube(new Vector3(x, y + 1, z));
                        }
                    }
                }
            }
        }
    }

    void CreateCube(Vector3 position)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = position;
        cube.transform.parent = this.transform; // Set parent to GameManager to keep hierarchy clean
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
