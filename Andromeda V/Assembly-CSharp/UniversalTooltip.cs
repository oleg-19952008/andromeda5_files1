using System;
using UnityEngine;

public class UniversalTooltip
{
	public UniversalTooltip()
	{
	}

	public static GuiWindow CreateTooltip(object prm)
	{
		string str = (string)((EventHandlerParam)prm).customData;
		GuiElement guiElement = (GuiElement)((EventHandlerParam)prm).customData2;
		Vector2 vector2 = new Vector2(guiElement.X + guiElement.boundries.get_width() / 2f, guiElement.Y);
		GuiLabel guiLabel = new GuiLabel()
		{
			boundries = new Rect(20f, 20f, 200f, 10f),
			WordWrap = true,
			Font = GuiLabel.FontBold,
			FontSize = 12,
			text = str,
			Alignment = 4
		};
		guiLabel.boundries.set_height(guiLabel.TextHeight);
		if (guiLabel.TextWidth < 200f)
		{
			guiLabel.boundries.set_width(guiLabel.TextWidth);
		}
		GuiWindow guiWindow = new GuiWindow()
		{
			boundries = new Rect(0f, 0f, Math.Max(guiLabel.boundries.get_width() + 40f, 52f), Math.Max(guiLabel.boundries.get_height() + 40f, 50f))
		};
		guiWindow.boundries.set_x(vector2.x - guiWindow.boundries.get_width() / 2f);
		guiWindow.boundries.set_y(vector2.y - guiWindow.boundries.get_height());
		guiWindow.zOrder = 230;
		guiWindow.isHidden = false;
		GuiTexture guiTexture = new GuiTexture();
		guiTexture.SetTexture("FrameworkGUI", "gui_tooltip_top_left");
		guiTexture.X = 0f;
		guiTexture.Y = 0f;
		GuiTexture _width = new GuiTexture();
		_width.SetTexture("FrameworkGUI", "gui_tooltip_top_right");
		_width.X = guiWindow.boundries.get_width() - 26f;
		_width.Y = 0f;
		GuiTexture guiTexture1 = new GuiTexture();
		guiTexture1.SetTexture("FrameworkGUI", "gui_tooltip_top_middle");
		guiTexture1.X = 26f;
		guiTexture1.Y = 0f;
		guiTexture1.boundries.set_width(guiWindow.boundries.get_width() - 52f);
		GuiTexture _height = new GuiTexture();
		_height.SetTexture("FrameworkGUI", "gui_tooltip_bottom_left");
		_height.X = 0f;
		_height.Y = guiWindow.boundries.get_height() - 25f;
		GuiTexture _width1 = new GuiTexture();
		_width1.SetTexture("FrameworkGUI", "gui_tooltip_bottom_right");
		_width1.X = guiWindow.boundries.get_width() - 26f;
		_width1.Y = guiWindow.boundries.get_height() - 25f;
		GuiTexture _height1 = new GuiTexture();
		_height1.SetTexture("FrameworkGUI", "gui_tooltip_bottom_middle");
		_height1.X = 26f;
		_height1.Y = guiWindow.boundries.get_height() - 25f;
		_height1.boundries.set_width(guiWindow.boundries.get_width() - 52f);
		GuiTexture guiTexture2 = new GuiTexture();
		guiTexture2.SetTexture("FrameworkGUI", "gui_tooltip_left_middle");
		guiTexture2.X = 0f;
		guiTexture2.Y = 25f;
		guiTexture2.boundries.set_height(guiWindow.boundries.get_height() - 50f);
		GuiTexture _width2 = new GuiTexture();
		_width2.SetTexture("FrameworkGUI", "gui_tooltip_right_middle");
		_width2.X = guiWindow.boundries.get_width() - 25f;
		_width2.Y = 25f;
		_width2.boundries.set_height(guiWindow.boundries.get_height() - 50f);
		GuiTexture guiTexture3 = new GuiTexture();
		guiTexture3.SetTexture("FrameworkGUI", "gui_tooltip_background");
		guiTexture3.X = 25f;
		guiTexture3.Y = 25f;
		guiTexture3.boundries.set_width(guiWindow.boundries.get_width() - 50f);
		guiTexture3.boundries.set_height(guiWindow.boundries.get_height() - 50f);
		guiWindow.AddGuiElement(guiTexture);
		guiWindow.AddGuiElement(_width);
		guiWindow.AddGuiElement(guiTexture1);
		guiWindow.AddGuiElement(_height);
		guiWindow.AddGuiElement(_width1);
		guiWindow.AddGuiElement(_height1);
		guiWindow.AddGuiElement(guiTexture2);
		guiWindow.AddGuiElement(_width2);
		guiWindow.AddGuiElement(guiTexture3);
		guiWindow.AddGuiElement(guiLabel);
		return guiWindow;
	}
}