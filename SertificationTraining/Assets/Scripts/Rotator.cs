using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float speed = 0.2f;
    private bool canRotate;
    public bool isSelected = false;
    private Vector3 sideRotation;
    private Vector3 normalRotation;
    private Vector3 flippedRotation;
    private Quaternion final;

    void Start()
    {
        normalRotation = new Vector3(90f, 0f, 0f);
        flippedRotation = new Vector3(180f, 0f, 0f);
        sideRotation = new Vector3(0f, 90f, 0f);
        
    }
    void Update()
    {
        if (canRotate)
            transform.rotation = Quaternion.Lerp(transform.rotation, final, speed);
        var dif = Quaternion.Angle(transform.rotation, final);
        Debug.Log(dif);
        if (dif == 0)
        {
            canRotate = false;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Selected");
            isSelected = !isSelected;
        }
        if (Input.GetMouseButtonDown(1))
            Flip();

        var wheel = Input.GetAxisRaw("Mouse ScrollWheel");
        if (wheel == 0.1f)
            RotateLeft();
        else if (wheel == -0.1f)
            RotateRight();

    }

    public void RotateLeft()
    {
        if (canRotate) return;
        final = Quaternion.Euler(transform.eulerAngles - sideRotation);
        canRotate = true;
    }

    public void RotateRight()
    {
        if (canRotate) return;
        final = Quaternion.Euler(transform.eulerAngles + sideRotation);
        canRotate = true;
    }

    public void Flip()
    {
        if (canRotate) return;
        final = Quaternion.Euler(transform.eulerAngles + flippedRotation);
        canRotate = true;
    }


}

