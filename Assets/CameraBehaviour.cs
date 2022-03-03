using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject Player;

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (PickUpScript.IsEndConditionMet == true)
            return;

        transform.position = Player.transform.position;

        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(Player.transform.position, Vector3.up, 90 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(Player.transform.position, Vector3.up, -90 * Time.deltaTime);
        }
    }
}
