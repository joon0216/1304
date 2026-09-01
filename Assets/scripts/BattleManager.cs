using System.Collections;
using UnityEngine;
using TMPro;

public class BattleManager : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI infoText;

    private Player player;
    private Monster monster;

    private bool isProcessingTurn = false;

    private void Start()
    {
        player = new Player("플레이어", 100, 25);
        monster = new Monster("고블린", 100, 20);

        UpdateDisplay("당신의 턴입니다. 행동을 선택하세요.");
    }

    public void OnClickAttack()
    {
        if (isProcessingTurn || player.IsDead || monster.IsDead) return;
        StartCoroutine(PlayerAttackRoutine());
    }

    public void OnClickHeal()
    {
        if (isProcessingTurn || player.IsDead || monster.IsDead) return;
        StartCoroutine(PlayerHealRoutine());
    }

    private IEnumerator PlayerAttackRoutine()
    {
        isProcessingTurn = true;

        int damage = player.GetCalculateDamage();
        monster.TakeDamage(damage);

        UpdateDisplay($"[플레이어 턴]\n{monster.Name}에게 {damage}의 피해를 입혔습니다!");

        yield return new WaitForSeconds(1.0f);

        if (monster.IsDead)
        {
            UpdateDisplay($"★ 승리했습니다! ★\n[{monster.Name}]을(를) 처치했습니다!");
            yield break;
        }

        yield return StartCoroutine(MonsterTurnRoutine());
    }

    private IEnumerator PlayerHealRoutine()
    {
        isProcessingTurn = true;

        player.Heal(20);
        UpdateDisplay($"[플레이어 턴]\n회복 아이템을 사용하여 20만큼 회복했습니다.");

        yield return new WaitForSeconds(1.0f);

        yield return StartCoroutine(MonsterTurnRoutine());
    }

    private IEnumerator MonsterTurnRoutine()
    {
        UpdateDisplay($"[{monster.Name}의 턴]\n공격을 준비 중입니다...");
        yield return new WaitForSeconds(0.8f);

        int damage = monster.GetCalculateDamage();
        player.TakeDamage(damage);

        UpdateDisplay($"[{monster.Name}의 턴]\n{monster.Name}이(가) {damage}의 피해를 주었습니다!");

        yield return new WaitForSeconds(1.0f);

        if (player.IsDead)
        {
            UpdateDisplay("☠ Game Over ☠\n플레이어가 패배했습니다.");
        }
        else
        {
            UpdateDisplay("당신의 턴입니다. 행동을 선택하세요.");
            isProcessingTurn = false;
        }
    }

    private void UpdateDisplay(string actionMessage)
    {
        if (infoText != null)
        {
            infoText.text = $"[ {player.Name} HP: {player.Hp}/{player.MaxHp} ]   [ {monster.Name} HP: {monster.Hp} ]\n\n" +
                            $"{actionMessage}";
        }
    }
}