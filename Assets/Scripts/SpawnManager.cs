using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // [1] declare a public GameObject array for animal prefabs
    public GameObject[] animalPrefabs;
    public GameObject speedItemPrefab;
    public GameObject slowItemPrefab;
    // [2] declare a public int variable for animal index for testing instantiation

    private float spawnRangeX = 15.0f;
    private float spawnPosZ = 20.0f;

    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;

    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    void SpawnRandomAnimal()
    {
        // 1. สปอว์นสัตว์
        int index = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        Instantiate(animalPrefabs[index], spawnPos, animalPrefabs[index].transform.rotation);

        // 2. เรียกใช้ฟังก์ชันสปอว์นไอเทม
        TrySpawnItem(spawnPos);
        TrySpawnSlowItem();
    } // <--- ต้องมีปีกกาปิดตรงนี้ เพื่อจบฟังก์ชัน SpawnRandomAnimal

    // --- แยกออกมาเป็นฟังก์ชันใหม่ ---
    void TrySpawnItem(Vector3 animalPos)
    {
        if (Random.Range(0, 100) < 30)
        {
            Vector3 itemPos = animalPos + new Vector3(2, 0, 0);
            Instantiate(speedItemPrefab, itemPos, speedItemPrefab.transform.rotation);
        }
    }

    // --- แยกออกมาเป็นฟังก์ชันใหม่ ---
    void TrySpawnSlowItem()
    {
        if (Random.Range(0, 100) < 20)
        {
            Vector3 slowItemPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
            Instantiate(slowItemPrefab, slowItemPos, slowItemPrefab.transform.rotation);
        }
    }
}
