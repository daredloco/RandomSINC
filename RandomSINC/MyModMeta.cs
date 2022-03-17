using System;
using UnityEngine;
using UnityEngine.UI;

namespace RandomSINC
{
    internal class MyModMeta : ModMeta
    {
        public override void ConstructOptionsScreen(RectTransform parent, bool inGame)
        {
            //Text text = WindowManager.SpawnLabel();
            //text.text = "This mod randomizes everything.";
            //WindowManager.AddElementToElement(text.gameObject, parent.gameObject, new Rect(0f, 0f, 400f, 128f),
            //    new Rect(0f, 0f, 0f, 0f));

            instance = this;
        }

        public override string Name => "Random SINC";
        public static MyModMeta instance;
    }
}
