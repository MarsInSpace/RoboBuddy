using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboLeg : MonoBehaviour
{
    [SerializeField] float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        if (PickUpScript.IsEndConditionMet == true)
            return;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, 0, -RotationSpeed * Time.deltaTime));
        }
    }
}
