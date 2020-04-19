
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PDollarGestureRecognizer1;
using Lean.Touch;
using QDollarGestureRecognizer;
using System.IO;
using System;

public class DemoManager : MonoBehaviour
{
    #region Recognizer
    private List<Gesture> trainingSet = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeID = -1;

    bool recognized = false;
    #endregion

    #region UI
    private RectTransform DrawArea;
    private Text resultText;

    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;

    public Transform GesturePrefab;

    public InputField InputField;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        DrawArea = GameObject.Find("DrawingCanvas").GetComponent<RectTransform>();
        resultText = GameObject.Find("ResultText").GetComponent<Text>();

        InputField = GameObject.Find("InputField").GetComponent<InputField>();

        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));

    }

    // Update is called once per frame
    void Update()
    {
        int fingers = LeanTouch.Fingers.Count;

        for (int i = 0; i < fingers; i++)
        {
            LeanFinger info = LeanTouch.Fingers[i];

            Vector2 localRectCoords = DrawArea.InverseTransformPoint(info.ScreenPosition);

            //If within drawing GUI
            if (DrawArea.rect.Contains(localRectCoords))
            {
                //Started pressing this frame
                if(info.Down)
                {
                    if (recognized)
                    {
                        recognized = false;
                        strokeID = -1;

                        points.Clear();

                        foreach (LineRenderer lineRenderer in gestureLinesRenderer)
                            Destroy(lineRenderer.gameObject);                        

                        gestureLinesRenderer.Clear();
                    } 
   
                    
                        strokeID++;

                        Transform tmpGesture = Instantiate(GesturePrefab, transform.position, transform.rotation) as Transform;
                        currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

                        gestureLinesRenderer.Add(currentGestureLineRenderer);
                        currentGestureLineRenderer.positionCount = 0;
                    

                } 
                else if(info.IsActive)
                {
                    points.Add(new Point(info.ScreenPosition.x, -info.ScreenPosition.y, strokeID));

                    currentGestureLineRenderer.positionCount = ++currentGestureLineRenderer.positionCount;
                    currentGestureLineRenderer.SetPosition(currentGestureLineRenderer.positionCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(info.ScreenPosition.x, info.ScreenPosition.y, 10)));
                }

            }

        }
    }

    public void RecognizeShape()
    {
        recognized = true;

        Gesture candidate = new Gesture(points.ToArray());

        Result gestureResult = QPointCloudRecognizer.ClassifyWithResult(candidate, trainingSet.ToArray());

        resultText.text = gestureResult.GestureClass;

    }

    public void AddShape()
    {
        if(!string.IsNullOrEmpty(InputField.text) && points.Count > 0)
        {
            string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, InputField.text, DateTime.Now.ToFileTime());

            GestureIO.WriteGesture(points.ToArray(), InputField.text, fileName);

            trainingSet.Add(new Gesture(points.ToArray(), InputField.text));

            InputField.text = "";
        }
    }
}
