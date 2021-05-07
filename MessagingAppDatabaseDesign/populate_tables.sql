-- ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY HH24:MI:SS';
-- ALTER SESSION SET TIME_ZONE='0:00';

-- CHANGE DATE FORMAT
--SELECT TO_CHAR(CREATED_AT, 'MM-DD-YYYY') FROM USERS;
--SELECT to_char(CURRENT_TIMESTAMP at time zone '+03:00', 'MM-DD-YYYY hh24:mm:ss')from USERS;
--SELECT CURRENT_TIMESTAMP from dual;

-- Inserare user
INSERT INTO Users(user_name, user_password, is_active) VALUES('CCC29', '1234', 'F');
INSERT INTO Users(user_name, user_password, is_active) VALUES('BRA', '134679', 'F');

-- Inserare setari aplicatie
INSERT INTO Application_settings(Users_user_id) VALUES((SELECT user_id FROM Users WHERE user_name = 'CCC29'));
INSERT INTO Application_settings(Users_user_id) VALUES((SELECT user_id FROM Users WHERE user_name = 'BRA'));

-- Inregistrare user
(SELECT CURRENT_DATE FROM dual);

INSERT INTO User_informations VALUES('Cojocaru', 'Cosmin-Constantin', 'cosminccc28@gmail.com', TO_DATE('29/04/1999', 'DD/MM/YYYY'), (SELECT user_id FROM Users WHERE user_name = 'CCC29'));
INSERT INTO User_informations VALUES('Budeanu', 'Radu-Andrei', 'budeanuradu99@gmail.com', TO_DATE('29/10/1999', 'DD/MM/YYYY'), (SELECT user_id FROM Users WHERE user_name = 'BRA'));

-- Adaugare prieten
INSERT INTO Friend_relationships(Users_user_id, users_user_id2, status) VALUES((SELECT user_id FROM Users WHERE user_name = 'CCC29'), (SELECT user_id FROM Users WHERE user_name = 'BRA'));

--Adaugare setari relatie
INSERT INTO Relationship_settings(Friend_relationships_relationship_id) VALUES (1);

-- Update friendship status
UPDATE Friend_relationships SET status='friends' WHERE relationship_id = 1;
UPDATE Relationship_settings SET nickname_user_1 = 'CEVA' WHERE Friend_relationships_relationship_id = 1;

-- Add new conversation
INSERT INTO Conversations(Users_user_id, Users_user_id2) VALUES(1, 2);

-- Add message
INSERT INTO Messages(format, message_data, seen, sent_at, seen_at, Users_user_id, Conversations_conversation_id) VALUES('txt', EMPTY_BLOB(), 'F', TO_DATE('28/10/1999 21:01:22', 'DD/MM/YYYY HH24:MI:SS'), NULL, 1, 1);

--- VERIFICARI ---

-- Conversation count
SELECT COUNT(*) FROM Conversations WHERE (Users_user_id = 1 and Users_user_id2 = 2) or (Users_user_id = 2 and Users_user_id2 = 1);
SELECT COUNT(*) FROM User_informations WHERE (Users_user_id = 1);
SELECT COUNT(*) FROM Friend_relationships WHERE (Users_user_id = 1 and Users_user_id2 = 2) or (Users_user_id = 2 and Users_user_id2 = 1);

-- Id-uri
SELECT user_id FROM Users WHERE user_name = 'CCC29';
SELECT user_id FROM Users WHERE user_name = 'BRA';

SELECT conversation_id FROM Conversations WHERE (Users_user_id = 1 and Users_user_id2 = 2) or (Users_user_id = 2 and Users_user_id2 = 1);
SELECT relationship_id FROM Friend_relationships WHERE (Users_user_id = 1 and Users_user_id2 = 2) or (Users_user_id = 2 and Users_user_id2 = 1);
