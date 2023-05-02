using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Oculus.Interaction.Surfaces;

public class RayInteractionManager : MonoBehaviour
{
    [SerializeField]
    private Transform _rayOrigin;
    [SerializeField]
    private float _maxRayLength = 10000f;
    [SerializeField]
    private GameObject selectUI;
    [SerializeField]
    private GameObject hideButton;

    public Vector3 Origin;
    public Quaternion Rotation;
    public Vector3 Forward;
    public Vector3 End;

    private Ray rightray;
    private RaycastHit hitInfo;

    private GameObject currentSelected;
    [SerializeField]
    private Material highlight;
    private Material originalMat;


    public float MaxRayLength
    {
        get
        {
            return _maxRayLength;
        }
        set
        {
            _maxRayLength = value;
        }
    }

    public SurfaceHit? CollisionInfo { get; protected set; }
    public Ray Ray { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        selectUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Origin = _rayOrigin.transform.position;
        Rotation = _rayOrigin.transform.rotation;
        Forward = Rotation * Vector3.forward;

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.8f)
        {
            rightray = new Ray(Origin, Forward);
            if (Physics.Raycast(rightray, out hitInfo))
            {

                if (hitInfo.collider.gameObject.CompareTag("MapboxBuildings"))
                {
                    if (currentSelected != null)
                    {
                        deselectObj();
                    }
                    selectUI.SetActive(true);
                    currentSelected = hitInfo.collider.gameObject;
                    originalMat = hitInfo.collider.gameObject.GetComponent<MeshRenderer>().material;
                    hitInfo.collider.gameObject.GetComponent<MeshRenderer>().material = highlight;
                }
                else if (hitInfo.collider.gameObject.CompareTag("selectedbutton"))
                {
                    //do nothing
                }
                else
                {
                    if(currentSelected != null)
                    {
                        deselectObj();
                    }
                }
            }
            else
            {
                deselectObj();
            }
        }

        hideObj();
    }

    private void deselectObj()
    {
        currentSelected.GetComponent<MeshRenderer>().material = originalMat;
        originalMat = null;
        currentSelected = null;
        selectUI.SetActive(false);
    }

    private void hideObj()
    {
        if(currentSelected != null)
        {
            if (hideButton.GetComponent<HideButton>().isPress)
            {
                currentSelected.GetComponent<LinkData>().dataBar.SetActive(false);
            }
            else
            {
                currentSelected.GetComponent<LinkData>().dataBar.SetActive(true);
            }
        }
    }
}
