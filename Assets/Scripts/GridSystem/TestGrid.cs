using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    private GridSystem<GridObject> grid;
    private Camera mainCamera;
    Vector3 worldPosition;
    Vector3 gridOrigin;

    public float cellSize;
    public int width,height;

    public GameObject virusEffect;
    public Transform AITransform;

    [SerializeField] StatsManager statsManager;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        gridOrigin = new Vector3(-width / 2f, 0, -height / 2f) * cellSize;
        grid = new GridSystem<GridObject>(width,height,cellSize, gridOrigin, (GridSystem<GridObject> g, int x, int z) => new GridObject(g,x,z));
    }
    
    public void SpreadInfection(Transform characterTransform)
    {
        grid.GetXZ(characterTransform.position, out int x, out int z);
        GridObject gridObject = grid.GetObject(x, z);

        if (gridObject.CanInfect())
        {
            GameObject buildObj = Instantiate(virusEffect, grid.GetWorldPosition(x, z) + new Vector3(cellSize,2,cellSize) * 0.5f, Quaternion.identity);
            gridObject.SetTransform(buildObj.transform);
            gridObject.SetInfectionValue(1);

            statsManager.UpdateVirusSpread(1);

            virusEffect.GetComponent<ParticleSystem>().emissionRate = gridObject.GetInfectionValue() / 10;
        }
        else
        {
            gridObject.SetInfectionValue(gridObject.GetInfectionValue() + 1);

            gridObject.GetTransform().GetComponent<ParticleSystem>().emissionRate = gridObject.GetInfectionValue() / 10;
        }
    }
    public float GetInfectionRate(Transform characterTransform)
    {
        grid.GetXZ(characterTransform.position, out int x, out int z);
        GridObject gridObject = grid.GetObject(x, z);

        if (!gridObject.CanInfect())
        {
            return gridObject.GetTransform().GetComponent<ParticleSystem>().emissionRate;
        }
        return 0f;
    }

    public class GridObject
    {
        private int MIN_INFECTION = 0;
        private int MAX_INFECTION = 100;

        private int infectionValue;

        private GridSystem<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;

        public GridObject(GridSystem<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }
        public void SetTransform(Transform transform)
        {
            this.transform = transform;
        }
        public Transform GetTransform()
        {
            return transform;
        }
        public void ClearTransform()
        {
            this.transform = null;
        }
        public bool CanInfect()
        {
            return transform == null;
        }
        public void SetInfectionValue(int value)
        {
            if (infectionValue == MAX_INFECTION)
                return;
            if (value > MAX_INFECTION)
                value = MAX_INFECTION;
            infectionValue = value;
        }
        public int GetInfectionValue()
        {
            return infectionValue;
        }
        public int GetMaxInfection()
        {
            return MAX_INFECTION;
        }
    }
}
