SELECT A.PATIENT_ID || A.VISIT_ID || O.OPERATION_NO Operate_Detail_ID, --*     
       A.PATIENT_ID || A.VISIT_ID Medical_Record_Id,
       O.OPERATION_CODE Operation_Code,
       O.OPERATING_DATE Operation_Date,
       O.WOUND_GRADE Operation_Scale,
       O.OPERATION_DESC Operation_Name,
       O.OPERATOR Surgeon_Name,
       O.FIRST_ASSISTANT First_Assistant_Name,
       O.SECOND_ASSISTANT Second_Assistant_Name,
       O.WOUND_GRADE Incision_Grading,
       O.HEAL Wound_Heal_Grade_Code,
       O.ANAESTHESIA_METHOD Anesthesia_Method,
       O.ANESTHESIA_DOCTOR Anesthesia_Dr_Name,
       O.OPERATOR Surgeon_Code,
       O.FIRST_ASSISTANT First_Assistant_Code,
       O.SECOND_ASSISTANT Second_Assistant_Code,
       O.ANESTHESIA_DOCTOR Anesthesia_Dr_Code,
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B, OPERATION O
 WHERE A.PATIENT_ID = B.PATIENT_ID
   AND A.PATIENT_ID = O.PATIENT_ID
   AND A.VISIT_ID = O.VISIT_ID
   AND A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
