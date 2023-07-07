using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cursor;
    [SerializeField]
    private Camera screen;
    [SerializeField]
    private Transform[] delimiters; // x:9_19 y:-3.69_6.29

    public float speed = 1f;

    private CursorLockMode lockMode;

    void Awake()
    {
        //lockMode = CursorLockMode.Locked;
        //Cursor.lockState = lockMode;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, speed);
        Vector3 newCursorPos = new Vector3(screen.ScreenToWorldPoint(mousePos).x, screen.ScreenToWorldPoint(mousePos).y, cursor.transform.position.z);

        Vector3 curCursorPor = cursor.transform.position;
        
        if ((newCursorPos.x >= delimiters[0].position.x) && (newCursorPos.x <= delimiters[1].position.x))
        {
            cursor.transform.position = new Vector3(newCursorPos.x, cursor.transform.position.y, cursor.transform.position.z);
        }

        if ((newCursorPos.y <= delimiters[2].position.y) && (newCursorPos.y >= delimiters[3].position.y))
        {
            cursor.transform.position = new Vector3(cursor.transform.position.x, newCursorPos.y, cursor.transform.position.z);
        }
    }

    public Vector3 GetCursorPosition()
    {
        return cursor.transform.position;
    }
}
