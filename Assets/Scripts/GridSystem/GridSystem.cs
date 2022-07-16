using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem<TGridObject>
{
    private int width,height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;

    public GridSystem(int width, int height,float cellSize, Vector3 originPosition, System.Func<GridSystem<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = createGridObject(this,x,z);
            }
        }

        //DEBUG
        //for (int x = 0; x < gridArray.GetLength(0); x++)
        //{
        //    for (int z = 0; z < gridArray.GetLength(1); z++)
        //    {
        //        //debugTextArray[x,z] = CreateWorldText(gridArray[x,z].ToString(),null, GetWorldPosition(x,z) + new Vector3(cellSize,cellSize)*0.5f,20,Color.white,TextAnchor.MiddleCenter);

        //        Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
        //        Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);

        //        //Debug.Log(x + "," + z);
        //    }
        //}
        //Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        //Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        //SetObject(1, 1, 20);

    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x,0,z) * cellSize + originPosition;
    }
    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public void SetObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            //debugTextArray[x, z].text = gridArray[x, z].ToString();
        }
    }
    public void SetObject(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        Debug.Log(x + "," +z);
        SetObject(x, z, value);
    }
    public TGridObject GetObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return gridArray[x, z];
        }
        else
        {
            return default(TGridObject);
        }
    }
    public TGridObject GetObject(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetObject(x, z);
    }


    private TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition = default(Vector3), int fontSize=40, Color color = new Color(), TextAnchor textAnchor=TextAnchor.UpperLeft, TextAlignment textAlignment=TextAlignment.Left, int sortingOrder=5000)
    {
        if (color == null)
        {
            color = Color.white;
        }
        return CreateWorldText(parent, text, localPosition, fontSize, color, textAnchor, textAlignment, sortingOrder);
    }
    private TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment,int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
