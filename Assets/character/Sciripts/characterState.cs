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
    public int MaxHP { get; private set; } = 100;   // �������ֵ
    public int HP { get; private set; } = 100;      // ��ǰ����ֵ
    public int MaxMP { get; private set; } = 50;    // �����ֵ
    public int MP { get; private set; } = 50;       // ��ǰ����ֵ


    public short STR { get; private set; } = 10;  // ������Ӱ����������
    public short AGI { get; private set; } = 5;   // ���ݣ�Ӱ�칥���ٶ�/���ܣ�
    public short INT { get; private set; } = 3;   // ������Ӱ��ħ��������
    public short FAI { get; private set; } = 8;   // ������Ӱ������ֵ��������
    public short VIT { get; private set; } = 8;   // ������Ӱ������ֵ��������

    // ����ս������
    public int AttackPower => STR * 2;  // ��������
    public int MagicPower => INT * 3;   // ħ��������
    public int Defense => VIT * 2;      // ���������

    // ����ֵϵͳ
    public int EXP { get; private set; } = 0;    // ��ǰ����ֵ
    public int EXPToNextLevel => Level * 100;   // �������辭��

    void Start()
    {
        Debug.Log($"��ɫ��ʼ��: HP={HP}, MP={MP}, ������={AttackPower}, ����={Defense}");
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
    /// ��ɫ�ܵ��˺�
    /// </summary>
    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(0, damage - Defense); // �����ȥ������ʵ���˺�
        HP -= actualDamage;
        HP = Mathf.Clamp(HP, 0, MaxHP); // ��ֹ HP ���� 0

        Debug.Log($"��ɫ����: -{actualDamage} HP, ��ǰ HP={HP}");

        if (HP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// ��ɫ����
    /// </summary>
    private void Die()
    {
        Debug.Log("��ɫ������");
        // TODO: ���������������ص��浵���
    }

    /// <summary>
    /// ��ɫ��þ���
    /// </summary>
    public void GainEXP(int amount)
    {
        EXP += amount;
        Debug.Log($"��� {amount} ���飬��ǰ���� {EXP}/{EXPToNextLevel}");

        // �ж��Ƿ�����
        if (EXP >= EXPToNextLevel)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// ��ɫ����
    /// </summary>
    private void LevelUp()
    {
        Level++;
        EXP = 0; // ��վ���ֵ
        MaxHP += 20; // �������ֵ����
        MaxMP += 10; // ��߷���ֵ����
        STR += 2;  // �����ɳ�
        AGI += 1;  // ���ݳɳ�
        INT += 1;  // �����ɳ�
        VIT += 2;  // �����ɳ�

        HP = MaxHP; // �ظ���Ѫ
        MP = MaxMP; // �ظ�����

        Debug.Log($"��ɫ������ {Level} ����������: HP={MaxHP}, MP={MaxMP}, ������={AttackPower}, ����={Defense}");
    }
}
