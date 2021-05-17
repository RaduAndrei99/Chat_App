-- ALTER SESSION SET NLS_DATE_FORMAT = 'DD-MON-YYYY HH24:MI:SS';
-- ALTER SESSION SET TIME_ZONE='0:00';

-- CHANGE DATE FORMAT
--SELECT TO_CHAR(CREATED_AT, 'MM-DD-YYYY') FROM USERS;
--SELECT to_char(CURRENT_TIMESTAMP at time zone '+03:00', 'MM-DD-YYYY hh24:mm:ss')from USERS;
--SELECT CURRENT_TIMESTAMP from dual;

-- OracleParameter oracleParameter = new OracleParameter("username", OracleDbType.Varchar2);
-- oracleParameter.Value = username;

-- Inserare user
INSERT INTO Users(user_name, user_password) VALUES('CCC29', '1234');
INSERT INTO Users(user_name, user_password) VALUES('BRA', '134679');

-- Inserare setari aplicatie
INSERT INTO Application_settings(Users_user_id) VALUES((SELECT user_id FROM Users WHERE user_name = 'CCC29'));
INSERT INTO Application_settings(Users_user_id) VALUES((SELECT user_id FROM Users WHERE user_name = 'BRA'));

-- Inregistrare user
(SELECT CURRENT_DATE FROM dual);

INSERT INTO User_informations VALUES('Cojocaru', 'Cosmin-Constantin', 'cosminccc28@gmail.com', TO_DATE('29/04/1999', 'DD/MM/YYYY'), (SELECT user_id FROM Users WHERE user_name = 'CCC29'));
INSERT INTO User_informations VALUES('Budeanu', 'Radu-Andrei', 'budeanuradu99@gmail.com', TO_DATE('29/10/1999', 'DD/MM/YYYY'), (SELECT user_id FROM Users WHERE user_name = 'BRA'));

-- Adaugare prieten
INSERT INTO Friend_relationships(Users_user_id, users_user_id2) VALUES((SELECT user_id FROM Users WHERE user_name = 'CCC29'), (SELECT user_id FROM Users WHERE user_name = 'BRA'));

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

-- Verificare credentiale
SELECT COUNT(*) FROM Users WHERE user_id = 1, and user_password = '1234';

-- Change is_active status
UPDATE Users SET is_active = 'T' WHERE user_id = 1;

-- Get messages
SELECT * FROM Messages WHERE Conversations_conversation_id = 1 AND message_id < 25 AND ROWNUM <= 5 ORDER BY Message_id DESC;

--Update username
UPDATE Users SET user_name = 'BRA' WHERE user_id = 1;

-- Mark seen messages
UPDATE Messages SET seen = 'T', seen_at = sysdate WHERE Conversations_conversation_id = 1 AND Users_user_id = 1 AND seen = 'F';

-- Set datetime formats
UPDATE Application_settings SET date_format = 'data' WHERE Users_user_id = 1;
UPDATE Application_settings SET hour_format = 'time' WHERE Users_user_id = 1;

-- Get datetime formats
SELECT date_format FROM Application_settings WHERE Users_user_id = 1;
SELECT hour_format FROM Application_settings WHERE Users_user_id = 1;

-- Friends
SELECT user_name FROM Users u, Friend_relationships fr WHERE (fr.users_user_id = u.user_id OR fr.users_user_id2 = u.user_id) AND u.user_id != 1 AND fr.status = 'friends' ORDER BY user_name;

-- Sent pending requests
SELECT user_name FROM Users u, Friend_relationships fr WHERE fr.users_user_id = 1 AND fr.users_user_id2 = u.user_id AND fr.status = 'pending' ORDER BY user_name;

-- Waiting to accept
SELECT user_name FROM Users u, Friend_relationships fr WHERE fr.users_user_id2 = 1 AND fr.users_user_id = u.user_id AND fr.status = 'pending' ORDER BY user_name;