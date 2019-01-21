
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp.Modules;

namespace WindowsFormsApp.Controller
{
    class MainController
    {
        private Commons comm;
        private Panel head, contents, view, controller;
        private Button btn1, btn2, btn3;
        private ListView listView;
        private TextBox textBox1, textBox2, textBox3, textBox4, textBox5, textBox6;
        private Form parentForm, tagetForm;
        private Hashtable hashtable;
        private WebClient client;
        private string SelectInfo, InsertInfo, UpdateInfo, DeleteInfo;

        public MainController(Form parentForm)
        {
            this.parentForm = parentForm;
            comm = new Commons();
            getView();
        }

        private void getView()
        {
            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 100);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 1);
            hashtable.Add("name", "head");
            head = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 700);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 100);
            hashtable.Add("color", 4);
            hashtable.Add("name", "contents");
            contents = comm.getPanel(hashtable, parentForm);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 20);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 3);
            hashtable.Add("name", "controller");
            controller = comm.getPanel(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("sX", 1000);
            hashtable.Add("sY", 680);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 20);
            hashtable.Add("color", 0);
            hashtable.Add("name", "view");
            view = comm.getPanel(hashtable, contents);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 100);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "추가");
            hashtable.Add("text", "입력");
            hashtable.Add("click", (EventHandler)btn_event);
            btn1 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 400);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "수정");
            hashtable.Add("text", "수정");
            hashtable.Add("click", (EventHandler)btn_event);
            btn2 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("sX", 200);
            hashtable.Add("sY", 80);
            hashtable.Add("pX", 700);
            hashtable.Add("pY", 10);
            hashtable.Add("color", 0);
            hashtable.Add("name", "삭제");
            hashtable.Add("text", "삭제");
            hashtable.Add("click", (EventHandler)btn_event);
            btn3 = comm.getButton(hashtable, head);

            hashtable = new Hashtable();
            hashtable.Add("color", 0);
            hashtable.Add("name", "listView");
            hashtable.Add("click", (MouseEventHandler)listView_click);
            listView = comm.getListView(hashtable, view);

            hashtable = new Hashtable();
            hashtable.Add("width", 45);
            hashtable.Add("pX", 0);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox1");
            hashtable.Add("enabled", false);
            textBox1 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 100);
            hashtable.Add("pX", 45);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox2");
            hashtable.Add("enabled", true);
            textBox2 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 350);
            hashtable.Add("pX", 145);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox3");
            hashtable.Add("enabled", true);
            textBox3 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 100);
            hashtable.Add("pX", 495);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox4");
            hashtable.Add("enabled", true);
            textBox4 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 200);
            hashtable.Add("pX", 595);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox5");
            hashtable.Add("enabled", false);
            textBox5 = comm.getTextBox(hashtable, controller);

            hashtable = new Hashtable();
            hashtable.Add("width", 200);
            hashtable.Add("pX", 795);
            hashtable.Add("pY", 0);
            hashtable.Add("color", 0);
            hashtable.Add("name", "textBox6");
            hashtable.Add("enabled", false);
            textBox6 = comm.getTextBox(hashtable, controller);


            Information();
            GetSelect();
        }

        public void Information()
        {
            string path = "/public/APIInfo.json";
            string result = new StreamReader(File.OpenRead(path)).ReadToEnd();

            JObject jo = JsonConvert.DeserializeObject<JObject>(result);
            Hashtable map = new Hashtable();
            foreach (JProperty col in jo.Properties())
            {
                //Console.WriteLine("{0} : {1}", col.Name, col.Value);
                map.Add(col.Name, col.Value);
            }

            SelectInfo = string.Format("{0}", map["select"]);
            InsertInfo = string.Format("{0}", map["insert"]);
            UpdateInfo = string.Format("{0}", map["update"]);
            DeleteInfo = string.Format("{0}", map["delete"]);
        }

        private void GetSelect()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            listView.Items.Clear();

            listView.Columns.Add("번호", 45, HorizontalAlignment.Center);        /* Notice 번호 */
            listView.Columns.Add("제목", 100, HorizontalAlignment.Left);      /* Notice 제목 */
            listView.Columns.Add("내용", 350, HorizontalAlignment.Left);   /* Notice 내용 */
            listView.Columns.Add("삭제여부", 100, HorizontalAlignment.Center);     /* Notice 작성자 이름 */
            listView.Columns.Add("생성날짜", 200, HorizontalAlignment.Left);     /* Notice 작성 현재날짜 */
            listView.Columns.Add("수정날짜", 200, HorizontalAlignment.Left);     /* Notice 수정 현재날짜 */

            client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            client.Encoding = Encoding.UTF8;    //한글처리

            string url = SelectInfo;
            Stream result = client.OpenRead(url);

            StreamReader sr = new StreamReader(result);
            string str = sr.ReadToEnd();

            ArrayList jList = JsonConvert.DeserializeObject<ArrayList>(str);
            ArrayList list = new ArrayList();
            foreach (JObject row in jList)
            {
                Hashtable ht = new Hashtable();
                foreach (JProperty col in row.Properties())
                {
                    ht.Add(col.Name, col.Value);
                }
                list.Add(ht);
            }
            foreach (Hashtable ht in list)
            {
                listView.Items.Add(new ListViewItem(new string[] { ht["nNo"].ToString(), ht["nTitle"].ToString(), ht["nContents"].ToString(), ht["delYn"].ToString(), ht["regDate"].ToString(), ht["modDate"].ToString() }));
            }
        }

        private bool GetInsert()
        {
            client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            NameValueCollection data = new NameValueCollection();

            data.Add("nTitle", textBox2.Text);
            data.Add("nContents", textBox3.Text);
            string url = InsertInfo;
            string method = "POST";

            byte[] result = client.UploadValues(url, method, data);
            string strResult = Encoding.UTF8.GetString(result);
            if (strResult == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetUpdate()
        {
            client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            NameValueCollection data = new NameValueCollection();

            data.Add("nNo", textBox1.Text);
            data.Add("nTitle", textBox2.Text);
            data.Add("nContents", textBox3.Text);

            string url = UpdateInfo;
            string method = "POST";
            byte[] result = client.UploadValues(url, method, data);
            string strResult = Encoding.UTF8.GetString(result);
            if (strResult == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetDelete()
        {
            client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            NameValueCollection data = new NameValueCollection();

            data.Add("nNo", textBox1.Text);

            string url = DeleteInfo;
            string method = "POST";
            byte[] result = client.UploadValues(url, method, data);
            string strResult = Encoding.UTF8.GetString(result);
            if (strResult == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void listView_click(object o, EventArgs a)
        {
            ListView lv = (ListView)o;
            ListView.SelectedListViewItemCollection itemGroup = lv.SelectedItems;
            for (int i = 0; i < itemGroup.Count; i++)
            {
                ListViewItem item = itemGroup[i];
                string no = item.SubItems[0].Text;

                textBox1.Text = item.SubItems[0].Text;
                textBox2.Text = item.SubItems[1].Text;
                textBox3.Text = item.SubItems[2].Text;
                textBox4.Text = item.SubItems[3].Text;
                textBox5.Text = item.SubItems[4].Text;
                textBox6.Text = item.SubItems[5].Text;
            }

        }

        private void btn_event(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "추가":
                    GetInsert();
                    GetSelect();
                    break;
                case "수정":
                    GetUpdate();
                    GetSelect();
                    break;
                case "삭제":
                    GetDelete();
                    GetSelect();
                    break;
                default:
                    break;
            }
        }
    }
}
