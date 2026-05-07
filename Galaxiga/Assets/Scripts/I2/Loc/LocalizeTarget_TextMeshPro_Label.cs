using System;
using TMPro;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_TextMeshPro_Label : LocalizeTarget<TextMeshPro>
	{
		static LocalizeTarget_TextMeshPro_Label()
		{
			LocalizeTarget_TextMeshPro_Label.AutoRegister();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<TextMeshPro, LocalizeTarget_TextMeshPro_Label>
			{
				Name = "TextMeshPro Label",
				Priority = 100
			});
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Font;
		}

		public override bool CanUseSecondaryTerm()
		{
			return true;
		}

		public override bool AllowMainTermToBeRTL()
		{
			return true;
		}

		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = ((!this.mTarget) ? null : this.mTarget.text);
			secondaryTerm = ((!(this.mTarget.font != null)) ? string.Empty : this.mTarget.font.name);
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			TMP_FontAsset tmp_FontAsset = cmp.GetSecondaryTranslatedObj<TMP_FontAsset>(ref mainTranslation, ref secondaryTranslation);
			if (tmp_FontAsset != null)
			{
				LocalizeTarget_TextMeshPro_Label.SetFont(this.mTarget, tmp_FontAsset);
			}
			else
			{
				Material secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Material>(ref mainTranslation, ref secondaryTranslation);
				if (secondaryTranslatedObj != null && this.mTarget.fontMaterial != secondaryTranslatedObj)
				{
					if (!secondaryTranslatedObj.name.StartsWith(this.mTarget.font.name, StringComparison.Ordinal))
					{
						tmp_FontAsset = LocalizeTarget_TextMeshPro_Label.GetTMPFontFromMaterial(cmp, (!secondaryTranslation.EndsWith(secondaryTranslatedObj.name, StringComparison.Ordinal)) ? secondaryTranslatedObj.name : secondaryTranslation);
						if (tmp_FontAsset != null)
						{
							LocalizeTarget_TextMeshPro_Label.SetFont(this.mTarget, tmp_FontAsset);
						}
					}
					LocalizeTarget_TextMeshPro_Label.SetMaterial(this.mTarget, secondaryTranslatedObj);
				}
			}
			if (this.mInitializeAlignment)
			{
				this.mInitializeAlignment = false;
				this.mAlignmentWasRTL = LocalizationManager.IsRight2Left;
				LocalizeTarget_TextMeshPro_Label.InitAlignment_TMPro(this.mAlignmentWasRTL, this.mTarget.alignment, out this.mAlignment_LTR, out this.mAlignment_RTL);
			}
			else
			{
				TextAlignmentOptions textAlignmentOptions;
				TextAlignmentOptions textAlignmentOptions2;
				LocalizeTarget_TextMeshPro_Label.InitAlignment_TMPro(this.mAlignmentWasRTL, this.mTarget.alignment, out textAlignmentOptions, out textAlignmentOptions2);
				if ((this.mAlignmentWasRTL && this.mAlignment_RTL != textAlignmentOptions2) || (!this.mAlignmentWasRTL && this.mAlignment_LTR != textAlignmentOptions))
				{
					this.mAlignment_LTR = textAlignmentOptions;
					this.mAlignment_RTL = textAlignmentOptions2;
				}
				this.mAlignmentWasRTL = LocalizationManager.IsRight2Left;
			}
			if (mainTranslation != null && this.mTarget.text != mainTranslation)
			{
				if (mainTranslation != null && cmp.CorrectAlignmentForRTL)
				{
					this.mTarget.alignment = ((!LocalizationManager.IsRight2Left) ? this.mAlignment_LTR : this.mAlignment_RTL);
					this.mTarget.isRightToLeftText = LocalizationManager.IsRight2Left;
					if (LocalizationManager.IsRight2Left)
					{
						mainTranslation = I2Utils.ReverseText(mainTranslation);
					}
				}
				this.mTarget.text = mainTranslation;
			}
		}

		internal static TMP_FontAsset GetTMPFontFromMaterial(Localize cmp, string matName)
		{
			string text = " .\\/-[]()";
			int i = matName.Length - 1;
			while (i > 0)
			{
				while (i > 0 && text.IndexOf(matName[i]) >= 0)
				{
					i--;
				}
				if (i <= 0)
				{
					break;
				}
				string translation = matName.Substring(0, i + 1);
				TMP_FontAsset @object = cmp.GetObject<TMP_FontAsset>(translation);
				if (@object != null)
				{
					return @object;
				}
				while (i > 0 && text.IndexOf(matName[i]) < 0)
				{
					i--;
				}
			}
			return null;
		}

		internal static void InitAlignment_TMPro(bool isRTL, TextAlignmentOptions alignment, out TextAlignmentOptions alignLTR, out TextAlignmentOptions alignRTL)
		{
			alignRTL = alignment;
			alignLTR = alignment;
			if (isRTL)
			{
				switch (alignment)
				{
				case TextAlignmentOptions.TopLeft:
					alignLTR = TextAlignmentOptions.TopRight;
					break;
				default:
					switch (alignment)
					{
					case TextAlignmentOptions.Left:
						alignLTR = TextAlignmentOptions.Right;
						break;
					default:
						switch (alignment)
						{
						case TextAlignmentOptions.BottomLeft:
							alignLTR = TextAlignmentOptions.BottomRight;
							break;
						default:
							switch (alignment)
							{
							case TextAlignmentOptions.BaselineLeft:
								alignLTR = TextAlignmentOptions.BaselineRight;
								break;
							default:
								switch (alignment)
								{
								case TextAlignmentOptions.MidlineLeft:
									alignLTR = TextAlignmentOptions.MidlineRight;
									break;
								default:
									switch (alignment)
									{
									case TextAlignmentOptions.CaplineLeft:
										alignLTR = TextAlignmentOptions.CaplineRight;
										break;
									case TextAlignmentOptions.CaplineRight:
										alignLTR = TextAlignmentOptions.CaplineLeft;
										break;
									}
									break;
								case TextAlignmentOptions.MidlineRight:
									alignLTR = TextAlignmentOptions.MidlineLeft;
									break;
								}
								break;
							case TextAlignmentOptions.BaselineRight:
								alignLTR = TextAlignmentOptions.BaselineLeft;
								break;
							}
							break;
						case TextAlignmentOptions.BottomRight:
							alignLTR = TextAlignmentOptions.BottomLeft;
							break;
						}
						break;
					case TextAlignmentOptions.Right:
						alignLTR = TextAlignmentOptions.Left;
						break;
					}
					break;
				case TextAlignmentOptions.TopRight:
					alignLTR = TextAlignmentOptions.TopLeft;
					break;
				}
			}
			else
			{
				switch (alignment)
				{
				case TextAlignmentOptions.TopLeft:
					alignRTL = TextAlignmentOptions.TopRight;
					break;
				default:
					switch (alignment)
					{
					case TextAlignmentOptions.Left:
						alignRTL = TextAlignmentOptions.Right;
						break;
					default:
						switch (alignment)
						{
						case TextAlignmentOptions.BottomLeft:
							alignRTL = TextAlignmentOptions.BottomRight;
							break;
						default:
							switch (alignment)
							{
							case TextAlignmentOptions.BaselineLeft:
								alignRTL = TextAlignmentOptions.BaselineRight;
								break;
							default:
								switch (alignment)
								{
								case TextAlignmentOptions.MidlineLeft:
									alignRTL = TextAlignmentOptions.MidlineRight;
									break;
								default:
									switch (alignment)
									{
									case TextAlignmentOptions.CaplineLeft:
										alignRTL = TextAlignmentOptions.CaplineRight;
										break;
									case TextAlignmentOptions.CaplineRight:
										alignRTL = TextAlignmentOptions.CaplineLeft;
										break;
									}
									break;
								case TextAlignmentOptions.MidlineRight:
									alignRTL = TextAlignmentOptions.MidlineLeft;
									break;
								}
								break;
							case TextAlignmentOptions.BaselineRight:
								alignRTL = TextAlignmentOptions.BaselineLeft;
								break;
							}
							break;
						case TextAlignmentOptions.BottomRight:
							alignRTL = TextAlignmentOptions.BottomLeft;
							break;
						}
						break;
					case TextAlignmentOptions.Right:
						alignRTL = TextAlignmentOptions.Left;
						break;
					}
					break;
				case TextAlignmentOptions.TopRight:
					alignRTL = TextAlignmentOptions.TopLeft;
					break;
				}
			}
		}

		internal static void SetFont(TMP_Text label, TMP_FontAsset newFont)
		{
			if (label.font != newFont)
			{
				label.font = newFont;
			}
			if (label.linkedTextComponent != null)
			{
				LocalizeTarget_TextMeshPro_Label.SetFont(label.linkedTextComponent, newFont);
			}
		}

		internal static void SetMaterial(TMP_Text label, Material newMat)
		{
			if (label.fontSharedMaterial != newMat)
			{
				label.fontSharedMaterial = newMat;
			}
			if (label.linkedTextComponent != null)
			{
				LocalizeTarget_TextMeshPro_Label.SetMaterial(label.linkedTextComponent, newMat);
			}
		}

		private TextAlignmentOptions mAlignment_RTL = TextAlignmentOptions.Right;

		private TextAlignmentOptions mAlignment_LTR = TextAlignmentOptions.Left;

		private bool mAlignmentWasRTL;

		private bool mInitializeAlignment = true;
	}
}
