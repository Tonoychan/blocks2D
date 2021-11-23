using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public BoxSpawner box_Spawner;

    public BoxScript currentBox;

    public CameraFollow cameraScript;
    int move_Count;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        box_Spawner.SpawnBox();
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }

    void DetectInput()
    {
        if (Input.GetMouseButton(0))
        {
            currentBox.DropBox();
        }
    }

    public void SpawnNewBox()
    {
        Invoke("NewBox", 2f);
    }

    void NewBox()
    {
        box_Spawner.SpawnBox();
    }

    public void MoveCamera()
    {
        move_Count++;

        if (move_Count == 3)
        {
            move_Count = 0;
            cameraScript.targetPos.y += 2f;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
