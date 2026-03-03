using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerExam02 : MonoBehaviour
{
    public float speed;
    public float yRange = 10f;
    public GameObject projectilePrefab;

    private float verticalInput;
    private InputAction moveAction;
    private InputAction shootAction;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = moveAction.ReadValue<Vector2>().y;
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        float clampY = Mathf.Clamp(transform.position.y, -yRange, yRange);
        transform.position = new Vector3(transform.position.x,clampY, transform.position.z);

        if (shootAction.triggered)
        {
            GameObject bullet=Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            bullet.transform.right = Vector3.right;
        }

    }
}
