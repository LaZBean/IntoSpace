using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSpriteAnimator : MonoBehaviour {

	[SerializeField]Sprite[] _frames;
    public bool clearSpriteOnEnd = false;
    public Mode mode = Mode.SingleRow;

    public float lifetime = 1.0f;
	public float timeMultiplier = 1f;

	[HideInInspector]public SpriteRenderer renderer;
    [HideInInspector]public Image image;

    public int curIndx = 0;
	float timeToNextFrame;
	float timer;

	public bool loop = true;
    public bool isEnd = false;

	bool isPlaying = false;

    [Header("[Grid Mode]")]
    public int gridRowLength = 8;
    public GridOrienation gridOrientation = GridOrienation.X;
    public int gridY;
    public int[] rowPattern = new int[] {0,1,2,3,4,5,6,7};

    public int actualID;


    [Header("*Offset")]
    public bool useOffset = false;
    public Transform offsetTransform;
    public Vector3 beginPos;
    [SerializeField]private Vector3[] _offsets;

    public SpriteAnimationPreset[] presets;
    public string presetID;

    //=================

    public enum Mode{
        SingleRow = 0,
        Grid = 1
    }

    public enum GridOrienation{
        X = 0,
        Y = 1
    }


    public Sprite[] frames{
		get{return _frames;}
		set{ 
			if (frames != value) {
				timer = 0;
			}
			_frames = value;
		}
	}




    //=================

	void Awake() {
        renderer = GetComponent<SpriteRenderer>();
		image = GetComponent<Image>();

        if (offsetTransform == null)
            offsetTransform = transform;
        beginPos = transform.localPosition;
        isPlaying = true;
	}
	

	void Update () {
		if (!isPlaying || frames.Length == 0)
        {
            if (clearSpriteOnEnd)
            {
                if (renderer != null)
                    renderer.sprite = null;
                else if (image != null)
                    image.sprite = null;
            }
            return;
        }
			

        switch (mode)
        {
            case Mode.SingleRow: SingleRow(); break;
            case Mode.Grid: Grid(); break;
        }

	}

    //

    public void SetPreset(string animID)
    {
        if (presetID == animID) return;

        foreach (var p in presets)
        {
            if(p.animationID == animID)
            {
                frames = p.sprites;
                loop = p.loop;
                mode = p.mode;
                gridRowLength = p.gridRowLength;

                lifetime = gridRowLength * 0.1f;

                presetID = animID;
                break;
            }
        }
    }
    //================

    void SingleRow()
    {
        timeToNextFrame = lifetime / (1.0f * frames.Length);

        timer -= Time.deltaTime * timeMultiplier;
        if (timer <= 0)
        {

            if (curIndx >= frames.Length - 1){
                if(!loop)
                    isPlaying = false;
                isEnd = true;
            }
            else{
                isEnd = false;
            }
                

            if(curIndx >= frames.Length - 1){
                curIndx = 0;
            }
            else{
                curIndx++;
            }

            timer = timeToNextFrame;
        }

        //-|-
        if (curIndx >= frames.Length)
            curIndx = curIndx % frames.Length;

        if (renderer != null)
            renderer.sprite = frames[curIndx];
        else if (image != null)
            image.sprite = frames[curIndx];

        //Offset
        if (useOffset && _offsets.Length > 0){
            int offsIndx = curIndx % frames.Length;
            offsetTransform.localPosition = beginPos + _offsets[offsIndx];
        }
            
    }

    void Grid()
    {
        timeToNextFrame = lifetime / (1.0f * rowPattern.Length);

        timer -= Time.deltaTime * timeMultiplier;
        if (timer <= 0)
        {

            if (curIndx >= rowPattern.Length - 1){
                if (!loop)
                    isPlaying = false;
                isEnd = true;
            }
            else{
                isEnd = false;
            }


            if (curIndx >= rowPattern.Length - 1){
                curIndx = 0;//curIndx % (rowPattern.Length);
            }
            else{
                curIndx++;
            }

            timer = timeToNextFrame;
        }

        //-|-
        if (curIndx >= rowPattern.Length)
            curIndx = 0;

        //Check Y
        int gridH = Mathf.CeilToInt(frames.Length * 1F / gridRowLength);
        gridY = ((gridY<0)? gridY+gridH : gridY) % gridH;

        //Change orientation
        actualID = (gridOrientation == GridOrienation.X)? (gridY * gridRowLength + (rowPattern[curIndx] % gridRowLength)) : (rowPattern[curIndx] * gridRowLength + gridY);

        if (actualID < frames.Length)
        {
            if (renderer != null)
                renderer.sprite = frames[actualID];
            else if (image != null)
                image.sprite = frames[actualID];
        }


        //Offset
        if (useOffset && _offsets.Length > 0)
        {
            int offsIndx = curIndx % rowPattern.Length;
            offsetTransform.localPosition = beginPos + _offsets[offsIndx];
        }
    }


    //=================

    public float frameTime
    {
        get{return lifetime / (1.0f * frames.Length); }
        set{ lifetime = frames.Length * value; }
    }


	public void Emit(){
		loop = false;
		isPlaying = true;
	}

	public void Stop(){
		isPlaying = false;
	}
}
