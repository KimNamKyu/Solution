using ClassLibrary;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Commons
    {
        public static ArrayList GetSelect()
        {
            DataBase db = new DataBase();
            string sql = string.Format("select * from Notice where delYn = 'N';");
            MySqlDataReader sdr = db.Reader(sql);

            ArrayList list = new ArrayList();   //string 이 아닌 배열 형식으로 
            while (sdr.Read())
            {
                Hashtable ht = new Hashtable();
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    ht.Add(sdr.GetName(i), sdr.GetValue(i));
                }
                list.Add(ht);
            }
            sdr.Close();
            db.ConnectionClose();
            return list;
        }

        public static ArrayList Getinsert(Notices test)
        {
            DataBase db = new DataBase();
            string sql = string.Format("insert into Notice (nTitle,nContents) values ('{0}','{1}');", test.nTitle, test.nContents);
            if (db.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetUpdate(Notices test)
        {
            DataBase db = new DataBase();
            string sql = string.Format("update Notice set nTitle = '{1}', nContents = '{2}' where nNo = {0};", test.nNo, test.nTitle, test.nContents);
            if (db.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetDelete(Notices test)
        {
            DataBase db = new DataBase();
            string sql = string.Format("update Notice set delYn = 'Y' where nNo = {0};", test.nNo);
            if (db.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }



    }
}
