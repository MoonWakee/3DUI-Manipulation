using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class ZoomToggle : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;
    void Start()
    {
        if (_map == null)
        {
            _map = FindObjectOfType<AbstractMap>();
        }

    }

    void Update()
    {

        if (_map.Zoom > 19.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
