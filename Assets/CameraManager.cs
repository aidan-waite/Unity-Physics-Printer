using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
  public float Speed = 0.1f;

  Camera camera;

  private void Awake()
  {
    camera = GetComponent<Camera>();
  }

  public void MoveLeft()
  {
    camera.transform.position = new Vector3(camera.transform.position.x - Speed, camera.transform.position.y, camera.transform.position.z);
  }

  public void MoveRight()
  {
    camera.transform.position = new Vector3(camera.transform.position.x + Speed, camera.transform.position.y, camera.transform.position.z);
  }

  public void MoveUp()
  {
    camera.orthographicSize += Speed;
  }

  public void MoveDown()
  {
    camera.orthographicSize -= Speed;
  }
}
