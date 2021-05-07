-- ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY HH24:MI:SS';
-- ALTER SESSION SET TIME_ZONE='0:00';

-- CHANGE DATE FORMAT
--SELECT TO_CHAR(CREATED_AT, 'MM-DD-YYYY') FROM USERS;
--SELECT to_char(CURRENT_TIMESTAMP at time zone '+03:00', 'MM-DD-YYYY hh24:mm:ss')from USERS;
--SELECT CURRENT_TIMESTAMP from dual;

-- Inserare user
INSERT INTO Users(user_id, user_name, user_password, is_active) VALUES(1, 'CCC29', '1234', 'F');
INSERT INTO Users(user_id, user_name, user_password, is_active) VALUES(2, 'BRA', '134679', 'F');

-- Inserare setari aplicatie
INSERT INTO application_settings(Users_user_id) VALUES(1);
INSERT INTO application_settings(Users_user_id) VALUES(2);

-- Inregistrare user
SELECT user_id FROM Users WHERE user_name = 'CCC29';
(SELECT CURRENT_DATE FROM dual);

INSERT INTO User_informations VALUES('Cojocaru', 'Cosmin-Constantin', 'cosminccc28@gmail.com', TO_DATE('29/04/1999', 'DD/MM/YYYY'), 1);
INSERT INTO User_informations VALUES('Budeanu', 'Radu-Andrei', 'budeanuradu99@gmail.com', TO_DATE('29/10/1999', 'DD/MM/YYYY'), 2);

-- Adaugare prieten
SELECT user_id FROM Users WHERE user_name = 'CCC29';
SELECT user_id FROM Users WHERE user_name = 'BRA';
INSERT INTO Friend_relationships VALUES(1, 1, 2, 'pending');

--Adaugare setari relatie
INSERT INTO Relationship_settings VALUES ('CCC', 'BRA', 1);

-- Update friendship status
UPDATE Friend_relationships SET status='friends' WHERE relationship_id = 1;

-- Add new conversation
INSERT INTO Conversations VALUES(1, 1, 2);

-- Add message
INSERT INTO Messages(message_id, format, message_data, seen, sent_at, seen_at, Users_user_id, Conversations_conversation_id) VALUES(1, 'txt', EMPTY_BLOB(), 'F', (SELECT CURRENT_DATE FROM dual), NULL, 1, 1);