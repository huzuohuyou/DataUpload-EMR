SELECT A.PATIENT_ID || A.VISIT_ID Obstetric_Delivery_Baby, --*     
       A.PATIENT_ID || A.VISIT_ID Medical_Record_Id,
       B.SEX Baby_Sex,
       '活胎' Delivery_Results,
       A.BABY_WEIGHT Baby_Weight,
       '出院' Infant_Outcomes,
       '' Baby_Breathe,
       A.EMER_TREAT_TIMES Baby_Rescue_Times,
       A.ESC_EMER_TIMES Baby_Rescue_Success_Times,
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B
 WHERE A.PATIENT_ID = B.PATIENT_ID
   AND A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
