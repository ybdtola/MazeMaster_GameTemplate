    
    /*
                            --------------------------------------------------------------------
                                PROCEDURAL MAZE GENERATION USING BINARY TREE ALGORITHM
                            --------------------------------------------------------------------
    */ 
    
    
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using static MazeCellObject;
    using UnityEngine.AI;


public class Maze : MonoBehaviour
    {
        
    /*
    --------------------------------------------------------------------
    1. Initialize the class variables and properties
    --------------------------------------------------------------------
    */
        [SerializeField] GameObject WallPrefab;

        //cell size
        public float CellSize;

        //maze dimensions - depth and width
        public Vector2Int mazeSize = new Vector2Int(10, 10);


        //startPosition position for maze
        public Vector2Int startPosition;
        //private Vector2Int startPositionPos;

        //current cell  
        Vector2Int currentCell;

        //array that stores maze cells
        MazeCell[,] maze;
        
        //update the current cell to match the new cell
        //map new cell to current cell
        public Vector2Int UpdateCurretCell(Vector2Int walls){
            currentCell = walls;
            return currentCell;
        }

        public NavMeshSurface surface;

        //public GameObject player;
    //private GameObject player1;
    Vector3 playerPos = new Vector3(0, 0, 0);
    //playerPos = transform.TransformDirection(playerPos);


    /*
    --------------------------------------------------------------------
    2. Visualize algorithm with Coroutines and Delayed execution
    --------------------------------------------------------------------
    */
    void Awake()
        {
            BuildMaze();
            surface.BuildNavMesh();
        //player = Instantiate(player, playerPos, Quaternion.identity);
        //player.transform.position += Vector3.forward * Time.deltaTime;

    }

    //IEnumerator SetPlayerVisibility()
    //    {
    //        //player1 = GameObject.Find("Player(Clone)");
    //        //find game object player(clone) in the scene
    //        //player.SetActive(true);
    //    //cheese = GameObject.Find("Cheese(Clone)");
    //    yield return new WaitForSeconds(5);
    //}

    //    void Update()
    //    {
    //    //if (Input.GetKeyDown("f"))
    //    //{ 
    //        Debug.Log("Hi");
    //            StartCoroutine(SetPlayerVisibility());
    //        //}
    //    }
    void BuildMaze()
        {
            //width and depth of the maze
            int width = mazeSize.x, depth = mazeSize.y; 
            
            //initialize the grid with specified size
            maze = new MazeCell[width, depth];

            //create 2D array for the grid coordinates
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < depth; z++)
                {
                    maze[x, z] = new MazeCell(new Vector2Int(x, z));
                }
            };


                //instantiate individual cell with walls prefab and place in the array
                for (int z = 0; z < depth; z++)
                    {
                    for (int x = 0; x < width; x++)
                        {
                            GameObject walls = Instantiate(WallPrefab, new Vector3(x * CellSize, 0f, z * CellSize), Quaternion.identity, transform);
                            MazeCellObject mazeCell = walls.GetComponent<MazeCellObject>();
                            walls.name = "Maze Cell " + x + ", " + z;
                        
                    // valid MazeDirections
                    bool north = maze[x, z].northWall;
                    bool east = maze[x, z].eastWall;

                    bool west = false;
                    bool south = false;

                    // handling weird situation of closed cell at (0,0) coordinate
                    var random = new System.Random();
                    int choice = random.Next(0, 2);

                    if (x == 0 && z == 0 || x == 1 && z == 0)
                    {
                        if (choice == 0)
                        {
                            north = false;
                        }
                        else if(choice == 1)
                        {
                            east = false;
                        }
                    }

                    // create boundaries
                    if(x == 0 ) west = true;
                    if (x == width - 1) east = true;
                    if (z == 0) south = true;

                    // create entrance and exit
                        //if(x == 0 && z == 0){
                        //    west = false;
                        //}
                        //if(x == width - 1 && z == depth - 1){
                        //    east = false;
                        //}


                    // call the Initialize function to buid maze
                    mazeCell.Initialize(north, south, east, west);

                    CarvePassage(startPosition);
                    //yield return new WaitForSeconds(0.05f);
                }

        }
        //Instantiate(cheese, new Vector3(22f, -2f, 39.55574f), Quaternion.identity);

    }

    /*
    --------------------------------------------------------------------
    3. Set up constants to aid with describing the passage MazeDirections
    --------------------------------------------------------------------
    */
    public enum MazeDirection   {
                                North,
                                South,
                                East,
                                West
        }

        // check the neighbour cell is within the bounds of the grid and has not yet been visited
        bool IsValidCell(Vector2Int neighbour)
        {
            int width = mazeSize.x, depth = mazeSize.y;
            if (neighbour.x < 0 || 
                neighbour.y < 0 || 
                neighbour.x > width - 1 || 
                neighbour.y > depth - 1 || 
                maze[neighbour.x, neighbour.y].visited){
                    return false;
                }
                else 
                    return true;
        }


    /* --------------------------------------------------------------------
    4. Find the next cell to visit
    -------------------------------------------------------------------- */


    // Check the left, right, top, and bottom neighbours of the cell returns coordinates of the neighbour cell
        Vector2Int CheckNeighbours()
        {
            List<MazeDirection> MazeDirections = new List<MazeDirection> {
                                        MazeDirection.North, 
                                        MazeDirection.South, 
                                        MazeDirection.East,
                                        MazeDirection.West 
            };

    /*randomly shuffling MazeDirections*/ 

            while (MazeDirections.Count > 0)
            {
            List <MazeDirection> rndList = new List<MazeDirection>();
            
                //Random rnd = new Random();
                var rnd = new System.Random();
                int rndInt = rnd.Next(0, MazeDirections.Count);

                // int rndInt = Random.Range(0, MazeDirections.Couant nt);
                rndList.Add(MazeDirections[rndInt]);
                MazeDirections.RemoveAt(rndInt);
                for(int i = 0; i < rndList.Count; i++)
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


    /*
    --------------------------------------------------------------------
    5. Binary Tree algorithm
    --------------------------------------------------------------------
    */

    // remove walls between two cells in the maze based on their relative positions
        void RemoveWall(Vector2Int currentCell, Vector2Int neighbourCell)
        {
            // checks cell along the x-axis i.e width
            if (currentCell.x > neighbourCell.x)
            {
                maze[neighbourCell.x, neighbourCell.y].eastWall = false;
            }
            else if (currentCell.x < neighbourCell.x)
            {
                maze[currentCell.x, currentCell.y].eastWall = false;
            }
            // checks cell along the z-axis i.e depth
            else if (currentCell.y < neighbourCell.y)
            {
                maze[currentCell.x, currentCell.y].northWall = false;
            }
            else if (currentCell.y > neighbourCell.y)
            {
                maze[neighbourCell.x, neighbourCell.y].northWall = false;
            }
        }


        void CarvePassage(Vector2Int startPos)
        {

            currentCell = new Vector2Int(startPos.x, startPos.y);

            //
            Stack<Vector2Int> visited = new Stack<Vector2Int>();

            bool deadEnd = false;
            
            while (!deadEnd)
            {
                Vector2Int nxtCell = CheckNeighbours();
                
                // cell has no valid neighbours, deadend is true
                if (nxtCell == currentCell)
                {
                    // backtrack until unvisited neighbour is found
                        while(visited.Count - 1 > 0){
                        currentCell = visited.Pop();

                        // has valid neighbours
                        if (nxtCell != currentCell) break;
                    }
                    // after successfully backtracking
                    if (nxtCell == currentCell)
                    {
                        deadEnd = true;
                    }
                }
                else
                {
                    //function removes the walls between the current cell and the next cell
                    RemoveWall(currentCell, nxtCell);


                    //marks the current cell as visited
                    maze[currentCell.x, currentCell.y].visited = true;


                    //sets the next cell as the current cell
                    currentCell = nxtCell;


                    //adds the current cell to the stack of visited cells
                    visited.Push(currentCell);
                }
            }
        }
    }



