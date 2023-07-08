using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}
