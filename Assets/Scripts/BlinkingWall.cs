using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingWall : MonoBehaviour
{
    public float BlinkInTime;
    public float BlinkOutTime;

    private bool _blinked;
    private float _timer;
    private Renderer _renderer;
    private Collider _col;

    // Start is called before the first frame update
    void Start()
    {
        _blinked = false;
        _timer = 0;
        _renderer = GetComponent<Renderer>();
        _col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        //If visible
        if (!_blinked && _timer >= BlinkInTime)
        {
            _timer = 0;
            BlinkOut();
        }else if(_blinked && _timer >= BlinkOutTime)
        {
            _timer = 0;
            BlinkIn();
        }
    }

    private void BlinkIn()
    {
        _blinked = false;
        _renderer.enabled = true;
        _col.enabled = true;
    }

    private void BlinkOut()
    {
        _blinked = true;
        _renderer.enabled = false;
        _col.enabled = false;
    }
}
