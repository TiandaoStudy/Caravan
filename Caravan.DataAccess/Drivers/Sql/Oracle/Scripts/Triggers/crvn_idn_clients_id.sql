-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_idn_clients_id
BEFORE INSERT ON mydb.crvn_idn_clients 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_idn_clients_id.nextval
    INTO :new.ccli_id
    FROM DUAL;
END;
/