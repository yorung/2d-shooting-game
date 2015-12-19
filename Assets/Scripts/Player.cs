using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    Spaceship spaceship;
    IEnumerator Start()
    {
        spaceship = GetComponent<Spaceship>();
        while (true)
        {
            spaceship.Shot(spaceship.transform);
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        spaceship.Move(direction);
    }
}
