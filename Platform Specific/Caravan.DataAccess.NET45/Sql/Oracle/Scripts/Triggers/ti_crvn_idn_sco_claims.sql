-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_sco_claims
BEFORE INSERT ON mydb.crvn_idn_sco_claims 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_sco_claims.nextval
    INTO :new.CSCL_ID
    FROM DUAL;
END;
/