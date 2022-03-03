using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] float HoverSpeed;

    public static bool IsEndConditionMet = false;

    private float StartHeight;

    [SerializeField] TMPro.TMP_Text CounterText;

    void Start()
    {
        StartHeight = transform.position.y;
        CounterText.text = "Picked up: " + BuddyScript.Counter;
    }

    // Update is called once per frame
    void Update()
    {
        SphereMovement();

        if (BuddyScript.Counter >= 3)
        {
            IsEndConditionMet = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PickedUp();
    }

    void PickedUp()
    {
        BuddyScript.Counter++;
        this.gameObject.SetActive(false);
        CounterText.text = "Picked up: " + BuddyScript.Counter;
        Debug.Log(BuddyScript.Counter);
    }

    void SphereMovement()
    {
        float sinY = Mathf.Sin(Time.time * HoverSpeed);
        transform.position = new Vector3(transform.position.x, StartHeight + sinY, transform.position.z);
    }
}
