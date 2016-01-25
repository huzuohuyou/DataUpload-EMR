SELECT A.PATIENT_ID || A.VISIT_ID Cancer_Specialist_Rec_ID, --*     
       A.PATIENT_ID || A.VISIT_ID Medical_Record_Id,
       T.TUMOR_TYPE Tumor_Staging_Type,
       T.TUMOR_T Tumor_T,
       T.TUMOR_N Tumor_N,
       T.TUMOR_M Tumor_M,
       T.STAGES Tumor_Staging,
       T.RT_MODE Radiotherapy_Way,
       T.RT_PROCESS Radiotherapy_Program,
       T.RT_DEVICE Radiotherapy_Device,
       T.ORIGINAL_TIMES Primary_Foci_Dosage,
       T.ORIGINAL_DOSAGE_START_DATE || T.ORIGINAL_DOSAGE_END_DATE Primary_Foci_Start_Date,
       T.RLN_TIMES Regional_Lymph_Dosage,
       T.RLN_DOSAGE_START_DATE || T.RLN_DOSAGE_END_DATE Regional_Lymph_Start_Date,
       T.BW_NAME_TRANS MET,
       T.BW_TRANS_DOSAGE_GY MET_Dosage,
       T.BW_TRANS_DOSAGE_START_DATE || T.BW_TRANS_DOSAGE_END_DATE MET_Date,
       T.CMTR_MODE Chemotherapy_Way,
       '' Chemotherap_Other_Way,
       CMTR_WAY Chemotherapy_Method,
       '' Chemotherapy_Method_Other,
       '' Chemotherapy_Start_Date,
       '' Chemotherapy_End_Date,
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B, PAT_TUMOUR T
 WHERE A.PATIENT_ID = B.PATIENT_ID
   AND A.PATIENT_ID = T.PATIENT_ID
   AND A.VISIT_ID = T.VISIT_ID
   AND A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
