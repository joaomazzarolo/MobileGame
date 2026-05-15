using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using DG.Tweening;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> items;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenCoins = .1f;
    public Ease ease = Ease.OutBack;

    void Start()
    {
        items = new List<ItemCollectableCoin>();
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByTime());
    }
    
    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!items.Contains(i))
        {
            items.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimations();
        }
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in items)
        {
            p.transform.localScale = Vector3.zero;
        }
        Sort();

        yield return null;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenCoins);
        }
    }

    private void Sort()
    {
        items = items.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
