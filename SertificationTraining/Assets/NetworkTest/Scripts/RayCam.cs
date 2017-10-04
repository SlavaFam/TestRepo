using UnityEngine;

public class RayCam : MonoBehaviour
{
    Camera cam;
    public GameObject pew;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Moused(hit.point);
        }
      
    }

    void Moused(Vector3 p)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ob = Instantiate(pew, p, Quaternion.identity);
            Destroy(ob, Random.Range(1, 5));
        }
    }
}