using UnityEngine;

public class SlowItem : MonoBehaviour
{
    public float slowAmount = 2.0f; // จะให้เหลือความเร็วเท่าไหร่
    public float duration = 5.0f;   // ระยะเวลาที่แสดงผล

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // หาวัตถุทุกตัวที่มีสคริปต์ MoveForward (ซึ่งก็คือพวกสัตว์)
            MoveForward[] enemies = Object.FindObjectsByType<MoveForward>(FindObjectsSortMode.None);

            foreach (MoveForward enemy in enemies)
            {
                // สั่งให้ศัตรูตัวนั้นๆ เริ่มทำงานฟังก์ชันลดความเร็ว
                enemy.StartSlowed(slowAmount, duration);
            }

            // ทำลายตัวไอเทมทิ้ง
            Destroy(gameObject);
        }
    }
}