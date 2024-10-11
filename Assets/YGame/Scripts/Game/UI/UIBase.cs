using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace YGame.Scripts.UI
{
    public enum UIAnimTypeEnum
    {
        Fade,
        ScaleAndFade,
        None,
        Paper,
        UIHandle,//UI自己处理
        UpAndDown,
        LeftAndRight,
    }

    public class UIBase : MonoBehaviour
    {
        private string _uiName;
        public UIAnimTypeEnum AnimType;
        public float ScaleStartSize = 0.7f;
        public Ease FadeCurve = Ease.InSine;
        //UI动画执行的时间
        public float UIAnimTime = 0.2f;
        
        protected CanvasGroup _canvas;
        public string UIName
        {
            get {
                if (_uiName != null)
                {
                    return _uiName;
                }
                return GetType().Name;
            }
            set { _uiName = value; }
        }
        public bool IsShow { get; private set; }
        protected bool NeedRelease  = true;

        private RectTransform _rect => transform.GetComponent<RectTransform>();
        
        public void Show()
        {
            if (IsShow) return;
            IsShow = true;
            if (TryGetComponent<CanvasGroup>(out CanvasGroup c))
            {
                _canvas = c;
            }
            else
            {
                _canvas = gameObject.AddComponent<CanvasGroup>();
            }
            this.gameObject.SetActive(true);
            AddListeners();
            OnShow();
            PlayerAnim();
        }

        public void Hide()
        {
            if (!IsShow) return;
            IsShow = false;
            this.gameObject.SetActive(false);
            if (NeedRelease)
            {
                RemoveListeners();
                OnHide();
                Destroy(gameObject);
            }
           
        }

        public void OnDestroy()
        {
            IsShow = false;
            RemoveListeners();
        }

        public virtual void OnShow(){}
        public virtual void OnHide(){}
        
        public virtual void AddListeners(){}
        public virtual void RemoveListeners(){}
        
        //动画相关Tweener列表，show时和hide时会互相打断
        private List<Tweener> _tweenerList = new List<Tweener>();
        private void KillAllTweeners()
        {
            if (_tweenerList.Count > 0)
            {
                foreach (var t in _tweenerList)
                {
                    t.Kill();
                }
                _tweenerList.Clear();
            }
        }

        private void SetAllTweeners()
        {
            foreach (var t in _tweenerList)
            {
                t.SetAutoKill(true).SetUpdate(true);
            }
        }
        
        public  void PlayerAnim()
        {
            //如果有OnHide动画，打断
            KillAllTweeners();

            //将UI置顶，防止被其他正在Hide的UI遮挡
            transform.SetAsLastSibling();
            //代替setActive，提高性能
            transform.localPosition = Vector3.zero;
            //播放动画
            switch (AnimType)
            {
                case UIAnimTypeEnum.Fade:
                    _canvas.alpha = 0;
                    _tweenerList.Add(_canvas.DOFade(1, UIAnimTime)
                        .SetEase(FadeCurve).SetUpdate(true));
                    break;
                case UIAnimTypeEnum.ScaleAndFade:
                    transform.localScale
                        = Vector3.one * ScaleStartSize;
                    _canvas.alpha = 0;
                    _tweenerList.Add(_canvas.DOFade(1, UIAnimTime)
                        .SetEase(FadeCurve).SetUpdate(true));
                    _tweenerList.Add(transform.DOScale(Vector3.one,
                            UIAnimTime)
                        .SetEase(FadeCurve).SetUpdate(true));
                    break;
                case UIAnimTypeEnum.UpAndDown:
                    _rect.sizeDelta = new Vector2(0, 600);
                    _canvas.alpha = 0;
                    _tweenerList.Add(_canvas.DOFade(1,
                            UIAnimTime).SetEase(FadeCurve).SetUpdate(true));
                    _tweenerList.Add(_rect.DOSizeDelta(Vector3.zero,
                            UIAnimTime)
                        .SetEase(FadeCurve).SetUpdate(true));//.SetDelay(UIAnimTime)

                    break;
                case UIAnimTypeEnum.LeftAndRight:
                    _rect.sizeDelta = new Vector2(600, 0);
                    _tweenerList.Add(_rect.DOSizeDelta(Vector3.zero,
                            UIAnimTime)
                        .SetEase(FadeCurve).SetUpdate(true));

                    break;
                case UIAnimTypeEnum.None:
                   
                    break;
                case UIAnimTypeEnum.Paper:
                    _canvas.alpha = 0f;
                    transform.localScale = Vector3.one * 0.1f;
                    _tweenerList.Add(_canvas.DOFade(1f, 0.4f)
                        .SetEase(FadeCurve));
                    _tweenerList.Add(transform.DORotate(Vector3.forward * 1800f,
                        0.4f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetUpdate(true));
                    _tweenerList.Add(transform.DOScale(Vector3.one, 0.4f)
                        .SetEase(FadeCurve).SetUpdate(true));
  
                    break;
            }

            //设置动画的默认属性
            SetAllTweeners();
        }

    }
}