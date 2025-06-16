using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVC_2020.Filters
{
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public MultiButtonAttribute(string name)
        {
            this.Name = name;
        }


        //[1]每一個有宣告==>[MultiButton("XXX")] ActionNameSelectorAttribute  的  Action_Method，都會類似迴圈輪詢一樣，一個一個傳進來做比對，
        //與[ controllerContext.HttpContext.Request.Form.AllKeys]==>所有PostBack 回給Form的Value值集合，做比對，看哪一個Action_Method
        //是true就執行那一個Action_Method。
        //===
        //[2]但是只能有一個是true，否則會產生[模稜兩可]的Error_Msg。
        //===
        //[3]注意：submit鈕的name的值，不可與ViewModel中的任何class、屬性成員同名，不然Compile會混淆，該ViewModel的該類別會變null。
        //Ex:把~/Views/One_View_Multi_Button/Use_ActionSelector.cshtml 的submit_A的name設成="Form_A"，
        //然後因為ViewModel==>CVwMd_CtrlHome_ActIndex==>也有一個屬性叫Form_A==>在前端HttpPost會變NULL。 
        //===
        //[4]只有[HttpPost]+*.cshtml 的FormMethod=HttpPost有效，HttpGet就像傳統aspe一樣，在網址會有明碼url?xxx=xxx_value。然後不會用到MultiButtonAttribute。
        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }
            //===
            bool Tp_IsValidName = controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name);
            return Tp_IsValidName;
        }
    }
}