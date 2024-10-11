using System.Collections.Generic;
using UnityEngine;
using YGame.Scripts.Addressable;
using YGame.Scripts.Common;
using YGame.Scripts.Log;

namespace YGame.Scripts.UI
{
   public class UIManager : MonoSingleton<UIManager>
   {
      private Canvas mainCanvas;
      public Canvas MainCanvas {
         get
         {
            if (mainCanvas == null)
            {
               InitCanvas();
               return mainCanvas;
            }
            return mainCanvas;
         }
            
      }
      public Camera UICamera { get; set; }

      public Dictionary<string, UIBase> UIPanelDic = new Dictionary<string, UIBase>(); 
      private void InitCanvas()
      {
         mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
         UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
         mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;
         mainCanvas.worldCamera = UICamera;
         mainCanvas.planeDistance = 10;
         mainCanvas.sortingOrder = 1;
            
      }


      public void Show(string uiName)
      {
         if (UIPanelDic.TryGetValue(uiName, out UIBase ui))
         {
            ui.Show();
            YLogger.LogInfo($"UI : {ui.UIName} show successfully");
         }
         else
         {
            this.LoadUIAsync(uiName, (ui) =>
            {
               var uiInstance = Instantiate(ui, UIManager.Instance.MainCanvas.transform);
               var uiBase = uiInstance.GetComponent<UIBase>();
               uiBase.UIName = uiName;
               AddUI(uiName, uiBase);
               uiBase.Show();
               YLogger.LogInfo($"UI : {uiBase.UIName} show successfully");
            });
         
         }
      }

      public void Hide(string uiName)
      {
         if (UIPanelDic.TryGetValue(uiName, out UIBase ui))
         {
            ui.Hide();
            RemoveUI(uiName);
            YLogger.LogInfo($"UI : {ui.UIName} hide successfully");
         }
      }

      public void AddUI(string uiName, UIBase ui)
      {
         if (UIPanelDic.ContainsKey(uiName))
         {
            YLogger.LogInfo($"UI : {ui.UIName} already exists");
            return;
         }
            
         UIPanelDic.Add(uiName, ui);
         YLogger.LogInfo($"UI : {ui.UIName} added successfully");
      }

      public void RemoveUI(string uiName)
      {
         if (UIPanelDic.TryGetValue(uiName, out UIBase ui))
         {
            UIPanelDic.Remove(uiName);
            Destroy(ui.gameObject);
            YLogger.LogInfo($"UI : {uiName} removed successfully");
         }
      }
   
   
   
   }
}
