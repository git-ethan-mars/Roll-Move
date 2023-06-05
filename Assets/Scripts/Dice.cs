using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    public event Action<int> DiceResult;
    private Rigidbody _rigidbody;
    private bool _isThrown;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        transform.position = new Vector3(0, 2.25f, 0.5f);
    }

    private void OnMouseUp()
    {
        Roll();
        _isThrown = true;
    }

    private void Roll()
    {
        transform.position = new Vector3(0, 5, 0.5f);
        transform.rotation = Random.rotation;
        _rigidbody.AddForce(Random.onUnitSphere * Random.Range(0f, 1f));
        _rigidbody.AddTorque(Random.onUnitSphere * Random.Range(0f, 1f), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (_rigidbody.IsSleeping() && _isThrown)
        {
            _isThrown = false;
            if (Vector3.Dot(transform.forward, Vector3.up) > 0.9f)
            {
                DiceResult?.Invoke(1);
            }
            if (Vector3.Dot(-transform.forward, Vector3.up) > 0.9f)
            {
                DiceResult?.Invoke(4);
            }
            if (Vector3.Dot(transform.up, Vector3.up) > 0.9f)
            {
                DiceResult?.Invoke(3);
            }
            if (Vector3.Dot(-transform.up, Vector3.up) > 0.9f)
            {
                DiceResult?.Invoke(6);
            }
            if (Vector3.Dot(transform.right, Vector3.up) > 0.9f)
            {
                DiceResult?.Invoke(5);
            }
            if (Vector3.Dot(-transform.right, Vector3.up) > 0.9f)
            {
                DiceResult?.Invoke(2);
            }
        }
    }
}
