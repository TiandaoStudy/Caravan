-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_clients
BEFORE INSERT ON mydb.crvn_idn_clients 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_clients_id.nextval
    INTO :new.CCLI_ID
    FROM DUAL;
END;
/