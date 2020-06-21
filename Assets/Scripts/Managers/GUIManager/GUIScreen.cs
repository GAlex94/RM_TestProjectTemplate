using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace testProjectTemplate
{
    public class GUIScreen : MonoBehaviour
    {
        private enum EffectType
        {
            Scale,
            None
        }

        [SerializeField] private ScreenLayer guiLayer = ScreenLayer.None;

        [SerializeField] private EffectType appearEffect = EffectType.None;

        [SerializeField] private EffectType fadeEffect = EffectType.None;
        [SerializeField] protected RectTransform panelScreenTransform = null;

        private bool showed = false;

        public bool IsShowed => showed;

        public ScreenLayer ScreenLayer => guiLayer;

        public int OffsetZ { get; set; } = 0;

        public void Show()
        {
            showed = true;

            ApplyEffect(true);

            StartCoroutine(OnShowNextFrame());
        }

        IEnumerator OnShowNextFrame()
        {
            yield return null;
            OnShow();
        }

        public void Hide()
        {
            showed = false;
            ApplyEffect(false);
            OnHide();
        }

        protected virtual void OnShow()
        {

        }

        protected virtual void OnHide()
        {

        }


        private void ApplyEffect(bool isAppear)
        {
            var curTransform = panelScreenTransform != null ? panelScreenTransform : transform;

            if (isAppear)
            {
                gameObject.SetActive(true);
                switch (appearEffect)
                {
                    case EffectType.Scale:
                    {
                        curTransform.localScale = Vector3.zero;
                        curTransform.transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
                        {
                            curTransform.localScale = Vector3.one;
                        });
                        break;
                    }

                    case EffectType.None:
                    {
                        curTransform.localScale = Vector3.one;
                        break;
                    }
                }
            }
            else
            {
                switch (fadeEffect)
                {
                    case EffectType.Scale:
                    {
                        curTransform.gameObject.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
                        {
                            gameObject.SetActive(false);
                        });

                        break;
                    }

                    case EffectType.None:
                    {
                        gameObject.SetActive(false);
                        break;
                    }
                }
            }
        }
    }
}
