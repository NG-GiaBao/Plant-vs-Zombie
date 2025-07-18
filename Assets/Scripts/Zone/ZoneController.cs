using Lean.Pool;
using UnityEngine;

public class ZoneController : BaseManager<ZoneController>
{
    [Header("Grid Settings")]
    [SerializeField] private int gridWidth = 3;
    [SerializeField] private int gridHeight = 3;
    [SerializeField] private float cellSize = 2f;
    [SerializeField] private Vector2 gridOrigin = Vector2.zero;
    [SerializeField] private GameObject tilePrefab;

    private void Start()
    {
        InitTile();
    }

    private void InitTile()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float posX = gridOrigin.x + x * cellSize;
                float posY = gridOrigin.y - y * cellSize;

                Vector3 tilePosition = new(posX, posY, 0);

                GameObject tile = LeanPool.Spawn(tilePrefab, this.transform);
                tile.transform.localPosition = tilePosition;
                tile.name = $"Tile_{posX}_{posY}";
            }
        }
    }

}
