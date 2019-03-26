using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private float topBufferSpace = 16;
    private float bottomBufferSpace = 66;

    public bool Open = true;
    public float transitionSpeed = .5f;

    private float verticalPosition;

    // Start is called before the first frame update
    void Start()
    {
        verticalPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Open)
        {
            verticalPosition = Mathf.Lerp(verticalPosition, Screen.height-topBufferSpace, transitionSpeed * Time.deltaTime);
        }
        else
        {
            verticalPosition = Mathf.Lerp(verticalPosition, bottomBufferSpace, transitionSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Screen.width/2, verticalPosition, 0);
    }

    public void toggleOpen()
    {
        Open = !Open;
    }
    public bool isOpen()
    {
        return Open;
    }
}
