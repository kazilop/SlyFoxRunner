using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    [SerializeField] private int firstChunkSpawnPosition = -10;
    [SerializeField] private int chunkOnScreen = 3;
    [SerializeField] private float despawnDistance = 5f;

    [SerializeField] private List<GameObject> chunkPrefab;
    [SerializeField] private Transform cameraTransform;
   
    private void Start()
    {
        if(chunkPrefab.Count == 0)
        {
            Debug.LogError("No chunk");
            return;
        }

        if (!cameraTransform)
        {
            cameraTransform = Camera.main.transform;
            Debug.LogError("Camera assignet auto");
        }
    }

    
    private void Update()
    {
        ScanPosition();
    }

    private void ScanPosition()
    {
        float cameraZ = cameraTransform.transform.position.z;
        Chunk lastChunk = activeChunks.Peek();

        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {

    }

    private void DeleteLastChunk()
    {

    }

    public void ResetWorld()
    {

    }
}
