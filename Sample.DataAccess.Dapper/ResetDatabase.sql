alter procedure ResetDatabase 
as
    exec ClearDatabase;

    -- Inserimento dei valori di test per: Candidate      
    insert into Candidate(Cand_Id, Cand_Name, Cand_Surname, Cand_Email, Gend_Id)
    values (00, 'Davide', 'Paparella', 'davide.paparella@finsa.it', 1),
           (01, 'Diana', 'Martinez Gonzalez', 'diana.martinezgonzalez@finsa.it', 0),
           (02, 'Alessio', 'Parma', 'alessio.parma@finsa.it', 1),
           (03, 'Giuseppe', 'Morchio', 'giuseppe.morchio@finsa.it', 1),
           (04, 'Marco', 'Campagna', 'marco.campagna@finsa.it', 1),
           (05, 'Angela', 'Farri', 'angela.farri@finsa.it', 0);

    -- Inserimento dei valori di test per: CandidateAttended      
    insert into CandidateAttended(Cand_Id, Schl_Id)
    values (00, 06), -- Davide Paparella, Liceo Scientifico Tecnologico
           (00, 00), -- Davide Paparella, Corso di Laurea in Informatica
           (01, 00), -- Diana Martinez Gonzalez, Corso di Laurea in Informatica
           (02, 05), -- Alessio Parma, Liceo Scientifico
           (02, 00), -- Alessio Parma, Corso di Laurea in Informatica
           (03, 00), -- Giuseppe Morchio, Corso di Laurea in Informatica
           (04, 06), -- Marco Campagna, Liceo Scientifico Tecnologico
           (05, 06); -- Marco Campagna, Liceo Scientifico Tecnologico