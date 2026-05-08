using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40.0f;
    private float originalSpeed;
    // Update is called once per frame
    void Start()
    {
        originalSpeed = speed; // เก็บค่าความเร็วปกติไว้
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    // ฟังก์ชันสั่งลดความเร็ว
    public void StartSlowed(float slowSpeed, float duration)
    {
        speed = slowSpeed;
        // เมื่อครบเวลา ให้กลับไปเร็วเท่าเดิม
        CancelInvoke("ResetSpeed"); // ป้องกันกรณีเก็บไอเทมซ้ำ
        Invoke("ResetSpeed", duration);
    }

    void ResetSpeed()
    {
        speed = originalSpeed;
    }
}