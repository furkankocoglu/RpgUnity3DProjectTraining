using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorTextureNormal=default;
    public Texture2D cursorTextureEnemy= default;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;
    public GameObject mousePoint = default;
    

    // Update is called once per frame
    void Update()
    {
        CursorChanger();
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.layer==LayerMask.NameToLayer("Ground"))
                {
                    Vector3 lasPos = hit.point;
                    lasPos.y = hit.point.y + 0.35f;
                    Instantiate(mousePoint,lasPos,Quaternion.identity);
                }
            }
        }
    }
    void CursorChanger()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                Cursor.SetCursor(cursorTextureEnemy, hotSpot, mode);
            }
            else
            {
                Cursor.SetCursor(cursorTextureNormal, hotSpot, mode);
            }
        }
    }
}
