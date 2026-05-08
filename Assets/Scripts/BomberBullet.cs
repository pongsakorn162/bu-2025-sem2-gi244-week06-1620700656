using UnityEngine;

public class BomberBullet : MonoBehaviour
{
    public float speed = 15.0f;
    public float explosionRadius = 5.0f; // รัศมีการระเบิด
    public float explosionForce = 500.0f; // แรงระเบิด (ถ้าศัตรูมี Rigidbody)

    void Update()
    {
        // ให้กระสุนวิ่งไปข้างหน้าตรงๆ
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // เมื่อชนศัตรู (เช็ก Tag ให้ตรงกับที่คุณใช้ เช่น Animal หรือ Enemy)
        if (other.CompareTag("Animal"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // สร้างอาณาเขตวงกลมเพื่อเช็กวัตถุรอบๆ จุดระเบิด
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            // ทำลายวัตถุทุกตัวที่มี Tag ว่า "Animal" ในระยะระเบิด
            if (nearbyObject.CompareTag("Animal"))
            {
                Destroy(nearbyObject.gameObject);
            }

            // แถม: ถ้าวัตถุมี Rigidbody ให้กระเด็นออกด้วย
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // ทำลายตัวกระสุนเองหลังจากระเบิดแล้ว
        Destroy(gameObject);

        // (ตัวเลือกเพิ่มเติม) คุณสามารถ Instantiate(explosionEffect, transform.position, Quaternion.identity); เพื่อโชว์ Effect ระเบิดได้ที่นี่
        Debug.Log("Bomber Bullet Exploded!");
    }

    // วาดเส้นรัศมีในหน้า Scene เพื่อให้เรามองเห็นระยะระเบิดตอน Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}