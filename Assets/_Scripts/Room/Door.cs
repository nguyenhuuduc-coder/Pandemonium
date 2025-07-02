using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] protected Transform previousRoom;
    [SerializeField] protected Transform nextRoom;
    [SerializeField] protected CameraController cam;

    private void Awake()
    {
        this.LoadPreviousRoom();
        this.LoadNextRoom();
        this.LoadCamera();
    }

    protected virtual void LoadPreviousRoom()
    {
        this.previousRoom = GameObject.Find("Room1").GetComponent<Transform>();
    }
    protected virtual void LoadNextRoom()
    {
        this.nextRoom = GameObject.Find("Room2").GetComponent<Transform>();
    }
    protected virtual void LoadCamera()
    {
        this.cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
                this.cam.MoveToNewRoom(nextRoom);
            else
                this.cam.MoveToNewRoom(previousRoom);
        }
    }
}
