using UnityEngine;

public class MazeCellObject : MonoBehaviour
{
    /*
    --------------------------------------------------------------------
    1. Initialize the class variables and properties
    --------------------------------------------------------------------
    */
    public class MazeCell
    {
        public bool visited;
        public Vector2Int position;

        public bool northWall;
        public bool eastWall;


        //maze cell constructor
        public MazeCell(Vector2Int Position)
        {
            visited = false;
            position = new Vector2Int(Position.x, Position.y);
            northWall = true;
            eastWall = true;
        }
    }

    // initialize the walls prefab game objects
    [SerializeField] GameObject northWall;
    [SerializeField] GameObject southWall;
    [SerializeField] GameObject eastWall;
    [SerializeField] GameObject westWall;
    [SerializeField] MeshRenderer floor;

    //function to draw the walls
    public void Initialize(bool north, bool south, bool east, bool west)
    {
        northWall.SetActive(north);
        southWall.SetActive(south);
        eastWall.SetActive(east);
        westWall.SetActive(west);
    }

    // function to return north wall
    public GameObject GetNorthWall()
    {
        return northWall;
    }
}


