using DG.Tweening;
using UnityEngine;

namespace testProjectTemplate
{
    public class GameAreaController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer areaRenderer = null;
        [SerializeField] private float timeSetSizeArea = 1f;

        private int pixelWidth;
        private bool isInit = false;
        public bool IsSetSize { get; private set; }

        public SpriteRenderer AreaRenderer => areaRenderer;

        public void Init(int areaWidth, int areaHeight)
        {
            IsSetSize = false;

            AreaRenderer.gameObject.transform.DOScale(new Vector3(areaWidth, areaHeight), timeSetSizeArea).OnComplete((() => IsSetSize = true));
            ChangeOrthographicSize();
            pixelWidth = Camera.main.pixelWidth;
            isInit = true;
        }

        private void ChangeOrthographicSize()
        {
            if (GameManager.Instance.CurrentGameSetting.gameAreaWidth > GameManager.Instance.CurrentGameSetting.gameAreaHeight  * Camera.main.aspect)
            {
                Camera.main.orthographicSize = (float)GameManager.Instance.CurrentGameSetting.gameAreaWidth  * Camera.main.pixelHeight / Camera.main.pixelWidth * .5f;
            }
            else
            {
                Camera.main.orthographicSize = GameManager.Instance.CurrentGameSetting.gameAreaHeight * .5f;
            }
        }

        private void Update()
        {
            if (!isInit)
            {
                return;
            }
            if (pixelWidth != Camera.main.pixelWidth)
            {
                pixelWidth = Camera.main.pixelWidth;
                ChangeOrthographicSize();
            }
        }
    }    
}
