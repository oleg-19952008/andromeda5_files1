using System;
using UnityEngine;

public class MiningStationGuiRelativeToGameObject : MonoBehaviour
{
	private const float WINDOW_WIDTH = 300f;

	private Vector3 screenPos;

	public MiningStation miningStation;

	private GuiWindow infoWindow;

	public MiningStationGuiRelativeToGameObject()
	{
	}

	private void HideMiningStationInfo()
	{
		if (this.infoWindow != null)
		{
			AndromedaGui.gui.RemoveWindow(this.infoWindow.handler);
			this.infoWindow = null;
		}
	}

	public void Populate()
	{
		if (this.infoWindow != null)
		{
			this.HideMiningStationInfo();
			this.ShowMiningStationInfo();
		}
	}

	private void ShowMiningStationInfo()
	{
		if (this.infoWindow != null)
		{
			return;
		}
		int num = 0;
		int num1 = 0;
		if (NetworkScript.player.vessel.teamNumber == 1)
		{
			num = this.miningStation.teamOneProgress;
			num1 = this.miningStation.teamTwoProgress;
		}
		else if (NetworkScript.player.vessel.teamNumber == 2)
		{
			num1 = this.miningStation.teamOneProgress;
			num = this.miningStation.teamTwoProgress;
		}
		this.infoWindow = new GuiWindow()
		{
			boundries = new Rect(-150f, -15f, 300f, 30f),
			isHidden = false,
			zOrder = 50,
			isClickTransparent = true
		};
		AndromedaGui.gui.AddWindow(this.infoWindow);
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("PvPDominationGui", "bar");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		this.infoWindow.AddGuiElement(guiTexture);
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("PvPDominationGui", "barGreen");
		guiTexture1.X = 24f;
		guiTexture1.Y = 12f;
		guiTexture1.boundries.set_width((float)(25 * num));
		this.infoWindow.AddGuiElement(guiTexture1);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("PvPDominationGui", "barRed");
		guiTexture2.X = (float)(275 - 25 * num1);
		guiTexture2.Y = 12f;
		guiTexture2.boundries.set_width((float)(25 * num1));
		this.infoWindow.AddGuiElement(guiTexture2);
		GuiTexture pOINTSOFPROGRESS = new GuiTexture();
		if (this.miningStation.get_OwnerTeam() == 0)
		{
			pOINTSOFPROGRESS.SetTexture("PvPDominationGui", "markerDefault");
			pOINTSOFPROGRESS.X = 146f;
		}
		else if (num <= num1)
		{
			pOINTSOFPROGRESS.SetTexture("PvPDominationGui", "markerRed");
			pOINTSOFPROGRESS.X = (float)(21 + 25 * (MiningStation.POINTS_OF_PROGRESS - num1));
		}
		else
		{
			pOINTSOFPROGRESS.SetTexture("PvPDominationGui", "markerGreen");
			pOINTSOFPROGRESS.X = (float)(21 + 25 * num);
		}
		pOINTSOFPROGRESS.Y = 0f;
		this.infoWindow.AddGuiElement(pOINTSOFPROGRESS);
	}

	private void Update()
	{
		this.screenPos = Camera.get_main().WorldToScreenPoint(base.get_transform().get_position());
		if (!this.miningStation.IsObjectInRange(NetworkScript.player.vessel))
		{
			this.HideMiningStationInfo();
		}
		else
		{
			this.ShowMiningStationInfo();
			this.infoWindow.boundries.set_x(this.screenPos.x - 150f);
			this.infoWindow.boundries.set_y((float)Screen.get_height() - this.screenPos.y + 15f);
		}
	}
}