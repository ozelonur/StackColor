using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour, IProperty
{
    private GameManager gameManager;
    private ObjectManager objectManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(transform.localEulerAngles.x, Random.Range(50, 60), transform.localEulerAngles.z), .2f, RotateMode.Fast).SetLoops(-1, LoopType.Incremental);
        gameManager = GameManager.Instance;
        objectManager = ObjectManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        gameManager.Coin++;
        objectManager.CoinText.text = gameManager.Coin.ToString();
        Destroy(gameObject);
    }
}
