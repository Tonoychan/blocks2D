using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    float min_x = -2.2f, max_x = 2.2f;
    bool canMove = false;
    float move_Speed = 2f;

    Rigidbody2D myBody2D;

    bool gameOver;
    bool ignoreCollision;
    bool ignoreTrigger;

    private void Awake()
    {
        myBody2D = GetComponent<Rigidbody2D>();
        myBody2D.gravityScale = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        if (Random.Range(0, 2) > 0)
        {
            move_Speed *= 1f;
        }
        GameplayController.instance.currentBox = this;
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += move_Speed * Time.deltaTime;
            if (temp.x > max_x)
            {
                move_Speed *= -1f;
            }
            else if (temp.x < min_x)
            {
                move_Speed *= -1f;
            }
            transform.position = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }

    public void DropBox()
    {
        canMove = false;
        myBody2D.gravityScale = Random.Range(2f,4f);
    }

    public void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.CompareTag("Platform"))
        {
            Invoke("Landed", 2f);
            ignoreCollision = true;
        }

        if (target.gameObject.CompareTag("Box"))
        {
            Invoke("Landed", 2f);
            ignoreCollision = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;

        if (target.CompareTag("GameOver"))
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;

            Invoke("RestartGame", 2f);
        }
    }
}
