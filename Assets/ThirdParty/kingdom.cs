using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
//using Vuforia;

namespace kingdom
{
    /// <summary>
    /// Txt格式文本处理
    /// </summary>
    public class TxtTreatment
    {
        public static string GetInfo(int ID)
        {
            string value = null;
            TextAsset txt = Resources.Load("Info/ParameterTable") as TextAsset;
            string[] taskinfoArray = txt.ToString().Split('\n');
            for (int i = 0; i < taskinfoArray.Length; i++)
            {
                string[] Fontlist = taskinfoArray[i].Split('|');
                int id = int.Parse(Fontlist[0]);
                while (ID == id)
                {
                    value = Fontlist[1];
                    break;
                }
            }
            return value;
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="文件创建目录"></param>
        /// <param name="文件的名称"></param>
        /// <param name="写入的内容"></param>
        public static void CreateFile(string path, string name, string info)
        {
            StreamWriter sw;
            FileInfo t = new FileInfo(path + "//" + name);
            if (!t.Exists)
            {
                //如果此文件不存在则创建
                sw = t.CreateText();
            }
            else
            {
                //如果此文件存在则打开
                sw = t.AppendText();
            }
            //以行的形式写入信息
            sw.WriteLine(info);
            //关闭流
            sw.Close();
            //销毁流
            sw.Dispose();
        }
        /**
       * path：读取文件的路径
       * name：读取文件的名称
       */
        public static ArrayList LoadFile(string path, string name)
        {
            //使用流的形式读取
            StreamReader sr = null;
            sr = File.OpenText(path + "//" + name);
            string line;
            ArrayList arrlist = new ArrayList();
            while ((line = sr.ReadLine()) != null)
            {
                //一行一行的读取
                //将每一行的内容存入数组链表容器中
                arrlist.Add(line);
            }
            //关闭流
            sr.Close();
            //销毁流
            sr.Dispose();
            //将数组链表容器返回
            return arrlist;
        }
        /**
        * path：删除文件的路径
        * name：删除文件的名称
        */
        public static void DeleteFile(string path, string name)
        {
            File.Delete(path + "//" + name);
        }
    }
    /// <summary>
    /// 时间处理类
    /// </summary>
    public class TimeTreatment
    {
        /// <summary>
        /// 延时调用
        /// </summary>
        /// <param name="action"></param>
        /// <param name="delaySeconds"></param>
        /// <returns></returns>
        public static IEnumerator DelayToInvokeDo(Action action, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);

            action();
        }
        /// <summary>
        /// 时间处理方法.
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        public static string TimeConversion(string Time)
        {
            string a = "";
            DateTime t1 = Convert.ToDateTime(Time);
            TimeSpan ts = DateTime.Now.Subtract(t1);
            if (ts.Days > 0)
                a = ts.Days.ToString() + "天前";
            else if (ts.Hours > 0)
                a = ts.Hours.ToString() + "小时前";
            else if (ts.Minutes > 0)
                a = ts.Minutes.ToString() + "分钟前";
            else if (ts.Seconds < 3)
                a = "刚刚";
            else
                a = ts.Seconds.ToString() + "秒前";
            return a;
        }

        internal static void DelayToInvokeDo(Action p)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// 场景处理类
    /// </summary>
    public class SceneTreatment
    {

    }
    /// <summary>
    /// 简单动画控制类
    /// </summary>
    public enum tween_How
    {
        position = 1,
        rotation = 2,
        scale = 3,
    }
    public class EasyAnimtationTreatment
    {
        #region 位移 旋转 缩放
        public static void TweenPosition(GameObject obj, Vector3 begin, Vector3 end, float BtoE_time)
        {
            Tween(obj, begin, end, BtoE_time).How = (int)tween_How.position;
        }
        public static void TweenRotation(GameObject obj, Vector3 begin, Vector3 end, float BtoE_time)
        {
            Tween(obj, begin, end, BtoE_time).How = (int)tween_How.rotation;
        }
        public static void TweenScale(GameObject obj, Vector3 begin, Vector3 end, float BtoE_time)
        {
            Tween(obj, begin, end, BtoE_time).How = (int)tween_How.scale;
        }

        static C_Tween_obj Tween(GameObject obj, Vector3 begin, Vector3 end, float BtoE_time)
        {
            C_Tween_obj tween = obj.AddComponent<C_Tween_obj>();

            tween.begin = begin;
            tween.end = end;
            tween.time = BtoE_time;
            return tween;
        }
        #endregion
        public class C_Tween_obj : MonoBehaviour
        {
            public Vector3 begin;
            public Vector3 end;
            public float time;

            public int How;

            void Start()
            {
                switch (How)
                {
                    case (int)tween_How.position: StartCoroutine(_TweenPosition()); break;
                    case (int)tween_How.rotation: StartCoroutine(_TweenRotation()); break;
                    case (int)tween_How.scale: StartCoroutine(_TweenScale()); break;
                }
            }
            public IEnumerator _TweenPosition()
            {
                float timer = 0, lerp = 0;
                while (lerp < 1)
                {
                    timer += Time.deltaTime;
                    lerp = timer / time;
                    transform.localPosition = Vector3.Lerp(begin, end, lerp);
                    yield return null;
                }
                Destroy(this);
            }
            public IEnumerator _TweenRotation()
            {

                float timer = 0, lerp = 0;
                while (lerp < 1)
                {
                    timer += Time.deltaTime;
                    lerp = timer / time;
                    transform.localEulerAngles = Vector3.Lerp(begin, end, lerp);
                    yield return null;
                }
                Destroy(this);

            }
            public IEnumerator _TweenScale()
            {

                float timer = 0, lerp = 0;
                while (lerp < 1)
                {
                    timer += Time.deltaTime;
                    lerp = timer / time;
                    transform.localScale = Vector3.Lerp(begin, end, lerp);
                    yield return null;
                }
                Destroy(this);
            }
        }
    }
    /// <summary>
    /// 弹窗处理类
    /// </summary>
    public class PopTreatment : MonoBehaviour
    {
        public static GameObject obj;
        /// <summary>
        /// 普通提示弹窗,弹出提示后消失
        /// </summary>
        /// <param name="message"></param>
        /// <param name="position"></param>
        /// <param name="WaitDestoryTime"></param>
        public static void StandardShow(string message = null, Vector3? position = null, int WaitDestoryTime = 1)
        {
            if (GameObject.Find("StandarShow") != null)
                return;
            var parent = GameObject.Find("Canvas").transform;
            obj = Instantiate(Resources.Load("StandarShow") as GameObject);
            if (message != null)
            {
                obj.transform.FindChild("Text").GetComponent<Text>().text = message;
            }
            else
            {
                obj.transform.FindChild("Text").GetComponent<Text>().text = "当前字符串为:null";
            }

            if (parent != null)
            {
                obj.transform.SetParent(parent, false);
            }
            if (position == null)
            {
                position = Vector3.zero;
            }
            DestoryObj ds = obj.AddComponent<DestoryObj>();
            ds.SetDestoryInfo(obj, WaitDestoryTime, obj);
            obj.transform.SetAsLastSibling();
            obj.transform.localPosition = (Vector3)position;
            obj.name = "StandarShow";
            obj.SetActive(true);
        }
        /// <summary>
        /// 当选择了继续选项
        /// </summary>
        public static UnityAction SelectPopShowTrueEvent;
        /// <summary>
        /// 当选择了取消选项
        /// </summary>
        public static UnityAction SelectPopShowErrorEvent;
        /// <summary>
        /// 带选项的弹窗
        /// </summary>
        public static void SelectPopShow(string TitleString = null, Vector3? position = null, bool TrueCallback = false, bool ErrorCallback = false, int WaitDestoryTime = 1, string ErrorText = null, string TrueText = null)
        {
            var parent = GameObject.Find("Canvas").transform;
            obj = Instantiate(Resources.Load("PopUpWindow") as GameObject);
            obj.transform.SetParent(parent, false);
            if (null == TitleString)
                GameObject.Find("BackText").GetComponent<Text>().text = "这是一条测试文字";
            else
				GameObject.Find("BackText").GetComponent<Text>().text = TitleString;
            if (position == null)
                position = Vector3.zero;
            if (ErrorText != null)
                GameObject.Find("CancelText").GetComponent<Text>().text = ErrorText;
            if (TrueText != null)
                GameObject.Find("ConfirmText").GetComponent<Text>().text = TrueText;
            obj.transform.SetAsLastSibling();
            obj.transform.localPosition = (Vector3)position;
            if (!TrueCallback)
                SelectPopShowTrueEvent += SelectDestory;
            if (!ErrorCallback)
                SelectPopShowErrorEvent += SelectDestory;
            GameObject.Find("True").GetComponent<Button>().onClick.AddListener(SelectPopShowTrueEvent);
            GameObject.Find("Error").GetComponent<Button>().onClick.AddListener(SelectPopShowErrorEvent);
            obj.SetActive(true);
        }
        public static void SelectDestory()
        {
            DestoryObj ds = obj.AddComponent<DestoryObj>();
            ds.SetDestoryInfo(obj, 0, obj);
            SelectPopShowErrorEvent -= SelectDestory;
            SelectPopShowTrueEvent -= SelectDestory;
        }
    }
    /// <summary>
    /// 销毁物体
    /// </summary>
    public class DestoryObj : MonoBehaviour
    {
        public static GameObject Obj;
        public static GameObject _NullObj;
        public static int time;
        /// <summary>
        /// 设置销毁相关参数
        /// </summary>
        /// <param 需要销毁的物体="_Obj"></param>
        /// <param 几秒后销毁="_time"></param>
        /// <param 需要清空内存的物体="__nullObj"></param>
        public void SetDestoryInfo(GameObject _Obj, int _time, GameObject __nullObj = null)
        {
            Obj = _Obj;
            _NullObj = __nullObj;
            time = _time;
        }
        void Start()
        {
            Invoke("Close", time);
        }
        void Close()
        {
            Destroy(Obj);
            if (Obj == null)
            {

            }
            else
            {
                _NullObj = null;
            }
        }
    }
    /// <summary>
    /// 字符串处理类
    /// </summary>
    public class StrinTreatment
    {
        public enum Regular
        {
            email,
            username,
            password,
            number,
        }
        //摘要...
        //根据正则表达式进行参数判断...
        public static bool CheckParmeter(string parameter, Regular type)
        {
            bool y;
            switch (type)
            {
                case Regular.email:
                    y = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$").IsMatch(parameter);
                    break;
                case Regular.password:
                    y = new Regex("^.{6,12}$").IsMatch(parameter);
                    break;
                case Regular.username:
                    y = new Regex("^.{6,12}$").IsMatch(parameter);
                    break;
                default:
                    y = false;
                    break;
            }
            return y;
        }
    }
    /// <summary>
    /// 我的DBUG类
    /// </summary>
    public enum DebugMode
    {
        test, Release
    }
    public class DebugTreatment
    {
        public static DebugMode dm = new DebugMode();
        public static Text debugText;
        public static GameObject debug;
        public static void MyDebug(string info, bool ifAddDebug = false)
        {
            dm = DebugMode.test;
            switch (dm)
            {
                case DebugMode.Release:
                    Debug.Log("Make in KingDom_================>_:    " + info);
                    break;
                case DebugMode.test:
                    if (debug == null)
                    {
                        debug = new GameObject();
                        debug.name = "debugText";
                        debug.transform.SetParent(GameObject.Find("Canvas").transform);
                        debug.AddComponent<RectTransform>();
                        debug.GetComponent<RectTransform>().SetUIMode(SetUI.SelectUIMode.全屏);
                        debug.AddComponent<Text>();
                        debug.GetComponent<Text>().fontSize = 30;
                        debug.GetComponent<Text>().raycastTarget = false;
                        debug.GetComponent<Text>().color = new Color(0, 0, 0, 1);
                        debug.GetComponent<Text>().font = Resources.Load("SIMHEI", typeof(Font)) as Font;
                        debugText = debug.GetComponent<Text>();
                    }
                    if (!ifAddDebug)
                    {
                        debugText.text = "";
                        debugText.text = "Make in KingDom_================>_:    " + info;
                    }
                    else
                    {
                        debugText.text += "\n Make in KingDom_================>_:    " + info;
                    }
                    break;
            }
        }
    }
    /// <summary>
    /// 设置UI模式
    /// </summary>
    public static class SetUI
    {
        public enum SelectUIMode
        {
            居中,
            左对齐,
            右对齐,
            左下角,
            右下角,
            左上角,
            右上角,
            全屏,
            中部拉伸,
            底部拉伸,
            上方拉伸,
            左拉伸,
            右拉伸,
        }
        /// <summary>
        /// 设置UI的自适应模式
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="mode"></param>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        public static void SetUIMode(this RectTransform rect, SelectUIMode mode, Vector3? position = null, Vector3? scale = null, Quaternion? rotation = null)
        {
            switch (mode)
            {
#region Swith
                case SelectUIMode.居中:
                    rect.anchorMin = new Vector2(0.5F, 0.5F);
                    rect.anchorMax = new Vector2(0.5F, 0.5F);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.左对齐:
                    rect.anchorMin = new Vector2(0, 0.5F);
                    rect.anchorMax = new Vector2(0, 0.5F);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.右对齐:
                    rect.anchorMin = new Vector2(1, 0.5F);
                    rect.anchorMax = new Vector2(1, 0.5F);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.左下角:
                    rect.anchorMin = new Vector2(0, 0);
                    rect.anchorMax = new Vector2(0, 0);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.右下角:
                    rect.anchorMin = new Vector2(1, 0);
                    rect.anchorMax = new Vector2(1, 0);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.左上角:
                    rect.anchorMin = new Vector2(0, 1);
                    rect.anchorMax = new Vector2(0, 1);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.右上角:
                    rect.anchorMin = new Vector2(1, 1);
                    rect.anchorMax = new Vector2(1, 1);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.全屏:
                    rect.anchorMin = new Vector2(0, 0);
                    rect.anchorMax = new Vector2(1, 1);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    rect.offsetMax = new Vector2(0, 0);
                    rect.offsetMin = new Vector2(0, 0);
                    break;
                case SelectUIMode.中部拉伸:
                    rect.anchorMin = new Vector2(0, 0.5f);
                    rect.anchorMax = new Vector2(1, 0.5f);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.底部拉伸:
                    rect.anchorMin = new Vector2(0, 0);
                    rect.anchorMax = new Vector2(1, 0);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.上方拉伸:
                    rect.anchorMin = new Vector2(0, 1);
                    rect.anchorMax = new Vector2(1, 1);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.左拉伸:
                    rect.anchorMin = new Vector2(0, 0);
                    rect.anchorMax = new Vector2(0, 1);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                case SelectUIMode.右拉伸:
                    rect.anchorMin = new Vector2(1, 0);
                    rect.anchorMax = new Vector2(1, 1);
                    rect.pivot = new Vector2(0.5F, 0.5F);
                    break;
                default: break;
#endregion
            }
            position = position ?? Vector3.zero;
            scale = scale ?? new Vector3(1, 1, 1);
            rotation = rotation ?? Quaternion.Euler(0, 0, 0);
            rect.localPosition = (Vector3)position;
            rect.localScale = (Vector3)scale;
            rect.localRotation = (Quaternion)rotation;
        }
        /// <summary>
        /// 设置物体的变换参数
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="rotation"></param>
        public static void SetTransform(this Transform trans, Vector3? position = null, Vector3? scale = null, Quaternion? rotation = null)
        {
            position = position ?? Vector3.zero;
            scale = scale ?? new Vector3(1, 1, 1);
            rotation = rotation ?? Quaternion.Euler(0, 0, 0);
            trans.localPosition = (Vector3)position;
            trans.localRotation = (Quaternion)rotation;
            trans.localScale = (Vector3)scale;
        }
    }
    //public static class Other
    //{
    //    public static void TouchCamera()
    //    {
    //        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    //    }
    //}

    /// <summary>
    /// 按钮继承
    /// </summary>
    public abstract class BTN_PAR : MonoBehaviour
    {

        void Main()
        {
            Button b;
            if (gameObject.GetComponent<Button>() == null)
                b = gameObject.AddComponent<Button>();
            else
                b = gameObject.GetComponent<Button>();
            b.onClick.AddListener(delegate { OnClick(); });
        }
        public abstract void OnClick();
    }
    public class EventTriggerListenter : EventTrigger
    {
        public delegate void VoidDelegate(GameObject go);
        public VoidDelegate OnClick;
        public VoidDelegate OnDown;
        public VoidDelegate OnEnter;
        public VoidDelegate OnExit;
        public VoidDelegate OnUp;
        //public VoidDelegate OnSelect;
        //public VoidDelegate OnUpdateSelect;
        static public EventTriggerListenter Get(GameObject go)
        {
            EventTriggerListenter listener = go.GetComponent<EventTriggerListenter>();
            if (listener == null)
                listener = go.AddComponent<EventTriggerListenter>();
            return listener;
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null) OnClick(gameObject);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            if (OnDown != null) OnDown(gameObject);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (OnEnter != null) OnEnter(gameObject);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            if (OnExit != null) OnExit(gameObject);
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (OnUp != null) OnUp(gameObject);
        }
        //public override void OnSelect(BaseEventData eventData)
        //{
        //    if (OnSelect != null) OnSelect(gameObject);
        //}
        //public override void OnUpdateSelect(BaseEventData eventData)
        //{
        //    if (OnUpdateSelect != null) OnUpdateSelect(gameObject);
        //}
    }
    /// <summary>
    /// 读取特定目录下的所有图片.
    /// </summary>
    public class ReadPictures : MonoBehaviour
    {
		public string path;
        public GameObject Btn;
        void Start()
        {
            print(".....");
            Texture2D texture;
            byte[] bytes;

            transform.root.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

            print("Screen.width:     " + Screen.width + "Screen.height:      " + Screen.height);
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width / 3 - 10, Screen.height / 3);
            GetComponent<GridLayoutGroup>().spacing = new Vector2(10, 5);
            string[] folder;
            if (Application.isEditor)//编辑器测试
            {
                folder = Directory.GetFiles(Application.dataPath + "/image");
            }
            else//安卓
            {
                folder = Directory.GetFiles(path);
            }
            GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, (folder.Length + 2) / 3 * (Screen.height / 3 + 5));
            for (int i = folder.Length - 1; i > -1; i--)
            {
                if (folder[i].Contains("meta"))
                {
                    print("格式不正确");
                }
                else if (folder[i].Contains("jpg") || folder[i].Contains("png"))
                {
                    texture = new Texture2D(Screen.width, Screen.height);
                    bytes = File.ReadAllBytes(folder[i]);
                    texture.LoadImage(bytes);
                    Btn = Instantiate(Btn);
                    Btn.SetActive(true);
                    Btn.transform.SetParent(transform);
                    Btn.name = folder[i];
                    Btn.GetComponent<RawImage>().texture = texture;
                    Btn.transform.GetChild(0).gameObject.AddComponent<DeletePicture>();
                    Btn.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
    public class DeletePicture : BTN_PAR
    {
        public override void OnClick()
        {
            //throw new NotImplementedException();
            File.Delete(transform.parent.name);
            Destroy(transform.parent.gameObject);
        }
    }
}
