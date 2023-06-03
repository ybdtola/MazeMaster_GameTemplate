using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeCellObject2;
using UnityEngine.AI;

/// <summary>
/// The Maze2 class is responsible for generating the maze and instantiating the walls prefab
/// </summary>
public class Maze2 : MonoBehaviour
{
    private MazeCell2[,] maze;
    private SpawnManager spawnCheese;
    private Vector2Int currentCell;
    private Vector3 playerPos;
    public float CellSize;
    public GameObject WallPrefab;
    public GameObject player;
    public GameObject cheese;
    public Vector2Int mazeSize = new Vector2Int(10, 10);
    public Vector2Int startPosition;
    public enum MazeDirection
    {
        North,
        South,
        East,
        West
    }

    /// <summary>
    /// The start function is called before the first frame update
    /// </summary>
    void Start()
    {
        playerPos = player.transform.localPosition;
        spawnCheese = GetComponent<SpawnManager>();
        BuildMaze();
    }

    /// <summary>
    /// Build maze by creating 2D array of cells and instantiate individual cell with walls prefab
    /// </summary>
    public void BuildMaze()
    {
        int width = mazeSize.x, depth = mazeSize.y;
        maze = new MazeCell2[width, depth];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                maze[x, z] = new MazeCell2(new Vector2Int(x, z));
            }
        };

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject walls = Instantiate(WallPrefab, new Vector3(x * CellSize, 0f, z * CellSize), Quaternion.identity, transform);
                MazeCellObject2 mazeCell = walls.GetComponent<MazeCellObject2>();
                walls.name = "Maze Cell " + x + ", " + z;
                bool north = maze[x, z].northWall;
                bool east = maze[x, z].eastWall;
                bool west = false;
                bool south = false;
                var random = new System.Random();
                int choice = random.Next(0, 2);

                if (x == 0 && z == 0 || x == 1 && z == 0)
                {
                    if (choice == 0)
                    {
                        north = false;
                    }
                    else if (choice == 1)
                    {
                        east = false;
                    }
                }
                if (x == 0) west = true;
                if (x == width - 1) east = true;
                if (z == 0) south = true;

                mazeCell.Initialize(north, south, east, west);
                CarvePassage(startPosition);
            }
        }
        Instantiate(player, new Vector3(0f, 1f, 0f), Quaternion.identity);
        spawnCheese.SpawnCheese(cheese);
    }

    /// <summary>
    /// Check neighbour cell is within the bounds of the grid and has not yet been visited
    /// </summary>
    /// <param name="neighbour"></param>
    /// <returns></returns>
    bool IsValidCell(Vector2Int neighbour)
    {
        int width = mazeSize.x, depth = mazeSize.y;
        if (neighbour.x < 0 ||
            neighbour.y < 0 ||
            neighbour.x > width - 1 ||
            neighbour.y > depth - 1 ||
            maze[neighbour.x, neighbour.y].visited)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Carve passage between cells by removing walls
    /// </summary>
    /// <param name="walls"></param>
    /// <returns></returns>
    public Vector2Int UpdateCurretCell(Vector2Int walls)
    {
        currentCell = walls;
        return currentCell;
    }

    /// <summary>
    /// Check the left, right, top, and bottom neighbours of the cell returns coordinates of the neighbour cell
    /// </summary>
    /// <returns></returns>
    Vector2Int CheckNeighbours()
    {
        List <MazeDirection> MazeDirections = new List<MazeDirection> {
                                        MazeDirection.North,
                                        MazeDirection.South,
                                        MazeDirection.East,
                                        MazeDirection.West
        };
        while (MazeDirections.Count > 0)
        {
            List<MazeDirection> rndList = new List<MazeDirection>();
            var rnd = new System.Random();
            int rndInt = rnd.Next(0, MazeDirections.Count);
            rndList.Add(MazeDirections[rndInt]);
            MazeDirections.RemoveAt(rndInt);

            for (int i = 0; i < rndList.Count; i++)
            {
                Vector2Int neighbourCell = UpdateCurretCell(currentCell);
                switch (rndList[i])
                {
                    case MazeDirection.North:
                        neighbourCell.y++;
                        break;
                    case MazeDirection.South:
                        neighbourCell.y--;
                        break;
                    case MazeDirection.East:
                        neighbourCell.x++;
                        break;
                    case MazeDirection.West:
                        neighbourCell.x--;
                        break;
                }
                if (IsValidCell(neighbourCell)) return neighbourCell;
            }
        }
        return currentCell;
    }

    /// <summary>
    /// Remove walls between two cells in the maze based on their relative positions
    /// </summary>
    /// <param name="currentCell"></param>
    /// <param name="neighbourCell"></param>
    void RemoveWall(Vector2Int currentCell, Vector2Int neighbourCell)
    {
        if (currentCell.x > neighbourCell.x)
        {
            maze[neighbourCell.x, neighbourCell.y].eastWall = false;
        }
        else if (currentCell.x < neighbourCell.x)
        {
            maze[currentCell.x, currentCell.y].eastWall = false;
        }
        else if (currentCell.y < neighbourCell.y)
        {
            maze[currentCell.x, currentCell.y].northWall = false;
        }
        else if (currentCell.y > neighbourCell.y)
        {
            maze[neighbourCell.x, neighbourCell.y].northWall = false;
        }
    }

    /// <summary>
    /// Carve a passage from the start position to the end position
    /// </summary>
    /// <param name="startPos"></param>
    void CarvePassage(Vector2Int startPos)
    {
        currentCell = new Vector2Int(startPos.x, startPos.y);
        Stack<Vector2Int> visited = new Stack<Vector2Int>();
        bool deadEnd = false;

        while (!deadEnd)
        {
            Vector2Int nxtCell = CheckNeighbours();
            if (nxtCell == currentCell)
            {
                while (visited.Count - 1 > 0)
                {
                    currentCell = visited.Pop();
                    if (nxtCell != currentCell) break;
                }
                if (nxtCell == currentCell)
                {
                    deadEnd = true;
                }
            }
            else
            {
                RemoveWall(currentCell, nxtCell);
                maze[currentCell.x, currentCell.y].visited = true;
                currentCell = nxtCell;
                visited.Push(currentCell);
            }
        }
    }
}



