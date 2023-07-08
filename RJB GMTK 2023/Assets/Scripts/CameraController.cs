using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player = null;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = Vector3.Lerp(transform.position, _player.transform.position, 5 * Time.deltaTime);
        playerPosition.z = transform.position.z;
        transform.position = playerPosition;
    }
}
