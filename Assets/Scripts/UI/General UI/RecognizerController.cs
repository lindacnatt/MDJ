using PDollarGestureRecognizer1;
using QDollarGestureRecognizer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class RecognizerController : MonoBehaviour
{
    #region Recognizer
    private List<Gesture> trainingSet = new List<Gesture>();

    private List<Point> points = new List<Point>();
    private int strokeID = -1;

    bool recognized = false;
    #endregion


    private List<UILineRendererList> gestureLinesRenderer = new List<UILineRendererList>();
    private UILineRendererList currentGestureLineRenderer;

    public Transform GesturePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
    }

    void OnEnable()
    {
        Lean.Touch.LeanTouch.OnFingerSet += idk;
    }

    void OnDisable()
    {
        Lean.Touch.LeanTouch.OnFingerSet -= idk;
    }

    void idk(Lean.Touch.LeanFinger finger)
    {

        //Workaround for now
        var test = Lean.Touch.LeanTouch.RaycastGui(finger.ScreenPosition);
        
        if(test.Count > 0)
        {
            var idk1 = test[0];

            if (idk1.gameObject.name == "RecognizeButtonText")
            {
                //Debug.Log("Clicked on button: " + idk1.gameObject.name);
                return;
            }
        }

        if(finger.IsOverGui)
        {
            //Started pressing this frame
            if (finger.Down)
            {
                if (recognized)
                {
                    recognized = false;
                    
                }


                strokeID++;

                Transform tmpGesture = Instantiate(GesturePrefab, transform.position, transform.rotation) as Transform;
                currentGestureLineRenderer = tmpGesture.GetComponent<UILineRendererList>();
                
                //Set the parent
                currentGestureLineRenderer.transform.SetParent(FindObjectOfType<RecognizerController>().transform.Find("Lines"));
                
                //TODO: better this out
                //Remember to set the rect transform in the proper position (bottom left)
                currentGestureLineRenderer.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                currentGestureLineRenderer.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
                currentGestureLineRenderer.transform.GetComponent<RectTransform>().pivot = new Vector2(0, 0);

                gestureLinesRenderer.Add(currentGestureLineRenderer);

            }
            else if (finger.IsActive)
            {
                points.Add(new Point(finger.ScreenPosition.x, -finger.ScreenPosition.y, strokeID));

                var point = new Vector2() { x = finger.ScreenPosition.x, y = finger.ScreenPosition.y };
                currentGestureLineRenderer.AddPoint(point);
                //currentGestureLineRenderer.SetAllDirty();
                //currentGestureLineRenderer.SetPosition(currentGestureLineRenderer.positionCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(info.ScreenPosition.x, info.ScreenPosition.y, 10)));
            }
        }
    }

    public void RecognizeShape()
    {
        recognized = true;

        if (points.Count > 0)
        {
            Gesture candidate = new Gesture(points.ToArray());

            Result gestureResult = QPointCloudRecognizer.ClassifyWithResult(candidate, trainingSet.ToArray());

            
            if(gestureResult.GestureClass == "pitchfork")
            {
                FindObjectOfType<PlayerController>().AssignSpell();
            }
            
            Debug.Log(gestureResult.GestureClass);
        }

        strokeID = -1;

        points.Clear();

        foreach (UILineRendererList lineRenderer in gestureLinesRenderer)
            Destroy(lineRenderer.gameObject);

        gestureLinesRenderer.Clear();
    }
}
