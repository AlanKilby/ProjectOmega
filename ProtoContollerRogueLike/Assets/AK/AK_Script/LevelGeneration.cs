using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int currentStage;

    public Transform[] startingPositions;
    public GameObject[] rooms; // index 0--> LR, index 1-->LRB, index 2-->LRT, index 3-->TLRB
    public GameObject[] startingRooms; // Rooms where the player will start
    public GameObject[] bossRooms; // Rooms where the boss will spawn

    public float moveAmountHorizontal;
    public float moveAmountVertical;

    [SerializeField]
    private int direction;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    public bool StopGeneration;

    public LayerMask room;
    public LayerMask env;

    private int downCounter;

    private Vector2 oldPos;

    

    [SerializeField]
    private bool isFirstRoom;
    private void Start()
    {
        oldPos = transform.position;
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(startingRooms[0], transform.position, Quaternion.identity);
        isFirstRoom = true;
        direction = Random.Range(1, 6);
        //currentStage = GameObject.FindGameObjectWithTag("DifficultyPanel").GetComponent<DifficultyPanel>().currentStage;
        currentStage = DifficultyPanel.currentStage;
    }

    private void Update()
    {
        if(timeBtwRoom <= 0 && StopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if(direction == 1 || direction == 2) // Move RIGHT
        {
            isFirstRoom = false;
            if (transform.position.x < maxX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmountHorizontal, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);

                if(direction == 3)
                {
                    direction = 2;
                }else if(direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
            
        }else if (direction == 3 || direction == 4) // Move LEFT
        {
            isFirstRoom = false;

            if (transform.position.x > minX) 
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmountHorizontal, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);


            }
            else
            {
                direction = 5;
            }

        }else if (direction == 5) // Move DOWN
        {
            downCounter++;

            if(transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                Debug.Log(roomDetection);
                //Collider2D roomDetection = collider2D1;

                if(roomDetection.GetComponent<RoomType>().type != 1  && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if(downCounter >= 2 && !isFirstRoom)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        if (isFirstRoom)
                        {
                            roomDetection.GetComponent<RoomType>().RoomDestruction();
                            Instantiate(startingRooms[1], transform.position, Quaternion.identity);
                            isFirstRoom = false;


                        }
                        else
                        {
                            roomDetection.GetComponent<RoomType>().RoomDestruction();

                            int randBottomRoom = Random.Range(1, 4);
                            if (randBottomRoom == 2)
                            {
                                randBottomRoom = 1;
                            }
                            Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                            isFirstRoom = false;

                        }

                    }
                   
                }
                oldPos = transform.position;
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmountVertical);
                transform.position = newPos;

                int rand = Random.Range(2, 3);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else //STOP LEVEL GENERATION
            {
                StopGeneration = true;

                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                Debug.Log(roomDetection);
                //Collider2D roomDetection = collider2D1;

                roomDetection.GetComponent<RoomType>().RoomDestruction();

                Instantiate(bossRooms[0], transform.position, Quaternion.identity);
            }
           
        }

    }

}
