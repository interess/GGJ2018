using UnityEditor;
using UnityEngine;

namespace FreakingEditor
{
	public static class Fstyle
	{
		public static int margin = 5;
		public static int padding = 2;
		public static float defaultHeight = 17f;

		public static GUIStyle boldLabelLeft;
		public static GUIStyle boldLabelRight;
		public static GUIStyle boldLabelCenter;
		public static GUIStyle label;
		public static GUIStyle labelLeft;
		public static GUIStyle labelRight;
		public static GUIStyle labelCenter;
		public static GUIStyle labelCenterInfo;
		public static GUIStyle miniLabelLeft;
		public static GUIStyle miniLabelRight;
		public static GUIStyle miniLabelCenter;

		public static GUIStyle textField;
		public static GUIStyle textFieldLeft;
		public static GUIStyle textFieldRight;

		public static GUIStyle button;
		public static GUIStyle buttonRight;
		public static GUIStyle buttonCenter;
		public static GUIStyle buttonLeft;

		public static GUIStyle popup;

		public static GUIStyle toggle;

		public static GUIStyle foldout;

		public static GUIStyle box;
		public static GUIStyle offsetBox;

		static Fstyle()
		{
			EditorGUIUtility.labelWidth = 1f;
			EditorGUIUtility.fieldWidth = 1f;

			#region Labels

			label = new GUIStyle(EditorStyles.label);
			label.alignment = TextAnchor.MiddleLeft;
			label.margin = new RectOffset(margin, margin, 0, margin);
			label.padding = new RectOffset(padding, padding, 0, 5);
			label.clipping = TextClipping.Overflow;
			label.stretchWidth = true;
			label.fontStyle = FontStyle.Normal;
			label.fixedHeight = defaultHeight;

			labelLeft = new GUIStyle(label);
			labelLeft.alignment = TextAnchor.MiddleLeft;
			labelLeft.margin = new RectOffset(margin, 0, 0, margin);

			labelRight = new GUIStyle(label);
			labelRight.alignment = TextAnchor.MiddleRight;
			labelRight.margin = new RectOffset(0, margin, 0, margin);

			labelCenter = new GUIStyle(label);
			labelCenter.alignment = TextAnchor.MiddleCenter;

			miniLabelLeft = new GUIStyle(labelLeft);
			miniLabelLeft.fontSize = 9;

			miniLabelRight = new GUIStyle(labelRight);
			miniLabelRight.fontSize = 9;

			miniLabelCenter = new GUIStyle(labelCenter);
			miniLabelCenter.fontSize = 9;

			boldLabelLeft = new GUIStyle(labelLeft);
			boldLabelLeft.fontStyle = FontStyle.Bold;

			boldLabelRight = new GUIStyle(labelRight);
			boldLabelRight.fontStyle = FontStyle.Bold;

			boldLabelCenter = new GUIStyle(labelCenter);
			boldLabelCenter.fontStyle = FontStyle.Bold;

			labelCenterInfo = new GUIStyle(labelCenter);
			labelCenterInfo.wordWrap = true;
			labelCenterInfo.stretchWidth = true;
			labelCenterInfo.stretchHeight = true;
			labelCenterInfo.fixedHeight = 0;

			#endregion

			#region Buttons

			button = new GUIStyle(EditorStyles.miniButton);
			button.alignment = TextAnchor.MiddleCenter;
			button.padding = new RectOffset(padding, padding, 0, 0);
			button.margin = new RectOffset(margin, margin, 0, margin);
			button.fixedHeight = defaultHeight;
			button.stretchWidth = true;

			buttonRight = new GUIStyle(EditorStyles.miniButtonRight);
			buttonRight.alignment = TextAnchor.MiddleCenter;
			buttonRight.padding = new RectOffset(padding, padding, 0, 0);
			buttonRight.fixedHeight = defaultHeight;
			buttonRight.margin = new RectOffset(0, margin, 0, margin);
			buttonRight.stretchWidth = true;

			buttonCenter = new GUIStyle(EditorStyles.miniButtonMid);
			buttonCenter.alignment = TextAnchor.MiddleCenter;
			buttonCenter.padding = new RectOffset(padding, padding, 0, 0);
			buttonCenter.margin = new RectOffset(0, 0, 0, margin);
			buttonCenter.fixedHeight = defaultHeight;
			buttonCenter.stretchWidth = true;

			buttonLeft = new GUIStyle(EditorStyles.miniButtonLeft);
			buttonLeft.alignment = TextAnchor.MiddleCenter;
			buttonLeft.padding = new RectOffset(padding, padding, 0, 0);
			buttonLeft.fixedHeight = defaultHeight;
			buttonLeft.margin = new RectOffset(margin, 0, 0, margin);
			buttonLeft.stretchWidth = true;

			#endregion

			#region Text fields

			textField = new GUIStyle(EditorStyles.textField);
			textField.alignment = TextAnchor.MiddleLeft;
			textField.padding = new RectOffset(padding, padding, 0, 0);
			textField.margin = new RectOffset(margin, margin, 0, margin);
			textField.fixedHeight = defaultHeight;
			textField.stretchWidth = true;

			textFieldLeft = new GUIStyle(textField);
			textFieldLeft.margin = new RectOffset(margin, 0, 0, margin);

			textFieldRight = new GUIStyle(textField);
			textFieldRight.margin = new RectOffset(0, margin, 0, margin);

			#endregion

			#region Popup

			popup = new GUIStyle(EditorStyles.popup);
			popup.padding = new RectOffset(padding * 2, padding * 2, 0, 0);
			popup.margin = new RectOffset(margin, margin, 0, margin);
			popup.fixedHeight = defaultHeight;
			popup.stretchWidth = true;

			#endregion

			#region Toggle

			toggle = new GUIStyle(EditorStyles.toggle);
			toggle.padding = new RectOffset(15 + padding, padding, 1, 0);
			toggle.margin = new RectOffset(margin, margin, 0, margin);
			toggle.fixedHeight = defaultHeight;
			toggle.stretchWidth = true;

			#endregion

			#region Foldout

			foldout = new GUIStyle(EditorStyles.foldout);
			foldout.padding = new RectOffset(padding + 14, padding, 1, padding);
			foldout.margin = new RectOffset(6 + margin, margin, margin, margin);
			foldout.fixedHeight = defaultHeight;
			foldout.stretchWidth = true;
			foldout.fontStyle = FontStyle.Bold;

			#endregion

			#region Box

			offsetBox = new GUIStyle();
			offsetBox.stretchWidth = true;
			offsetBox.padding = new RectOffset(padding, padding, padding, padding);
			offsetBox.margin = new RectOffset(0, 0, 4, 0);

			box = new GUIStyle(UnityEngine.GUI.skin.box);
			box.stretchWidth = true;
			box.clipping = TextClipping.Clip;
			box.padding = new RectOffset(padding, padding, padding, padding);
			box.margin = new RectOffset(3, 2, 4, 0);

			#endregion
		}
	}
}
