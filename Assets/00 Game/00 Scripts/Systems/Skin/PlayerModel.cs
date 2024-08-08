using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("----Model----")]
    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private Skin _currentSkin;
    [SerializeField] private PlayerController _playerController;

    [Header("----GameEvent----")]
    [SerializeField] private GameEventListener<Item> _changeSkin;

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
        //var currentModel = this.GetComponentInChildren<SkinController>();
        var skin = _currentSkin.Spawn(this.transform.position, this.transform.rotation, this.transform);

        _playerController.animancer.Animator.avatar = _currentSkin.avatar;
        SkinnedMeshRenderer _skinnedMeshRenderer = skin.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] bones = _skinnedMeshRenderer.bones;

        foreach (Transform bone in bones)
        {
            if (bone.name.ToLower().Contains("lefthand"))
            {
                leftHand = bone.transform;
            }

            if (bone.name.ToLower().Contains("righthand"))
            {
                rightHand = bone.transform;
            }

            if (bone.name.ToLower().Contains("hand_l"))
            {
                leftHand = bone.transform;
            }

            if (bone.name.ToLower().Contains("hand_r"))
            {
                rightHand = bone.transform;
            }
        }
    }

    public void ChangeSkin(Item item)
    {
        var skin = this.GetComponentInChildren<SkinController>();
        _currentSkin.Despawn(skin.gameObject);
        _currentSkin = (Skin)item;
        InnitSkin();
    }
}
