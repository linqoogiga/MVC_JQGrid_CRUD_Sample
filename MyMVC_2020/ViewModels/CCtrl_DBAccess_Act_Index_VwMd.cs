using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMVC_2020.DataModels;

namespace MyMVC_2020.ViewModels
{
    public class CCtrl_DBAccess_Act_Index_VwMd
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public List<CTbMember_DataModel> List_CTbMember_DataModel { get; set; }
    }
}