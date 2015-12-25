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
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }
    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 pos = transform.position;
        pos += direction * spaceship.speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        transform.position = pos;
    }
    /*
        void Touch()
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                //            Camera.main.ScreenToWorldPoint(t.position);
                Ray r = Camera.main.ScreenPointToRay(t.position);
                Vector3 v = r.GetPoint(0);
                float deltaX = v.x - transform.position.x;
                float deltaY = v.y - transform.position.y;
                Vector2 direction = new Vector2(deltaX, deltaY).normalized;
                Move(direction);
            }
        }
        void Keyboard()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector2 direction = new Vector2(x, y).normalized;
            Move(direction);
        }
    */
    void Mouse()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 v = r.GetPoint(0);
        float deltaX = v.x - transform.position.x;
        float deltaY = v.y - transform.position.y;
        Vector2 direction = new Vector2(deltaX, deltaY);
        if (direction.sqrMagnitude > 1)
        {
            direction = direction.normalized;
        }
        Move(direction);
    }

    void Update()
    {
        Mouse();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        if (layerName == "Bullet (Enemy)")
        {
            Destroy(c.gameObject);
        }

        if (layerName == "Bullet (Enemy)" || layerName == "Enemy")
        {
            spaceship.Explosion();
            Destroy(gameObject);
        }
    }
}
