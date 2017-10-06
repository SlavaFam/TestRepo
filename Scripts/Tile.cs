using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool canRotate;
    private Vector3 flippedRotation;
    private Vector3 sideRotation;
    private Quaternion final;

    public TileControler tileControler;
    public Vector3 startPosition;
    public float speed = 0.2f;
    public bool isSelected;
    public bool isPlanted = false;




    void Start()
    {
        startPosition = transform.position;
        flippedRotation = new Vector3(180f, 0f, 0f);
        sideRotation = new Vector3(0f, 90f, 0f);
        tileControler = FindObjectOfType<TileControler>();
    }

    void Update()
    {
        if (canRotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, final, speed);
            var dif = Quaternion.Angle(transform.rotation, final);
            if (dif == 0)
            {
                transform.rotation = final;
                canRotate = false;
            }
        }
    }

    void OnMouseOver()
    {
        if (!isPlanted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (transform.position != startPosition)
                {
                    tileControler.PlaceTile();
                }
                else
                {
                    tileControler.SelectTile(this);
                }
            }
            if (Input.GetMouseButtonDown(1))
                Flip();

            var wheel = Input.GetAxisRaw("Mouse ScrollWheel");
            if (wheel == 0.1f)
                RotateLeft();
            else if (wheel == -0.1f)
                RotateRight();
        }
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

