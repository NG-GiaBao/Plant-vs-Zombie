using UnityEngine;

public class ZoneController : BaseManager<ZoneController>
{
    //[SerializeField] private int amountTile;
    [SerializeField] private GameObject tilePrefab;

    private void Start()
    {
        InitTile();
    }

    private void InitTile()
    {
        for (int X = -4; X <= 0; X += 2)
        {
            for (int Y = 0; Y >= -4; Y -= 2)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.SetParent(this.transform);
                tile.transform.localPosition = new Vector3(X,Y, 0);
                tile.name = $"Tile_{X}_{Y}";
              
            }
        }
    }

}
