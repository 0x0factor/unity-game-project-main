using UnityEngine;
using GameProject.ProjectAssets.Units.UnitFunction.UnitSelection;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;

    [SerializeField] private RectTransform boxVisual;

    private Rect selectionBox;

    Vector2 startPosition;
    Vector2 endPosition;

    public void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    public void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }

    public void DrawSelection()
    {
        //do the x calculations
        if (Input.mousePosition.x < startPosition.x)
        {
            //dragging left
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            //dragging right
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        //do the y calculations
        if (Input.mousePosition.y < startPosition.y)
        {
            //dragging down
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            //dragging up
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    public void SelectUnits()
    {
        foreach (var unit in UnitSelections.Instance.unitList)
        {
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                // if on 'clickable units' layer
                if (unit.layer == 8)
                {
                    UnitSelections.Instance.DragSelect(unit);
                }
            }
        }
    }
}
