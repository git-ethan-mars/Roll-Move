using System;
using Data;
using UnityEngine;

public class PlayerFigure : MonoBehaviour
{
    public PlayerData Owner { get; private set; }
    public Action<Cell, PlayerFigure> CellChanged;
    public bool CanMove { get; set; }
    private Vector3 _screenPoint;
    private Vector3 _offset;
    private Camera _camera;
    private Transform _transform;
    private Vector3 _startPosition;


    public void Construct(PlayerData player)
    {
        Owner = player;
    }

    private void Start()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    void OnMouseDown()
    {
        if (!CanMove) return;
        var position = _transform.position;
        _startPosition = position;
        _screenPoint = _camera.WorldToScreenPoint(position);
        _offset = position -
                 _camera.ScreenToWorldPoint(
                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!CanMove) return;
        var cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        var cursorPosition = _camera.ScreenToWorldPoint(cursorPoint) + _offset;
        _transform.position = cursorPosition;
    }

    private void OnMouseUp()
    {
        if (!CanMove) return;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var result = Physics.Raycast(ray, out var hitInfo, 50, ~LayerMask.NameToLayer("Cell"));
        if (result)
        {
            CellChanged?.Invoke(hitInfo.transform.GetComponent<Cell>(), this);
        }
        else
        {
            _transform.position = _startPosition;
        }
    }
}