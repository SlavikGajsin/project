using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public Sprite[] planetSprites;
    public GameObject planet;
    private List<GameObject> planets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        GeneratePlanet(true);
        GeneratePlanet(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject p in planets) {
            if (p.transform.position.y <= -5 && planets.Count < 2) {
          
                    GeneratePlanet(false);
   
                break;
            }

            if (p.transform.position.y <= -15)
            {
                planets.Remove(p);
                if (p.transform.localScale.x == 1)
                {
                    GeneratePlanet(true);
                }
                else
                {
                    GeneratePlanet(false);
                }

                Destroy(p);
                break;
            }
        }
    }

    void GeneratePlanet(bool near) {
        Vector3 position = new Vector3(Random.Range(-7.0f, 7.0f), 15f, 0);

        GameObject newPlanet = Instantiate(planet, position, Quaternion.identity);

        int i = Random.Range(0, planetSprites.Length);
        SpriteRenderer spriteRenderer = newPlanet.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = planetSprites[i];
        spriteRenderer.sortingOrder = 3;

        Rigidbody2D rb2d = newPlanet.GetComponent<Rigidbody2D>();
        float fallSpeed = -1f;

        if (!near)
        {
            fallSpeed = -0.5f;
            newPlanet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            spriteRenderer.sortingOrder = 2;
        }
        
        rb2d.velocity = new Vector2(0, fallSpeed);
        planets.Add(newPlanet);
    }
}
