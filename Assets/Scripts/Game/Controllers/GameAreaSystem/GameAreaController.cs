using DG.Tweening;
using UnityEngine;

namespace testProjectTemplate
{
    public class GameAreaController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer areaRenderer = null;
        [SerializeField] private float timeSetSizeArea = 1f;
        public bool IsSetSize { get; private set; }

        public SpriteRenderer AreaRenderer => areaRenderer;

        public void Init(int areaWidth, int areaHeight)
        {
            IsSetSize = false;

            AreaRenderer.gameObject.transform.DOScale(new Vector3(areaWidth, areaHeight), timeSetSizeArea).OnComplete((() => IsSetSize = true));

            if (areaWidth > areaHeight * Camera.main.aspect)
            {
                Camera.main.orthographicSize = (float)areaWidth * Camera.main.pixelHeight / Camera.main.pixelWidth * .5f;
            }
            else
            {
                Camera.main.orthographicSize = areaHeight * .5f;
            }
        }
    }
}
