SELECT A.PATIENT_ID || A.VISIT_ID Oncology_Medication_ID, --*     
       A.PATIENT_ID || A.VISIT_ID Cancer_Specialist_Rec_ID,
       A.PATIENT_ID || A.VISIT_ID Medical_Record_Id,
       T.CMTR_START_DATE || T.CMTR_END_DATE RECORD_DATE,
       T.CMTR_MEDICATION_NAME DrugName,
       T.CMTR_MEDICATION_DOSAGE DrugDosage,
       '' DrugDosageUnit,
       T.CMTR_PERIOD TreatmentDuration,
       T.CMTR_EFFECT TreatmentOutcomes,       
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B, PAT_TUMOUR_DETAIL T
 WHERE A.PATIENT_ID = B.PATIENT_ID
   AND A.PATIENT_ID = T.PATIENT_ID
   AND A.VISIT_ID = T.VISIT_ID
   AND A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
