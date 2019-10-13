using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldController : MonoBehaviour
{
    [SerializeField] private Tilemap _worldTilemap;
    
    [Header("House")]
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private float _houseBuffer = 10f;
    private Bounds _worldBounds;
    private Bounds _houseBounds;

    private void Start()
    {
        _worldBounds = _worldTilemap.localBounds;
        _houseBounds = _housePrefab.GetComponent<BoxCollider2D>().bounds;
    }
    public (float minX, float maxX) GetWorldBoundaries()
    {
        var minX = _worldBounds.center.x - _worldBounds.extents.x + 1f;
        var maxX = _worldBounds.center.x + _worldBounds.extents.x - 1f;
        return (minX: minX, maxX: maxX);
    }

    public (float minX, float maxX) GetHouseBoundaries()
    {
        var minX = (_houseBounds.center.x - _houseBounds.extents.x) - _houseBuffer;
        var maxX = (_houseBounds.center.x + _houseBounds.extents.x) + _houseBuffer;
        
        return (minX: minX, maxX: maxX);
    }

}
