using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MenuUIAnim : MonoBehaviour
{
    [SerializeField]
    private GameObject go_wholeTitle;
    [SerializeField]
    private GameObject go_title;
    [SerializeField]
    private GameObject go_button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       StartCoroutine( PlayAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayAnimation()
    {
        go_wholeTitle.SetActive(false);
        go_button.SetActive(false);
        go_wholeTitle.transform.localScale = Vector3.zero;
        go_wholeTitle.SetActive(true);
        go_wholeTitle.transform.DOScale(1, 1);
        yield return new WaitForSeconds(1f);
        go_title.transform.DOScale(0.8f, 0.5f).SetLoops(3, LoopType.Yoyo);
        yield return new WaitForSeconds(1.5f);
        go_title.transform.localScale = Vector3.one;
        go_button.SetActive(true);
        go_button.transform.DOScale(1.2f,1).SetLoops(-1,LoopType.Yoyo);

    }
}
