using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class characterState : MonoBehaviour
{
    public Animator animator;
    public float maxSpeed = 5.0f;
    public float jumpHeight = 2f;
    public bool isFlighting;
    public short Level { get; private set; } = 1; 
    public int MaxHP { get; private set; } = 100;   // 最大生命值
    public int HP { get; private set; } = 100;      // 当前生命值
    public int MaxMP { get; private set; } = 50;    // 最大法力值
    public int MP { get; private set; } = 50;       // 当前法力值


    public short STR { get; private set; } = 10;  // 力量（影响物理攻击）
    public short AGI { get; private set; } = 5;   // 敏捷（影响攻击速度/闪避）
    public short INT { get; private set; } = 3;   // 智力（影响魔法攻击）
    public short FAI { get; private set; } = 8;   // 信仰（影响生命值、防御）
    public short VIT { get; private set; } = 8;   // 耐力（影响生命值、防御）

    // 计算战斗属性
    public int AttackPower => STR * 2;  // 物理攻击力
    public int MagicPower => INT * 3;   // 魔法攻击力
    public int Defense => VIT * 2;      // 物理防御力

    // 经验值系统
    public int EXP { get; private set; } = 0;    // 当前经验值
    public int EXPToNextLevel => Level * 100;   // 升级所需经验

    void Start()
    {
        Debug.Log($"角色初始化: HP={HP}, MP={MP}, 攻击力={AttackPower}, 防御={Defense}");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HP -= 20;
        }
        if (HP <= 0)
        {
            animator.SetTrigger("isDie");
            ResetObject();
        }


    }
    public void ResetObject()
    {
        HP = MaxHP;
    }
    /// <summary>
    /// 角色受到伤害
    /// </summary>
    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(0, damage - Defense); // 计算减去防御的实际伤害
        HP -= actualDamage;
        HP = Mathf.Clamp(HP, 0, MaxHP); // 防止 HP 低于 0

        Debug.Log($"角色受伤: -{actualDamage} HP, 当前 HP={HP}");

        if (HP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 角色死亡
    /// </summary>
    private void Die()
    {
        Debug.Log("角色死亡！");
        // TODO: 触发死亡动画、回到存档点等
    }

    /// <summary>
    /// 角色获得经验
    /// </summary>
    public void GainEXP(int amount)
    {
        EXP += amount;
        Debug.Log($"获得 {amount} 经验，当前经验 {EXP}/{EXPToNextLevel}");

        // 判断是否升级
        if (EXP >= EXPToNextLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// 角色升级
    /// </summary>
    private void LevelUp()
    {
        Level++;
        EXP = 0; // 清空经验值
        MaxHP += 20; // 提高生命值上限
        MaxMP += 10; // 提高法力值上限
        STR += 2;  // 力量成长
        AGI += 1;  // 敏捷成长
        INT += 1;  // 智力成长
        VIT += 2;  // 耐力成长

        HP = MaxHP; // 回复满血
        MP = MaxMP; // 回复满蓝

        Debug.Log($"角色升级到 {Level} 级！新属性: HP={MaxHP}, MP={MaxMP}, 攻击力={AttackPower}, 防御={Defense}");
    }
}
