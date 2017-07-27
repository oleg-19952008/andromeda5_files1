using System;
using UnityEngine;

public class GlowEffect : MonoBehaviour
{
	public Material glowMaterial;

	public Shader glowReplaceShader;

	public GlowEffect.GlowMode glowMode;

	public GlowEffect.BlendMode blendMode;

	public int downsampleSize = 256;

	public int blurIterations = 4;

	public float blurSpread = 1f;

	public float glowStrength = 1.2f;

	public Color glowColorMultiplier = Color.get_white();

	private Camera shaderCamera;

	private Rect normalizedRect;

	private RenderTexture replaceRenderTexture;

	public GlowEffect()
	{
	}

	private void calculateGlow(RenderTexture source, RenderTexture destination)
	{
		if (this.glowMode >= GlowEffect.GlowMode.SimpleGlow)
		{
			Graphics.Blit(source, destination, this.glowMaterial, ((int)this.glowMode % (int)GlowEffect.GlowMode.SimpleGlow != (int)GlowEffect.GlowMode.AlphaGlow ? 3 : 4));
		}
		else
		{
			RenderTexture temporary = RenderTexture.GetTemporary(this.downsampleSize, this.downsampleSize, 0, 0);
			RenderTexture renderTexture = RenderTexture.GetTemporary(this.downsampleSize, this.downsampleSize, 0, 0);
			if (this.blurIterations % 2 != 0)
			{
				this.glowMaterial.SetTexture("_Glow", renderTexture);
			}
			else
			{
				this.glowMaterial.SetTexture("_Glow", temporary);
			}
			if ((int)this.glowMode % (int)GlowEffect.GlowMode.SimpleGlow != (int)GlowEffect.GlowMode.AlphaGlow)
			{
				Graphics.Blit(this.replaceRenderTexture, renderTexture, this.glowMaterial, 1);
			}
			else
			{
				Graphics.Blit(source, renderTexture, this.glowMaterial, 2);
			}
			for (int i = 1; i < this.blurIterations; i++)
			{
				if (i % 2 != 0)
				{
					temporary.DiscardContents();
					Graphics.Blit(renderTexture, temporary, this.glowMaterial, 1);
				}
				else
				{
					renderTexture.DiscardContents();
					Graphics.Blit(temporary, renderTexture, this.glowMaterial, 1);
				}
			}
			Graphics.Blit(source, destination, this.glowMaterial, 0);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
	}

	private void disableShaderKeywords()
	{
		Shader.DisableKeyword("GLOWEFFECT_BLEND_ADDITIVE");
		Shader.DisableKeyword("GLOWEFFECT_BLEND_SCREEN");
		Shader.DisableKeyword("GLOWEFFECT_BLEND_MULTIPLY");
		Shader.DisableKeyword("GLOWEFFECT_USE_MAINTEX");
		Shader.DisableKeyword("GLOWEFFECT_USE_MAINTEX_OFF");
		Shader.DisableKeyword("GLOWEFFECT_USE_GLOWTEX");
		Shader.DisableKeyword("GLOWEFFECT_USE_GLOWTEX_OFF");
		Shader.DisableKeyword("GLOWEFFECT_USE_GLOWCOLOR");
		Shader.DisableKeyword("GLOWEFFECT_USE_GLOWCOLOR_OFF");
		Shader.DisableKeyword("GLOWEFFECT_USE_VERTEXCOLOR");
		Shader.DisableKeyword("GLOWEFFECT_USE_VERTEXCOLOR_OFF");
		Shader.DisableKeyword("GLOWEFFECT_MULTIPLY_COLOR");
		Shader.DisableKeyword("GLOWEFFECT_MULTIPLY_COLOR_OFF");
	}

	public void OnDisable()
	{
		this.glowMaterial.set_mainTexture(null);
		base.get_camera().set_targetTexture(null);
		Object.DestroyObject(this.shaderCamera);
		this.disableShaderKeywords();
	}

	public void OnEnable()
	{
		// 
		// Current member / type: System.Void GlowEffect::OnEnable()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnEnable()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..(Expression , Instruction ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 291
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 48
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 93
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	public void OnPreRender()
	{
		// 
		// Current member / type: System.Void GlowEffect::OnPreRender()
		// File path: C:\Program Files (x86)\XS-Software\Andromeda V\Andromeda5v164\Andromeda5_Data\Managed\Assembly-CSharp.dll
		// 
		// Product version: 2017.2.502.1
		// Exception in: System.Void OnPreRender()
		// 
		// Ссылка на объект не указывает на экземпляр объекта.
		//    в ..(Expression , Instruction ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 291
		//    в ..(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Steps\FixBinaryExpressionsStep.cs:строка 48
		//    в Telerik.JustDecompiler.Decompiler.ExpressionDecompilerStep.(DecompilationContext ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\ExpressionDecompilerStep.cs:строка 93
		//    в ..(MethodBody ,  , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 88
		//    в ..(MethodBody , ILanguage ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\DecompilationPipeline.cs:строка 70
		//    в Telerik.JustDecompiler.Decompiler.Extensions.( , ILanguage , MethodBody , DecompilationContext& ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 95
		//    в Telerik.JustDecompiler.Decompiler.Extensions.(MethodBody , ILanguage , DecompilationContext& ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\Extensions.cs:строка 58
		//    в ..(ILanguage , MethodDefinition ,  ) в C:\Builds\556\Behemoth\ReleaseBranch Production Build NT\Sources\OpenSource\Cecil.Decompiler\Decompiler\WriterContextServices\BaseWriterContextService.cs:строка 117
		// 
		// mailto: JustDecompilePublicFeedback@telerik.com

	}

	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.calculateGlow(source, destination);
	}

	public void Start()
	{
		if (!SystemInfo.get_supportsImageEffects())
		{
			Debug.Log("Disabling the Glow Effect. Image effects are not supported (do you have Unity Pro?)");
			base.set_enabled(false);
		}
		this.normalizedRect = new Rect(0f, 0f, 1f, 1f);
	}

	public enum BlendMode
	{
		Additive,
		Multiply,
		Screen
	}

	public enum GlowMode
	{
		Glow,
		AlphaGlow,
		SimpleGlow,
		SimpleAlphaGlow
	}
}