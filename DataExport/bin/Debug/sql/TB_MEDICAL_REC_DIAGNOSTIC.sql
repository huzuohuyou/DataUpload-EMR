SELECT A.PATIENT_ID || A.VISIT_ID || D.DIAGNOSIS_TYPE || D.DIAGNOSIS_NO Diagnosis_Id, --*     
       A.PATIENT_ID || A.VISIT_ID Medical_Record_Id,
       '西医诊断' Diagnosis_Type,
       '' Diagnosis_Subtype,
       '1' Diagnosis_Flag,
       C.DIAGNOSIS_CODE Diagnosis_Code,
       D.DIAGNOSIS_DESC Diagnosis_Name,
       '' OCondi_ID,
       d.diagnosis_date Record_Time,
       
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B, DIAGNOSIS D, DIAGNOSTIC_CATEGORY C
 WHERE A.PATIENT_ID = B.PATIENT_ID
   AND A.PATIENT_ID = D.PATIENT_ID
   AND A.VISIT_ID = D.VISIT_ID
   AND A.PATIENT_ID = C.PATIENT_ID
   AND A.VISIT_ID = C.VISIT_ID
   AND D.DIAGNOSIS_TYPE = C.DIAGNOSIS_TYPE
   AND D.DIAGNOSIS_NO = C.DIAGNOSIS_NO
   AND D.DIAGNOSIS_TYPE = '3'
   AND A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
