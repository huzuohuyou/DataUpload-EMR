SELECT A.PATIENT_ID || A.VISIT_ID FIRST_COURSE_OF_DISEASE_ID, --*     
       A.PATIENT_ID || A.VISIT_ID Vis_Serial_Id,
       A.DEPT_ADMISSION_TO Code_Of_Visit_Dept,
       A.DEPT_ADMISSION_TO Name_Of_Visit_Dept,
       A.WARD_ADMISSION_TO Room_Of_Stay_Code,
       A.WARD_ADMISSION_TO Room_Of_Stay_Name,
       '' Room_Of_Stay_No,
       A.BED_NO Bed_Number,
       A.ADMISSION_DATE_TIME Admission_Date,
       '@主诉' Visit_Description,
       '无' Case_Characteristics,
       '' CHN_Medi_4Diag_Observation,
       '' Discriminate_Basis,
       '无' Admitting_Diag_West_Medi_Name,
       '无' Admitting_Diag_CHN_Medi_Name,
       '' Admit_Diag_CHN_Medi_Symp_Name,
       '无' Treatment_Plans,
       '无' Therapeutical_Principle,
       A.DOCTOR_IN_CHARGE Resident_Physician_Code,
       A.SUPER_DOCTOR_ID Superior_Physician_Code,
       A.DOCTOR_IN_CHARGE Resident_Physician_Signature,
       A.SUPER_DOCTOR_ID Superior_Physician_Signature,
       A.ADMISSION_DATE_TIME Recording_Time,
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
