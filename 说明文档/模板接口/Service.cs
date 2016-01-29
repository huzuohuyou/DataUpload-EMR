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




    //常用字符串定义为常量，节省处理字符串时间和内存
    private const string LISConnectionString = "LISConnectionString";
    private const string HISConnectionString = "HISConnectionString";
    private const string EMRConnectionString = "EMRConnectionString";
    private const string strDBEMR = "EMR";

    public EmrService()
    {
    }

    /// <summary>
    /// 获取病历模板信息
    /// 2015-09-09
    /// 吴海龙
    /// </summary>
    /// <param name="strPatientId">病人id</param>
    /// <param name="nVisitID">住院次</param>
    /// <param name="p_strElemName">元素名</param>
    /// <returns></returns>
    [WebMethod]
    public string GetMRInfoByEleMentName(string strPatientId, int nVisitID, string p_strElemName)
    {
        string _strMark = p_strElemName;
        string _strResult = string.Empty;
        switch (_strMark)
        {
            case "主诉":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName,"AB",null);
                } break;
            case "现病史":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "传染病史":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "疾病史":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, "专科现病史", "AB", null);
                } break;
            case "手术史":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "手术外伤史", "AB", null);
                } break;
            case "输血史":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "过敏史":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "个人史":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "婚育史":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "月经史":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, "月经史女", "AB", null);
                } break;
            case "家族史":
                {
                    _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "预防接种史":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            //case "婚育史":
            //    {
            //        _strResult = GetCengCiValue(strPatientId, nVisitID, p_strElemName, "AB", null);
            //    } break;

            case "体温":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName,"AB",null);
                } break;
            case "呼吸":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "脉搏":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "收缩压":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "舒张压":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "身高":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "体重":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "一般检查结果":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "皮肤粘膜检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "全身淋巴浅表":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "全身浅表淋巴结", "AB", null);
                } break;
            case "头部及其他":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "头颅异常", "AB", null);
                } break;
            case "颈部检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "颈", "AB", null);
                } break;
            case "胸部检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "胸廓", "AB", null);
                } break;
            case "腹部检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "腹外形", "AB", null) + GetElementValue(strPatientId, nVisitID, "腹部紧张度", "AB", null) + GetElementValue(strPatientId, nVisitID, "腹壁静脉曲张", "AB", null);
                } break;
            case "肛门检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "肛门直肠", "AB", null);
                } break;
            case "外生殖器检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "肛门直肠", "AB", null);
                } break;
            case "脊柱检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "脊柱", "AB", null);
                } break;
            case "四肢检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, "四肢", "AB", null);
                } break;
            case "神经系统检查":
                {
                    _strResult = GetElementValue(strPatientId, nVisitID, p_strElemName, "AB", null);
                } break;
            case "专科情况":
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
    /// 获取模板层次号值
    /// 2016-01-29
    /// 吴海龙
    /// </summary>
    /// <param name="strPatientId">病人id</param>
    /// <param name="nVisitID">住院次</param>
    /// <param name="p_strMark"></param>
    /// <param name="strMrClass">文件类别</param>
    /// <param name="strMrCode">文件编码</param>
    /// <returns></returns>
    public string GetCengCiValue(string strPatientId, int nVisitID, string p_strMark,string strMrClass,string strMrCode)
    {
        string strResult = "";
        string _strEleName = p_strMark;
        DataSet dsReturn = new DataSet();
        try
        {
            string _strFilePath = "F:\\数据上传专用网站\\temp\\tmpyxmr001";//Server.MapPath("tmpyxmr001")
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
    /// 取文件中的元素
    /// 2016-01-29
    /// wuhailong 
    /// </summary>
    /// <param name="strPatientId">病人id</param>
    /// <param name="nVisitID">住院次</param>
    /// <param name="p_strMark">元素值</param>
    /// <param name="strMrClass">文件类别</param>
    /// <param name="strMrCode">文件编码</param>
    /// <returns></returns>
    public string GetElementValue(string strPatientId, int nVisitID, string p_strMark,string strMrClass,string strMrCode)
    {
        try
        {
            string _strFilePath = "F:\\数据上传专用网站\\temp\\tmpyxmr001";
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
    /// 获取病历文件打散的xml
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
                strPath = strPatientID.Substring(strPatientID.Length - 2, 2); //取末尾两位
                strTemp = strPatientID.Substring(0, strPatientID.Length - 2);  //取剩余部分
            }
            else
            {
                strPath = strPatientID.Substring(strPatientID.Length - 4, 4); //取末尾四位
                strTemp = strPatientID.Substring(0, strPatientID.Length - 4);  //取剩余部分
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
    /// 指定的文件复制到临时目录下
    /// 入院记录的mr_code 建议传值null
    /// AB 入院记录
    /// AC 病程
    /// 2016-01-29
    /// 吴海龙
    /// </summary>
    /// <param name="host_addr"></param>
    /// <param name="strPatientID"></param>
    /// <param name="nVisitID"></param>
    /// <param name="local_file">本地临时位置</param>
    /// <param name="strMrClass">文件类别</param>
    /// <param name="strMrCode">编码</param>
    /// <returns></returns>
    public string get_file(string host_addr, string strPatientID, int nVisitID, string local_file,string strMrClass,string strMrCode)
    {
        string filename = local_file;
        //从mr_work_path取出文件路径,服务器ip,以及用户名和密码
        //连接EMR数据库串
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

