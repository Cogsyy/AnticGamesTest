using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private AntUnit _ant;

    void Update()
    {
        if (_ant == null)
        {
            Destroy(gameObject);
            return;
        }

        if (_ant.currentTarget != null)
        {
            Debug.DrawLine(_ant.GetPosition(), _ant.currentTarget.position, Color.red);
        }

        //Not working
/*        _lineRenderer.enabled = _ant.currentTarget != null;

        if (_ant.currentTarget != null)
        {
            _lineRenderer.SetPosition(0, _ant.GetPosition());
            _lineRenderer.SetPosition(1, _ant.currentTarget.position);
        }*/
    }

    public void SetAnt(AntUnit ant)
    {
        _ant = ant;
    }
}
