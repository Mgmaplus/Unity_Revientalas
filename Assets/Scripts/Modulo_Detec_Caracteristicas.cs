using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;

public class Modulo_Detec_Caracteristicas : MonoBehaviour
{
    public static Action onMouthClosed;
    [SerializeField] private float bubbleDelay = 0.15f;
    private float currentDelay;
    private ARFace arFace;
    private ARFaceManager arFaceManager;
    public Transform mouth;
    public List<GameObject> waterBubbles;
    private int indexOfListWater;
    public List<GameObject> poisonBubbles;
    private int indexOfListPoison;
    int numberofBubblesMax = 15;
    int numberofBubbles = 0;
    private float randomX;
    private float randomY;
    private Vector3 target;
    [SerializeField] private float baseSpeed = 0.06f;
    private float speed;
    private int level = 1;
    private int lado = 1;

    // Start is called before the first frame update

    
    void Awake()
    {
        arFace = GetComponent<ARFace>();
        arFaceManager = FindObjectOfType<ARFaceManager>();

        Debug.Log("Instantiated debug objects");
    }

    void OnEnable()
    {
        ControlPuntaje.onLevelChanged += ChangeNivel;
    }

    void OnDisable()
    {
        ControlPuntaje.onLevelChanged -= ChangeNivel;
    }
    // Update is called once per frame
    void Update()
    {   
        
        if (!arFace)
            return;
        if (arFace.trackingState == TrackingState.Tracking)
        {
            var upper_lipVertex = arFace.vertices[13];
            var lower_lipVertex = arFace.vertices[16];
            float distancemouthopening = Vector3.Distance(upper_lipVertex, lower_lipVertex);
            var mouthVertex = arFace.vertices[14];
            mouth.localPosition = mouthVertex;

            if (distancemouthopening > 0.02)
            {
                if (currentDelay > 0)
                {
                    currentDelay -= Time.deltaTime;
                    return;
                }

                currentDelay = bubbleDelay;
                if (numberofBubbles < numberofBubblesMax)
                {
                    var burbujaPrefab = SeleccionarRandomWaterOrPoison();
                    var instanceOfWater = Instantiate(burbujaPrefab, mouth.position, Quaternion.identity, transform).GetComponent<BurbujaMov>();
                    target = GenerateTarget();
                    speed = baseSpeed * level * 2;
                    instanceOfWater.SetVars(target, speed);
                    Debug.Log($"Nivel es {level} NumeroBurbujas es {numberofBubbles}, target es {target}");
                    numberofBubbles += 1;
                }

                else
                {
                    numberofBubblesMax += 20;
                }
            }
        
        }
    }

    private void ChangeNivel(int nivel)
    {
        if (nivel == 1)
            numberofBubbles = 0;
            numberofBubblesMax = 15;
    }

    private GameObject SeleccionarRandomWaterOrPoison()
    {
        if (UnityEngine.Random.value > 0.7 - (level/100))
        {   
            indexOfListPoison = indexOfListPoison == poisonBubbles.Count - 1 ? 0 : indexOfListPoison + 1;
            return poisonBubbles[indexOfListPoison];
            
        }
        else
        {
            indexOfListWater = indexOfListWater == waterBubbles.Count - 1 ? 0 : indexOfListWater + 1;
            return waterBubbles[indexOfListWater];
        }
    }

    private Vector3 GenerateTarget()
    {
        if (UnityEngine.Random.value > 0.5 )
            lado = -1;
        else
            lado = 1;
        randomX = UnityEngine.Random.value * 1000 * lado;
        randomY = UnityEngine.Random.value * 1000;
        return new Vector3(randomX, randomY, 0);
    }

}
