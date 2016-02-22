using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ConfirmFileName;
using ToolFunction;
using DataExport.�ⲿ�ӿ�;
using DataExport.�ļ��ӿ�;
using System.Collections;

namespace DataExport
{
    class ExportXml : IExport,IValidate
    {

   

        public string m_strObjectName = string.Empty;
        public string m_strPatientId = string.Empty;
        public string m_strVisitId = string.Empty;
        public string m_strFileNo = string.Empty;
        public DataSet m_dsPatInfo = new DataSet();
        public DataTable m_dtPatInfo = new DataTable();

        public ExportXml() { }

        public ExportXml(DataTable p_dtPatInfo)
        {
            m_dtPatInfo = p_dtPatInfo;
        }

        public ExportXml(DataSet p_dsPatInfo, string p_strObjectName, string p_strPatientId, string p_strVisitId)
        {
            m_strObjectName = p_strObjectName;
            m_strPatientId = p_strPatientId;
            m_strVisitId = p_strVisitId;
            m_dsPatInfo = p_dsPatInfo;
        }

        

        #region IExport ��Ա

        public void Export()
        {
            DoReplace(m_dtPatInfo);
        }

        public static DataTable GetObject(string p_strObjectName)
        {
            DataTable _dtTemp = new DataTable();
            _dtTemp.Columns.Add("CLASS");
            _dtTemp.Columns.Add("CHAPTER_NAME");
            List<string> _listField = GetFieldFromXmlObject(p_strObjectName);
            foreach (string var in _listField)
            {
                _dtTemp.Rows.Add("FILE", var);
            }
            return _dtTemp;
        }

        #endregion

        public void ReplaceValue(object o)
        {
            DataSet ds = (DataSet)o;
            DoReplace(m_dtPatInfo);
        }

        public string GetFixXml(string p_strXml) 
        {
            return p_strXml.Replace("TABLE_", "");
        }

        /// <summary>
        /// ���Ԫ���滻
        /// </summary>
        /// <param name="p_strOldValue"></param>
        /// <param name="p_dtSource"></param>
        /// <returns></returns>
        public string DoTableReplace(string p_strXml, string p_strOldValue, string p_strSQL, string p_strPatientId, string p_strVisitId)
        {
            try
            {
                string _strSQL = p_strSQL.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId);
                DataTable _dtSoucr = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
                string _strMultiXml = string.Empty;
                ArrayList _listField = new ArrayList();
                Regex reg = new Regex(@"\[[^\[^\]]*\]");
                int _nCount = reg.Matches(p_strOldValue).Count;
                for (int i = 0; i < _nCount; i++)
                {
                    string _strValue = reg.Matches(p_strOldValue)[i].Captures[0].Value;
                    _listField.Add(_strValue);
                }
                foreach (DataRow _drSource in _dtSoucr.Rows)
                {
                    string _strNewXml = p_strOldValue;
                    _strNewXml = ClearXml(_strNewXml);
                    foreach (string var in _listField)
                    {
                        _strNewXml = _strNewXml.Replace(var, _drSource[var.Replace("[", "").Replace("]", "")].ToString());
                    }
                    _strMultiXml += _strNewXml;
                }
                if (p_strXml.IndexOf(p_strOldValue) > 0)
                {
                    p_strXml = p_strXml.Replace(p_strOldValue, _strMultiXml);
                }
                return p_strXml;
            }
            catch (Exception ex)
            {
                CommonFunction.WriteError("DoTableReplace" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// ����xmlɾ��{}
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <returns></returns>
        public string ClearXml(string p_strXml)
        {
            return p_strXml.Replace("{", "").Replace("}", "");
        }

        /// <summary>
        /// ����Ԫ���滻
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <param name="p_dtSource"></param>
        /// <returns></returns>
        public string DoDBReplace(string p_strXml, string p_strOldValue, string p_strSQL, string p_strPatientId, string p_strVisitId)
        {
            try
            {
                string _strXml = p_strXml;
                string _strSQL = p_strSQL.Replace("@PATIENT_ID", p_strPatientId).Replace("@VISIT_ID", p_strVisitId);
                string _strNewValue = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR").Rows[0][0].ToString();
                if (_strXml.IndexOf(p_strOldValue) > 0)
                {
                    _strXml = _strXml.Replace(p_strOldValue, _strNewValue);
                    RemoteMessage.SendMessage(p_strOldValue + "....................." + _strNewValue);
                }
                return _strXml;
            }
            catch (Exception ex)
            {
                CommonFunction.WriteError("DoDBReplace"+ex.Message);
            }
            return null;
        }

        /// <summary>
        /// �ӿ������滻
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <param name="p_dtSource"></param>
        /// <returns></returns>
        public string DoInterfaceReplace(string p_strXml, string p_strOldValue, string p_strPatientId, string p_strVisitId)
        {
            try
            {
                string _strXml = p_strXml;
                string _strNewValue = SingleObjectDBExport.CallMrInfo2(p_strPatientId, int.Parse(p_strVisitId), p_strOldValue);
                if (_strXml.IndexOf(p_strOldValue) > 0)
                {
                    _strXml = _strXml.Replace(p_strOldValue, _strNewValue);
                    RemoteMessage.SendMessage(p_strOldValue + "....................." + _strNewValue);
                }
                return _strXml;
            }
            catch (Exception ex)
            {
                CommonFunction.WriteError("DoInterfaceReplace" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// ͨ��������ʽ�жϴ˶�xml�ַ����Ƿ�Ҫ��table
        /// </summary>
        /// <param name="p_strValue"></param>
        /// <returns></returns>
        public bool IsTableSource(string p_strValue)
        {
            Regex reg = new Regex(@"\{[^\{^\}]*\}");
            //string _strXml = GetXML(p_strObject);
            int _nCount = reg.Matches(p_strValue).Count;
            if (_nCount > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ͨ�����жϴ�����Դ�Ƿ�󶨱�
        /// </summary>
        /// <param name="p_dtSource"></param>
        /// <returns></returns>
        public bool IsTableSource(DataTable p_dtSource)
        {
            if (p_dtSource.TableName.Contains("TABLE"))
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// ��xml�ַ����е�[FILED_NAME]��滻Ϊ����Դ�еĶ�Ӧ��
        /// ����Դ���������PATIENT_ID,VISIT_ID
        /// 2015-11-04
        /// �⺣��
        /// </summary>
        /// <param name="p_dsSoucre"></param>
        public void DoReplace(DataTable p_dtSoucre)
        {
            string _strXml = GetXML(PublicVar.m_strCurrentObj);
            StringBuilder _sbXml = new StringBuilder(_strXml);
            DataTable _dtSoucr = p_dtSoucre;
            DataTable _dtFieldDict = CommonFunction.OleExecuteBySQL(string.Format(@"select  table_name,class,chapter_name,data_detail,'' CHOSE from pt_chapter_dict where table_name = '{0}'", PublicVar.m_strCurrentObj), "", PublicVar.m_strEmrConnection);
            foreach (DataRow pat in _dtSoucr.Rows)
            {
                string _strPatient_id = pat["PATIENT_ID"].ToString();
                string _strVisitId = pat["VISIT_ID"].ToString();
                foreach (DataRow var in _dtFieldDict.Rows)
                {
                    if (var["class"].ToString().ToUpper() == "DB")
                    {
                        _strXml = DoDBReplace(_strXml, var["chapter_name"].ToString(), var["data_detail"].ToString(), _strPatient_id, _strVisitId);
                    }
                    else if (var["class"].ToString().ToUpper() == "FILE")
                    {
                        _strXml = DoInterfaceReplace(_strXml, var["chapter_name"].ToString(), _strPatient_id, _strVisitId);
                    }
                    else if (var["class"].ToString().ToUpper() == "TABLE")
                    {
                        _strXml = DoTableReplace(_strXml, var["chapter_name"].ToString(), var["data_detail"].ToString(), _strPatient_id, _strVisitId);
                    }
                }
                string _strFiledName = PublicVar.m_strCurrentObj + "_" + _strPatient_id + "_" + _strVisitId;
                string _strPath = uctlBaseConfig.GetConfig("XmlOutPutPath");
                if (SaveXML(_strXml, _strFiledName, _strPath))
                {
                    PublicVar.m_nObjSuccessCount++;
                    PublicVar.m_nSuccessCount++;
                }
                else
                {
                    PublicVar.m_nObjFalseCount++;
                    PublicVar.m_nFalseCount++;
                }
            }
        }

        public bool IsUseInterface()
        {
            string _strUseInterface = uctlBaseConfig.GetConfig("UseInterface");
            if ("TRUE" == _strUseInterface.ToUpper())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ͨ��Ŀ�������ƻ�ȡxml�ű�
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static string GetXML(DataSet p_dsSource)
        {
            string _strFileName = string.Empty;
            foreach (DataTable var in p_dsSource.Tables)
            {
                if (var.TableName!="TABLE")
                {
                    _strFileName = var.TableName;
                }
            }
            string _strSQL = string.Empty;
            string _strPath = Application.StartupPath + "/xml/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + _strFileName + ".xml";
                if (File.Exists(_strPath))
                {
                    using (StreamReader sr = new StreamReader(_strPath, Encoding.UTF8))
                    {
                        _strSQL = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                throw new Exception("Directory not Found Exception!");
                //return string.Empty;
            }
            return _strSQL;
        }

        /// <summary>
        /// ͨ��Ŀ�������ƻ�ȡxml�ű�
        /// </summary>
        /// <param name="p_strFileName"></param>
        /// <returns></returns>
        public static string GetXML(string p_strFileName)
        {
            string _strSQL = string.Empty;
            string _strPath = Application.StartupPath + "/xml/";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".xml";
                if (File.Exists(_strPath))
                {
                    using (StreamReader sr = new StreamReader(_strPath, Encoding.UTF8))
                    {
                        _strSQL = sr.ReadToEnd();
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                throw new Exception("Directory not Found Exception!");
                //return string.Empty;
            }
            return _strSQL;
        }

        /// <summary>
        /// ��xml�����ڸ�Ŀ¼ ��xml�ļ�����
        /// </summary>
        /// <param name="p_strXML">xml�ű�</param>
        /// <param name="p_strFileName">Ŀ��������</param>
        public static bool SaveXML(string p_strXML, string p_strFileName)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////д��<add>Ԫ�ص�Value
            string _strPath = Application.StartupPath+"\\xml\\";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".xml";
                using (StreamWriter sw = new StreamWriter(_strPath, false, Encoding.UTF8))
                {
                    sw.WriteLine(p_strXML);
                    return true;
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                SaveXML(p_strXML, p_strFileName);
            }
            return false;
        }

        /// <summary>
        /// ��xml�������õ�Ŀ¼���ڸ�Ŀ¼ ��xml�ļ�����
        /// </summary>
        /// <param name="p_strXML">xml�ı�</param>
        /// <param name="p_strFileName">�ļ���</param>
        /// <param name="p_strDirectory">���õ�·����</param>
        public bool SaveXML(string p_strXML, string p_strFileName, string p_strDirectory)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ////д��<add>Ԫ�ص�Value
            string _strPath = p_strDirectory;// config.AppSettings.Settings[p_strDirectory].Value + "\\";
            if (Directory.Exists(_strPath))
            {
                _strPath = _strPath + p_strFileName + ".xml";
                using (StreamWriter sw = new StreamWriter(_strPath,false,Encoding.UTF8))
                {
                    sw.WriteLine(p_strXML);
                    return true;
                }
            }
            else
            {
                Directory.CreateDirectory(_strPath);
                SaveXML(p_strXML, p_strFileName);
            }
            return false;
        }

        /// <summary>
        /// ��xmlstring�л�ȡ�ֶ�
        /// </summary>
        /// <param name="p_strXml"></param>
        /// <returns></returns>
        public static List<string> GetFiledFromXml(string p_strXml)
        {
            List<string> _listField = new List<string>();
            Regex reg = new Regex(@"\[[^\[^\]]*\]");
            string _strXml = p_strXml;
            int _nCount = reg.Matches(_strXml).Count;
            for (int i = 0; i < _nCount; i++)
            {
                string _strField = reg.Matches(_strXml)[i].Captures[0].Value;
                _listField.Add(_strField);
            }
            return _listField;
        }

        /// <summary>
        /// ͨ��������ʽ��ȡxml�б�ע���ֶ�
        /// ��עʾ�� [field_name]
        /// </summary>
        /// <param name="p_strObject">��������</param>
        /// <returns>�ֶμ���</returns>
        public static List<string> GetFieldFromXmlObject(string p_strObject)
        {
            List<string> _listField = new List<string>();
            Regex reg = new Regex(@"\[[^\[^\]]*\]");
            string _strXml = GetXML(p_strObject);
            int _nCount = reg.Matches(_strXml).Count;
            for (int i = 0; i < _nCount; i++)
            {
                string _strField = reg.Matches(_strXml)[i].Captures[0].Value;
                if (_strField.ToUpper().StartsWith("[COL"))
                {
                    continue;
                }
                _listField.Add(_strField);
            }
            reg = new Regex(@"\{[^\{^\}]*\}");
            _nCount = reg.Matches(_strXml).Count;
            for (int i = 0; i < _nCount; i++)
            {
                string _strValue = reg.Matches(_strXml)[i].Captures[0].Value;
                _listField.Add(_strValue);
            }
            return _listField;
        }

        #region IValidate ��Ա

        public string ValidateData(string p_strTableName)
        {
            ShowFixInfo.m_dtSource.Rows.Clear();
            string _strSQLValue = ExportDB.GetSQL(p_strTableName);
            List<string> _listField = GetFieldFromXmlObject(p_strTableName);
            string _strSQL = _strSQLValue.ToUpper().Trim().Replace("@PATIENT_ID", "1").Replace("@VISIT_ID", "1");
            DataTable _dtLocal = CommonFunction.OleExecuteBySQL(_strSQL, "", "EMR");
            if (_dtLocal == null)
            {
                throw new Exception("SQL�������" + _strSQL);
            }
            if (!_dtLocal.Columns.Contains("PATIENT_ID") || !_dtLocal.Columns.Contains("VISIT_ID"))
            {
                ShowFixInfo.m_strWarning = (" [PATIENT_ID][VISIT_ID] ");
            }
            string _strUnCmpareItems = string.Empty;
            foreach (string var in _listField)
            {
                string _strField = var.Replace("[", "").Replace("]","");
                if (_dtLocal.Columns.Contains(_strField))
                {
                    ShowFixInfo.m_dtSource.Rows.Add(_strField, "TRUE");
                }
                else
                {
                    ShowFixInfo.m_dtSource.Rows.Add(_strField, "FALSE");
                    _strUnCmpareItems += "[" + _strField + "]";
                }
            }
            return " ����[" + p_strTableName.PadRight(30,'*') + "]δ������Ŀ:" + _strUnCmpareItems;
           
        }

        public void ValidateAll(DataTable p_dt)
        {
            CommonFunction.WriteError("validate", "====================================================�����ָ���====================================================", false);
            string _strError = string.Empty;
            foreach (DataRow var in p_dt.Rows)
            {
                CommonFunction.WriteError("validate", "������:" + ValidateData(var["TABLE_NAME"].ToString()) + ShowFixInfo.m_strWarning + "��");
            }
        }

        #endregion


        #region IExport ��Ա


        public void LogFalse(List<string> p_list)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IExport ��Ա


        public string SynSQL(string p_strObjName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
