using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 10;

    private CollectibleManager collectibleManager;

    private void Awake()
    {
        collectibleManager = FindObjectOfType<CollectibleManager>();
    }

    public void Collect()
    {
        //GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
