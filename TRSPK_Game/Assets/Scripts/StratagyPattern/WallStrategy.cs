using System;
using UnityEngine;
using UnityEngine.UI;

public class WallStrategy: MonoBehaviour, IStrategy
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject Action;
    [SerializeField] private Context _context;

    private void Awake()
    {
        _context = Action.GetComponent<Context>();
        enabled = false;
    }

    private void OnEnable()
    {
        _context.ExecuteStrategy += Execute;
    }

    private void OnDisable()
    {
        _context.ExecuteStrategy -= Execute;
    }
    public void Execute()
    {
        GridLayoutGroup grid = GameObject.Find("Field").GetComponent<GridLayoutGroup>();
        GameObject.Find("FieldScroll").AddComponent<ScrollRect>();
        GameObject.Find("FieldScroll").AddComponent<RectMask2D>().enabled = true;
        GameObject.Find("FieldScroll").AddComponent<Image>();
        Image image = GameObject.Find("FieldScroll").GetComponent<Image>();
        GameObject.Find("FieldScroll").GetComponent<Image>().color =
            new Color(image.color.r, image.color.g, image.color.b, 0f);
        GameObject.Find("FieldScroll").GetComponent<ScrollRect>().content = GameObject.Find("Field").GetComponent<RectTransform>();
        GameObject.Find("FieldScroll").GetComponent<ScrollRect>().horizontal = true;
        GameObject.Find("FieldScroll").GetComponent<ScrollRect>().vertical = false;
        GameObject.Find("FieldScroll").GetComponent<ScrollRect>().viewport =
            GameObject.Find("FieldScroll").GetComponent<RectTransform>();
        //GameObject.Find("FieldScroll").GetComponent<ScrollRect>().elasticity = 10f;
        GameObject.Find("FieldScroll").GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Unrestricted;
        GameObject.Find("FieldScroll").GetComponent<ScrollRect>().scrollSensitivity = 20f;
        grid.cellSize = new Vector2(90, 110);
        grid.spacing = new Vector2(10, 10);
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = 1;
        for (int i = 0; i < 9; i++)
        {
            GameObject slot = Instantiate(prefab);
            slot.transform.SetParent(GameObject.Find("Field").transform);
        }

        this.enabled = false;
    }
}