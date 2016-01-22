-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_sco_claims
(
     CSCL_ID                    NUMBER(10)                  NOT NULL
   , CSCO_ID                    NUMBER(10)                  NOT NULL
   , CSCL_NAME                  NVARCHAR2(200)              NOT NULL
   , CSCL_DESCR                 NVARCHAR2(1000)  
   , CSCL_ALWAYS_INCL_IN_TOKEN  NUMBER(1)       DEFAULT 0   NOT NULL

   , CONSTRAINT pk_crvn_idn_sco_claims PRIMARY KEY (CSCL_ID) ENABLE
   , CONSTRAINT fk_crvnidnsco_claims_scopes FOREIGN KEY (CSCO_ID) REFERENCES mydb.crvn_idn_scopes (CSCO_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_sco_claims NOCACHE;
