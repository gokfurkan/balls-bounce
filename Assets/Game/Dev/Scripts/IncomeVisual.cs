using DG.Tweening;
using Template.Scripts;
using TMPro;
using UnityEngine;

namespace Game.Dev.Scripts
{
    public class IncomeVisual : MonoBehaviour
    {
        public TextMeshPro text;
        public RectTransform textRect;

        [Space(10)]
        public Color initColor;
        public Color fadeColor;
        
        [Space(10)]
        public float moveValue;
        public float moveDuration;
        public float fadeDuration;

        public void InitIncome()
        {
            text.DOColor(initColor, 0);
            textRect.transform.DOLocalMoveY(0, 0);
            textRect.transform.DOLocalMoveY(moveValue, moveDuration).OnComplete(() =>
            {
                text.DOColor(fadeColor, fadeDuration).OnComplete(() =>
                {
                    Pooling.instance.poolObjects[(int)PoolType.IncomeVisual].PutItem(gameObject);
                });
            });
        }
    }
}