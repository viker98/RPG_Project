using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleActions : MonoBehaviour, IBattleActions
{
    BattleManager battleManager;


    private void Start()
    {
        battleManager = GameManager.m_Instance.GetBattleManager();
    }

    public void Attack(UnitCharacter targetToAttack)
    {
        int attackersPower;
        int reciversHealth;
        Debug.Log(this.name + "Attacks" + targetToAttack.name);
        attackersPower = battleManager.GetTurnOrder()[0].GetComponent<UnitCharacter>().GetCharacterStats().GetStat(EStatType.Power).GetBaseValue();
        reciversHealth = targetToAttack.GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();

        battleManager.GetTurnOrder()[0].GetComponent<BattleAnimation>().GetAnimator().SetTrigger("IsPunching");


        reciversHealth -= attackersPower + GameManager.m_Instance.DiceRoll(1, 20);

        targetToAttack.GetCharacterStats().GetStat(EStatType.Health).SetBaseValue(reciversHealth);

        if (targetToAttack.GetCharacterStats().GetStat(EStatType.Health).GetBaseValue() <= 0)
        {
            battleManager.GetTurnOrder().Remove(targetToAttack.gameObject);
            targetToAttack.GetComponent<BattleAnimation>().GetAnimator().SetBool("IsDead", true);
        }
        battleManager.endTurn();
    }
    public void Heal(UnitCharacter targetToHeal)
    {
        int reciversHealth;

        Debug.Log(this.name + "Heals " + targetToHeal.name);
        reciversHealth = targetToHeal.GetCharacterStats().GetStat(EStatType.Health).GetBaseValue();
        reciversHealth += 10;
        targetToHeal.GetCharacterStats().GetStat(EStatType.Health).SetBaseValue(reciversHealth);
        battleManager.GetTurnOrder()[0].GetComponent<BattleAnimation>().GetAnimator().SetTrigger("IsHealing");

        battleManager.endTurn();
    }
}
