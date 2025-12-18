using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public Terrain terrain;
    public GameObject tree;

    public int treeCount = 50;
    private float minHeight = 5f;
    private float maxSlope = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnTree();

    }

    void SpawnTree()
    {
        TerrainData data = terrain.terrainData;
        Vector3 terrainPos = terrain.transform.position;

        for (int i = 0; i < treeCount; i++)
        {
            Vector3 pos = GetRandomPosition(data, terrainPos);

            if (pos != Vector3.zero)
            {
                Instantiate(tree, pos, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPosition(TerrainData data, Vector3 terrainPos)
    {
        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(0f, data.size.x);
            float z = Random.Range(0f, data.size.z);

            float y = data.GetInterpolatedHeight(x / data.size.x, z / data.size.z);

            Vector3 worldPos = new Vector3(terrainPos.x + x, terrainPos.y + y, terrainPos.z + z);

            float height = worldPos.y - terrainPos.y;
            float slope = data.GetSteepness(x / data.size.x, z / data.size.z);

            if (height > minHeight && slope < maxSlope)
            {
                return worldPos;
            }
           ;
        }
        return Vector3.zero;
    }
}
