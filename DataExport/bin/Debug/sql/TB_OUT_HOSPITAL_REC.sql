SELECT A.PATIENT_ID || A.VISIT_ID Discharge_Record_Id, --*       
       B.INP_NO || A.VISIT_ID Vis_Serial_Id,
       B.INP_NO Admission_Id,
       A.BED_NO Bed_Number,
       A.WARD_ADMISSION_TO Room_Of_Stay_Code,
       A.WARD_ADMISSION_TO Room_Of_Stay_Name,
       '' Room_Of_Stay_No,
       A.DEPT_ADMISSION_TO Code_Of_Visit_Dept,
       A.DEPT_ADMISSION_TO Name_Of_Visit_Dept,
       A.CHIEF_DOCTOR Code_Of_Chief_Physician,
       A.CHIEF_DOCTOR Name_Of_Chief_Physician,
       A.ATTENDING_DOCTOR Code_Of_Attending_Physician,
       A.ATTENDING_DOCTOR Name_Of_Attending_Physician,
       A.DOCTOR_IN_CHARGE Code_Of_Resident_Physician,
       A.DOCTOR_IN_CHARGE Name_Of_Resident_Physician,
       A.ADMISSION_DATE_TIME admission_DT,
       (SELECT S.DIAGNOSIS_DESC
          FROM DIAGNOSIS S
         WHERE S.PATIENT_ID = A.PATIENT_ID
           AND S.VISIT_ID = A.VISIT_ID
           AND S.DIAGNOSIS_TYPE = '2'
           AND S.DIAGNOSIS_NO = 1) Admitting_Diagnosis,
       '' Positive_Assist_Check_Result,
       '' CHN_Medi_4Diag_Observation,
       '' Therapeutical_Principle,
       '无' Treatment_Process_Description,
       '' Trad_CHN_Medi_Mecoction_Method,
       '' Trad_CHN_Medi_Drug_Method,
       a.discharge_date_time DT_Of_Discharge,
       (SELECT S.DIAGNOSIS_DESC
          FROM DIAGNOSIS S
         WHERE S.PATIENT_ID = A.PATIENT_ID
           AND S.VISIT_ID = A.VISIT_ID
           AND S.DIAGNOSIS_TYPE = '3'
           AND S.DIAGNOSIS_NO = 1) Final_Diagnosis,
       '' Final_Diagnosis_Description,
       '无' Discharge_Order,
       '' Operation_Start_DT,
       '' Operation_Name,
       a.admission_date_time Signature_DT,
       a.discharge_date_time - a.admission_date_time Actual_HOD,       
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
