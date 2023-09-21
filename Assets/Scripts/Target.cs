using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float speed = 2;

    int index = 0;

    private Vector3 startPos;

    public bool isLoop = true;

    public GameObject vfxOnDeath;

    private bool canMove = true;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        canMove = false;
        Destroy();
    }

    private void Update()
    {
        if (!canMove) return;

        Vector3 destination = GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].waypoints[index].transform.position;
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.position = newPos;

        float distance = Vector3.Distance(transform.position, destination);
        if (distance <= 0.05)
        {
            if (index < GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].waypoints.Count - 1)
            {
                index++;
            }
            else
            {
                if (isLoop)
                {
                    index = 0;
                    transform.position = startPos;
                }
            }
        }
    }

    public void Destroy()
    {
        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
        GameObject vfx = Instantiate(vfxOnDeath, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
        GameManager.Instance.CheckLevelUp();
    }
}