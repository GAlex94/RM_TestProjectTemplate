using System.Collections;
using UnityEngine;

namespace testProjectTemplate
{
    public class EffectFly : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AudioClip openScreenAudioClip;

        private Coroutine destroyCoroutine;
        public bool IsPooledObject { get; set; }
        public void Deactivate()
        {
            
        }

        public void Activate(GameObject templatePrefab)
        {
            destroyCoroutine = StartCoroutine(DestroyCorutine());
        }
        private IEnumerator DestroyCorutine()
        {
            yield return new WaitForSeconds(.4f);
            animator.SetTrigger("DestroyMoney");
          //  if (openScreenAudioClip != null) SoundManager.PlaySound(openScreenAudioClip);
            yield return new WaitForSeconds(1f);
            DestroyPool();
        }

        private void DestroyPool()
        {
            if (destroyCoroutine != null)
            {
                StopCoroutine(destroyCoroutine);
                destroyCoroutine = null;
            }
        //    DeathGame.Instance.Pool.DestroyFromPool(gameObject);
        }
    }
}