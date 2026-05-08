using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public float speedBoost = 5.0f; // ค่าความเร็วที่จะเพิ่ม
    public float duration = 5.0f;   // ระยะเวลาที่เพิ่มความเร็ว

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // เรียกใช้ฟังก์ชันเพิ่มความเร็วในสคริปต์ PlayerController
            PlayerController playerScript = other.GetComponent<PlayerController>();
            if (playerScript != null)
            {
                playerScript.ApplySpeedBoost(speedBoost, duration);
            }

            Destroy(gameObject); // เก็บไอเทมแล้วทำลายตัวเอง
        }
    }
}