using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlPuntaje : MonoBehaviour
{
    public static Action<int> onLevelChanged;
    public GameObject TextoFlotante;
    public Text scoreText;
    public Text nivelText;

    private LayerMask bubbleLayer;
    private int nivel = 1;
    private int puntaje = 0;

    // Start is called before the first frame update
    void Start()
    {
        bubbleLayer = LayerMask.GetMask(new string[] {"Water Bubble", "Poison Bubble"});
    }

    void OnEnable()
    {
        ControlPuntaje.onLevelChanged += ChangeNivel;
        Modulo_Detec_Caracteristicas.onMouthClosed += ResetLevel;
    }
    void OnDisable()
    {
        ControlPuntaje.onLevelChanged -= ChangeNivel;
        Modulo_Detec_Caracteristicas.onMouthClosed -= ResetLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray mouseRay = GenerateMouseRay(Input.mousePosition);

            if (Physics.Raycast (mouseRay.origin, mouseRay.direction, out hit, 100, bubbleLayer))
            {
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, hit.transform.position);
                GameObject UI_Text = Instantiate(TextoFlotante, hit.transform.position, Quaternion.identity) as GameObject;
                UI_Text.transform.SetParent(this.transform);
                UI_Text.GetComponent<RectTransform>().anchoredPosition = screenPoint - this.GetComponent<RectTransform>().sizeDelta/2f;
                
                Destroy(hit.transform.gameObject);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Water Bubble"))
                {
                    puntaje += 100;
                    scoreText.text = "Puntaje: " + puntaje;
                    if (puntaje % 500 == 0 && puntaje != 0)
                    {
                        nivel += 1;
                        onLevelChanged?.Invoke(nivel);
                    }
                }
                else
                {
                    nivel = 1;
                    UI_Text.GetComponent<Text>().text = "Ughh";
                    nivelText.text = "Nivel: " + nivel;
                    puntaje = 0;
                    scoreText.text = "Puntaje: " + puntaje;
                    ResetLevel();
                }
            }
        }
    }

    void ChangeNivel(int newNivel)
    {
        nivel = newNivel;
        nivelText.text = "Nivel: " + nivel;
        if (newNivel == 1)
        {
            puntaje = 0;
            scoreText.text = "Puntaje: " + puntaje;
        }
    }

    Ray GenerateMouseRay(Vector3 touchPos){
        Vector3 mousePosFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane);
        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF-mousePosN);

        return mr;

    }
    public void ResetLevel()
    {
        nivel = 1;
        onLevelChanged?.Invoke(nivel);
    }
}   



