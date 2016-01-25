SELECT A.INP_NO Vis_Serial_Id,
       A.INP_NO Admission_Id,
       A.VISIT_ID Admission_Times,
       A.PATIENT_ID Pat_Id,
       B.NAME Name,
       '1' Id_Card_Type_Code,
       B.ID_NO Id_Card_No,
       B.SEX Patient_Sex,
       TO_CHAR(B.DATE_OF_BIRTH, 'YYYYMMDD') Patient_Birth_Date,
       round(months_between( a.admission_date_time,b.date_of_birth) / 12, 0) Age_Year,
       (case months_between(b.date_of_birth, a.admission_date_time)
         when 1 then
          1
         when 2 then
          2
         when 3 then
          3
         when 4 then
          4
         when 5 then
          5
         when 6 then
          6
         when 7 then
          7
         when 8 then
          8
         when 9 then
          9
         when 10 then
          10
         when 11 then
          11
         else
          0
       end) Age_Month,
       '无' Card_Tpye,
       '无' Card_ID,
       A.MARITAL_STATUS AS Marital_Status,
       B.CITIZENSHIP AS Nationality_Code,
       B.NATION AS National_Code,
       B.BIRTH_PLACE Birth_Place,
       '' Phone,
       '' Mobile_Phone_Number,
       '' AS Classification_And_Occupations,
       A.OCCUPATION Occupation_Name,
       '' Qualifications_code,
       '' Qualifications_name,
       A.SERVICE_AGENCY Name_of_Patient_Employer,
       B.PHONE_NUMBER_BUSINESS Pat_Empl_Phone_Number,
       '' email_address,
       (SUBSTR(A.SERVICE_AGENCY,
               0,
               (SELECT INSTR(SERVICE_AGENCY, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Pat_Empl_Addr_Privince,
       (SUBSTR(B.BIRTH_PLACE,
               (SELECT INSTR(SERVICE_AGENCY, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(SERVICE_AGENCY, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(SERVICE_AGENCY, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Pat_Empl_Addr_City,
       (SUBSTR(A.SERVICE_AGENCY,
               (SELECT INSTR(A.SERVICE_AGENCY, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(A.SERVICE_AGENCY, '县')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(A.SERVICE_AGENCY, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Pat_Empl_Addr_County,
       '' Pat_Empl_Addr_Countryside,
       '' Pat_Empl_Addr_Village,
       '' Pat_Empl_Addr_House_Id,
       B.ZIP_CODE AS Pat_Empl_Addr_Zipcode,
       (SUBSTR(B.XIAN_PLACE,
               0,
               (SELECT INSTR(B.XIAN_PLACE, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Living_Address_Province,
       (SUBSTR(B.XIAN_PLACE,
               (SELECT INSTR(XIAN_PLACE, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(XIAN_PLACE, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(XIAN_PLACE, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Living_Address_City,
       (SUBSTR(B.XIAN_PLACE,
               (SELECT INSTR(XIAN_PLACE, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(XIAN_PLACE, '县')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(XIAN_PLACE, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Living_Address_County,
       '' Living_Address_Countryside,
       '' Living_Address_Village,
       nvl(A.XIAN_PLACE, '无') Living_Address_House_Id,
       B.XIAN_CODE AS Living_Address_Zipcode,
       (SUBSTR(B.MAILING_ADDRESS,
               0,
               (SELECT INSTR(B.MAILING_ADDRESS, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Residence_Addr_Province,
       (SUBSTR(B.MAILING_ADDRESS,
               (SELECT INSTR(MAILING_ADDRESS, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(MAILING_ADDRESS, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(MAILING_ADDRESS, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Residence_Addr_City,
       (SUBSTR(B.MAILING_ADDRESS,
               (SELECT INSTR(MAILING_ADDRESS, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(MAILING_ADDRESS, '县')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(MAILING_ADDRESS, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Residence_Addr_County,
       '' Residence_Addr_Countryside,
       '' Residence_Addr_Village,
       A.MAILING_ADDRESS Residence_Add_House_Id,
       A.ZIP_CODE AS Residence_Addr_Zipcode,
       A.NEXT_OF_KIN AS Contact_Person_Name,
       '' AS Contact_Person_Type_Code,
       A.NEXT_OF_KIN_PHONE AS Contact_Person_Phone_Number,
       (SUBSTR(B.NEXT_OF_KIN_ADDR,
               0,
               (SELECT INSTR(B.NEXT_OF_KIN_ADDR, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Contact_Person_Addr_Provice,
       (SUBSTR(B.NEXT_OF_KIN_ADDR,
               (SELECT INSTR(NEXT_OF_KIN_ADDR, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(NEXT_OF_KIN_ADDR, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(NEXT_OF_KIN_ADDR, '省')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Contact_Person_Addr_City,
       (SUBSTR(B.NEXT_OF_KIN_ADDR,
               (SELECT INSTR(NEXT_OF_KIN_ADDR, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) + 1,
               (SELECT INSTR(NEXT_OF_KIN_ADDR, '县')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID) -
               (SELECT INSTR(NEXT_OF_KIN_ADDR, '市')
                  FROM PAT_MASTER_INDEX
                 WHERE PATIENT_ID = A.PATIENT_ID))) AS Contact_Person_Addr_County,
       '' ContactPerson_Addr_Countryside,
       '' Contact_Person_Addr_Village,
       a.NEXT_OF_KIN_ADDR Contact_Person_Addr_House_Id,
       '' Contact_Person_Addr_Zipcode,
       A.INSURANCE_TYPE Insurance_Type,
       A.INSURANCE_NO Insurance_No,
       A.DEPT_ADMISSION_TO Admission_Dept_Code,
       A.DEPT_ADMISSION_TO Admission_Dept,
       TO_CHAR(A.ADMISSION_DATE_TIME, 'YYYYMMDDHHMISS') Admission_Date,
       A.DOCTOR_IN_CHARGE Code_Of_Consulting_Doctor,
       A.DOCTOR_IN_CHARGE Name_Of_Consulting_Doctor,
       '1' Admission_Flag,
       '嘉和美康信息技术有限公司' Healthcare_Serv_Provider_Code,
       '0' Secrecy_Level,
       sysdate Data_Generation_Date,
       sysdate Data_Modify_Date,
       sysdate Filling_Date,
       '1' Modify_Flag
  FROM PAT_VISIT A, PAT_MASTER_INDEX B
 WHERE A.PATIENT_ID = B.PATIENT_ID
   and A.PATIENT_ID = '@PATIENT_ID'
   AND A.VISIT_ID = @VISIT_ID
