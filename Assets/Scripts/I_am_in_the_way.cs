using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_in_the_way : MonoBehaviour
{
    public Material solid;
    public Material transparent;

    private void Awake()
    {
        ShowSolid();
    }

    public void ShowTransparent()
    {
        GetComponent<MeshRenderer>().material = transparent;
    }
    public void ShowSolid()
    {
        GetComponent<MeshRenderer>().material = solid;
    }
}
