using System;
using System.Collections.Generic;
using SkyGameKit;

public class FlyShape : TweenEnemy
{
	public override void OnChoosingMe()
	{
		if (this.enemyAction != null)
		{
			this.enemyAction();
		}
	}

	public override void OnHoldPositionEnd(int shapeIndex, string shapeName)
	{
		if (this.startMoveChange != null)
		{
			this.startMoveChange();
		}
	}

	public override void OnHoldPositionStart(int shapeIndex, string shapeName)
	{
		if (this.endMoveShape != null)
		{
			this.endMoveShape();
		}
	}

	public override void OnTransformShapeComplete(int shapeIndex, string shapeName)
	{
		if (this.changedShape != null)
		{
			this.changedShape();
		}
		if (shapeIndex + 1 == this.numberOfTransformations)
		{
			if (this.shapeEnd != null)
			{
				this.shapeEnd();
			}
			if (this.EveryXSEvent2 != null)
			{
				this.EveryXSEvent2.ForEach(delegate(EnemyEventUnit<int> x)
				{
					this.DoActionEveryTime((float)x.param, new Action(x.Invoke), false, false);
				});
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/1 Check Enemy Trái")]
	public void ActionCheckLeft()
	{
		if (this.CheckLeft1())
		{
			if (this.leftEventEnemy1 != null)
			{
				this.leftEventEnemy1();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/2 Check Enemy Phải")]
	public void ActionCheckRight()
	{
		if (this.CheckRight2())
		{
			if (this.rightEventEnemy2 != null)
			{
				this.rightEventEnemy2();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/3 Check Enemy Trên Cùng")]
	public void ActionCheckTop()
	{
		if (this.CheckTop3())
		{
			if (this.topEventEnemy3 != null)
			{
				this.topEventEnemy3();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/4 Check Enemy Dưới Cùng")]
	public void ActionCheckBottom()
	{
		if (this.CheckBottom4())
		{
			if (this.bottomEventEnemy4 != null)
			{
				this.bottomEventEnemy4();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/6 Check Enemy Cột Bên Trái Dưới Cùng")]
	public void ActionCheckLeftBottom()
	{
		if (this.CheckLeftBottom6())
		{
			if (this.bottomleftEventEnemy6 != null)
			{
				this.bottomleftEventEnemy6();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/5 Check Enemy Cột Bên Trái Trên Cùng")]
	public void ActionCheckLeftTop5()
	{
		if (this.CheckTopLeft5())
		{
			if (this.topleftEventEnemy5 != null)
			{
				this.topleftEventEnemy5();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/7 Check Enemy Hàng Dưới Cùng Bên Trái")]
	public void ActionCheckBottomLeft7()
	{
		if (this.CheckBottomLeft7())
		{
			if (this.leftbottomEventEnemy7 != null)
			{
				this.leftbottomEventEnemy7();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/8 Check Enemy Hàng Dưới Cùng Bên Phải")]
	public void ActionCheckBottomRight8()
	{
		if (this.CheckBottomRight8())
		{
			if (this.rightbottomEventEnemy8 != null)
			{
				this.rightbottomEventEnemy8();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/9 Check Enemy Cột Bên Phải Dưới Cùng")]
	public void ActionCheckRightBottom9()
	{
		if (this.CheckRightBottom9())
		{
			if (this.toprightEventEnemy9 != null)
			{
				this.toprightEventEnemy9();
			}
		}
	}

	[EnemyAction(displayName = "Check Xếp Hình Cuối Cùng/10 Check Enemy Cột Bên PHải Trên Cùng")]
	public void ActionCheckRightTop10()
	{
		if (this.CheckTopRight10())
		{
			if (this.bottomrightEventEnemy10 != null)
			{
				this.bottomrightEventEnemy10();
			}
		}
	}

	private bool CheckLeft1()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if (base.transform.position.x > list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckRight2()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if (base.transform.position.x < list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckTop3()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if (base.transform.position.y < list[i].transform.position.y && !(list[i] as EnemyGeneral).stattackPath)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckBottom4()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if (base.transform.position.y > list[i].transform.position.y && !(list[i] as EnemyGeneral).stattackPath)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckTopLeft5()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if ((base.transform.position.x > list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath) || (base.transform.position.x == list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath && base.transform.position.y < list[i].transform.position.y))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckBottomLeft7()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if ((base.transform.position.x > list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath) || (base.transform.position.x == list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath && base.transform.position.y > list[i].transform.position.y))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckTopRight10()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if ((base.transform.position.x < list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath) || (base.transform.position.x == list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath && base.transform.position.y < list[i].transform.position.y))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckBottomRight8()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if ((base.transform.position.x < list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath) || (base.transform.position.x == list[i].transform.position.x && !(list[i] as EnemyGeneral).stattackPath && base.transform.position.y > list[i].transform.position.y))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckRightBottom9()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if ((base.transform.position.y > list[i].transform.position.y && !(list[i] as EnemyGeneral).stattackPath) || (base.transform.position.y == list[i].transform.position.y && !(list[i] as EnemyGeneral).stattackPath && base.transform.position.x < list[i].transform.position.x))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckLeftBottom6()
	{
		bool result = true;
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.MotherTurn.GetAliveEnemy();
		for (int i = 0; i < list.Count; i++)
		{
			if ((base.transform.position.y > list[i].transform.position.y && !(list[i] as EnemyGeneral).stattackPath) || (base.transform.position.y == list[i].transform.position.y && !(list[i] as EnemyGeneral).stattackPath && base.transform.position.x > list[i].transform.position.x))
			{
				result = false;
				break;
			}
		}
		return result;
	}

	[EnemyEventCustom(displayName = "Shape/Tất Cả Vào Vị Trí")]
	public EnemyEvent changedShape;

	[EnemyEventCustom(displayName = "Shape/Vào Vị Trí")]
	public EnemyEvent endMoveShape;

	[EnemyEventCustom(displayName = "Shape/Bắt Đầu Di Chuyển")]
	public EnemyEvent startMoveChange;

	[EnemyEventCustom(displayName = "Shape/Chọn Enemy Thực Hiện Hành Động")]
	public EnemyEvent enemyAction;

	[EnemyEventCustom(displayName = "Shape/Tất cả xếp vào hình cuối cùng")]
	public EnemyEvent shapeEnd;

	[EnemyEventCustom(paramName = "Second", displayName = "Shape/Tất cả xếp vào hình cuối cùng- Lặp")]
	public EnemyEvent<int> EveryXSEvent2 = new EnemyEvent<int>();

	[EnemyEventCustom(paramName = "Second", displayName = "Xếp Hình Cuối Cùng Galaga/Con Ngoài Cùng Bên Trái")]
	public EnemyEvent<float> EventEnemyLeft = new EnemyEvent<float>();

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/1 Enemy Bên Trái")]
	public EnemyEvent leftEventEnemy1;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/2 Enemy Bên Phải")]
	public EnemyEvent rightEventEnemy2;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/3 Enemy Trên Cùng")]
	public EnemyEvent topEventEnemy3;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/4 Enemy Dưới Cùng")]
	public EnemyEvent bottomEventEnemy4;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/5 Enemy Cột Bên Trái Trên Cùng")]
	public EnemyEvent topleftEventEnemy5;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/6 Enemy Cột Bên Trái Dưới Cùng")]
	public EnemyEvent bottomleftEventEnemy6;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/7 Enemy Hàng Dưới Cùng Bên Trái")]
	public EnemyEvent leftbottomEventEnemy7;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/8 Enemy Hàng Dưới Cùng Bên Phải")]
	public EnemyEvent rightbottomEventEnemy8;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/9 Enemy Cột Bên Phải Trên Cùng")]
	public EnemyEvent toprightEventEnemy9;

	[EnemyEventCustom(displayName = "Hình Cuối Cùng/10 Enemy Cột Bên PHải Dưới Cùng")]
	public EnemyEvent bottomrightEventEnemy10;
}
