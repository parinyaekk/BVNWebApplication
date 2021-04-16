using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizErpBVN.Models
{
    public class Insert_mt_cust

    {
        public string oid { get; set; }
        public string  mt_code { get; set; }
        public string  mt_name { get; set; }
        public string org_type { get; set; }
        public string cust_type { get; set; }
        public string custgrp_oid { get; set; }
        public string  brn_num { get; set; }
        public string brn_name { get; set; }
        public string reg_num { get; set; }
        public string tax_num { get; set; }
        public string  salerep_oid { get; set; }
        public string saledelry_type { get; set; }
        public string pymt_oid { get; set; }
        public string curr_oid { get; set; }
        public string crlimit { get; set; }
        public string acct_oid { get; set; }
        public string contname { get; set; }
        public string addr1 { get; set; }
        public string addr2 { get; set; }
        public string addr3 { get; set; }
        public string cntry_oid { get; set; }
        public string phn1 { get; set; }
        public string phn2 { get; set; }
        public string fax1 { get; set; }
        public string fax2 { get; set; }
        public string email { get; set; }
        public string addr_text { get; set; }
        public string add_time { get; set; }
        public string add_oid { get; set; }
        public string edit_time { get; set; }
        public string edit_oid { get; set; }
        public int edit_seq { get; set; }
        public string old_code { get; set; }
        public string prov_code { get; set; }
        public string amphur_code { get; set; }
        public string locat_code{ get; set; }
        public string zipcode { get; set; }
        public string prov_name { get; set; }
        public string amphur_name { get; set; }
        public string locat_name { get; set; }
    }
}