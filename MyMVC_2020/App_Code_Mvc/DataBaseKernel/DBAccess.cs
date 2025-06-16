using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using DataBaseKernel;

namespace DataBaseKernel
{
    public class DBAccess<T>
    {
        /// <summary>logger</summary>
        //private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(T));
        //private readonly DBList _dBList = new DBList();

        public DBAccess()
        {
        }

        public DBAccess(string p_ConnectString)
        {
            //_dBList.OraDB = p_ConnectString;
        }

        public async Task<Tuple<string, string>> Get_ExecuteScalar(string p_SQL, Object p_Para = null)
        {
            //Logger.Info($"Sql={p_SQL}");
            //Logger.Info($"Param={JsonConvert.SerializeObject(p_Para)}");
            //===
            string result = string.Empty;
            string Tp_Exception_ErrMsg = string.Empty;
            //===
            try
            {                
                using (SqlConnection conn = new SqlConnection(DBList.MSSQL_Conn))
                {
                    conn.Open();
                    result = await conn.ExecuteScalarAsync<string>(p_SQL, p_Para);
                }
            }
            catch (Exception ex)
            {
                //Logger.Debug($"Sql={p_SQL}");
                //Logger.Debug($"Param={JsonConvert.SerializeObject(p_Para)}");
                //Logger.Debug($"Exception={ex}");
                Tp_Exception_ErrMsg = ex.Message;
                //throw ex;
            }
            //===
            Tuple<string, string> Tp_Tuple = new Tuple<string, string>(result, Tp_Exception_ErrMsg);
            //===
            return Tp_Tuple;
        }

        public async Task<Tuple<IEnumerable<T>, string>> GetDataIEnumerable(string p_SQL, Object p_Para = null)
        {
            //Logger.Info($"Sql={p_SQL}");
            //Logger.Info($"Param={JsonConvert.SerializeObject(p_Para)}");
            //===
            IEnumerable<T> result = default;
            string Tp_Exception_ErrMsg = string.Empty;
            //===
            try
            {
                using (SqlConnection conn = new SqlConnection(DBList.MSSQL_Conn))
                {
                    conn.Open();
                    result = await conn.QueryAsync<T>(p_SQL, p_Para);
                }
            }
            catch (Exception ex)
            {
                //Logger.Debug($"Sql={p_SQL}");
                //Logger.Debug($"Param={JsonConvert.SerializeObject(p_Para)}");
                //Logger.Debug($"Exception={ex}");
                Tp_Exception_ErrMsg = ex.Message;
                //throw ex;
            }
            //===
            Tuple<IEnumerable<T>, string> Tp_Tuple = new Tuple<IEnumerable<T>, string>(result, Tp_Exception_ErrMsg);
            //===
            return Tp_Tuple;
        }

        public async Task<Tuple<T, string>> GetFirstOrDefaultData(string p_SQL, Object p_Para = null)
        {
            //Logger.Info($"Sql={p_SQL}");
            //Logger.Info($"Param={JsonConvert.SerializeObject(p_Para)}");
            //===
            T result = default;
            string Tp_Exception_ErrMsg = string.Empty;
            //===
            try
            {
                using (SqlConnection conn = new SqlConnection(DBList.MSSQL_Conn))
                {
                    conn.Open();
                    result = await conn.QueryFirstOrDefaultAsync<T>(p_SQL, p_Para);
                }
            }
            catch (Exception ex)
            {
                //Logger.Debug($"Sql={p_SQL}");
                //Logger.Debug($"Param={JsonConvert.SerializeObject(p_Para)}");
                //Logger.Debug($"Exception={ex}");
                Tp_Exception_ErrMsg = ex.Message;
                //throw ex;
            }
            //===
            Tuple<T, string> Tp_Tuple = new Tuple<T, string>(result, Tp_Exception_ErrMsg);
            //===
            return Tp_Tuple;
        }

        /// <summary>
        /// 執行Insert/Delete/Update SQL
        /// </summary>
        /// <param name="p_SQL"></param>
        /// <param name="p_Para"></param>
        /// <returns></returns>
        public async Task<Tuple<int, string>> Execute(string p_SQL, Object p_Para = null)
        {
            //Logger.Info($"Sql={p_SQL}");
            //Logger.Info($"Param={JsonConvert.SerializeObject(p_Para)}");
            //===
            int exec_sn = 0;
            string Tp_Exception_ErrMsg = string.Empty;
            //===
            using (SqlConnection conn = new SqlConnection(DBList.MSSQL_Conn))
            {
                await conn.OpenAsync();
                using (SqlTransaction Transaction = conn.BeginTransaction())
                {
                    try
                    {
                        exec_sn = await conn.ExecuteAsync(p_SQL, p_Para, Transaction);
                        Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Logger.Debug($"Sql={p_SQL}");
                        //Logger.Debug($"Param={JsonConvert.SerializeObject(p_Para)}");
                        //Logger.Debug($"Exception={ex}");
                        exec_sn = -1;
                        Transaction.Rollback();
                        Tp_Exception_ErrMsg = ex.Message;
                        //throw ex;
                    }
                }
            }
            //===
            Tuple<int, string> Tp_Tuple = new Tuple<int, string>(exec_sn, Tp_Exception_ErrMsg);
            //===
            return Tp_Tuple;
        }


        /// <summary>
        /// 一次執行同一筆Transaction內的多筆Insert/Delete/Update SQL
        /// </summary>
        /// <param name="p_List_Tuple_Cmds"></param>
        /// <returns></returns>
        public async Task<Tuple<int, string>> Multi_Execute(List<Tuple<string, Object>> p_List_Tuple_Cmds)
        {
            int exec_sn = 0;
            string Tp_Exception_ErrMsg = string.Empty;
            //===
            if (p_List_Tuple_Cmds != null && p_List_Tuple_Cmds.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(DBList.MSSQL_Conn))
                {
                    await conn.OpenAsync();
                    using (SqlTransaction Transaction = conn.BeginTransaction())
                    {
                        string Tp_SQL = string.Empty;
                        Object Tp_Para = null;
                        try
                        {
                            foreach (var vCmd in p_List_Tuple_Cmds)
                            {
                                Tp_SQL = vCmd.Item1;
                                Tp_Para = vCmd.Item2;
                                exec_sn = await conn.ExecuteAsync(Tp_SQL, Tp_Para, Transaction);
                                if (exec_sn <= 0)
                                {
                                    //Logger.Debug($"Sql={Tp_SQL}");
                                    //Logger.Info($"Param={JsonConvert.SerializeObject(Tp_Para)}");
                                    //Logger.Debug($"ResultData={exec_sn}");
                                }
                            }
                            Transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            //Logger.Debug($"Sql={Tp_SQL}");
                            //Logger.Debug($"Param={JsonConvert.SerializeObject(Tp_Para)}");
                            //Logger.Debug($"Exception={ex}");
                            exec_sn = -1;
                            Transaction.Rollback();
                            Tp_Exception_ErrMsg = ex.Message;
                            //throw ex;                                                                              
                        }
                    }
                }
            }
            else
            {
                exec_sn = -998;
                Tp_Exception_ErrMsg = "無SQL語法資料";
            }
            //===
            Tuple<int, string> Tp_Tuple = new Tuple<int, string>(exec_sn, Tp_Exception_ErrMsg);
            //===
            return Tp_Tuple;
        }
    }
}