using DataBaseKernel;
using MyMVC_2020.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyMVC_2020.Controllers.DBAccess
{
    public class JQGrid_CRUDController : Controller
    {
        // GET: JQGrid_CRUD
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult assign_jqgrid_datasource()
        {
            return View();
        }

        public async Task<IEnumerable<CTbMember_DataModel>> Get_Data(string p_ID, string p_Name, string sort_column = "name", string sort_direction = "asc")
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
            if (string.IsNullOrWhiteSpace(sort_column) == false)
            {
                TpSQL += $"  order by {sort_column}  {sort_direction}";
            }
            else
            {
                TpSQL += $"  order by id  asc";
            }
            //===
            Object Tp_Para = new
            {
                ID = p_ID,
                NAME = "%" + p_Name + "%"
            };
            //===                         
            Tuple<IEnumerable<CTbMember_DataModel>, string> Tp_Tuple = await _dBAccess.GetDataIEnumerable(TpSQL, Tp_Para);
            //===
            if (Tp_Tuple.Item2 != string.Empty)
            {
                TempData["message_type"] = "error";
                TempData["message"] = "查詢失敗:" + Tp_Tuple.Item2;
            }
            //===
            return  Tp_Tuple.Item1;
        }


        public async Task<JsonResult> GetCustomers(string id, string name, int page = 1, int rows = 5, string sidx = "id", string sord = "asc")
        {
            //http://localhost:12692/JQGrid_CRUD/GetCustomers?_search=false&nd=1749541825300&rows=5&page=1&sidx=ID&sord=desc
            //===
            IEnumerable<CTbMember_DataModel> Results = await Get_Data(id, name, sidx, sord);
            //#2 Setting Paging  
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            //#3 Linq Query to Get Customer   
            

            //#4 Get Total Row Count  
            int totalRecords = Results.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            //#5 Setting Sorting  
            //if (sord.ToUpper() == "DESC")
            //{
            //    Results = Results.OrderByDescending(s => s.ID);
            //    Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            //else
            //{
            //    Results = Results.OrderBy(s => s.ID);
            //    Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            //}
            //===
            Results = Results.Skip(pageIndex * pageSize).Take(pageSize);
            //===
            //#7 Sending Json Object to View.  
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Results
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Add_Member(CTbMember_DataModel p_Model)
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
            if (Convert.ToInt64(Tp_Tuple.Item1) > 0 && Tp_Tuple.Item2 == string.Empty)
            {
                string tmp_Msg = "Saved Successfully";
                TempData["message_type"] = "success";
                TempData["message"] = tmp_Msg;
                return Json(tmp_Msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string tmp_ErrorMsg = "新增失敗:" + Tp_Tuple.Item2;
                TempData["message_type"] = "error";
                TempData["message"] = tmp_ErrorMsg;
                return Json(tmp_ErrorMsg, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Edit_Member(CTbMember_DataModel p_Model)
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
            if (Convert.ToInt64(Tp_Tuple.Item1) > 0 && Tp_Tuple.Item2 == string.Empty)
            {
                string tmp_Msg = "Update Successfully";
                TempData["message_type"] = "success";
                TempData["message"] = tmp_Msg;
                return Json(tmp_Msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string tmp_ErrorMsg = "更新失敗:" + Tp_Tuple.Item2;
                TempData["message_type"] = "error";
                TempData["message"] = tmp_ErrorMsg;
                return Json(tmp_ErrorMsg, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> Delete_Member(CTbMember_DataModel p_Model)
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
            if (Convert.ToInt64(Tp_Tuple.Item1) > 0 && Tp_Tuple.Item2 == string.Empty)
            {
                string tmp_Msg = "Delete Successfully";
                TempData["message_type"] = "success";
                TempData["message"] = tmp_Msg;
                return Json(tmp_Msg, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string tmp_ErrorMsg = "刪除失敗:" + Tp_Tuple.Item2;
                TempData["message_type"] = "error";
                TempData["message"] = tmp_ErrorMsg;
                return Json(tmp_ErrorMsg, JsonRequestBehavior.AllowGet);
            }            
        }
    }
}