using System;
using BringMvc.RectGame.Controllers;
using UnityEngine;


namespace BringMvc.RectGame.Views
{
    /// <summary>
    /// View implementation. Contains code responsible for visual aspects
    /// of view's behaviour. Such as concrete implementation of color changing etc.
    /// References all necessary components
    /// </summary>
    public class FunnyRectView : MonoBehaviour, IFunnyRectView
    {
        #region Fields

        /// <summary>
        /// This is the reference to controller. This reference is necessary
        /// here for two reasons: 1) view handles reference to
        /// instance of controller(it prevents controller from being collected by GC),
        /// view calls Initialize() and Uninitialize()
        /// </summary>
        private readonly IFunnyRectPresenter _presenter;

        /// <summary>
        /// Indicates either input enable or disabled
        /// </summary>
        private bool _isInputEnabled;

        private Animator _animator;

        private const string JumpAnimationTriggerName = "JumpTrigger";

        private const string RotateAnimationTriggerName = "RotateTrigger";

        #endregion

        #region Constructors

        public FunnyRectView()
        {
            _presenter = new FunnyRectPresenter(this);
        }

        #endregion

        #region Methods

        public void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
            _presenter.Initialize();
        }

        public void OnDestroy()
        {
            _presenter.Uninitialize();
        }

        /// <summary>
        /// Fires due to Collider on GameObject
        /// </summary>
        public void OnMouseDown()
        {
            if (!_isInputEnabled) return;
            OnClicked();
        }

        /// <summary>
        /// Called from FunnyRectJumpAnimation
        /// </summary>
        public void NotifyRotationEnded()
        {
            OnRotateEnd();
        }

        /// <summary>
        /// Called from FunnyRectRotateAnimation
        /// </summary>
        public void NotifyJumpEnded()
        {
            OnJumpEnded();
        }

        #endregion

        #region IFunnyRectView

        #region Events

        public event EventHandler RotationEnd;

        protected void OnRotateEnd()
        {
            var handler = RotationEnd;
            if (handler == null) return;
            handler(this, EventArgs.Empty);
        }

        public event EventHandler JumpEnd;

        protected void OnJumpEnded()
        {
            var handler = JumpEnd;
            if (handler == null) return;
            handler(this, EventArgs.Empty);
        }

        public event EventHandler Clicked;

        protected void OnClicked()
        {
            var handler = Clicked;
            if (handler == null) return;
            handler(this, EventArgs.Empty);
        }

        #endregion
        
        #region Methods

        public void DisableInput()
        {
            _isInputEnabled = false;
        }

        public void EnableInput()
        {
            _isInputEnabled = true;
        }

        public void Rotate()
        {
            _animator.SetTrigger(RotateAnimationTriggerName);
        }

        public void Jump()
        {
            _animator.SetTrigger(JumpAnimationTriggerName);
        }

        public void ChangeColor(Color color)
        {
            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
            _isInputEnabled = true;
        }

        #endregion

        #endregion
    }
}