using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMVC_2020.DataModels;
using MyMVC_2020.ViewModels;
using System.Threading.Tasks;
using MyMVC_2020.Filters;
using DataBaseKernel;

namespace MyMVC_2020.Controllers.DBAccess
{
    public class DBAccessController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index(CCtrl_DBAccess_Act_Index_VwMd p_Model)
        {
            p_Model = new CCtrl_DBAccess_Act_Index_VwMd();
            p_Model.List_CTbMember_DataModel = await Get_Data(p_Model.ID, p_Model.Name);
            return View(p_Model);
        }

        public async Task<List<CTbMember_DataModel>> Get_Data(string p_ID = "", string p_Name = "")
        {
            DBAccess<CTbMember_DataModel> _dBAccess = new DBAccess<CTbMember_DataModel>();
            //===
            string TpSQL = @"select * from TempJohn_TbMember where 1=1 ";
            if (String.IsNullOrWhiteSpace(p_ID) == false)
            {
                TpSQL += " and ID=@ID";
            }
            //===            
            if (String.IsNullOrWhiteSpace(p_Name) == false)
            {
                TpSQL += " and Name like @NAME";
            }
            //===
            TpSQL += "  order by ID";
            //===
            Object Tp_Para = new
            {
                ID = p_ID,
                NAME = "%" + p_Name + "%"
            };
            //===                         
            Tuple<IEnumerable<CTbMember_DataModel>, string> Tp_Tuple = await _dBAccess.GetDataIEnumerable(TpSQL, Tp_Para);
            //===
            if (Tp_Tuple.Item2.ToString() != string.Empty)
            {
                TempData["message_type"] = "error";
                TempData["message"] = "查詢失敗:" + Tp_Tuple.Item2.ToString();
            }
            //===
            return (Tp_Tuple.Item1!=null) ? Tp_Tuple.Item1.ToList() : null;
        }

        [HttpPost]
        [MultiButton("Btn_Get")]
        public async Task<ActionResult> Index_Btn_Get_Click(CCtrl_DBAccess_Act_Index_VwMd p_Model)
        {            
            p_Model.List_CTbMember_DataModel = await Get_Data(p_Model.ID, p_Model.Name);
            return View(p_Model);
        }

        [HttpPost]
        [MultiButton("Btn_Add")]
        public async Task<ActionResult> Index_Btn_Add_Click(CCtrl_DBAccess_Act_Index_VwMd p_Model)
        {
            DBAccess<CTbMember_DataModel> _dBAccess = new DBAccess<CTbMember_DataModel>();
            //===
            string TpSQL = "INSERT INTO TempJohn_TbMember (name) OUTPUT Inserted.id  VALUES (@NAME)";
            //===
            Object Tp_Para = new
            {
                NAME = p_Model.Name
            };
            //===                         
            Tuple<int, string> Tp_Tuple = await _dBAccess.Execute(TpSQL, Tp_Para);
            //===
            if (Convert.ToInt64(Tp_Tuple.Item1) > 0 && Tp_Tuple.Item2.ToString() == string.Empty)
            {
                TempData["message_type"] = "success";
                TempData["message"] = "新增會員成功";
            }
            else
            {
                TempData["message_type"] = "error";
                TempData["message"] = "新增失敗:" + Tp_Tuple.Item2.ToString();
            }
            //===
            p_Model.List_CTbMember_DataModel = await Get_Data();
            return View(p_Model);
        }

        [HttpPost]
        [MultiButton("Btn_Update")]
        public async Task<ActionResult> Index_Btn_Update_Click(CCtrl_DBAccess_Act_Index_VwMd p_Model)
        {
            DBAccess<CTbMember_DataModel> _dBAccess = new DBAccess<CTbMember_DataModel>();
            //===
            string TpSQL = @"UPDATE TempJohn_TbMember set                            
                           [name] =@NAME                          
                            WHERE id=@MEMBER_ID";
            //===
            Object Tp_Para = new
            {
                MEMBER_ID = p_Model.ID,
                NAME = p_Model.Name
            };
            //===                         
            Tuple<int, string> Tp_Tuple = await _dBAccess.Execute(TpSQL, Tp_Para);
            //===
            if (Convert.ToInt64(Tp_Tuple.Item1) > 0 && Tp_Tuple.Item2.ToString() == string.Empty)
            {
                TempData["message_type"] = "success";
                TempData["message"] = "更新會員成功";
            }
            else
            {
                TempData["message_type"] = "error";
                TempData["message"] = "更新失敗:" + Tp_Tuple.Item2.ToString();
            }
            //===
            p_Model.List_CTbMember_DataModel = await Get_Data();
            return View(p_Model);
        }

        [HttpPost]
        [MultiButton("Btn_Delete")]
        public async Task<ActionResult> Index_Btn_Delete_Click(CCtrl_DBAccess_Act_Index_VwMd p_Model)
        {
            DBAccess<CTbMember_DataModel> _dBAccess = new DBAccess<CTbMember_DataModel>();
            //===
            string TpSQL = @"delete from TempJohn_TbMember where id=@MEMBER_ID";
            //===
            Object Tp_Para = new
            {
                MEMBER_ID = p_Model.ID
            };
            //===                         
            Tuple<int, string> Tp_Tuple = await _dBAccess.Execute(TpSQL, Tp_Para);
            //===
            if (Convert.ToInt64(Tp_Tuple.Item1) > 0 && Tp_Tuple.Item2.ToString() == string.Empty)
            {
                TempData["message_type"] = "success";
                TempData["message"] = "刪除會員成功";
            }
            else
            {
                TempData["message_type"] = "error";
                TempData["message"] = "刪除失敗:" + Tp_Tuple.Item2.ToString();
            }
            //===
            p_Model.List_CTbMember_DataModel = await Get_Data();
            return View(p_Model);
        }
    }
}
//20250618_PM1413:Master1。
//20250618_PM1423:Master2。