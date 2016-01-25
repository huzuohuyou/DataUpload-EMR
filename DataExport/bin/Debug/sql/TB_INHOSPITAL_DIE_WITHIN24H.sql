SELECT A.PATIENT_ID || A.VISIT_ID InHospital_Death_24H_Rec, --*     
       A.PATIENT_ID || A.VISIT_ID Vis_Serial_Id,
       A.DEPT_ADMISSION_TO Code_Of_Visit_Dept,
       A.DEPT_ADMISSION_TO Name_Of_Visit_Dept,
       A.WARD_ADMISSION_TO Room_Of_Stay_Code,
       A.WARD_ADMISSION_TO Room_Of_Stay_Name,
       '' Room_Of_Stay_No,
       A.BED_NO Bed_Number,
       A.ADMISSION_DATE_TIME Admission_Date,
       A.DISCHARGE_DATE_TIME Death_Time,
       A.PAT_ADM_CONDITION Admitting_Diagnosis,
       '' CHN_Medi_4Diag_Observation,
       '' Therapeutical_Principle,
       '无' Treatment_Process_Description,
       '无' Cause_Of_Death,
       '' Attend_Rescue_Person,
       '' Vis_Physician_Code,       
       '' SIGNATURE_NAME,
       '' VIS_PHYSICIAN_SIGNATURE,
       A.DOCTOR_IN_CHARGE Resident_Physician_Code,
       A.DOCTOR_IN_CHARGE Resident_Physician_Signature,
       A.CHIEF_DOCTOR Chief_Physician_Code,
       A.CHIEF_DOCTOR Chief_Physician_Signature,
       A.ATTENDING_DOCTOR Attending_Physician_Code,
       A.ATTENDING_DOCTOR Attending_Physician_Signature,
       A.DISCHARGE_DATE_TIME Recording_Time,
       '@主诉' Vis_Description,
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B, PAT_TUMOUR_DETAIL T
 WHERE A.PATIENT_ID = B.PATIENT_ID
   AND A.DISCHARGE_PASS = '5'
   AND (A.DISCHARGE_DATE_TIME - A.ADMISSION_DATE_TIME) * 24 < 5
   AND A.PATIENT_ID = T.PATIENT_ID
   AND A.VISIT_ID = T.VISIT_ID
   AND A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
