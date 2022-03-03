using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BuddyScript : MonoBehaviour
{
    //Camera Panning

    [SerializeField] Camera Camera;
    [SerializeField] List<GameObject> RobotPartPrefabs;

    [SerializeField] TMP_Dropdown LimbDropdown;

    [SerializeField] GameObject Dropdown;
    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject PauseScreen;

    [SerializeField] TMP_Text ScreenText;
    [SerializeField] GameObject CounterText;
    public static int Counter = 0;

    bool IsAssembling;
    bool IsPaused;

    Rigidbody BuddyBody;
    [SerializeField] int Bouncyness;
    float Timer;


    // Start is called before the first frame update
    void Start()
    {
        PauseScreen.SetActive(false);
        CounterText.SetActive(false);

        IsAssembling = true;
        IsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAssembling == true)
            AssemblyMode();

        if (Input.GetKeyDown(KeyCode.Escape) && IsPaused == false && !PickUpScript.IsEndConditionMet)
            InGameScreen();
        else if (Input.GetKeyDown(KeyCode.Escape) && IsPaused == true && !PickUpScript.IsEndConditionMet)
        {
            PauseScreen.SetActive(false);
            IsPaused = false;
        }

        Timer += Time.deltaTime;

        EndGame();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && Timer >= 2 && !IsAssembling)
        {
            BuddyBody.AddForce(new Vector3(0, Bouncyness, 0), ForceMode.Impulse);
            Debug.Log("IsJumping");
            Timer = 0;
        }
    }

    void AssemblyMode()
    {
        IsAssembling = true;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(mouseRay);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.green);

        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit, 100f, LayerMask.GetMask("Robo")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.normal);

                Quaternion rotation = Quaternion.LookRotation(hit.normal);
                Instantiate(RobotPartPrefabs[LimbDropdown.value], hit.point, rotation, hit.collider.transform);
            }
        }
    }

    public void EnterGameMode()
    {
        IsAssembling = false;

        gameObject.AddComponent<Rigidbody>();
        BuddyBody = gameObject.GetComponent<Rigidbody>();

        Dropdown.SetActive(false);
        StartButton.SetActive(false);
        CounterText.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void PlayMode()
    {
        
    }

    void EndGame()
    {
        if(PickUpScript.IsEndConditionMet == true)
        {
            PauseScreen.SetActive(true);
            ScreenText.text = "Nice!";
        }
    }

    void InGameScreen()
    {
        IsPaused = true;
        PauseScreen.SetActive(true);
        ScreenText.text = "Pause";
    }
}
