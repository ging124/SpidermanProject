using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private Transform _cam;
    [SerializeField] private Slider _hpImage;
    [SerializeField] private Slider _hpEffectImage;
    private void Awake()
    {
        _cam = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Start()
    {
        /*followActiveImage.transform.DOScale(new Vector3(0, 0, 0), fadeDurationFollow);
        followActiveImage.DOFade(0, fadeDurationFollow);*/
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _cam.forward);
        SetEffectHPBar();
    }

    void SetEffectHPBar()
    {
        if (_hpImage.value != _hpEffectImage.value)
        {
            _hpEffectImage.value = Mathf.Lerp(_hpEffectImage.value, _hpImage.value, _lerpSpeed);
        }
    }

    /*public void ActiveHPbar()
    {
        this.transform.DOScale(new Vector3(1, 1, 1), fadeDurationDead);
        hpbar.image.DOFade(1, fadeDurationDead);
        hpbarAfter.image.DOFade(1, fadeDurationDead);
        enemyName.DOFade(1, fadeDurationDead);
    }


    public void DeactiveHPbar()
    {
        this.transform.DOScale(new Vector3(0, 0, 0), fadeDurationDead);
        hpbar.image.DOFade(0, fadeDurationDead);
        hpbarAfter.image.DOFade(0, fadeDurationDead);
        enemyName.DOFade(0, fadeDurationDead);
    }*/

    public void EnemyHPChange(float currentHP, float maxHP)
    {
        _hpImage.value = (float)currentHP / maxHP;
    }
}
