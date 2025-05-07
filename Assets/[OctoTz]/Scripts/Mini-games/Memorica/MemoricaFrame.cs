
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class MemoricaFrame : MiniGameFrame {

    [Header("-----Memorica-----")]

    /// [TODO] - scriptable setup, grid
    [SerializeField] private Sprite[] _icons;

    [SerializeField] private MemoricaCard _cardPrefab;
    [SerializeField] private RectTransform _spawnParent;

    public override bool IsRunning { get; protected set; } = false;

    private int _findedPairs = 0;
    private int _mistakes = 0;

    private MemoricaCard[] _slected = new MemoricaCard[2];

    private MonoPull<MemoricaCard> _pull;
    private List<MemoricaCard> _activeCards = new List<MemoricaCard>();
    
    [ContextMenu("Memorica - Start game")]
    public override void StartGame() {

        if (_pull == null) {
            _pull = new MonoPull<MemoricaCard>(_cardPrefab, _spawnParent, false, _icons.Length * 2);
        }

        _pull.Clear();
        _slected = new MemoricaCard[2];
        _findedPairs = _mistakes = 0;

        var indexes = Enumerable.Range(0, _icons.Length * 2).ToArray();
        indexes.Shuffle();

        _activeCards = _pull.GetItems(_icons.Length * 2).ToList();

        int iconId = 0;
        for (int i = 0; i < _activeCards.Count; i++) {

            if (i != 0 && i % 2 == 0) {
                iconId += 1;
            }

            _activeCards[indexes[i]].Setup(iconId.ToString(), _icons[iconId]);
            _activeCards[indexes[i]].Clicked -= ClickedonCard;
            _activeCards[indexes[i]].Clicked += ClickedonCard;

        }

        IsRunning = true;

    }

    [ContextMenu("Memorica - Stop game")]
    public override void StopGame() {
        IsRunning = false;
        _pull.Clear();
        _slected = new MemoricaCard[2];
        _findedPairs = _mistakes = 0;
    }

	private void ClickedonCard(MemoricaCard card) {

        if (card.IsOpen || (_slected[0] != null && _slected[1] != null)) return;

        if (_slected[0] == null) {
            _slected[0] = card;
            _slected[0].SetOpened(true);
            return;
        }

        _slected[1] = card;
        _slected[1].SetOpened(true);

        if (_slected[0].Id == _slected[1].Id) {
            _findedPairs += 1;

            CoroutineBehavior.Delay(1f, () => {
                _slected[0].PlayComparisonAnimation(true);
                _slected[1].PlayComparisonAnimation(true);

                if (_findedPairs == _icons.Length) {
                    CoroutineBehavior.Delay(1f, ()=> EndGame(true));
                } else {
                    _slected[0] = _slected[1] = null;
                }
                
            });

        } else {
            _mistakes += 1;

            CoroutineBehavior.Delay(1f, () => {
                _slected[0].PlayComparisonAnimation(false);
                _slected[1].PlayComparisonAnimation(false);

                CoroutineBehavior.Delay(1f, () => {
                    _slected[0].SetOpened(false);
                    _slected[1].SetOpened(false);
                    _slected[0] = _slected[1] = null;

                });

            });

        }

    }

    private void EndGame(bool isWin) {

        Debug.Log("[Mini-Game] Memorica ended");
        IsRunning = false;
        //_endGameCallback?.Invoke(isWin ? EndGameResult.Win : EndGameResult.Louse);

    }

#if UNITY_EDITOR || DEVELOPMENT_BUILD

    private void OnGUI() {

        if (!IsRunning) return;

        if (GUI.Button(new Rect(10, 10, 100, 24), "Restart")) {
            StartGame();
        }

        if (GUI.Button(new Rect(10, 36, 100, 24), "Skip-Win")) {
            EndGame(true);
        }

        if (GUI.Button(new Rect(10, 62, 100, 24), "Skip-Louse")) {
            EndGame(false);
        }

    }


#endif


}
