using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CubeController : MonoBehaviour
{
    public GameObject redCube;
    public GameObject greenCube;
    public GameObject[] spheres;
    public TextMeshProUGUI distanceText;

    public GameObject spherePrefab;
    public int numberOfSpheres = 20;
    public float spawnRadius = 5f;

    private float distance;

    void Start()
    {
        spheres = new GameObject[numberOfSpheres];
        for (int i = 0; i < numberOfSpheres; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0,
                Random.Range(-spawnRadius, spawnRadius)
            );

            spheres[i] = Instantiate(spherePrefab, randomPosition, Quaternion.identity);
            spheres[i].GetComponent<Renderer>().enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) redCube.transform.Translate(Vector3.forward * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) redCube.transform.Translate(Vector3.back * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)) redCube.transform.Translate(Vector3.left * Time.deltaTime);
        if (Input.GetKey(KeyCode.D)) redCube.transform.Translate(Vector3.right * Time.deltaTime);

        if (Input.GetKey(KeyCode.UpArrow)) greenCube.transform.Translate(Vector3.forward * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow)) greenCube.transform.Translate(Vector3.back * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow)) greenCube.transform.Translate(Vector3.left * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow)) greenCube.transform.Translate(Vector3.right * Time.deltaTime);

        distance = Vector3.Distance(redCube.transform.position, greenCube.transform.position);

        distanceText.text = "Distance: " + distance.ToString("F2") + " meters";

        if (distance < 2)
        {
            foreach (GameObject sphere in spheres)
            {
                sphere.GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            foreach (GameObject sphere in spheres)
            {
                sphere.GetComponent<Renderer>().enabled = false;
            }
        }

        if (distance < 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
