using System.Collections.Generic;
using UnityEngine;

namespace SpaceMiner
{
    /// <summary>
    /// Generates asteroid field chunks around the player using noise and a seed value.
    /// </summary>
    public class ProceduralAsteroidFieldGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject asteroidPrefab;

        [SerializeField]
        private int seed = 0;

        [SerializeField]
        private float chunkSize = 500f;

        [SerializeField]
        private int asteroidsPerChunk = 50;

        private readonly Dictionary<Vector2Int, List<GameObject>> chunks = new();
        private Transform player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;
            Random.InitState(seed);
        }

        private void Update()
        {
            GenerateChunksAroundPlayer();
            RecycleDistantChunks();
        }

        private void GenerateChunksAroundPlayer()
        {
            Vector2Int playerChunk = WorldToChunkCoords(player.position);
            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    Vector2Int chunkCoord = new(playerChunk.x + x, playerChunk.y + z);
                    if (!chunks.ContainsKey(chunkCoord))
                    {
                        CreateChunk(chunkCoord);
                    }
                }
            }
        }

        private void RecycleDistantChunks()
        {
            Vector2 playerPos = new(player.position.x, player.position.z);
            List<Vector2Int> toRemove = new();
            foreach (var kvp in chunks)
            {
                Vector2 chunkWorld = new(kvp.Key.x * chunkSize, kvp.Key.y * chunkSize);
                if (Vector2.Distance(playerPos, chunkWorld) > chunkSize * 2f)
                {
                    foreach (var asteroid in kvp.Value)
                    {
                        Destroy(asteroid);
                    }
                    toRemove.Add(kvp.Key);
                }
            }
            foreach (var key in toRemove)
            {
                chunks.Remove(key);
            }
        }

        private void CreateChunk(Vector2Int coord)
        {
            var list = new List<GameObject>();
            for (int i = 0; i < asteroidsPerChunk; i++)
            {
                Vector3 pos = new(
                    coord.x * chunkSize + Random.Range(-chunkSize / 2f, chunkSize / 2f),
                    Random.Range(-chunkSize / 2f, chunkSize / 2f),
                    coord.y * chunkSize + Random.Range(-chunkSize / 2f, chunkSize / 2f));
                GameObject asteroid = Instantiate(asteroidPrefab, pos, Random.rotation, transform);
                list.Add(asteroid);
            }
            chunks[coord] = list;
        }

        private Vector2Int WorldToChunkCoords(Vector3 pos)
        {
            int x = Mathf.FloorToInt(pos.x / chunkSize);
            int z = Mathf.FloorToInt(pos.z / chunkSize);
            return new Vector2Int(x, z);
        }
    }
}
