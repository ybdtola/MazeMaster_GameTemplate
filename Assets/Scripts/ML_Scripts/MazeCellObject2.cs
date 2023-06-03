using UnityEngine;

/// <summary>
/// MazeCellObject2 class is responsible for drawing the walls
/// </summary>
public class MazeCellObject2 : MonoBehaviour
{
    public GameObject northWall;
    public GameObject southWall;
    public GameObject eastWall;
    public GameObject westWall;
    public MeshRenderer floor;

    /// <summary>
    /// MazeCell2 class is responsible for generating the maze
    /// </summary>
    public class MazeCell2
    {
        public bool visited;
        public Vector2Int position;
        public bool northWall;
        public bool eastWall;

        /// <summary>
        /// Function to initialize the maze cell
        /// </summary>
        /// <param name="Position"></param>
        public MazeCell2(Vector2Int Position)
        {
            visited = false;
            position = new Vector2Int(Position.x, Position.y);
            northWall = true;
            eastWall = true;
        }
    }

    /// <summary>
    /// Function to initialize the walls
    /// </summary>
    /// <param name="north"></param>
    /// <param name="south"></param>
    /// <param name="east"></param>
    /// <param name="west"></param>
    public void Initialize(bool north, bool south, bool east, bool west)
    {
        northWall.SetActive(north);
        southWall.SetActive(south);
        eastWall.SetActive(east);
        westWall.SetActive(west);
    }
}


