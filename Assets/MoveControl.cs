using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;
    private CollectibleManager collectibleManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collectibleManager = FindObjectOfType<CollectibleManager>();
        CollectibleManager.OnCollectibleCollected += OnCollectibleCollected;
    }

    private void OnDestroy()
    {
        CollectibleManager.OnCollectibleCollected -= OnCollectibleCollected;
    }

    private void Update()
    {
        // Obtener las entradas de direcci�n del jugador
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Si hay alguna entrada de direcci�n, actualizamos la velocidad del rigidbody
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
            rb.velocity = direction * speed;

            // Rotar el personaje hacia la direcci�n del movimiento
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Letra"))
        {
            Collectible collectible = collision.GetComponent<Collectible>();
            if (collectible != null)
            {
                collectible.Collect();
                collectibleManager.CollectibleCollected(); // Notificamos al CollectibleManager que el coleccionable ha sido recolectado
            }
        }
    }

    private void OnCollectibleCollected()
    {
        // Aqu� puedes implementar cualquier l�gica adicional que desees cuando se recolecte un objeto coleccionable
    }
}
