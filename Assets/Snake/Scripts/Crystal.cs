using UnityEngine;

public class Crystal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CollisionCone")
        {
            GameStats.AddCrystalCounter();

            Transform snake = other.transform.parent.parent;
            Snake snakeComponent = snake.GetComponent<Snake>();

            snakeComponent.AddPart();
            snakeComponent.CrystalEating();

            Destroy(gameObject);
        }
    }
}

