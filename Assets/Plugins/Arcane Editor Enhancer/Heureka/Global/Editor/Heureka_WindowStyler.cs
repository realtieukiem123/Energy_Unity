using System;
using UnityEditor;
using UnityEngine;

namespace HeurekaGames
{
    public static class Heureka_WindowStyler
    {
        public static float HeaderHeight { get; private set; } = 24f;

        public static readonly Color clr_Black = new Color((26f / 256f), (26f / 256f), (26f / 256f), 1);
        public static readonly Color clr_Dark = new Color((26f / 256f), (26f / 256f), (26f / 256f), 1);
        public static readonly Color clr_dBlue = new Color((255f / 256f), (0f / 256f), (31f / 256f), 1);
        public static readonly Color clr_lBlue = new Color((255f / 256f), (0f / 256f), (31f / 256f), 1);
        public static readonly Color clr_White = new Color((217f / 256f), (220f / 256f), (214f / 256f), 1);
        public static readonly Color clr_Red = new Color((255f / 256f), (0f / 256f), (31f / 256f), 1);
        public static readonly Color clr_middleGreen = new Color((134f / 256f), (0f / 256f), (16f / 256f), 1);


        public static void DrawGlobalHeader(Color color, string label, string version = "", Action additionHeaderContent = null)
        {
            EditorGUI.DrawRect(new Rect(0, 0, EditorGUIUtility.currentViewWidth, HeaderHeight), color);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(4);

            if (Heureka_EditorData.Instance.HeadlineStyle != null)
                GUILayout.Label(label + "  ", Heureka_EditorData.Instance.HeadlineStyle, GUILayout.ExpandWidth(false));

            if (version != "")
            {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.Space();
                GUILayout.Label(version, EditorStyles.whiteLabel);
                EditorGUILayout.EndVertical();
                additionHeaderContent?.Invoke();
            }

            if(additionHeaderContent==null)
                GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
        }

        public static void DrawCenteredImage(EditorWindow window, Texture image)
        {
            if (window == null)
                return;

            GUI.Box(new Rect((window.position.width * .5f) - (image.width * .5f), (window.position.height * .5f) - (image.height * .5f), image.width, image.height), image, GUIStyle.none);
        }

        public static void DrawCenteredMessage(EditorWindow window, Texture icon, float msgWidth, float msgHeight, string messsage)
        {
            if (window == null)
                return;

            Vector2 iconSize = Vector2.zero;
            if (icon != null)
                iconSize = new Vector2(icon.width, icon.height);

            Vector2 outerBoxSize = new Vector2(msgWidth, msgHeight);
            float frameWidth = 5;
            Vector2 innerBoxSize = new Vector2(outerBoxSize.x - frameWidth * 2, outerBoxSize.y - frameWidth * 2);

            Vector2 rectStartPos = new Vector2((window.position.width * .5f) - (outerBoxSize.x * .5f), (window.position.height * .5f) - (outerBoxSize.y * .5f) + (iconSize.y * .5f));

            EditorGUI.DrawRect(new Rect(rectStartPos.x, rectStartPos.y, outerBoxSize.x, outerBoxSize.y), Heureka_WindowStyler.clr_Black);
            EditorGUI.DrawRect(new Rect(rectStartPos.x + frameWidth, rectStartPos.y + frameWidth, innerBoxSize.x, innerBoxSize.y), Heureka_WindowStyler.clr_dBlue);

            float bounds = 20;
            Vector2 logoStartPos = rectStartPos + new Vector2(bounds, bounds);
            GUI.Box(new Rect(logoStartPos.x, logoStartPos.y, iconSize.x, iconSize.y), icon, GUIStyle.none);

            Vector2 labelStartPos = logoStartPos + new Vector2(iconSize.x + frameWidth * 2, 0);
            float textWidth = innerBoxSize.x - iconSize.x - (bounds * 2);
            float textHeight = 30;

            string[] lines = messsage.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
                );

            GUI.Label(new Rect(labelStartPos.x, labelStartPos.y, textWidth, textHeight), lines[0], Heureka_EditorData.Instance.HeadlineStyle);

            var blackStyle = new GUIStyle(EditorStyles.label);
            blackStyle.normal.textColor = Color.black;

            labelStartPos.y += 20;
            for (int i = 1; i < lines.Length; i++)
            {
                GUI.Label(new Rect(labelStartPos.x, labelStartPos.y, textWidth, textHeight), lines[i], blackStyle);
                labelStartPos.y += 16;
            }
        }

        private static GUIStyle imageBtnStyle;
        public static GUIStyle ImageBtnStyle
        {
            get
            {
                if (imageBtnStyle == null)
                {
                    imageBtnStyle = new GUIStyle(GUI.skin.button);
                    imageBtnStyle.padding = new RectOffset(0, 0, 0, 0);
                }
                return imageBtnStyle;
            }
            set => imageBtnStyle = value;
        }
    }
}