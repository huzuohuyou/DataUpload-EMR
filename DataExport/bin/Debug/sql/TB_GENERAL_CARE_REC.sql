SELECT A.PATIENT_ID || A.VISIT_ID || C.OPERATION_NO Nursing_Record_Id,
       B.INP_NO || A.VISIT_ID Vis_Serial_Id,
       SYSDATE Record_DT,
       A.BED_NO Bed_Number,
       '' Room_Of_Stay_No,
       A.WARD_ADMISSION_TO Room_Of_Stay_Code,
       A.WARD_ADMISSION_TO Room_Of_Stay_Name,
       A.DEPT_ADMISSION_TO Code_Of_Visit_Dept,
       A.DEPT_ADMISSION_TO Name_Of_Visit_Dept,
       '无' Nursing_Code, --*
       '无' Nursing_Name, --*
       sysdate Signature_DT, --*
       '' Diagnosis_Code,
       '' Diagnosis_Name,
       '' Consciouness,
       '' Grade_Of_Nursing,--*
       '' Type_Of_Nursing,
       '1' Diet_Instruction_Code,
       '1' Diet_Situation_Code,
       'N' Isolated_Signs,
       '' Isolated_Type_Code,
       '' Brief_Case,
       '' Nursing_Operate_Subject_Name,
       '' Nursing_Operate_Name,
       '' Nursing_Operate_Result,
       '' Special_Record,
       '' Catheter_Care_Description,
       '' Trachea_Care_Code,
       '' Posture_Nursing,
       '' Skin_Nursing,
       '' Nutrition_Nursing,
       '1' Psychological_Nursing_Code,
       '' Safety_Nursing_Code,       
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B, OPERATION C
 WHERE A.PATIENT_ID = B.PATIENT_ID
   AND A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
