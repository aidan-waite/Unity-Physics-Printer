using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    public MainManager MainManager;
    public GameObject PaintContainer;
    public GameObject CubePrefab;

    Shader lit;

    private void Awake() {
        lit = Shader.Find("Universal Render Pipeline/Lit");
    }

    public void Paint() {
        if (MainManager.InputImageTexture == null) {
            return;
        }

        StartCoroutine("DoPainting");
    }

    IEnumerator DoPainting() {
        Texture2D tex = MainManager.InputImageTexture;

        int count = 0;

        yield return new WaitForSeconds(0.02f);

        for ( int x = 0; x < tex.width; x++ ) {
            for ( int y = 0; y < tex.height; y++ ) {                
                Color color = tex.GetPixel(x, y);
                spawn(color, x, y);

                count++;

                if (count % 25 == 0) {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    void spawn(Color color, int x, int y) {
        GameObject newCube = Instantiate(CubePrefab, new Vector3(x, 4f, y), Quaternion.identity);

        Material mat = new Material(lit);
        mat.color = color;
        newCube.GetComponent<Renderer>().material = mat;

        newCube.transform.SetParent(PaintContainer.transform);
    }
}
