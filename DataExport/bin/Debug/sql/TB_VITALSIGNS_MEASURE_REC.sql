SELECT A.PATIENT_ID || A.VISIT_ID ||
       (SELECT MAX(RECORDING_DATE)
          FROM VITAL_SIGNS_REC CC
         WHERE CC.PATIENT_ID = A.PATIENT_ID
           AND CC.VISIT_ID = A.VISIT_ID) Nursing_Record_Id, --*
       A.PATIENT_ID || A.VISIT_ID NURSING_OBSERVATION_REC_ID,
       B.INP_NO || A.VISIT_ID Vis_Serial_Id,
       A.DEPT_ADMISSION_TO Dept_Name,
       A.DEPT_ADMISSION_TO Dept_Code,
       round(SYSDATE - A.ADMISSION_DATE_TIME, 0) Actual_HOD,
       '' Date_After_Operate,
       (SELECT MAX(RECORDING_DATE)
          FROM VITAL_SIGNS_REC CC
         WHERE CC.PATIENT_ID = A.PATIENT_ID
           AND CC.VISIT_ID = A.VISIT_ID) Record_DT,
       '' Room_Of_Stay_No,
       A.WARD_ADMISSION_TO Room_Of_Stay_Code,
       A.WARD_ADMISSION_TO Room_Of_Stay_Name,
       sysdate - a.admission_date_time,
       A.BED_NO Bed_Number,
       SYSDATE Record_DT,
       nvl((select c.vital_signs_values_c
             from vital_signs_rec c
            where C.patient_id = a.patient_id
              and C.visit_id = a.visit_id
              and c.vital_signs = '呼吸'
              and rownum = 1),
           0) Respiratory_Frequency,
       '' Breathing_Machine_Flag,
       nvl((select c.vital_signs_values_c
             from vital_signs_rec c
            where patient_id = a.patient_id
              and visit_id = a.visit_id
              and c.vital_signs = '脉搏'
              and rownum = 1),
           0) Pause_Rate,
       '' Pacemaker_Rate,
       nvl((select c.vital_signs_values_c
             from vital_signs_rec c
            where patient_id = a.patient_id
              and visit_id = a.visit_id
              and c.vital_signs = '体温'
              and rownum = 1),
           0) Temperature,
       nvl((select c.vital_signs_values_c
             from vital_signs_rec c
            where patient_id = a.patient_id
              and visit_id = a.visit_id
              and c.vital_signs = '血压'
              and rownum = 1),
           0) Systolic_Pressure,
       nvl((select c.vital_signs_values_c
             from vital_signs_rec c
            where patient_id = a.patient_id
              and visit_id = a.visit_id
              and c.vital_signs = '血压'
              and rownum = 1),
           0) Diastolic_Pressure,
       nvl((select c.vital_signs_values_c
             from vital_signs_rec c
            where patient_id = a.patient_id
              and visit_id = a.visit_id
              and c.vital_signs = '体重'
              and rownum = 1),
           0) Body_Weight,
       '' Abdominal_Circumference,
       '2' Temperature_Type,
       '' PHYSICAL_COOLING,
       '' Event,
       '' Event_Time,
       (select C.RECORDER
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and rownum = 1) Nurse_Code,
       (select C.RECORDER
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and rownum = 1) Nurse_Signature,
       (select C.RECORDER_DATE
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and rownum = 1) Signature_DT,
       '' Postprandial_blood_glucose,
       '' Special_Record,
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
