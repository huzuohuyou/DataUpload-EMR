using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Configuration;
using System.Web.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Data.OleDb;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EmrService : System.Web.Services.WebService
{

    [DllImport("EMRAssist.dll")]
    private static extern bool SaveFileToFieldElem(string strOpenFileName, string strSaveFileName, int nDocumentMode);

    [DllImport("EMRAssist", SetLastError = true)]
    static extern bool GetXmlNodeContent(string strOpenFileName, int nDocumentMode, int nType, string strFindText, short nLayerNo, short nFindType, [Out, MarshalAs(UnmanagedType.LPArray)]char[] strBuff, int nBufSize);//




    //�����ַ�������Ϊ��������ʡ�����ַ���ʱ����ڴ�
    private const string LISConnectionString = "LISConnectionString";
    private const string HISConnectionString = "HISConnectionString";
    private const string EMRConnectionString = "EMRConnectionString";
    private const string strDBEMR = "EMR";

    public EmrService()
    {
    }

    /// <summary>
    /// ��ȡ����ģ����Ϣ
    /// 2015-09-09
    /// �⺣��
    /// </summary>
    /// <param name="strPatientId">����id</param>
    /// <param name="nVisitID">סԺ��</param>
    /// <param name="p_strElemName">Ԫ����</param>
    /// <returns></returns>
    [WebMethod]
    public string GetMRInfoByEleMentName(string strPatientId, int nVisitID, string p_strElemName)
    {
        string _strMark = p_strElemName;
        string _strResult = string.Empty;
        switch (_strMark)
        {
            case "����":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName,"AB",null);
                } break;
            case "�ֲ�ʷ":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "��Ⱦ��ʷ":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����ʷ":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, "ר���ֲ�ʷ", "AB", null);
                } break;
            case "����ʷ":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "��������ʷ", "AB", null);
                } break;
            case "��Ѫʷ":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����ʷ":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����ʷ":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����ʷ":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "�¾�ʷ":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, "�¾�ʷŮ", "AB", null);
                } break;
            case "����ʷ":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "Ԥ������ʷ":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            //case "����ʷ":
            //    {
            //        _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
            //    } break;

            case "����":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName,"AB",null);
                } break;
            case "����":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����ѹ":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����ѹ":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "���":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "����":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "һ������":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "Ƥ��ճĤ���":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "ȫ���ܰ�ǳ��":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "ȫ��ǳ���ܰͽ�", "AB", null);
                } break;
            case "ͷ��������":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "ͷ­�쳣", "AB", null);
                } break;
            case "�������":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "��", "AB", null);
                } break;
            case "�ز����":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "����", "AB", null);
                } break;
            case "�������":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "������", "AB", null) + GetElementValue(strPatientId, nVisitID, "�������Ŷ�", "AB", null) + GetElementValue(strPatientId, nVisitID, "���ھ�������", "AB", null);
                } break;
            case "���ż��":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "����ֱ��", "AB", null);
                } break;
            case "����ֳ�����":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "����ֱ��", "AB", null);
                } break;
            case "�������":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "����", "AB", null);
                } break;
            case "��֫���":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "��֫", "AB", null);
                } break;
            case "��ϵͳ���":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "ר�����":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            default:
                _strResult = "";
                break;
        }
        return _strResult;
    }

    /// <summary>
    /// ��ȡģ���κ�ֵ
    /// 2016-01-29
    /// �⺣��
    /// </summary>
    /// <param name="strPatientId">����id</param>
    /// <param name="nVisitID">סԺ��</param>
    /// <param name="p_strMark"></param>
    /// <param name="strMrClass">�ļ����</param>
    /// <param name="strMrCode">�ļ�����</param>
    /// <returns></returns>
    public string GetCengCiValue(string strPatientId, int nVisitID, string p_strMark,string strMrClass,string strMrCode)
    {
        string strResult = "";
        string _strEleName = p_strMark;
        DataSet dsReturn = new DataSet();
        try
        {
            string _strFilePath = "F:\\�����ϴ�ר����վ\\temp\\tmpyxmr001";//Server.MapPath("tmpyxmr001")
            File.Delete(_strFilePath);
            bool bReturn;
            char[] sReCheckCodeList = new char[2560];
            string strFilePath = get_file("", strPatientId, nVisitID, _strFilePath, strMrClass, strMrCode);
            if (strFilePath != "")
            {
                bReturn = GetXmlNodeContent(_strFilePath, 0, 0, _strEleName, 1, 1, sReCheckCodeList, 2560);
                if (bReturn)
                {
                    strResult = new string(sReCheckCodeList);
                    strResult = strResult.Replace("\0", "");
                }
                File.Delete(_strFilePath);
            }
        }
        catch (Exception ex)
        {
            strResult = ex.Message;
            ex.ToString();
        }
        return strResult;
    }

    /// <summary>
    /// ȡ�ļ��е�Ԫ��
    /// 2016-01-29
    /// wuhailong 
    /// </summary>
    /// <param name="strPatientId">����id</param>
    /// <param name="nVisitID">סԺ��</param>
    /// <param name="p_strMark">Ԫ��ֵ</param>
    /// <param name="strMrClass">�ļ����</param>
    /// <param name="strMrCode">�ļ�����</param>
    /// <returns></returns>
    public string GetElementValue(string strPatientId, int nVisitID, string p_strMark,string strMrClass,string strMrCode)
    {
        try
        {
            string _strFilePath = "F:\\�����ϴ�ר����վ\\temp\\tmpyxmr001";
            File.Delete(_strFilePath);
            string strFilePath = get_file("", strPatientId, nVisitID, _strFilePath, strMrClass, strMrCode);
            string _strXml = GetXmlContent(strFilePath);
            DataSet _dsReturn = new DataSet();
            StringReader _srXml = new StringReader(_strXml);
            XmlTextReader Xmlrdr = new XmlTextReader(_srXml);
            _dsReturn.ReadXml(Xmlrdr);
            if (_dsReturn.Tables[0].Rows.Count > 0)
            {
                int count = _dsReturn.Tables[0].Rows.Count;
                DataRow[] drCur = _dsReturn.Tables[0].Select(string.Format(@"FIELD_NAME='{0}'", p_strMark));
                if (drCur.Length == 1)
                {
                    return drCur[0]["FIELD_TEXT"].ToString().Replace("{", "").Replace("}", "").Trim();
                }
            }
        }
        catch (Exception exp)
        {
            return "";
        }
        return "";
    }

    /// <summary>
    /// ��ȡ�����ļ���ɢ��xml
    /// </summary>
    /// <param name="strMrFile"></param>
    /// <returns></returns>
    public static string GetXmlContent(string strMrFile)
    {
        try
        {
            string strRootPath = strMrFile + ".xml";
            string strContent = string.Empty;
            if (SaveFileToFieldElem(strMrFile, strRootPath, 1))
            {
                strContent = System.IO.File.ReadAllText(strRootPath, Encoding.Default);
            }
            return strContent;
        }
        catch (Exception exp)
        {
            return exp.Message;
        }
    }

   


    private string genMrPath(string strPatientID)
    {
        string strPath, strTemp;
        if (strPatientID.Length < 2)
        {
            strPath = strPatientID;
            strTemp = "";
        }
        else
        {
            if (strPatientID.Length <= 8)
            {
                strPath = strPatientID.Substring(strPatientID.Length - 2, 2); //ȡĩβ��λ
                strTemp = strPatientID.Substring(0, strPatientID.Length - 2);  //ȡʣ�ಿ��
            }
            else
            {
                strPath = strPatientID.Substring(strPatientID.Length - 4, 4); //ȡĩβ��λ
                strTemp = strPatientID.Substring(0, strPatientID.Length - 4);  //ȡʣ�ಿ��
            }

        }

        if (strPath.Length == 1)
            strPath = "0" + strPath;
        strPath = strPath + "\\";

        string strPrefix = "00000000000000";

        if (strTemp.Length <= 8)
            strTemp = strPrefix.Substring(0, 8 - strTemp.Length) + strTemp;
        else
            strTemp = strPrefix.Substring(0, 12 - strTemp.Length) + strTemp;

        strPath = strPath + strTemp + "\\";
        return strPath;

    }

    /// <summary>
    /// ָ�����ļ����Ƶ���ʱĿ¼��
    /// ��Ժ��¼��mr_code ���鴫ֵnull
    /// AB ��Ժ��¼
    /// AC ����
    /// 2016-01-29
    /// �⺣��
    /// </summary>
    /// <param name="host_addr"></param>
    /// <param name="strPatientID"></param>
    /// <param name="nVisitID"></param>
    /// <param name="local_file">������ʱλ��</param>
    /// <param name="strMrClass">�ļ����</param>
    /// <param name="strMrCode">����</param>
    /// <returns></returns>
    public string get_file(string host_addr, string strPatientID, int nVisitID, string local_file,string strMrClass,string strMrCode)
    {
        string filename = local_file;
        //��mr_work_pathȡ���ļ�·��,������ip,�Լ��û���������
        //����EMR���ݿ⴮
        ConnectionStringSettings sEmr = ConfigurationManager.ConnectionStrings["EMRConnectionString"];
        string ftpServerIP = "", ftpUserID = "", ftpPassword = "", ftpMrPath = "", ftpMrFileName = "";
        using (OleDbConnection connEmr = new OleDbConnection(sEmr.ConnectionString))
        {
            try
            {
                connEmr.Open();
                OleDbCommand cmdEmr = connEmr.CreateCommand();
                OleDbDataAdapter daEmr = new OleDbDataAdapter();
                DataSet dsInfo = new DataSet();
                cmdEmr.CommandText = "select mr_path,file_user,file_pwd,ip_addr from mr_work_path";
                daEmr.SelectCommand = cmdEmr;
                daEmr.Fill(dsInfo);
                if (dsInfo.Tables[0].Rows.Count > 0)
                {
                    ftpServerIP = dsInfo.Tables[0].Rows[0]["ip_addr"].ToString();
                    ftpUserID = dsInfo.Tables[0].Rows[0]["file_user"].ToString();
                    ftpPassword = dsInfo.Tables[0].Rows[0]["file_pwd"].ToString();
                    ftpMrPath = dsInfo.Tables[0].Rows[0]["mr_path"].ToString();
                }
                else
                    return "";
                string _strAppend = string.Empty;
                if (strMrClass!=null)
                {
                    _strAppend += " and mr_class='" + strMrClass + "' ";
                }
                if (strMrCode != null)
                {
                    _strAppend += " and mr_code'" + strMrCode + "' ";
                }
                cmdEmr.CommandText = "select file_name from mr_file_index where patient_id='" + strPatientID + "' and visit_id=" + nVisitID.ToString() + _strAppend;
                daEmr.SelectCommand = cmdEmr;
                DataSet dsFile = new DataSet();
                daEmr.Fill(dsFile);
                if (dsFile.Tables[0].Rows.Count > 0)
                {
                    ftpMrFileName = dsFile.Tables[0].Rows[0]["file_name"].ToString();
                }
                else
                    return "";

                connEmr.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "";
            }

        }
        string strMrPath = "";
        strMrPath = ftpMrPath + genMrPath(strPatientID) + ftpMrFileName;
        File.Copy(strMrPath, filename);
        return strMrPath;
    }

}

