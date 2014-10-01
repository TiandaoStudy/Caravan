alter procedure ClearDatabase 
as
    -- Pulizia delle tabelle coinvolte: Associazioni
    delete from CandidateAttended;

    -- Pulizia delle tabelle coinvolte: Entità
    delete from Candidate;
    delete from Gender;
    delete from School;

    -- Inserimento dei valori di default per: School
    insert into School(Schl_Id, Schl_Description)
    values (00, 'Corso di Laurea in Informatica'),
           (01, 'Corso di Laurea in Lettere Antiche'),
           (02, 'Corso di Laurea in Lettere Moderne'),
           (03, 'Liceo Classico'),
           (04, 'Liceo Psico Pedagogico'),
           (05, 'Liceo Scientifico'),
           (06, 'Liceo Scientifico Tecnologico');

    -- Inserimento dei valori di default per: Gender
    insert into Gender(Gend_Id, Gend_Description)
    values (0, 'Female'),
           (1, 'Male');