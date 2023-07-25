using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int numCollectibles = 10;

    private GridLayout gridLayout;
    private GameObject currentCollectible;

    public delegate void CollectibleCollectedEvent();
    public static event CollectibleCollectedEvent OnCollectibleCollected;

    private void Start()
    {
        gridLayout = GetComponentInParent<GridLayout>();
        for (int i = 0; i < numCollectibles; i++)
        {
            GenerateCollectible();
        }
    }

    private void GenerateCollectible()
    {
        if (currentCollectible == null)
        {
            Vector3Int randomCell = new Vector3Int(Random.Range(-4, 4), Random.Range(-4, 4), 0);
            Vector3 collectiblePosition = gridLayout.CellToWorld(randomCell) + gridLayout.cellSize / 2f;
            currentCollectible = Instantiate(collectiblePrefab, collectiblePosition, Quaternion.identity, transform);
        }
    }

    public void CollectibleCollected()
    {
        currentCollectible = null; // Indicamos que el objeto coleccionable ha sido recolectado
        GenerateCollectible(); // Generamos un nuevo objeto coleccionable

        if (OnCollectibleCollected != null)
        {
            OnCollectibleCollected(); // Disparamos el evento para notificar a los suscriptores que se recolectó un objeto
        }
    }
}
