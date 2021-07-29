using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Coin : MonoBehaviour, IProperty
{
    private GameManager gameManager;
    private ObjectManager objectManager;

    private bool isCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(transform.localEulerAngles.x, Random.Range(50, 60), transform.localEulerAngles.z), .2f, RotateMode.Fast).SetLoops(-1, LoopType.Incremental);
        gameManager = GameManager.Instance;
        objectManager = ObjectManager.Instance;
    }
    public void Interact()
    {
        if (!isCollided)
        {
            gameManager.CoinAction += CoinAction;
            gameManager.CoinAction();
            isCollided = true;
        }
    }

    private void CoinAction()
    {
        gameManager.Coin++;
        transform.DOMove(new Vector3(objectManager.CoinText.transform.position.x, objectManager.CoinText.transform.position.y, transform.position.z + .2f), .4f);
        objectManager.CoinText.text = gameManager.Coin.ToString();
        Invoke(Constants.DESTROY_COIN, .4f);
    }

    private void DestroyCoin()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        gameManager.CoinAction -= CoinAction;
    }
}
