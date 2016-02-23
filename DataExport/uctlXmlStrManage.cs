using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ToolFunction;
using System.IO;

namespace DataExport
{
    public partial class uctlXmlStrManage : UserControl
    {
        public uctlXmlStrManage()
        {
            InitializeComponent();
            InitData();
        }

        /// <summary>
        /// ��ȡ��ǰ������
        /// </summary>
        /// <returns></returns>
        public string GetCurrentObjectName()
        {
            if (dataGridView1.CurrentRow != null)
            {
                return dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString();
            }
            return "";
        }

        public void InitData()
        {
            string _strSQL = string.Format("select TABLE_NAME,MS,EXPORTFLAG  from pt_tables_dict ");
            DataTable _dtTemp = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView1.DataSource = _dtTemp;

            //dataGridView1.DataSource = _dtOject;
            _strSQL = string.Format(@"select  table_name,class,chapter_name,data_detail,'' CHOSE from pt_chapter_dict where table_name = '{0}'", GetCurrentObjectName());
            DataTable _dtChapter = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView2.DataSource = _dtChapter;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _strXML = string.Empty;
            DataGridViewRow var = dataGridView1.CurrentRow;
            string _strTableName = var.Cells["TABLE_NAME"].Value.ToString();
            string _strMs = var.Cells["MS"].Value.ToString();
            _strXML = rtb_xml.Text;

            if (true==ExportXml.SaveXML(_strXML, _strTableName))
            {
                uctlMessageBox.frmDisappearShow("����ɹ���");
            }else
            {
                uctlMessageBox.frmDisappearShow("����ʧ��,���������־��");
            }
            
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string _strXML = ExportXml.GetXML(dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString());
            this.rtb_xml.Text = _strXML;
            string _strSQL = string.Format(@"select  table_name,class,chapter_name,data_detail,'' CHOSE from pt_chapter_dict where table_name = '{0}'", GetCurrentObjectName());
            DataTable _dtChapter = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            dataGridView2.DataSource = _dtChapter;
        }

        /// <summary>
        /// ͬ���½���Ϣ
        /// 2015-11-06
        /// �⺣��
        /// </summary>
        public void SycnChapterData(string p_strObjectName, DataTable p_dtObject)
        {
            if (null == p_dtObject)
            {
                return;
            }
            foreach (DataRow var in p_dtObject.Rows)
            {
                if (!ExistChapter(p_strObjectName, var["CHAPTER_NAME"].ToString()))
                {
                    string _strSQL = string.Format(@"insert into pt_chapter_dict(table_name,chapter_name,class) values('{0}','{1}','{2}')", p_strObjectName, var["CHAPTER_NAME"].ToString(), var["CLASS"].ToString());
                    CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
                }
            }
        }
        /// <summary>
        /// �ж��Ƿ�����½�
        /// </summary>
        /// <param name="p_strObjectName"></param>
        /// <param name="p_strChapterName"></param>
        /// <returns></returns>
        public bool ExistChapter(string p_strObjectName, string p_strChapterName)
        {
            string _strSQL = string.Format(@"select count(*) mycount from pt_chapter_dict where table_name = '{0}' and chapter_name = '{1}'", p_strObjectName, p_strChapterName);
            DataTable _dtCount = CommonFunction.OleExecuteBySQL(_strSQL, "", PublicVar.m_strEmrConnection);
            if (_dtCount != null)
            {
                int _nCount = int.Parse(_dtCount.Rows[0]["mycount"].ToString());
                if (_nCount > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string _strBandedSQL = string.Empty;
            DataGridViewRow _drObject = dataGridView1.CurrentRow;
            string _strExportType = uctlBaseConfig.GetConfig("ExportType");
            string _strObjectName = _drObject.Cells["TABLE_NAME"].Value.ToString();
            string _strSQL = string.Empty;// string.Format(@"delete pt_chapter_dict where table_name = '{0}'", _strObjectName);
            DataTable _dtObject = null;
            if (DialogResult.OK == MessageBox.Show("ȷ��ͬ��[�½�]���ݣ�", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                _dtObject = ExportXml.GetObject(_strObjectName);
                //rtb_sql.Text = _strBandedSQL;
                SycnChapterData(_strObjectName, _dtObject);
                InitData();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveChapter();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Add();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show(this, "ȷ��ɾ��", "", MessageBoxButtons.OKCancel))
            {
                DataGridViewRow var = dataGridView1.CurrentRow;
                string _strTableName = var.Cells["TABLE_NAME"].Value.ToString();
                string _strSQL = string.Format("delete pt_tables_dict where table_name = '{0}'", _strTableName);
                CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
                InitData();
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _strSQL = string.Empty;
            int _nResult = 1;
            foreach (DataGridViewRow var in dataGridView1.Rows)
            {
                string _strTableName = var.Cells["TABLE_NAME"].Value.ToString();
                string _strExportFlag = var.Cells["EXPORTFLAG"].Value.ToString().ToUpper();

                //string _strExportFlag2 = var.Cells["Column1"].Value.ToString().ToUpper();
                
                if (_strExportFlag != "TRUE")
                {
                    _strExportFlag = "FALSE";
                }
                string _strMs = var.Cells["MS"].Value.ToString();
                _strSQL = string.Format("select count(*) mycount from pt_tables_dict where  TABLE_NAME = '{0}'", _strTableName);
                int _nCount = int.Parse(CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR").Rows[0]["mycount"].ToString());
                if (0 == _nCount)
                {
                    _strSQL = string.Format("insert into pt_tables_dict(table_name,MS,exportflag) values('{0}','{1}','{2}')", _strTableName, _strMs, _strExportFlag);
                }
                else
                {
                    _strSQL = string.Format("update pt_tables_dict set table_name= '{0}',ms = '{1}' ,exportflag ='{2}' where table_name = '{0}'", _strTableName, _strMs, _strExportFlag);
                }
                _nResult = _nResult * CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
            }
            if (_nResult > 0)
            {
                uctlMessageBox.frmDisappearShow("����ɹ���");
            }
            else
            {
                uctlMessageBox.frmDisappearShow("����ʧ��,���������־��");
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //string _strSQLValue = ExportDB.GetSQL(dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString());
            //rtb_sql.Text = _strSQLValue;
            string _strTableName = dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString();
            this.rtb_xml.Text = ExportXml.GetXML(_strTableName);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView2.CurrentCell.ColumnIndex == 4)
            {
                string _strTableChapter = dataGridView2.CurrentRow.Cells["CHAPTER_NAME"].Value.ToString();
                string _strDataDetail = dataGridView2.CurrentRow.Cells["DATA_DETAIL"].Value.ToString();
                DBTemplet dt = new DBTemplet(_strTableChapter, _strDataDetail);
                dt.ShowDialog();
                if (dt.m_bSave)
                {
                    dataGridView2.CurrentRow.Cells["DATA_DETAIL"].Value = dt.m_strDataDetail;
                    dataGridView2.CurrentRow.Cells["CLASS"].Value = dt.m_strClass;
                    SaveChapter();
                }

            }
        }

        public void SaveChapter()
        {
            DataGridViewRow _drObject = dataGridView1.CurrentRow;
            DataGridViewRow _drChapter = dataGridView2.CurrentRow;
            string _strTableName = _drObject.Cells["TABLE_NAME"].Value.ToString();
            string _strChapterName = _drChapter.Cells["CHAPTER_NAME"].Value.ToString();
            string _strClass = _drChapter.Cells["CLASS"].Value.ToString();
            string _strDataDetail = _drChapter.Cells["DATA_DETAIL"].Value.ToString().Replace("'", "''");
            string _strSQL = @"update pt_chapter_dict set data_detail = '{0}', class = '{1}' where table_name = '{2}' and chapter_name = '{3}'";
            _strSQL = string.Format(_strSQL, _strDataDetail, _strClass, _strTableName, _strChapterName);
            if (1 == CommonFunction.OleExecuteNonQuery(_strSQL, "EMR"))
            {
                uctlMessageBox.frmDisappearShow("����ɹ���");
            }
            else
            {
                uctlMessageBox.frmDisappearShow("����ʧ��,���������־��");
            }
        }

        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string _strType = GetCurrentChapterClass();
        }

        /// <summary>
        /// ��ȡ��ǰ�����
        /// </summary>
        /// <returns></returns>
        public string GetCurrentChapterClass()
        {
            if (dataGridView2.CurrentRow != null)
            {
                return dataGridView2.CurrentRow.Cells["CLASS"].Value.ToString();
            }
            return "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string _strSQL = string.Format(@"delete  pt_chapter_dict where table_name='{0}' and  chapter_name='{1}'", dataGridView1.CurrentRow.Cells["TABLE_NAME"].Value.ToString(), dataGridView2.CurrentRow.Cells["CHAPTER_NAME"].Value.ToString());
            int _n = CommonFunction.OleExecuteNonQuery(_strSQL, "EMR");
            if (_n==1)
            {
                uctlMessageBox.frmDisappearShow("ɾ���ɹ���");
                InitData();
            }
        }
     
    }
}
