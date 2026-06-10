using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCardCreator : MonoBehaviour
{
    public static GameObject CreateCard(Transform parent)
    {
        GameObject card = new GameObject("LevelCard");
        card.transform.SetParent(parent, false);

        RectTransform rt = card.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 190);

        Image bg = card.AddComponent<Image>();
        bg.color = new Color(0.06f, 0.12f, 0.22f, 1f);

        Button btn = card.AddComponent<Button>();

        LayoutElement le = card.AddComponent<LayoutElement>();
        le.minHeight = 190;
        le.preferredHeight = 190;

        // Sol renkli serit
        GameObject stripe = new GameObject("Stripe");
        stripe.transform.SetParent(card.transform, false);
        RectTransform stripeRt = stripe.AddComponent<RectTransform>();
        stripeRt.anchorMin = new Vector2(0, 0);
        stripeRt.anchorMax = new Vector2(0, 1);
        stripeRt.pivot = new Vector2(0, 0.5f);
        stripeRt.offsetMin = new Vector2(0, 0);
        stripeRt.offsetMax = new Vector2(6, 0);
        Image stripeImg = stripe.AddComponent<Image>();
        stripeImg.color = new Color(0.96f, 0.78f, 0.26f);

        // Bina ikonu
        GameObject bldObj = new GameObject("BuildingIcon");
        bldObj.transform.SetParent(card.transform, false);
        RectTransform bldRt = bldObj.AddComponent<RectTransform>();
        bldRt.anchorMin = new Vector2(0, 0.5f);
        bldRt.anchorMax = new Vector2(0, 0.5f);
        bldRt.pivot = new Vector2(0, 0.5f);
        bldRt.anchoredPosition = new Vector2(20, 0);
        bldRt.sizeDelta = new Vector2(110, 160);
        Image bldImg = bldObj.AddComponent<Image>();
        bldImg.color = new Color(0.11f, 0.30f, 0.43f, 1f);

        // Bina pencereleri
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                GameObject win = new GameObject("Win");
                win.transform.SetParent(bldObj.transform, false);
                RectTransform wrt = win.AddComponent<RectTransform>();
                wrt.anchorMin = new Vector2(0, 0);
                wrt.anchorMax = new Vector2(0, 0);
                wrt.pivot = new Vector2(0, 0);
                wrt.anchoredPosition = new Vector2(12 + col * 30, 14 + row * 28);
                wrt.sizeDelta = new Vector2(22, 20);
                Image wImg = win.AddComponent<Image>();
                wImg.color = new Color(0.22f, 0.53f, 0.86f, 1f);
            }
        }

        // Status Text
        GameObject statusObj = new GameObject("StatusText");
        statusObj.transform.SetParent(card.transform, false);
        RectTransform srt = statusObj.AddComponent<RectTransform>();
        srt.anchorMin = new Vector2(0, 1);
        srt.anchorMax = new Vector2(1, 1);
        srt.pivot = new Vector2(0.5f, 1);
        srt.offsetMin = new Vector2(145, -42);
        srt.offsetMax = new Vector2(-100, -8);
        TextMeshProUGUI statusTmp = statusObj.AddComponent<TextMeshProUGUI>();
        statusTmp.text = "OYNA";
        statusTmp.fontSize = 26;
        statusTmp.fontStyle = FontStyles.Bold;
        statusTmp.color = new Color(0.96f, 0.78f, 0.26f);

        // Title Text
        GameObject titleObj = new GameObject("TitleText");
        titleObj.transform.SetParent(card.transform, false);
        RectTransform trt = titleObj.AddComponent<RectTransform>();
        trt.anchorMin = new Vector2(0, 1);
        trt.anchorMax = new Vector2(1, 1);
        trt.pivot = new Vector2(0.5f, 1);
        trt.offsetMin = new Vector2(145, -105);
        trt.offsetMax = new Vector2(-100, -46);
        TextMeshProUGUI titleTmp = titleObj.AddComponent<TextMeshProUGUI>();
        titleTmp.text = "Bolum";
        titleTmp.fontSize = 34;
        titleTmp.fontStyle = FontStyles.Bold;
        titleTmp.color = Color.white;

        // Sub Text
        GameObject subObj = new GameObject("SubText");
        subObj.transform.SetParent(card.transform, false);
        RectTransform subrt = subObj.AddComponent<RectTransform>();
        subrt.anchorMin = new Vector2(0, 1);
        subrt.anchorMax = new Vector2(1, 1);
        subrt.pivot = new Vector2(0.5f, 1);
        subrt.offsetMin = new Vector2(145, -155);
        subrt.offsetMax = new Vector2(-100, -110);
        TextMeshProUGUI subTmp = subObj.AddComponent<TextMeshProUGUI>();
        subTmp.text = "12 blok - Hedef: 100 puan";
        subTmp.fontSize = 24;
        subTmp.color = new Color(0.55f, 0.67f, 0.78f);

        // Sag badge
        GameObject badgeObj = new GameObject("Badge");
        badgeObj.transform.SetParent(card.transform, false);
        RectTransform brt = badgeObj.AddComponent<RectTransform>();
        brt.anchorMin = new Vector2(1, 0.5f);
        brt.anchorMax = new Vector2(1, 0.5f);
        brt.pivot = new Vector2(1, 0.5f);
        brt.anchoredPosition = new Vector2(-16, 0);
        brt.sizeDelta = new Vector2(80, 60);
        Image badgeImg = badgeObj.AddComponent<Image>();
        badgeImg.color = new Color(0.20f, 0.28f, 0.08f);

        GameObject badgeTextObj = new GameObject("BadgeText");
        badgeTextObj.transform.SetParent(badgeObj.transform, false);
        RectTransform btrt = badgeTextObj.AddComponent<RectTransform>();
        btrt.anchorMin = Vector2.zero;
        btrt.anchorMax = Vector2.one;
        btrt.offsetMin = Vector2.zero;
        btrt.offsetMax = Vector2.zero;
        TextMeshProUGUI badgeTmp = badgeTextObj.AddComponent<TextMeshProUGUI>();
        badgeTmp.text = ">";
        badgeTmp.fontSize = 28;
        badgeTmp.alignment = TextAlignmentOptions.Center;
        badgeTmp.color = new Color(0.96f, 0.78f, 0.26f);

        // Ayirici cizgi
        GameObject divider = new GameObject("Divider");
        divider.transform.SetParent(card.transform, false);
        RectTransform drt = divider.AddComponent<RectTransform>();
        drt.anchorMin = new Vector2(0, 0);
        drt.anchorMax = new Vector2(1, 0);
        drt.pivot = new Vector2(0.5f, 0);
        drt.offsetMin = new Vector2(145, 0);
        drt.offsetMax = new Vector2(-16, 1);
        Image dImg = divider.AddComponent<Image>();
        dImg.color = new Color(0.12f, 0.22f, 0.35f);

        LevelCardUI cardUI = card.AddComponent<LevelCardUI>();
        cardUI.statusText = statusTmp;
        cardUI.titleText = titleTmp;
        cardUI.subText = subTmp;
        cardUI.cardBackground = bg;
        cardUI.button = btn;
        cardUI.buildingIcon = bldImg;
        cardUI.badgeText = badgeTmp;
        cardUI.badgeBackground = badgeImg;
        cardUI.stripe = stripeImg;

        return card;
    }
}