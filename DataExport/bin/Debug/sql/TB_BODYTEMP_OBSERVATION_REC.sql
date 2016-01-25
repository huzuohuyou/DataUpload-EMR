SELECT A.PATIENT_ID || A.VISIT_ID NURSING_OBSERVATION_REC_ID,
       A.PATIENT_ID || A.VISIT_ID ||
       (SELECT MAX(RECORDING_DATE)
          FROM VITAL_SIGNS_REC CC
         WHERE CC.PATIENT_ID = A.PATIENT_ID
           AND CC.VISIT_ID = A.VISIT_ID) Nursing_Record_Id, --*       
       B.INP_NO || A.VISIT_ID Vis_Serial_Id,
       A.DEPT_ADMISSION_TO Code_Of_Visit_Dept,
       A.DEPT_ADMISSION_TO Name_Of_Visit_Dept,
       A.WARD_ADMISSION_TO Room_Of_Stay_Code,
       A.WARD_ADMISSION_TO Room_Of_Stay_Name,
       '' Room_Of_Stay_No,
       A.BED_NO Bed_Number,       
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '液入量'
           and rownum = 1) Liquid_intake_total,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '液出量'
           and rownum = 1) Liquid_output_total,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '大便'
           and rownum = 1) Amount_of_stool_total,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '尿量'
           and rownum = 1) Urine_volume_total,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '小便'
           and rownum = 1) Input_and_output_other,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '血压'
           and rownum = 1) Systolic_Pressureumber_am,
       (select c.vital_signs_values_c
          from vital_signs_rec c
         where C.patient_id = a.patient_id
           and C.visit_id = a.visit_id
           and c.vital_signs = '血压'
           and rownum = 1) Diastolic_Pressure_am,
       '' Systolic_Pressureumber_pm,
       '' Diastolic_Pressure_pm,
       '' Body_Weight,
       '' Skin_test_result,
       '' FITEM,
       '' Pain_assess_level,       
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
