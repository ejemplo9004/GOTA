using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshBaker : MonoBehaviour
{
    public void BakeMap(GameObject walkableTerrain)
    {
        var surf = walkableTerrain.GetComponent<NavMeshSurface>();
        surf.collectObjects = CollectObjects.Children;
        surf.BuildNavMesh();
    }
}
