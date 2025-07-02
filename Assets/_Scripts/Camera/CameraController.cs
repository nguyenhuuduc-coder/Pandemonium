using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] protected float cameraSpeed = 0.5f;
    protected float currentPosX;
    protected Vector3 velocity = Vector3.zero;

    private void Update()
    {
        this.MoveCamera();
    }

    protected virtual void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), 
            ref this.velocity, this.cameraSpeed);
    }

    public virtual void MoveToNewRoom(Transform _newRoom)
    {
        this.currentPosX = _newRoom.position.x;
    }
}
