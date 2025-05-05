using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenarSınır : MonoBehaviour
{
    public EdgeCollider2D solCollider;
    public EdgeCollider2D sagCollider;
    public EdgeCollider2D ustCollider;

    void Start()
    {
        if (solCollider != null)
        {
            Vector3 screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
            solCollider.transform.position = new Vector3(screenLeft.x, screenLeft.y, 0f);

            Vector2[] colliderPoints = new Vector2[2];
            colliderPoints[0] = Vector2.zero;
            colliderPoints[1] = Vector2.up;

            solCollider.points = colliderPoints;
        }

        if (sagCollider != null)
        {
            Vector3 screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.transform.position.z));
            sagCollider.transform.position = new Vector3(screenRight.x, screenRight.y, 0f);

            Vector2[] colliderPoints = new Vector2[2];
            colliderPoints[0] = Vector2.zero;
            colliderPoints[1] = Vector2.up;

            sagCollider.points = colliderPoints;
        }

        if (ustCollider != null)
        {
            Vector3 screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.transform.position.z));
            ustCollider.transform.position = new Vector3(screenTop.x, screenTop.y, 0f);

            Vector2[] colliderPoints = new Vector2[2];
            colliderPoints[0] = Vector2.zero;
            colliderPoints[1] = Vector2.up;

            ustCollider.points = colliderPoints;
        }


    }
}
