select A.DIAGNOSIS_TYPE || A.DIAGNOSIS_NO || A.PATIENT_ID || A.VISIT_ID Diagnosis_Id,
       D.INP_NO || A.VISIT_ID Vis_Serial_Id,
       B.DEPT_ADMISSION_TO Code_Of_Visit_Dept,
       B.DEPT_ADMISSION_TO Name_Of_Visit_Dept,
       B.DOCTOR_IN_CHARGE Physician_Code,
       B.DOCTOR_IN_CHARGE Physician_Name,
       '1' Diagnosis_Type,
       '' Diagnosis_Subtype,
       A.DIAGNOSIS_TYPE Diagnosis_Class,
       C.DIAGNOSIS_CODE Diagnosis_Code,
       A.DIAGNOSIS_DESC Diagnosis_Name,
       '1' Diagnosis_Flag,
       A.DIAGNOSIS_DATE Record_Time,
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  from diagnosis A, PAT_VISIT B, DIAGNOSTIC_CATEGORY C,PAT_MASTER_INDEX D
 where A.PATIENT_ID = B.PATIENT_ID
   AND A.VISIT_ID = B.VISIT_ID
   AND A.PATIENT_ID = C.PATIENT_ID
   AND A.VISIT_ID = C.VISIT_ID
   AND A.DIAGNOSIS_TYPE = C.DIAGNOSIS_TYPE
   AND A.DIAGNOSIS_NO = C.DIAGNOSIS_NO
   AND A.PATIENT_ID = D.PATIENT_ID
   AND A.patient_id = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
