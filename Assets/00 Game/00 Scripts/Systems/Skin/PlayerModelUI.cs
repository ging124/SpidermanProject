using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelUI : MonoBehaviour
{
    [SerializeField] private Skin _currentSkin;
    [SerializeField] private GameObject _currentSkinGameObject;
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private ClipTransition anim;

    [Header("----GameEvent----")]
    [SerializeField] private GameEventListener<Item> _changeSkin;

    private void Awake()
    {
        animancer = this.GetComponent<AnimancerComponent>();
    }

    private void Start()
    {
        InnitSkin();
    }

    private void OnEnable()
    {
        _changeSkin.Register();
    }

    private void OnDisable()
    {
        _changeSkin.Unregister();
    }

    void InnitSkin()
    {
        if (_currentSkinGameObject.GetComponent<SkinController>().itemData != _currentSkin)
        {
            _currentSkinGameObject = _currentSkin.Spawn(this.transform.position, this.transform.rotation, this.transform);
        }
        //var currentModel = this.GetComponentInChildren<SkinController>();

        animancer.Animator.avatar = _currentSkin.avatar;
        animancer.Play(anim, 0.25f, FadeMode.FromStart);

    }

    public void ChangeSkin(Item item)
    {
        if (item == _currentSkin) return;

        var skin = this.GetComponentInChildren<SkinController>();
        _currentSkin.Despawn(skin.gameObject);
        _currentSkin = (Skin)item;

        InnitSkin();
    }
}
