SELECT A.PATIENT_ID || A.VISIT_ID NURSING_OBSERVATION_REC_ID,
       A.PATIENT_ID || A.VISIT_ID ||
       (SELECT MAX(RECORDING_DATE)
          FROM VITAL_SIGNS_REC CC
         WHERE CC.PATIENT_ID = A.PATIENT_ID
           AND CC.VISIT_ID = A.VISIT_ID) Inpati_ID, --*       
       B.INP_NO || A.VISIT_ID Vis_Serial_Id,
       A.DEPT_ADMISSION_TO Code_Of_Visit_Dept,
       A.DEPT_ADMISSION_TO Name_Of_Visit_Dept,
       A.WARD_ADMISSION_TO Room_Of_Stay_Code,
       A.WARD_ADMISSION_TO Room_Of_Stay_Name,
       '' Room_Of_Stay_No,
       A.BED_NO Bed_Number,
       A.PATIENT_CLASS Admission_Way,
       '' Admitting_Diagnosis_Code,
       (SELECT S.DIAGNOSIS_DESC
          FROM DIAGNOSIS S
         WHERE A.PATIENT_ID = S.PATIENT_ID
           AND A.VISIT_ID = S.VISIT_ID
           AND S.DIAGNOSIS_TYPE = '2'
           AND S.DIAGNOSIS_NO = 1) Admitting_Diagnosis_Name,
       (SELECT S.DIAGNOSIS_DATE
          FROM DIAGNOSIS S
         WHERE A.PATIENT_ID = S.PATIENT_ID
           AND A.VISIT_ID = S.VISIT_ID
           AND S.DIAGNOSIS_TYPE = '2'
           AND S.DIAGNOSIS_NO = 1) Admission_Date,
       '' Major_Symptom,
       '' Body_Weight,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '体温'
           and rownum = 1) Temperature,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '呼吸'
           and rownum = 1) Respiratory_Frequency,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '脉搏'
           and rownum = 1) Pause_Rate,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '血压'
           and rownum = 1) Systolic_Pressure,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '血压'
           and rownum = 1) Diastolic_Pressure,
       '' Admission_Reason,
       a.RUYUAN_PASS Admission_Way_Code, --*
       '' Apgar_Score,
       '' Diet_Situation_Code,
       '' Growth_Degree_Code,
       '' Mental_State_Normal_Flag,
       '' Sleep_Status,
       '' Special_Situation,
       '' Psychological_Status_Code,
       '' Nutritional_Status_Code,
       '' Self_Care_Ability_Code,
       '' Allergies,
       '' General_Health_Condition_Flag,
       '' Pat_Disease_His,
       '' Patients_With_Infectious_Signs,
       '' Infectious_Disease,
       '' Preventive_Inoculation_History,
       '@手术史' Operation_History,
       '@输血史' Blood_Transfusion_History,
       '@家族史' Family_History,
       '' Recording_Items_Of_Nursing,
       '' Result_Of_Nursing,
       '' Smoking_Sign,
       '' Stop_Smoking_Days,
       '' Smoking_Status_Code,
       '' Daily_Smoking_Piece,
       '' Drinking_Sign,
       '' Drinking_Frequency_Code,
       '' Daily_Drinking_Ml,
       '' Language_Expression,
       '' Vision,
       '' Listening,
       '' Stool,
       '' Urine,
       '' Skin_And_Mucosa,
       '' Physical_Activities,
       '' Fall_within_month,
       '' Fall_Consequences,
       '' Inform_Dr_Sign,
       '' Inform_Dr_Date,
       a.admission_date_time Assessment_DT,
       '' Primary_Nurse_Code,
       '' Accepting_Nurse_Code,
       '' Primary_Nurse_Signature,
       '' Accepting_Nurse_Signature,
       a.admission_date_time Signature_DT,
       '' Inhospital_Notify,
       '' InhosOther_Notify,
       '' Nursing_Safety,
       '' Nursing_Specific,
       '' Special_Condition,
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
